## Ghi chú học tập: Thêm xác thực dữ liệu (Validation) cho biểu mẫu tạo danh mục

### Khái niệm cơ bản
- Mục đích: Thêm xác thực dữ liệu (validation) cho biểu mẫu tạo danh mục để đảm bảo dữ liệu hợp lệ trước khi lưu vào cơ sở dữ liệu, hiển thị thông báo lỗi cho người dùng khi dữ liệu không hợp lệ.
- Phương pháp:
  - Sử dụng **Data Annotations** trong model `Category` để định nghĩa các quy tắc xác thực (ví dụ: trường bắt buộc, độ dài tối đa, phạm vi giá trị).
  - Kiểm tra trạng thái model (`ModelState.IsValid`) trong action `Create` (POST) để quyết định có lưu dữ liệu hay không.
  - Sử dụng **Tag Helpers** (`asp-validation-for`) trong `Create.cshtml` để hiển thị thông báo lỗi.
- Công cụ: Data Annotations (xác thực phía server), Tag Helpers (hiển thị lỗi), Entity Framework Core, Razor View (`Create.cshtml`).

### Các bước thực hiện

#### 1. Thêm Data Annotations trong Model Category
- Mở file `Category.cs` và thêm các thuộc tính Data Annotations để định nghĩa quy tắc xác thực:
  - `[Required]`: Đảm bảo trường `Name` không được để trống.
  - `[MaxLength(30)]`: Giới hạn độ dài tối đa của `Name` là 30 ký tự.
  - `[Range(1, 100)]`: Giới hạn giá trị `DisplayOrder` từ 1 đến 100, với thông báo lỗi tùy chỉnh.
- Mã nguồn:
  ```csharp
  using System.ComponentModel.DataAnnotations;

  public class Category
  {
      public int Id { get; set; }

      [Required]
      [Display(Name = "Category Name")]
      [MaxLength(30)]
      public string Name { get; set; }

      [Display(Name = "Display Order")]
      [Range(1, 100, ErrorMessage = "Display Order must be between 1-100")]
      public int DisplayOrder { get; set; }
  }
  ```

#### 2. Kiểm tra xác thực trong Action Create (POST)
- Trong `CategoryController`, cập nhật action `Create` (POST) để kiểm tra `ModelState.IsValid` trước khi lưu danh mục.
- Nếu `ModelState.IsValid` là `false` (dữ liệu không hợp lệ), trả về lại view `Create` để hiển thị biểu mẫu cùng thông báo lỗi.
- Nếu hợp lệ, lưu danh mục và chuyển hướng về action `Index`.
- Mã nguồn:
  ```csharp
  [HttpPost]
  public IActionResult Create(Category obj)
  {
      if (ModelState.IsValid)
      {
          _db.Categories.Add(obj);
          _db.SaveChanges();
          return RedirectToAction("Index");
      }
      return View(obj);
  }
  ```
- **Giải thích**:
  - `ModelState.IsValid`: Kiểm tra xem đối tượng `Category` (`obj`) có thỏa mãn các quy tắc xác thực trong model hay không.
  - Nếu không hợp lệ (ví dụ: `Name` trống hoặc `DisplayOrder` ngoài phạm vi 1-100), trả về view `Create` với đối tượng `obj` để giữ lại dữ liệu đã nhập.
  - Nếu hợp lệ, thực hiện lưu và chuyển hướng.

#### 3. Hiển thị thông báo lỗi trong Create.cshtml
- Trong file `Create.cshtml`, thêm Tag Helper `asp-validation-for` để hiển thị thông báo lỗi cho từng trường (`Name`, `DisplayOrder`).
- Sử dụng lớp Bootstrap `text-danger` để định dạng thông báo lỗi bằng màu đỏ.
- Cập nhật biểu mẫu:
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
                  <span asp-validation-for="Name" class="text-danger"></span>
              </div>
              <div class="mb-3 p-0">
                  <label asp-for="DisplayOrder"></label>
                  <input asp-for="DisplayOrder" class="form-control" />
                  <span asp-validation-for="DisplayOrder" class="text-danger"></span>
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

#### 4. Kiểm tra và gỡ lỗi
- Thêm điểm ngắt (breakpoint) trong action `Create` (POST) để kiểm tra `ModelState`:
  - Nếu nhập `Name` trống hoặc `DisplayOrder = 0`, `ModelState.IsValid` sẽ là `false`.
  - `ModelState` chứa thông tin chi tiết về lỗi (ví dụ: "The Name field is required" hoặc "Display Order must be between 1-100").
- Kiểm tra giao diện:
  - Nhấn "Create" với `Name` trống: Hiển thị lỗi "The Name field is required".
  - Nhập `DisplayOrder = 0`: Hiển thị lỗi "Display Order must be between 1-100".
  - Nhập dữ liệu hợp lệ (ví dụ: `Name = "Test"`, `DisplayOrder = 5`): Lưu thành công và chuyển hướng về danh sách.

#### 5. Tùy chỉnh thông báo lỗi
- Thông báo lỗi mặc định được tạo tự động từ Data Annotations, nhưng có thể tùy chỉnh thông qua thuộc tính `ErrorMessage` trong `[Range]` hoặc các annotation khác.
- Ví dụ: `[Range(1, 100, ErrorMessage = "Display Order must be between 1-100")]` thay thế thông báo lỗi mặc định bằng thông điệp tùy chỉnh.

### Ghi chú thêm
- **ModelState.IsValid**: Kiểm tra tất cả các quy tắc xác thực trong model, dựa trên Data Annotations.
- **asp-validation-for**: Tag Helper hiển thị thông báo lỗi cho từng thuộc tính, tích hợp chặt chẽ với `ModelState`.
- **Data Annotations**: Hỗ trợ xác thực phía server và cung cấp thông báo lỗi tự động, giảm thiểu mã JavaScript thủ công.
- **Hot Reload**: Hỗ trợ cập nhật giao diện nhanh, nhưng có thể cần khởi động lại ứng dụng khi thay đổi model hoặc controller.
- Trong bài học tiếp theo: Có thể thêm xác thực phía client (client-side validation) với jQuery để kiểm tra dữ liệu trước khi gửi yêu cầu POST.

### Mã nguồn đầy đủ
**Category.cs**:
```csharp
using System.ComponentModel.DataAnnotations;

public class Category
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Category Name")]
    [MaxLength(30)]
    public string Name { get; set; }

    [Display(Name = "Display Order")]
    [Range(1, 100, ErrorMessage = "Display Order must be between 1-100")]
    public int DisplayOrder { get; set; }
}
```

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
        if (ModelState.IsValid)
        {
            _db.Categories.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(obj);
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
                <span asp-validation-for="Name" class="text-danger"></span>
            </div>
            <div class="mb-3 p-0">
                <label asp-for="DisplayOrder"></label>
                <input asp-for="DisplayOrder" class="form-control" />
                <span asp-validation-for="DisplayOrder" class="text-danger"></span>
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
- **Data Annotations**: Ghi chú dữ liệu để xác thực và tùy chỉnh giao diện (data attributes for validation and UI customization).
- **ModelState.IsValid**: Kiểm tra tính hợp lệ của model dựa trên Data Annotations (model validity check).
- **asp-validation-for**: Tag Helper hiển thị thông báo lỗi xác thực (Tag Helper for validation error messages).
- **Required**: Thuộc tính Data Annotation yêu cầu trường không được để trống (attribute for mandatory fields).
- **Range**: Thuộc tính Data Annotation giới hạn giá trị trong một phạm vi (attribute for value range validation).