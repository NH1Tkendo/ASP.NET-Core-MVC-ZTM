## Cập nhật dự án để sử dụng Unit of Work thay vì CategoryRepository

### Mục tiêu
- Thay thế `CategoryRepository` bằng `UnitOfWork` trong dự án web.
- Cấu hình dependency injection trong `Program.cs` để sử dụng `IUnitOfWork`.
- Cập nhật các phương thức trong `CategoryController` để sử dụng `UnitOfWork`.
- Đánh giá ưu và nhược điểm của việc sử dụng `UnitOfWork` so với repository riêng lẻ.

### 1. Cấu hình Dependency Injection trong Program.cs
- **Mục đích**: Đăng ký `IUnitOfWork` vào hệ thống dependency injection để thay thế `ICategoryRepository`.
- **Các bước thực hiện**:
  - Mở tệp `Program.cs`.
  - Xóa đăng ký của `ICategoryRepository` (nếu có).
  - Thêm đăng ký cho `IUnitOfWork` và triển khai `UnitOfWork`:
    ```csharp
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    ```
  - **Giải thích**:
    - `UnitOfWork` sẽ tự động tạo các đối tượng repository bên trong (như `CategoryRepository`) khi được tiêm.
    - Điều này giúp giảm số lượng đăng ký repository riêng lẻ trong `Program.cs`.

### 2. Cập nhật CategoryController
- **Mục đích**: Chuyển từ sử dụng `CategoryRepository` sang `UnitOfWork` trong `CategoryController`.
- **Các bước thực hiện**:
  - Thay thế tiêm `ICategoryRepository` bằng `IUnitOfWork` trong constructor:
    ```csharp
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
    ```
  - Cập nhật các phương thức hành động (action methods):
    - Thay `categoryRepository.GetAll()` bằng `_unitOfWork.Category.GetAll()`.
    - Thay `categoryRepository.Update()` bằng `_unitOfWork.Category.Update()`.
    - Thay `categoryRepository.Save()` bằng `_unitOfWork.Save()`.
  - **Ví dụ**:
    ```csharp
    public IActionResult Index()
    {
        var categories = _unitOfWork.Category.GetAll();
        return View(categories);
    }

    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Add(category);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        return View(category);
    }
    ```
  - **Lưu ý**:
    - Phương thức `Save` được gọi trực tiếp từ `_unitOfWork` thay vì từ repository.
    - Các phương thức CRUD khác (như `Add`, `Update`, `GetAll`) được truy cập thông qua `_unitOfWork.Category`.

### 3. Kiểm tra hoạt động
- **Thử nghiệm**:
  - Chạy dự án để kiểm tra các chức năng:
    - Lấy danh sách danh mục (GetAll).
    - Tạo mới danh mục (Create).
    - Xóa danh mục (Delete).
  - **Kết quả**: Tất disparity: Tất cả các chức năng hoạt động bình thường khi sử dụng `UnitOfWork`.

### 4. Ưu và nhược điểm của Unit of Work
- **Ưu điểm**:
  - Cung cấp một điểm truy cập duy nhất cho tất cả các repository (như `CategoryRepository`, `ProductRepository`, `OrderRepository`, v.v.).
  - Làm mã nguồn gọn gàng hơn, đặc biệt khi làm việc với nhiều repository.
  - Phương thức `Save` được quản lý tập trung trong `UnitOfWork`, tránh lặp lại trong các repository.
- **Nhược điểm**:
  - Trong `CategoryController`, chỉ cần `CategoryRepository` nhưng `UnitOfWork` sẽ khởi tạo tất cả các repository đã đăng ký (như `ProductRepository`, `OrderRepository`), gây lãng phí tài nguyên nếu không sử dụng hết.
- **Đề xuất**:
  - Sử dụng `UnitOfWork` trong các dự án lớn, cần quản lý nhiều repository.
  - Nếu dự án nhỏ và chỉ sử dụng một vài repository, có thể cân nhắc dùng repository riêng lẻ để đơn giản hóa.

### 5. Ghi chú thêm
- **Kết quả**:
  - Việc chuyển sang `UnitOfWork` giúp mã nguồn rõ ràng và dễ bảo trì hơn, đặc biệt trong các dự án phức tạp.
  - Cần đảm bảo tất cả các repository được tích hợp vào `UnitOfWork` được đăng ký đúng trong `Program.cs`.
- **Đề xuất liên kết**:
  - Liên kết ghi chú này với các ghi chú về **Dependency Injection**, **Repository Pattern**, hoặc **Unit of Work Pattern** trong Obsidian để tra cứu chéo.
- **Lưu ý thực tế**:
  - Kiểm tra kỹ các tham chiếu đến repository trong mã nguồn sau khi chuyển sang `UnitOfWork`.
  - Đảm bảo phương thức `Save` được gọi trực tiếp từ `_unitOfWork` thay vì repository riêng lẻ.

---
