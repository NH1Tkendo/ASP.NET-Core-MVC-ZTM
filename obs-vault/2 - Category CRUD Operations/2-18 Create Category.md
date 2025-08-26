## Ghi chú học tập: Xử lý biểu mẫu POST để tạo danh mục mới

### Khái niệm cơ bản
- Mục đích: Xử lý yêu cầu POST từ biểu mẫu trong `Create.cshtml` để lưu danh mục mới (`Category`) vào cơ sở dữ liệu sử dụng Entity Framework Core, sau đó chuyển hướng về trang danh sách danh mục.
- Phương pháp: 
  - Tạo action method `Create` với thuộc tính `[HttpPost]` trong `CategoryController` để nhận dữ liệu từ biểu mẫu.
  - Sử dụng Entity Framework Core để thêm danh mục mới và lưu thay đổi vào cơ sở dữ liệu.
  - Chuyển hướng về action `Index` để hiển thị danh sách danh mục đã cập nhật.
- Công cụ: Entity Framework Core (thêm và lưu dữ liệu), Tag Helpers (liên kết biểu mẫu), Razor View (`Create.cshtml`), và phương thức `RedirectToAction`.

### Các bước thực hiện

#### 1. Tạo Action Method POST trong CategoryController
- Trong `CategoryController`, thêm action method `Create` với thuộc tính `[HttpPost]` để xử lý yêu cầu POST từ biểu mẫu.
- Action này nhận tham số là đối tượng `Category` (được tự động liên kết từ biểu mẫu nhờ Tag Helpers `asp-for`).
- Sử dụng `_db` (ApplicationDbContext) để thêm danh mục mới và lưu thay đổi vào cơ sở dữ liệu.
- Chuyển hướng về action `Index` sau khi lưu thành công.
- Mã nguồn:
  ```csharp
  [HttpPost]
  public IActionResult Create(Category obj)
  {
      _db.Categories.Add(obj);
      _db.SaveChanges();
      return RedirectToAction("Index");
  }
  ```
- **Giải thích**:
  - `obj`: Đối tượng `Category` chứa dữ liệu từ biểu mẫu (`Name`, `DisplayOrder`). Thuộc tính `Id` mặc định là `0` (sẽ được cơ sở dữ liệu tự động gán khi lưu).
  - `_db.Categories.Add(obj)`: Thêm đối tượng `Category` vào `DbSet<Category>`, đánh dấu để thêm vào cơ sở dữ liệu.
  - `_db.SaveChanges()`: Thực thi các thay đổi (thêm danh mục) vào cơ sở dữ liệu.
  - `RedirectToAction("Index")`: Chuyển hướng đến action `Index` trong cùng controller để hiển thị danh sách danh mục đã cập nhật.

#### 2. Kiểm tra liên kết biểu mẫu trong Create.cshtml
- Biểu mẫu trong `Create.cshtml` sử dụng phương thức `POST` và Tag Helpers (`asp-for`) để liên kết các trường nhập liệu với thuộc tính của model `Category`.
- Khi người dùng nhấn nút "Create", dữ liệu từ các trường `<input asp-for="Name">` và `<input asp-for="DisplayOrder">` được gửi đến action `Create` (POST).
- Mã nguồn biểu mẫu (đã tối ưu từ trước):
  ```cshtml
  <form method="post">
      <div class="mb-3 p-0">
          <label asp-for="Name"></label>
          <input asp-for="Name" class="form-control" />
      </div>
      <div class="mb-3 p-0">
          <label asp-for="DisplayOrder"></label>
          <input asp-for="DisplayOrder" class="form-control" />
      </div>
      <div class="row">
          <div class="col-6 col-md-3">
              <button type="submit" class="btn btn-primary form-control">Create</button>
          </div>
          <div class="col-6 col-md-3">
              <a asp-controller="Category" asp-action="Index" class="btn btn-outline-secondary form-control">Back to List</a>
          </div>
      </div>
  </form>
  ```

