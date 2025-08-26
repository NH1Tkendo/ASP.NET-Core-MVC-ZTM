## Cập nhật và Xử lý Lỗi khi Chỉnh sửa Danh mục trong ASP.NET Core

### Mục tiêu
- Kiểm tra chức năng chỉnh sửa (Edit) danh mục đã triển khai để đảm bảo hoạt động đúng.
- Hiểu vai trò của trường ẩn `Id` trong biểu mẫu chỉnh sửa và cách xử lý lỗi khi ID không được truyền đúng.
- Chuẩn bị cho việc triển khai chức năng xóa (Delete) danh mục.

### Khái niệm
- **Chức năng Chỉnh sửa**:
  - Cho phép cập nhật thông tin danh mục (như `Name` và `DisplayOrder`) dựa trên `Id` của danh mục.
  - Dữ liệu được gửi từ biểu mẫu (form) đến phương thức `POST` trong `CategoryController` để cập nhật cơ sở dữ liệu.
- **Trường ẩn `Id`**:
  - Sử dụng `<input type="hidden" asp-for="Id" />` để gửi `Id` của danh mục khi cập nhật.
  - Nếu `Id` không được gửi hoặc có giá trị `0`, Entity Framework Core sẽ tạo bản ghi mới thay vì cập nhật bản ghi hiện có.
- **Hành vi Entity Framework Core**:
  - Phương thức `_db.Update(obj)` dựa trên `Id` để xác định bản ghi cần cập nhật.
  - Nếu `Id = 0`, Entity Framework Core coi đây là bản ghi mới, dẫn đến thêm mới thay vì cập nhật.

### Cách thực hiện
1. **Kiểm tra chức năng chỉnh sửa**:
   - Trong ứng dụng:
     - Truy cập trang danh sách (`Index`), nhấn nút Edit cho một danh mục (ví dụ: `Action`).
     - Cập nhật giá trị (ví dụ: `Name = "Action 111"`, `DisplayOrder = 5`).
     - Nhấn nút Update, kiểm tra xem danh mục được cập nhật chính xác trong cơ sở dữ liệu và hiển thị trên trang `Index`.
   - Đặt breakpoint trong phương thức `Edit` (POST) để kiểm tra giá trị `obj.Id`.

2. **Đảm bảo trường ẩn `Id`**:
   - Trong `Edit.cshtml`, trường ẩn `<input type="hidden" asp-for="Id" />` đảm bảo `Id` được gửi cùng biểu mẫu.
   - Nếu tên thuộc tính không phải `Id` (ví dụ: `CategoryId`), phải thêm trường ẩn tương ứng:

   ```html
   <!-- Trong Edit.cshtml -->
   <input type="hidden" asp-for="Id" />
   ```

3. **Xử lý lỗi khi cập nhật**:
   - Nếu `Id` không được gửi hoặc bằng `0`, Entity Framework Core sẽ tạo bản ghi mới thay vì cập nhật.
   - Để khắc phục:
     - Đảm bảo trường ẩn `Id` có trong biểu mẫu.
     - Đặt breakpoint trong phương thức `Edit` (POST) để kiểm tra `obj.Id`.
     - Nếu `Id = 0`, kiểm tra view `Edit.cshtml` để đảm bảo trường ẩn được cấu hình đúng.

4. **Triển khai phương thức POST**:
   - Phương thức `Edit` (POST) nhận đối tượng `Category`, kiểm tra `ModelState`, cập nhật bản ghi, và chuyển hướng về `Index`.

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

### Ví dụ
- **Cập nhật danh mục**:
  - Truy cập danh mục `Action` (`Id = 1`), chỉnh sửa thành `Name = "Action 111"`, `DisplayOrder = 5`.
  - Nhấn Update, danh mục được cập nhật trong cơ sở dữ liệu và hiển thị trên trang `Index`.
  - Nếu quay lại chỉnh sửa về giá trị ban đầu (`Action`, `DisplayOrder = 1`), cập nhật cũng thành công.
- **Kiểm tra lỗi**:
  - Nếu `Id` không được gửi (hoặc `Id = 0`):
    - Entity Framework Core tạo bản ghi mới với `Name = "Action 111"`, thay vì cập nhật danh mục hiện có.
    - Kiểm tra bằng breakpoint trong phương thức `Edit` (POST): Nếu `obj.Id = 0`, thêm trường ẩn `<input type="hidden" asp-for="Id" />` vào `Edit.cshtml`.

### Mã nguồn
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

### Ghi chú thêm
- **Trường ẩn `Id`**:
  - Nếu mô hình sử dụng tên khác `Id` (ví dụ: `CategoryId`), phải cấu hình trường ẩn tương ứng (ví dụ: `<input type="hidden" asp-for="CategoryId" />`).
  - Không có trường ẩn hoặc `Id = 0` dẫn đến thêm bản ghi mới thay vì cập nhật.
- **Debugging**:
  - Đặt breakpoint trong phương thức `Edit` (POST) để kiểm tra giá trị `obj.Id`.
  - Nếu thấy bản ghi mới được tạo, kiểm tra `Edit.cshtml` để đảm bảo trường ẩn `Id` được thêm đúng.
- **Entity Framework Core**:
  - Phương thức `_db.Update(obj)` yêu cầu `Id` hợp lệ để xác định bản ghi cần cập nhật.
  - Gọi `_db.SaveChanges()` để lưu thay đổi vào cơ sở dữ liệu.
- **Chuẩn bị cho chức năng xóa**:
  - Chức năng xóa (Delete) sẽ được triển khai tiếp theo, yêu cầu phương thức GET và POST trong `CategoryController` để xóa danh mục theo `Id`.

--- 
*Ghi chú này được tối ưu hóa để lưu trữ trong Obsidian, hỗ trợ ôn tập và liên kết chéo với các ghi chú về ASP.NET Core, Entity Framework Core, và xử lý biểu mẫu.*