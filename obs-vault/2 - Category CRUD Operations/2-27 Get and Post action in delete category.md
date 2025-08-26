## Xóa Danh mục trong ASP.NET Core

### Mục tiêu
- Triển khai chức năng xóa (Delete) danh mục, bao gồm phương thức `GET` để hiển thị thông tin danh mục và phương thức `POST` để xóa danh mục khỏi cơ sở dữ liệu.
- Tạo view `Delete.cshtml` tương tự `Edit.cshtml`, nhưng các trường nhập liệu sẽ bị vô hiệu hóa (disabled).
- Đảm bảo hành động xóa sử dụng Entity Framework Core để xóa bản ghi dựa trên `Id`.

### Khái niệm
- **Chức năng Xóa**:
  - Hiển thị thông tin danh mục (như `Name` và `DisplayOrder`) trong một biểu mẫu với các trường bị vô hiệu hóa để người dùng xác nhận trước khi xóa.
  - Phương thức `GET` lấy danh mục từ cơ sở dữ liệu và hiển thị trong view.
  - Phương thức `POST` xóa danh mục khỏi cơ sở dữ liệu và chuyển hướng về trang danh sách (`Index`).
- **Phương thức `Remove` của Entity Framework Core**:
  - Sử dụng `_db.Categories.Remove(obj)` để xóa bản ghi dựa trên đối tượng được cung cấp.
  - Yêu cầu gọi `_db.SaveChanges()` để lưu thay đổi vào cơ sở dữ liệu.
- **Xử lý tên phương thức**:
  - Nếu phương thức `GET` và `POST` có cùng tên (`Delete`) và cùng tham số (`int? id`), cần đặt tên endpoint rõ ràng bằng `[ActionName("Delete")]` cho phương thức `POST`.

### Cách thực hiện
1. **Tạo phương thức GET và POST trong CategoryController**:
   - Phương thức `GET Delete`: Lấy danh mục theo `Id` và truyền vào view.
   - Phương thức `POST DeletePost`: Xóa danh mục khỏi cơ sở dữ liệu dựa trên `Id`.

   ```csharp
   // Trong CategoryController
   public IActionResult Delete(int? id)
   {
       if (id == null || id == 0)
       {
           return NotFound();
       }

       var obj = _db.Categories.Find(id);
       if (obj == null)
       {
           return NotFound();
       }

       return View(obj);
   }

   [HttpPost]
   [ValidateAntiForgeryToken]
   [ActionName("Delete")]
   public IActionResult DeletePost(int? id)
   {
       if (id == null || id == 0)
       {
           return NotFound();
       }

       var obj = _db.Categories.Find(id);
       if (obj == null)
       {
           return NotFound();
       }

       _db.Categories.Remove(obj);
       _db.SaveChanges();
       return RedirectToAction("Index");
   }
   ```

2. **Tạo View `Delete.cshtml`**:
   - Sao chép nội dung từ `Edit.cshtml`, nhưng:
     - Vô hiệu hóa các trường nhập liệu bằng thuộc tính `disabled`.
     - Đổi tiêu đề thành "Delete Category" và nút thành "Delete".
     - Đảm bảo gửi `Id` qua trường ẩn (`<input type="hidden" asp-for="Id" />`).

   ```html
   <!-- Trong Delete.cshtml -->
   @model Category

   <h2>Delete Category</h2>
   <form asp-action="Delete">
       <input type="hidden" asp-for="Id" />
       <div class="form-group">
           <label asp-for="Name"></label>
           <input asp-for="Name" class="form-control" disabled />
       </div>
       <div class="form-group">
           <label asp-for="DisplayOrder"></label>
           <input asp-for="DisplayOrder" class="form-control" disabled />
       </div>
       <button type="submit" class="btn btn-danger">Delete</button>
   </form>
   ```

3. **Liên kết từ Index View**:
   - Đảm bảo nút Delete trong `Index.cshtml` gọi đúng hành động `Delete` và truyền `Id`.

   ```html
   <!-- Trong Index.cshtml -->
   <a asp-controller="Category" asp-action="Delete" asp-route-id="@category.Id" class="btn btn-danger mx-2">
       <i class="bi bi-trash-fill"></i> Delete
   </a>
   ```

### Ví dụ
- **Hiển thị trang xóa**:
  - Nhấn nút Delete trên danh mục `Action` (`Id = 1`).
  - View `Delete.cshtml` hiển thị `Name = "Action"` và `DisplayOrder = 1` trong các trường bị vô hiệu hóa.
- **Xóa danh mục**:
  - Nhấn nút Delete, phương thức `DeletePost` được gọi.
  - Danh mục được xóa khỏi cơ sở dữ liệu, ứng dụng chuyển hướng về trang `Index`.

### Mã nguồn
```csharp
// Trong CategoryController
public IActionResult Delete(int? id)
{
    if (id == null || id == 0)
    {
        return NotFound();
    }

    var obj = _db.Categories.Find(id);
    if (obj == null)
    {
        return NotFound();
    }

    return View(obj);
}

[HttpPost]
[ValidateAntiForgeryToken]
[ActionName("Delete")]
public IActionResult DeletePost(int? id)
{
    if (id == null || id == 0)
    {
        return NotFound();
    }

    var obj = _db.Categories.Find(id);
    if (obj == null)
    {
        return NotFound();
    }

    _db.Categories.Remove(obj);
    _db.SaveChanges();
    return RedirectToAction("Index");
}
```

```html
<!-- Trong Delete.cshtml -->
@model Category

<h2>Delete Category</h2>
<form asp-action="Delete">
    <input type="hidden" asp-for="Id" />
    <div class="form-group">
        <label asp-for="Name"></label>
        <input asp-for="Name" class="form-control" disabled />
    </div>
    <div class="form-group">
        <label asp-for="DisplayOrder"></label>
        <input asp-for="DisplayOrder" class="form-control" disabled />
    </div>
    <button type="submit" class="btn btn-danger">Delete</button>
</form>
```

### Ghi chú thêm
- **Trường ẩn `Id`**:
  - Bắt buộc có `<input type="hidden" asp-for="Id" />` trong biểu mẫu để gửi `Id` đến phương thức `POST`.
  - Nếu thiếu, phương thức `DeletePost` có thể không xác định được danh mục cần xóa.
- **[ActionName("Delete")]**:
  - Được sử dụng để đảm bảo phương thức `DeletePost` ánh xạ đúng với hành động `Delete` trong biểu mẫu, tránh xung đột với phương thức `GET Delete`.
- **Entity Framework Core**:
  - Phương thức `_db.Categories.Remove(obj)` xóa bản ghi dựa trên đối tượng được cung cấp.
  - Gọi `_db.SaveChanges()` để xác nhận xóa trong cơ sở dữ liệu.
- **Tối ưu hóa**:
  - Các trường trong `Delete.cshtml` được vô hiệu hóa (`disabled`) để người dùng chỉ xác nhận xóa mà không chỉnh sửa dữ liệu.
  - Thêm `[ValidateAntiForgeryToken]` để bảo vệ chống lại các cuộc tấn công CSRF.
  - Kiểm tra `id` hợp lệ (`null` hoặc `0`) và danh mục tồn tại để tránh lỗi.

--- 
*Ghi chú này được tối ưu hóa để lưu trữ trong Obsidian, hỗ trợ ôn tập và liên kết chéo với các ghi chú về ASP.NET Core, Entity Framework Core, và quản lý danh mục.*