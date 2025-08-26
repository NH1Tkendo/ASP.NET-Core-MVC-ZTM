## Hoàn thiện Trang Chỉnh sửa Danh mục trong ASP.NET Core

### Mục tiêu
- Tạo view `Edit.cshtml` cho trang chỉnh sửa danh mục, tái sử dụng giao diện từ trang `Create.cshtml` với các trường được điền sẵn.
- Triển khai phương thức `POST` trong `CategoryController` để cập nhật danh mục trong cơ sở dữ liệu sử dụng Entity Framework Core.
- Hiểu cách sử dụng Razor View và các tùy chọn tạo view trong Visual Studio.
- Đảm bảo client-side validation hoạt động trên trang chỉnh sửa.

### Khái niệm
- **Trang Chỉnh sửa (Edit View)**:
  - Giao diện tương tự trang tạo (`Create`), nhưng các trường như `Name` và `DisplayOrder` được tự động điền dựa trên dữ liệu danh mục được lấy từ cơ sở dữ liệu.
  - Sử dụng **tag helpers** để tự động hiển thị giá trị từ mô hình (model).
- **Razor View**:
  - Là một loại view trong ASP.NET Core, hỗ trợ các mẫu (template) như Create, Edit, Details, hoặc Empty.
  - Có thể chọn sử dụng layout mặc định hoặc bỏ qua để tùy chỉnh.
- **Cập nhật danh mục**:
  - Sử dụng phương thức `Update` của Entity Framework Core để cập nhật bản ghi trong cơ sở dữ liệu dựa trên ID.
- **Client-Side Validation**:
  - Tích hợp sẵn từ trang `Create`, đảm bảo kiểm tra hợp lệ (validation) hoạt động tương tự trên trang `Edit`.

### Cách thực hiện
1. **Tạo View `Edit.cshtml`**:
   - Thêm view mới trong thư mục `Views/Category` bằng cách:
     - Nhấp chuột phải > Add > Razor View.
     - Chọn tên view là `Edit`, sử dụng template `Empty` (hoặc có thể chọn `Edit` nếu muốn scaffolding tự động).
     - Sử dụng layout mặc định (thường là `_Layout.cshtml`) để đảm bảo giao diện nhất quán.
   - Sao chép nội dung từ `Create.cshtml` và điều chỉnh:
     - Đổi tiêu đề từ "Create Category" thành "Edit Category".
     - Đổi `asp-action` từ `Create` thành `Edit`.
     - Thêm trường ẩn (`<input type="hidden" asp-for="Id" />`) để gửi ID khi cập nhật.

   ```html
   <!-- Trong Edit.cshtml -->
   @model Category

   <h2>Edit Category</h2>
   <form asp-action="Edit">
       <input type="hidden" asp-for="Id" />
       <div class="form-group">
           <label asp-for="Name"></label>
           <input asp-for="Name" class="form-control" />
           <span asp-validation-for="Name" class="text-danger"></span>
       </div>
       <div class="form-group">
           <label asp-for="DisplayOrder"></label>
           <input asp-for="DisplayOrder" class="form-control" />
           <span asp-validation-for="DisplayOrder" class="text-danger"></span>
       </div>
       <button type="submit" class="btn btn-primary">Update</button>
   </form>

   @section Scripts {
       <partial name="_ValidationScriptsPartial" />
   }
   ```

2. **Triển khai phương thức POST trong CategoryController**:
   - Tạo phương thức `Edit` (POST) để nhận đối tượng `Category` từ biểu mẫu và cập nhật vào cơ sở dữ liệu.
   - Sử dụng phương thức `Update` của Entity Framework Core để cập nhật bản ghi dựa trên `Id`.

   ```csharp
   // Trong CategoryController
   [HttpPost]
   [ValidateAntiForgeryToken]
   public IActionResult Edit(Category obj)
   {
       if (!ModelState.IsValid)
       {
           return View(obj);
       }

       _db.Update(obj);
       _db.SaveChanges();
       return RedirectToAction("Index");
   }
   ```

3. **Tích hợp Client-Side Validation**:
   - Đảm bảo partial view `_ValidationScriptsPartial` được nhúng trong section `Scripts` để kích hoạt client-side validation.
   - Validation sẽ tự động áp dụng cho các trường như `Name` và `DisplayOrder`, hiển thị lỗi ngay lập tức nếu dữ liệu không hợp lệ.

