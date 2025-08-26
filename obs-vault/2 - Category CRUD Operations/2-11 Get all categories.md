## Ghi chú học tập: Truy xuất và hiển thị danh mục trong Entity Framework Core

### Khái niệm cơ bản
- Mục đích: Truy xuất dữ liệu từ bảng danh mục (`Category`) trong cơ sở dữ liệu và truyền dữ liệu này đến `Index View` để hiển thị trên giao diện người dùng (UI).
- Phương pháp: Sử dụng `ApplicationDbContext` trong controller để truy xuất danh sách danh mục thông qua Entity Framework Core, sau đó truyền dữ liệu đến view.
- Công cụ: Dependency Injection (DI) trong .NET Core để quản lý `ApplicationDbContext`, thay vì tạo đối tượng thủ công như trong các ứng dụng .NET legacy.

### Các bước thực hiện

#### 1. Sử dụng Dependency Injection để nhận ApplicationDbContext
- Trong .NET Core, `ApplicationDbContext` được đăng ký trong container dịch vụ (services container) để sử dụng thông qua Dependency Injection.
- Không cần tạo đối tượng `ApplicationDbContext` thủ công hay quản lý kết nối cơ sở dữ liệu (mở/đóng kết nối).
- Cách thực hiện:
  - Trong constructor của controller, yêu cầu một thể hiện (instance) của `ApplicationDbContext`.
  - Lưu thể hiện này vào một biến private để sử dụng trong các action method.
- Mã nguồn:
  ```csharp
  private readonly ApplicationDbContext _db;

  public CategoryController(ApplicationDbContext db)
  {
      _db = db;
  }
  ```

#### 2. Truy xuất danh sách danh mục trong action Index
- Trong action `Index` của controller, sử dụng `_db` để truy xuất toàn bộ danh mục từ bảng `Category`.
- Sử dụng Entity Framework Core để lấy danh sách danh mục mà không cần viết câu lệnh SQL.
- Phương thức `ToList()` chuyển dữ liệu từ `DbSet<Category>` thành danh sách (`List<Category>`).
- Mã nguồn:
  ```csharp
  public IActionResult Index()
  {
      List<Category> objCategoryList = _db.Categories.ToList();
      return View(objCategoryList);
  }
  ```

#### 3. Kết quả truy xuất
- Sau khi chạy action `Index`, danh sách danh mục (`objCategoryList`) sẽ chứa các bản ghi từ bảng `Category` (ví dụ: `Action`, `SciFi`, `History`).
- Có thể sử dụng breakpoint trong quá trình debug để kiểm tra dữ liệu:
  - Kết quả: Danh sách chứa ba đối tượng với các thuộc tính `Id`, `Name`, và `DisplayOrder`.

#### 4. Truyền dữ liệu đến View
- Danh sách `objCategoryList` được truyền đến `Index View` thông qua `return View(objCategoryList)`.
- Trong view, cần viết mã để truy xuất và hiển thị danh sách này (sẽ được trình bày trong bài tiếp theo).

### Ghi chú thêm
- **Dependency Injection**: Cơ chế trong .NET Core giúp cung cấp các thể hiện của `ApplicationDbContext` mà không cần tạo thủ công, dựa trên cấu hình trong container dịch vụ.
- **Entity Framework Core**: Cho phép truy xuất dữ liệu dễ dàng với cú pháp đơn giản (`_db.Categories.ToList()`), tự động sinh câu lệnh SQL (`SELECT * FROM Categories`).
- **Ưu điểm**: Không cần viết SQL thủ công, giảm thiểu lỗi và tăng tính bảo trì.
- Trong bài học tiếp theo: Tìm hiểu cách hiển thị danh sách danh mục trong `Index View`.

### Mã nguồn đầy đủ
```csharp
public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        List<Category> objCategoryList = _db.Categories.ToList();
        return View(objCategoryList);
    }
}
```

### Thuật ngữ chuyên ngành
- **Dependency Injection (DI)**: Tiêm phụ thuộc (mechanism for providing dependencies).
- **ApplicationDbContext**: Ngữ cảnh cơ sở dữ liệu (database context).
- **DbSet**: Tập hợp thực thể trong Entity Framework Core (entity set in Entity Framework Core).
- **ToList()**: Phương thức chuyển dữ liệu thành danh sách (convert query results to a list).
- **Index View**: Giao diện hiển thị danh sách (list display interface).