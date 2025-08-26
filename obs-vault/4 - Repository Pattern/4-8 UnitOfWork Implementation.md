## Triển khai Unit of Work trong dự án .NET

### Mục tiêu
- Hiểu khái niệm **Unit of Work** và lý do sử dụng nó thay vì chỉ dùng các repository riêng lẻ.
- Tìm hiểu cách triển khai giao diện `IUnitOfWork` và lớp `UnitOfWork` để quản lý các repository.
- Loại bỏ phương thức `Save` khỏi các repository riêng lẻ và chuyển nó vào `UnitOfWork` để đảm bảo tính toàn cục.

### 1. Vấn đề với Repository riêng lẻ
- **Hiện trạng**:
  - Dự án hiện sử dụng `CategoryRepository` để xử lý các thao tác CRUD (Create, Read, Update, Delete) cho danh mục (category).
  - Mỗi repository (như `CategoryRepository`, `ProductRepository`, `OrderRepository`, v.v.) đều có phương thức `Save` để lưu thay đổi vào cơ sở dữ liệu.
  - Nếu controller sử dụng nhiều repository (ví dụ: 10 repository), cần tiêm (inject) tất cả chúng qua dependency injection, dẫn đến mã nguồn phức tạp.
- **Hạn chế**:
  - Phương thức `Save` không liên quan trực tiếp đến logic của từng repository hoặc mô hình (model), mà là một chức năng toàn cục (global).
  - Việc lặp lại `Save` trong mỗi repository không tối ưu và làm tăng độ phức tạp.

### 2. Khái niệm Unit of Work
- **Unit of Work**:
  - Là một mẫu thiết kế (design pattern) dùng để quản lý tất cả các repository trong một dự án.
  - Tập hợp các repository (như `CategoryRepository`, `ProductRepository`) và cung cấp một phương thức toàn cục (như `Save`) để lưu các thay đổi vào cơ sở dữ liệu.
- **Lợi ích**:
  - Giảm số lượng repository cần tiêm vào controller, làm mã nguồn gọn gàng hơn.
  - Tích hợp các phương thức toàn cục (như `Save`) vào một nơi duy nhất.
- **Hạn chế**:
  - Không bắt buộc trong mọi dự án, đặc biệt nếu chỉ sử dụng một vài repository.
  - Có thể làm tăng độ phức tạp trong các dự án nhỏ.

### 3. Triển khai giao diện IUnitOfWork
- **Mục đích**: Định nghĩa một giao diện (`IUnitOfWork`) để quản lý các repository và phương thức toàn cục.
- **Các bước thực hiện**:
  - Thêm tệp giao diện mới:
    - Nhấp chuột phải vào dự án, chọn `Add > New Item`.
    - Chọn `Interface`, đặt tên là `IUnitOfWork.cs`.
  - Định nghĩa giao diện:
    ```csharp
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        void Save();
    }
    ```
    - `Category`: Thuộc tính để truy cập `CategoryRepository`.
    - `Save`: Phương thức toàn cục để lưu thay đổi vào cơ sở dữ liệu.

### 4. Triển khai lớp UnitOfWork
- **Mục đích**: Cung cấp triển khai cụ thể cho giao diện `IUnitOfWork`.
- **Các bước thực hiện**:
  - Thêm lớp mới:
    - Nhấp chuột phải, chọn `Add > New Item > Class`, đặt tên là `UnitOfWork.cs`.
  - Triển khai giao diện:
    ```csharp
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
    ```
    - Tiêm `ApplicationDbContext` qua constructor để sử dụng trong các repository.
    - Khởi tạo `CategoryRepository` và gán vào thuộc tính `Category`.
    - Triển khai phương thức `Save` để lưu thay đổi vào cơ sở dữ liệu.

### 5. Cập nhật Repository và Controller
- **Loại bỏ phương thức Save khỏi Repository**:
  - Xóa phương thức `Save` khỏi giao diện `ICategoryRepository` và lớp `CategoryRepository`, vì nó đã được chuyển sang `UnitOfWork`.
  - Ví dụ, trong `ICategoryRepository`:
    ```csharp
    public interface ICategoryRepository
    {
        // Các phương thức CRUD khác, không có Save
    }
    ```
- **Cập nhật CategoryController**:
  - Thay vì tiêm `CategoryRepository`, controller sẽ sử dụng `UnitOfWork`.
  - Ví dụ:
    ```csharp
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var categories = _unitOfWork.Category.GetAll();
            return View(categories);
        }
    }
    ```
  - Truy cập `CategoryRepository` thông qua `_unitOfWork.Category`.

### 6. Ghi chú thêm
- **Lợi ích của Unit of Work**:
  - Tích hợp các repository vào một điểm truy cập duy nhất, giảm sự phụ thuộc trực tiếp vào từng repository.
  - Phương thức `Save` được quản lý tập trung, tránh lặp lại mã nguồn.
- **Lưu ý**:
  - Nếu dự án chỉ sử dụng một hoặc vài repository, có thể không cần `UnitOfWork` để tránh phức tạp hóa.
  - Đảm bảo cấu hình dependency injection chính xác trong `Startup.cs` hoặc `Program.cs` để tiêm `IUnitOfWork`.
- **Đề xuất**:
  - Liên kết ghi chú này với các ghi chú về **Dependency Injection**, **Repository Pattern**, hoặc **.NET 6** trong Obsidian để tra cứu chéo.

---