4. **Truyền ID từ Index View**:
   - Đảm bảo thẻ `<a>` trong `Index.cshtml` truyền `Id` thông qua `asp-route-id`.

   ```html
   <!-- Trong Index.cshtml -->
   <a asp-controller="Category" asp-action="Edit" asp-route-id="@category.Id" class="btn btn-primary mx-2">
       <i class="bi bi-pencil"></i> Edit
   </a>
   ```

### Ví dụ
- **Hiển thị dữ liệu**:
  - Khi nhấn nút Edit trên danh mục (ví dụ: `Id = 2`, `Name = "SciFi"`, `DisplayOrder = 2`):
    - View `Edit.cshtml` tự động hiển thị `Name = "SciFi"` và `DisplayOrder = 2` trong các trường nhập liệu.
    - Điều này nhờ vào tag helpers (`asp-for`) và mô hình `Category` được truyền từ phương thức GET.
- **Cập nhật dữ liệu**:
  - Người dùng chỉnh sửa `Name` hoặc `DisplayOrder`, nhấn nút Update.
  - Phương thức POST nhận đối tượng `Category`, gọi `_db.Update(obj)` để cập nhật bản ghi dựa trên `Id`, sau đó lưu thay đổi bằng `_db.SaveChanges()`.
  - Chuyển hướng về trang `Index` sau khi cập nhật thành công.
- **Client-Side Validation**:
  - Nếu người dùng xóa `DisplayOrder` hoặc nhập giá trị không hợp lệ, lỗi hiển thị ngay trên trình duyệt mà không cần gửi yêu cầu đến máy chủ.

### Mã nguồn
```csharp
// Trong CategoryController
public IActionResult Edit(int? id)
{
    if (id == null || id == 0)
    {
        return NotFound();
    }

    var categoryFromDb = _db.Categories.Find(id);
    if (categoryFromDb == null)
    {
        return NotFound();
    }

    return View(categoryFromDb);
}

[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Edit(Category obj)
{
    if (!ModelState.IsValid)
    {
        return View(obj);
    }

    _db.Update(obj);
    _db.SaveChanges();
    return RedirectToAction("Index");
}
```

```html
<!-- Trong Edit.cshtml -->
@model Category

<h2>Edit Category</h2>
<form asp-action="Edit">
    <input type="hidden" asp-for="Id" />
    <div class="form-group">
        <label asp-for="Name"></label>
        <input asp-for="Name" class="form-control" />
        <span asp-validation-for="Name" class="text-danger"></span>
    </div>
    <div class="form-group">
        <label asp-for="DisplayOrder"></label>
        <input asp-for="DisplayOrder" class="form-control" />
        <span asp-validation-for="DisplayOrder" class="text-danger"></span>
    </div>
    <button type="submit" class="btn btn-primary">Update</button>
</form>

@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}
```

### Ghi chú thêm
- **Sử dụng Layout**:
  - Khi tạo Razor View, chọn layout mặc định (`_Layout.cshtml`) để đảm bảo giao diện nhất quán với các trang khác.
  - Nếu không chọn layout, view sẽ render như một trang HTML độc lập, không mong muốn trong trường hợp này.
- **Scaffolding**:
  - Visual Studio hỗ trợ scaffolding tự động cho các view (Create, Edit, Details), nhưng có thể gặp lỗi nếu cấu hình không chính xác.
  - Sử dụng template `Empty` và sao chép từ `Create.cshtml` là cách an toàn và linh hoạt.
- **Entity Framework Core**:
  - Phương thức `_db.Update(obj)` tự động cập nhật tất cả thuộc tính của đối tượng dựa trên `Id`.
  - Đảm bảo gọi `_db.SaveChanges()` để lưu thay đổi vào cơ sở dữ liệu.
- **Tối ưu hóa**:
  - Thêm trường ẩn `Id` trong biểu mẫu để đảm bảo gửi đúng ID khi cập nhật.
  - Kiểm tra `ModelState.IsValid` trong phương thức POST để xử lý lỗi validation trước khi cập nhật.
  - Sử dụng `[ValidateAntiForgeryToken]` để bảo vệ chống lại các cuộc tấn công CSRF.

--- 
*Ghi chú này được tối ưu hóa để lưu trữ trong Obsidian, hỗ trợ ôn tập và liên kết chéo với các ghi chú về ASP.NET Core, Entity Framework Core, và Razor View.*