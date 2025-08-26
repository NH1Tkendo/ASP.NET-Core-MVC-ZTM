## Ghi chú học tập: Sử dụng CategoryRepository trong CategoryController

### Mục đích
- Hiểu cách thay thế việc sử dụng trực tiếp `ApplicationDbContext` bằng `ICategoryRepository` trong `CategoryController` thông qua tiêm phụ thuộc (Dependency Injection), đồng thời cấu hình dịch vụ trong hệ thống.

### Khái niệm
- **Tiêm phụ thuộc (Dependency Injection)**: Cho phép cung cấp triển khai của `ICategoryRepository` vào `CategoryController` thay vì sử dụng trực tiếp `ApplicationDbContext`.
- **Đăng ký dịch vụ**: Cần đăng ký `ICategoryRepository` và triển khai của nó (`CategoryRepository`) vào bộ chứa tiêm phụ thuộc để hệ thống có thể cung cấp đúng triển khai khi cần.
- **Vòng đời dịch vụ (Service Lifetime)**: Sử dụng vòng đời `Scoped` để đảm bảo một phiên bản dịch vụ được sử dụng trong suốt một yêu cầu (request).

### Cách thực hiện
- **Thay thế `ApplicationDbContext` bằng `ICategoryRepository`**:
  - Trong `CategoryController`, thay vì khai báo `_db` kiểu `ApplicationDbContext`, sử dụng `_categoryRepo` kiểu `ICategoryRepository`.
  - Tiêm `ICategoryRepository` vào constructor của controller thông qua tiêm phụ thuộc.
- **Sử dụng các phương thức của `ICategoryRepository`**:
  - `GetAll`: Lấy danh sách tất cả các `Category`.
  - `Add`: Thêm một `Category` mới.
  - `Save`: Lưu các thay đổi vào cơ sở dữ liệu.
  - `Get`: Lấy một `Category` theo điều kiện (ví dụ: ID).
  - `Update`: Cập nhật thông tin của một `Category`.
  - `Remove`: Xóa một `Category`.
- **Đăng ký dịch vụ trong bộ chứa tiêm phụ thuộc**:
  - Trong tệp cấu hình (thường là `Program.cs` hoặc `Startup.cs`), đăng ký dịch vụ với vòng đời `Scoped`:
    ```csharp
    services.AddScoped<ICategoryRepository, CategoryRepository>();
    ```
  - Điều này đảm bảo khi `CategoryController` yêu cầu `ICategoryRepository`, hệ thống sẽ cung cấp triển khai `CategoryRepository`.
- **Xử lý lỗi "Unable to resolve service"**:
  - Lỗi này xảy ra khi dịch vụ chưa được đăng ký trong bộ chứa tiêm phụ thuộc.
  - Kiểm tra và đảm bảo rằng `ICategoryRepository` và `CategoryRepository` đã được đăng ký đúng.

### Mã nguồn
```csharp
public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepo;

    public CategoryController(ICategoryRepository categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }

    // Lấy tất cả danh mục
    public IActionResult Index()
    {
        var categories = _categoryRepo.GetAll();
        return View(categories);
    }

    // Tạo danh mục mới
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            _categoryRepo.Add(category);
            _categoryRepo.Save();
            return RedirectToAction("Index");
        }
        return View(category);
    }

    // Chỉnh sửa danh mục
    public IActionResult Edit(int id)
    {
        var category = _categoryRepo.Get(u => u.Id == id);
        if (category == null) return NotFound();
        return View(category);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            _categoryRepo.Update(category);
            _categoryRepo.Save();
            return RedirectToAction("Index");
        }
        return View(category);
    }

    // Xóa danh mục
    public IActionResult Delete(int id)
    {
        var category = _categoryRepo.Get(u => u.Id == id);
        if (category == null) return NotFound();
        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var category = _categoryRepo.Get(u => u.Id == id);
        if (category != null)
        {
            _categoryRepo.Remove(category);
            _categoryRepo.Save();
        }
        return RedirectToAction("Index");
    }
}
```

### Đăng ký dịch vụ
```csharp
// Trong Program.cs hoặc Startup.cs
services.AddScoped<ICategoryRepository, CategoryRepository>();
```

### Ghi chú thêm
- **Lợi ích của Repository Pattern**:
  - Tăng tính trừu tượng, giảm sự phụ thuộc trực tiếp vào `ApplicationDbContext`.
  - Dễ dàng thay đổi hoặc mở rộng logic mà không ảnh hưởng đến controller.
- **Xử lý lỗi**:
  - Nếu gặp lỗi "Unable to resolve service for type", kiểm tra xem dịch vụ đã được đăng ký đúng trong bộ chứa tiêm phụ thuộc chưa.
- **Kiểm tra ứng dụng**:
  - Sau khi thay đổi, các chức năng CRUD (Create, Read, Update, Delete) vẫn hoạt động như trước, nhưng sử dụng `CategoryRepository` thay vì `ApplicationDbContext`.

