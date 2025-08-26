## Thiết kế và Xử lý Trang Chỉnh sửa Danh mục trong ASP.NET Core

### Mục tiêu
- Tạo trang chỉnh sửa (`Edit`) danh mục, tương tự trang tạo (`Create`), nhưng tự động điền thông tin danh mục dựa trên ID được chọn.
- Triển khai phương thức `GET` và `POST` trong `CategoryController` để lấy và cập nhật thông tin danh mục.
- Sử dụng các phương thức truy xuất dữ liệu từ cơ sở dữ liệu (database) để lấy danh mục theo ID.
- Đảm bảo truyền ID từ trang danh sách (`Index`) đến hành động `Edit`.

### Khái niệm
- **Trang Chỉnh sửa (Edit Page)**:
  - Giao diện tương tự trang tạo (`Create`), nhưng các trường (fields) như `Name` và `DisplayOrder` được điền sẵn dựa trên danh mục được chọn.
  - Yêu cầu ID của danh mục để xác định bản ghi cần chỉnh sửa.
- **Phương thức GET**:
  - Lấy thông tin danh mục từ cơ sở dữ liệu dựa trên ID và truyền vào view để hiển thị.
- **Phương thức POST**:
  - Xử lý cập nhật dữ liệu danh mục khi người dùng gửi biểu mẫu (form).
- **Truy xuất dữ liệu**:
  - Sử dụng các phương thức của Entity Framework Core như `Find`, `FirstOrDefault`, hoặc `Where` để lấy bản ghi từ cơ sở dữ liệu.
- **Tag Helper `asp-route-id`**:
  - Sử dụng để truyền tham số ID từ trang `Index` đến hành động `Edit` trong controller.

### Cách thực hiện
1. **Thêm phương thức GET trong CategoryController**:
   - Tạo phương thức `Edit` để lấy danh mục theo ID.
   - Kiểm tra tính hợp lệ của ID và xử lý trường hợp không tìm thấy danh mục.

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
   ```

2. **Truyền ID từ Index View**:
   - Trong `Index.cshtml`, sử dụng tag helper `asp-route-id` để truyền ID của danh mục đến hành động `Edit`.

   ```html
   <!-- Trong Index.cshtml -->
   <a asp-controller="Category" asp-action="Edit" asp-route-id="@category.Id" class="btn btn-primary mx-2">
       <i class="bi bi-pencil"></i> Edit
   </a>
   ```

3. **Tạo View cho Edit**:
   - Tạo tệp `Edit.cshtml` trong thư mục `Views/Category`, sao chép nội dung từ `Create.cshtml`.
   - Đ確保 biểu mẫu sử dụng mô hình (model) để hiển thị giá trị hiện có của danh mục.

   ```html
   <!-- Trong Edit.cshtml -->
   @model Category

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
       <button type="submit" class="btn btn-primary">Save</button>
   </form>

   @section Scripts {
       <partial name="_ValidationScriptsPartial" />
   }
   ```

4. **Các phương thức truy xuất dữ liệu**:
   - **Find**:
     - Truy xuất bản ghi dựa trên khóa chính (primary key) như `Id`.
     - Cú pháp: `_db.Categories.Find(id)`.
     - Ưu điểm: Hiệu quả, đơn giản khi tìm theo khóa chính.
     - Hạn chế: Chỉ hoạt động với khóa chính, không hỗ trợ điều kiện phức tạp.
   - **FirstOrDefault**:
     - Sử dụng LINQ để tìm bản ghi đầu tiên thỏa mãn điều kiện.
     - Cú pháp: `_db.Categories.FirstOrDefault(u => u.Id == id)`.
     - Linh hoạt hơn, có thể tìm kiếm dựa trên bất kỳ thuộc tính nào (ví dụ: `Name`).
   - **Where + FirstOrDefault**:
     - Kết hợp điều kiện lọc với `Where`, sau đó lấy bản ghi đầu tiên.
     - Cú pháp: `_db.Categories.Where(u => u.Id == id).FirstOrDefault()`.
     - Phù hợp với các truy vấn phức tạp hơn, nhưng ít được sử dụng nếu chỉ tìm theo khóa chính.

   ```csharp
   // Các cách truy xuất dữ liệu
   var categoryFromDb = _db.Categories.Find(id); // Phương thức Find
   var categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id); // Phương thức FirstOrDefault
   var categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault(); // Phương thức Where
   ```

### Ví dụ
- **Truy xuất danh mục**:
  - Khi người dùng nhấn nút Edit trên danh mục có `Id = 2` (ví dụ: danh mục "SciFi"):
    - Hành động `Edit` được gọi với tham số `id=2`.
    - Phương thức `_db.Categories.Find(2)` trả về đối tượng danh mục với `Name = "SciFi"` và `DisplayOrder` tương ứng.
    - View hiển thị biểu mẫu với các trường đã được điền sẵn giá trị.

- **Kiểm tra lỗi**:
  - Nếu `id` là `null` hoặc `0`, trả về `NotFound()`.
  - Nếu không tìm thấy danh mục, trả về `NotFound()`.

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
```

```html
<!-- Trong Index.cshtml -->
<table class="table">
    <tbody>
        @foreach (var category in Model)
        {
            <tr>
                <td>@category.Name</td>
                <td>@category.DisplayOrder</td>
                <td>
                    <div class="w-75 btn-group" role="group">
                        <a asp-controller="Category" asp-action="Edit" asp-route-id="@category.Id" class="btn btn-primary mx-2">
                            <i class="bi bi-pencil"></i> Edit
                        </a>
                        <a asp-controller="Category" asp-action="Delete" asp-route-id="@category.Id" class="btn btn-danger mx-2">
                            <i class="bi bi-trash-fill"></i> Delete
                        </a>
                    </div>
                </td>
            </tr>
        }
    </tbody>
</table>
```

### Ghi chú thêm
- **Khác biệt giữa Create và Edit**:
  - Trang `Create` không yêu cầu ID, trong khi `Edit` cần ID để xác định danh mục cần chỉnh sửa.
  - Biểu mẫu `Edit` phải bao gồm trường ẩn (`<input type="hidden" asp-for="Id" />`) để gửi ID khi cập nhật.
- **Xử lý lỗi**:
  - Kiểm tra `id` hợp lệ (`null` hoặc `0`) và kiểm tra xem danh mục có tồn tại trong cơ sở dữ liệu hay không.
  - Có thể trả về trang lỗi tùy chỉnh thay vì `NotFound()` để cải thiện trải nghiệm người dùng.
- **Lựa chọn phương thức truy xuất**:
  - Sử dụng `Find` khi tìm theo khóa chính để tối ưu hiệu suất.
  - Sử dụng `FirstOrDefault` hoặc `Where` khi cần lọc theo các điều kiện phức tạp (ví dụ: tìm theo `Name` hoặc các tiêu chí khác).
- **Tối ưu hóa**:
  - Đảm bảo tên action (`Edit`) và tham số (`id`) khớp chính xác giữa tag helper và controller.
  - Kiểm tra giao diện trên các thiết bị để đảm bảo hiển thị nhất quán.

--- 
*Ghi chú này được tối ưu hóa để lưu trữ trong Obsidian, hỗ trợ ôn tập và liên kết chéo với các ghi chú về ASP.NET Core, Entity Framework Core, và giao diện người dùng.*