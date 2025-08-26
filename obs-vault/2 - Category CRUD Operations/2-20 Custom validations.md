## Ghi chú học tập: Thêm xác thực tùy chỉnh trong ASP.NET Core

### Khái niệm cơ bản
- **Mục đích**: Tạo xác thực tùy chỉnh (custom validation) để kiểm tra điều kiện đặc biệt, chẳng hạn như đảm bảo `Name` và `DisplayOrder` của danh mục (`Category`) không được giống nhau.
- **Phương pháp**: 
  - Thêm logic xác thực tùy chỉnh trong action `Create` (POST) của `CategoryController` bằng cách kiểm tra `ModelState`.
  - Sử dụng phương thức `ModelState.AddModelError` để thêm thông báo lỗi tùy chỉnh.
  - Hiển thị thông báo lỗi tổng hợp bằng Tag Helper `asp-validation-summary` trong `Create.cshtml`.
- **Công cụ**: `ModelState` (quản lý trạng thái xác thực), Tag Helper (`asp-validation-summary`, `asp-validation-for`), Data Annotations, Razor View (`Create.cshtml`).

### Các bước thực hiện

#### 1. Thêm logic xác thực tùy chỉnh trong CategoryController
- Trong action `Create` (POST), thêm kiểm tra để đảm bảo `Name` không giống với `DisplayOrder`.
- Nếu điều kiện không thỏa mãn, sử dụng `ModelState.AddModelError` để thêm thông báo lỗi tùy chỉnh.
- Mã nguồn:
  ```csharp
  [HttpPost]
  public IActionResult Create(Category obj)
  {
      if (obj.Name == obj.DisplayOrder.ToString())
      {
          ModelState.AddModelError("Name", "The Display Order cannot exactly match the Category Name.");
      }

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
  - `obj.Name == obj.DisplayOrder.ToString()`: So sánh `Name` (kiểu `string`) với `DisplayOrder` (kiểu `int`, được chuyển thành `string`).
  - `ModelState.AddModelError("Name", "...")`: Gắn thông báo lỗi vào trường `Name`. Tham số đầu tiên (`"Name"`) là khóa (key) tương ứng với tên thuộc tính trong model, giúp hiển thị lỗi đúng vị trí trong biểu mẫu.
  - Nếu `ModelState.IsValid` là `false` (do lỗi tùy chỉnh hoặc Data Annotations), trả về view `Create` với dữ liệu đã nhập và thông báo lỗi.

#### 2. Hiển thị thông báo lỗi tổng hợp trong Create.cshtml
- Trong file `Create.cshtml`, thêm Tag Helper `asp-validation-summary` để hiển thị tất cả thông báo lỗi (bao gồm lỗi tùy chỉnh và lỗi từ Data Annotations) ở đầu biểu mẫu.
- Sử dụng thuộc tính `All` để hiển thị tất cả lỗi (cả lỗi liên quan đến trường cụ thể và lỗi không liên quan đến trường cụ thể).
- Thêm lớp Bootstrap `text-danger` để định dạng thông báo lỗi bằng màu đỏ.
- Cập nhật mã nguồn:
  ```cshtml
  @model Category

  <div class="container">
      <div class="row pt-4 pb-2">
          <h2 class="text-primary">Create Category</h2>
          <hr />
          <div asp-validation-summary="All" class="text-danger"></div>
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

#### 3. Kiểm tra và gỡ lỗi
- Thêm điểm ngắt (breakpoint) trong action `Create` (POST) để kiểm tra `ModelState`:
  - Nhập `Name = "5"` và `DisplayOrder = 5`: `ModelState.IsValid` sẽ là `false` do lỗi tùy chỉnh ("The Display Order cannot exactly match the Category Name").
  - Kiểm tra `ModelState` trong debugger để xem chi tiết lỗi.
- Kiểm tra giao diện:
  - Nhấn "Create" với `Name` và `DisplayOrder` giống nhau: 
    - Thông báo lỗi tùy chỉnh hiển thị dưới trường `Name` (do `asp-validation-for="Name"`).
    - Thông báo lỗi tổng hợp hiển thị ở đầu biểu mẫu (do `asp-validation-summary="All"`).
  - Nhập dữ liệu hợp lệ (ví dụ: `Name = "Test"`, `DisplayOrder = 5`): Lưu thành công và chuyển hướng về danh sách.

#### 4. Kết quả
- Khi nhập `Name` và `DisplayOrder` giống nhau (ví dụ: `Name = "5"`, `DisplayOrder = 5`):
  - Lỗi hiển thị dưới trường `Name`: "The Display Order cannot exactly match the Category Name."
  - Lỗi hiển thị ở đầu biểu mẫu (validation summary) với cùng nội dung.
- Các lỗi từ Data Annotations (như `Name` trống hoặc `DisplayOrder` ngoài phạm vi 1-100) cũng hiển thị trong validation summary và dưới các trường tương ứng.
- Giao diện giữ lại dữ liệu đã nhập khi có lỗi, giúp người dùng dễ dàng sửa.

### Ghi chú thêm
- **ModelState.AddModelError**: Cho phép thêm lỗi tùy chỉnh vào `ModelState`, liên kết với trường cụ thể (ví dụ: `Name`) hoặc lỗi chung (sử dụng khóa rỗng `""`).
- **asp-validation-summary**: Tag Helper hiển thị tất cả lỗi xác thực trong một khu vực tổng hợp, hữu ích để cung cấp cái nhìn tổng quan về lỗi.
  - `All`: Hiển thị cả lỗi liên quan đến trường và lỗi chung.
  - Có thể sử dụng `ModelOnly` để chỉ hiển thị lỗi chung hoặc `None` để tắt.
- **Hot Reload**: Hỗ trợ cập nhật giao diện nhanh, nhưng thay đổi trong controller hoặc model có thể yêu cầu khởi động lại ứng dụng.
- Trong bài học tiếp theo: Có thể tích hợp xác thực phía client (client-side validation) với jQuery để kiểm tra dữ liệu trước khi gửi yêu cầu POST.

### Mã nguồn đầy đủ
**Category.cs** (không thay đổi):
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
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("Name", "The Display Order cannot exactly match the Category Name.");
        }

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
        <div asp-validation-summary="All" class="text-danger"></div>
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
- **Custom Validation**: Xác thực tùy chỉnh (customized validation logic).
- **ModelState.AddModelError**: Phương thức thêm lỗi tùy chỉnh vào trạng thái model (method to add custom errors to ModelState).
- **asp-validation-summary**: Tag Helper hiển thị tổng hợp các lỗi xác thực (Tag Helper for validation error summary).
- **Data Annotations**: Ghi chú dữ liệu để xác thực và tùy chỉnh giao diện (data attributes for validation and UI customization).
- **ModelState.IsValid**: Kiểm tra tính hợp lệ của model (model validity check).