#### 3. Kiểm tra và gỡ lỗi
- Thêm điểm ngắt (breakpoint) trong action `Create` (POST) để kiểm tra đối tượng `Category` (`obj`) nhận được từ biểu mẫu.
- Ví dụ:
  - Nhập `Name = "Test Category"` và `DisplayOrder = 5` trong biểu mẫu.
  - Khi nhấn "Create", đối tượng `obj` sẽ có:
    - `Id = 0` (mặc định, cơ sở dữ liệu sẽ tự động gán).
    - `Name = "Test Category"`.
    - `DisplayOrder = 5`.
- Sau khi thực thi `_db.SaveChanges()`, danh mục mới được lưu vào cơ sở dữ liệu.
- Chuyển hướng đến action `Index` sẽ tải lại danh sách danh mục, bao gồm danh mục vừa thêm.

#### 4. Kết quả
- Sau khi nhấn "Create", danh mục mới (ví dụ: "Test Category", `DisplayOrder = 5`) xuất hiện trong danh sách danh mục ở trang `Index`.
- Không cần viết câu lệnh SQL thủ công; Entity Framework Core tự động xử lý việc thêm dữ liệu và quản lý kết nối cơ sở dữ liệu.

### Ghi chú thêm
- **Tag Helpers**: Tự động liên kết dữ liệu biểu mẫu với model, giảm thiểu mã thủ công và lỗi.
- **_db.SaveChanges()**: Quan trọng để thực thi các thay đổi (thêm, sửa, xóa) vào cơ sở dữ liệu. Nếu thiếu, dữ liệu chỉ được đánh dấu mà không thực sự lưu.
- **RedirectToAction**: Đảm bảo trải nghiệm người dùng mượt mà bằng cách chuyển hướng đến danh sách sau khi tạo danh mục, thay vì hiển thị view trực tiếp.
- **Entity Framework Core**: Loại bỏ nhu cầu quản lý kết nối cơ sở dữ liệu hoặc viết SQL, giúp mã ngắn gọn và dễ bảo trì.
- Trong bài học tiếp theo: Có thể thêm xác thực dữ liệu (validation) để đảm bảo thông tin nhập vào hợp lệ trước khi lưu.

### Mã nguồn đầy đủ
**CategoryController.cs**:
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

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Category obj)
    {
        _db.Categories.Add(obj);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
}
```

**Create.cshtml**:
```cshtml
@model Category

<div class="container">
    <div class="row pt-4 pb-2">
        <h2 class="text-primary">Create Category</h2>
        <hr />
    </div>
    <div class="row mb-3">
        <form method="post">
            <div class="mb-3 p-0">
                <label asp-for="Name"></label>
                <input asp-for="Name" class="form-control" />
            </div>
            <div class="mb-3 p-0">
                <label asp-for="DisplayOrder"></label>
                <input asp-for="DisplayOrder" class="form-control" />
            </div>
            <div class="row">
                <div class="col-6 col-md-3">
                    <button type="submit" class="btn btn-primary form-control">Create</button>
                </div>
                <div class="col-6 col-md-3">
                    <a asp-controller="Category" asp-action="Index" class="btn btn-outline-secondary form-control">Back to List</a>
                </div>
            </div>
        </form>
    </div>
</div>
```

### Thuật ngữ chuyên ngành
- **HttpPost**: Thuộc tính chỉ định action xử lý yêu cầu POST (attribute for POST request handling).
- **RedirectToAction**: Phương thức chuyển hướng đến action khác (method for redirecting to another action).
- **SaveChanges**: Phương thức Entity Framework Core thực thi thay đổi vào cơ sở dữ liệu (method to commit database changes).
- **Tag Helper (asp-for)**: Công cụ liên kết biểu mẫu với model (form-model binding tool).
- **Entity Framework Core**: Framework quản lý cơ sở dữ liệu (database management framework).