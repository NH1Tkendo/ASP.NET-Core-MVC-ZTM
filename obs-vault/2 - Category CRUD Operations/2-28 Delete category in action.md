## Hoàn thiện Chức năng Xóa Danh mục trong ASP.NET Core

### Mục tiêu
- Tạo view `Delete.cshtml` để hiển thị thông tin danh mục cần xóa với các trường bị vô hiệu hóa (disabled).
- Đảm bảo các phương thức `GET` và `POST` cho hành động xóa hoạt động đúng, xóa danh mục khỏi cơ sở dữ liệu.
- Xác nhận việc truyền `Id` từ trang `Index` đến hành động `Delete` và sử dụng trường ẩn trong biểu mẫu.

### Khái niệm
- **View Xóa (Delete.cshtml)**:
  - Tương tự `Edit.cshtml`, nhưng các trường nhập liệu (`Name`, `DisplayOrder`) bị vô hiệu hóa để người dùng chỉ xác nhận xóa.
  - Sử dụng trường ẩn (`<input type="hidden" asp-for="Id" />`) để gửi `Id` đến phương thức `POST`.
- **Truyền `Id`**:
  - Trong `Index.cshtml`, sử dụng `asp-route-id` để truyền `Id` đến hành động `Delete`.
  - Trường ẩn `Id` trong biểu mẫu đảm bảo `Id` được gửi khi người dùng xác nhận xóa.
- **Entity Framework Core**:
  - Sử dụng `_db.Categories.Remove(obj)` để xóa danh mục và `_db.SaveChanges()` để lưu thay đổi vào cơ sở dữ liệu.

### Cách thực hiện
1. **Tạo View `Delete.cshtml`**:
   - Thêm view mới:
     - Nhấp chuột phải vào `Delete` trong `CategoryController` > Add View > Chọn template `Empty` > Đặt tên là `Delete.cshtml`.
   - Sao chép nội dung từ `Edit.cshtml` và điều chỉnh:
     - Đổi tiêu đề thành "Delete Category".
     - Thêm thuộc tính `disabled` cho các trường nhập liệu (`Name`, `DisplayOrder`).
     - Đổi nút thành `btn-danger` và văn bản thành "Delete".
     - Xóa các thẻ validation (`<span asp-validation-for>`) vì không cần kiểm tra hợp lệ khi xóa.
     - Thêm trường ẩn `<input type="hidden" asp-for="Id" />`.

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

2. **Cập nhật Index View**:
   - Đảm bảo nút Delete trong `Index.cshtml` truyền `Id` bằng `asp-route-id`.

   ```html
   <!-- Trong Index.cshtml -->
   <a asp-controller="Category" asp-action="Delete" asp-route-id="@category.Id" class="btn btn-danger mx-2">
       <i class="bi bi-trash-fill"></i> Delete
   </a>
   ```

3. **Kiểm tra phương thức Delete trong CategoryController**:
   - Phương thức `GET Delete` lấy danh mục theo `Id` và truyền vào view.
   - Phương thức `POST DeletePost` xóa danh mục và chuyển hướng về `Index`.

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

4. **Kiểm tra ứng dụng**:
   - Chạy ứng dụng, truy cập trang danh sách (`Index`), nhấn nút Delete cho danh mục (ví dụ: `Test`).
   - Trang `Delete.cshtml` hiển thị thông tin danh mục với các trường bị vô hiệu hóa.
   - Nhấn nút Delete, danh mục được xóa khỏi cơ sở dữ liệu, và ứng dụng chuyển hướng về `Index`.
   - Kiểm tra cơ sở dữ liệu (SQL Server) để xác nhận danh mục đã bị xóa.

### Ví dụ
- **Xóa danh mục**:
  - Chọn danh mục `Test` (`Id = 3`, `DisplayOrder = 7`) trong trang `Index`.
  - Nhấn nút Delete, view `Delete.cshtml` hiển thị `Name = "Test"`, `DisplayOrder = 7` (các trường bị vô hiệu hóa).
  - Nhấn nút Delete, danh mục `Test` bị xóa khỏi cơ sở dữ liệu, và trang `Index` được làm mới không còn danh mục này.

### Mã nguồn
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

### Ghi chú thêm
- **Trường ẩn `Id`**:
  - Mặc dù `Id` được truyền qua `asp-route-id` trong `Index.cshtml`, việc thêm `<input type="hidden" asp-for="Id" />` trong biểu mẫu giúp đảm bảo tính rõ ràng và tránh lỗi.
  - Tương tự, có thể thêm trường ẩn trong `Edit.cshtml` để thống nhất cách xử lý, dù không bắt buộc với thuộc tính `Id`.
- **Validation**:
  - Không cần validation trong `Delete.cshtml` vì người dùng không chỉnh sửa dữ liệu, chỉ xác nhận xóa.
- **Entity Framework Core**:
  - Phương thức `_db.Categories.Remove(obj)` xóa bản ghi dựa trên đối tượng được cung cấp.
  - Gọi `_db.SaveChanges()` để xác nhận xóa trong cơ sở dữ liệu.
- **Tối ưu hóa**:
  - Đảm bảo tên hành động (`Delete`) trong `asp-action` khớp với `[ActionName("Delete")]` trong phương thức `POST`.
  - Kiểm tra cơ sở dữ liệu sau khi xóa để xác nhận bản ghi không còn tồn tại.
- **Hoàn thiện CRUD**:
  - Với việc hoàn thành chức năng xóa, ứng dụng đã hỗ trợ đầy đủ các thao tác CRUD (Create, Read, Update, Delete) cho danh mục.

--- 
*Ghi chú này được tối ưu hóa để lưu trữ trong Obsidian, hỗ trợ ôn tập và liên kết chéo với các ghi chú về ASP.NET Core, Entity Framework Core, và quản lý danh mục.*