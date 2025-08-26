## Tạo Chức năng Chỉnh sửa và Xóa Danh mục trong ASP.NET Core

### Mục tiêu
- Thêm các nút **Edit** (Chỉnh sửa) và **Delete** (Xóa) trên trang danh sách danh mục (`Index` view) để hỗ trợ chỉnh sửa và xóa các danh mục hiện có.
- Sử dụng **Bootstrap icons** và các lớp Bootstrap để tạo giao diện nút thân thiện với người dùng.
- Chuẩn bị cấu trúc giao diện để liên kết với các hành động (actions) trong `CategoryController`.

### Khái niệm
- **Index View**: Trang danh sách danh mục, hiển thị tất cả các danh mục và cung cấp các nút hành động (Edit/Delete) để thao tác trên từng danh mục.
- **Bootstrap Icons**: Thư viện biểu tượng của Bootstrap, sử dụng để thêm các biểu tượng trực quan (ví dụ: biểu tượng bút chì cho Edit, thùng rác cho Delete).
- **Button Group**: Sử dụng lớp Bootstrap `btn-group` để nhóm các nút hành động, tạo giao diện gọn gàng và nhất quán.
- **Anchor Tag Helper**: Sử dụng thẻ `<a>` với thuộc tính `asp-controller` và `asp-action` để liên kết đến các hành động trong controller.

### Cách thực hiện
1. **Thêm cột hành động trong Index View**:
   - Trong tệp `Index.cshtml` của danh mục, thêm một cột (`<td>`) vào bảng để chứa các nút Edit và Delete.
   - Sử dụng thẻ `<div>` với lớp Bootstrap `btn-group` để nhóm các nút.

2. **Tích hợp Bootstrap Icons**:
   - Tìm và sao chép các biểu tượng từ [Bootstrap Icons](https://icons.getbootstrap.com/) (ví dụ: `pencil` cho Edit, `trash-fill` cho Delete).
   - Nhúng biểu tượng vào các thẻ `<a>` để tăng tính trực quan.

3. **Tạo liên kết đến các hành động**:
   - Sử dụng thẻ `<a>` với các thuộc tính:
     - `asp-controller="Category"`: Chỉ định controller là `CategoryController`.
     - `asp-action="Edit"`: Liên kết đến hành động `Edit` (chưa được triển khai).
     - `asp-action="Delete"`: Liên kết đến hành động `Delete` (chưa được triển khai).
   - Áp dụng các lớp Bootstrap như `btn`, `btn-primary` (cho Edit), `btn-danger` (cho Delete), và `mx-2` (margin ngang).

### Mã nguồn
```html
<!-- Trong Index.cshtml -->
<table class="table">
    <thead>
        <tr>
            <th>Tên danh mục</th>
            <th>Thứ tự hiển thị</th>
            <th>Hành động</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var category in Model)
        {
            <tr>
                <td>@category.Name</td>
                <td>@category.DisplayOrder</td>
                <td>
                    <div class="w-75 btn-group" role="group">
                        <a asp-controller="Category" asp-action="Edit" class="btn btn-primary mx-2">
                            <i class="bi bi-pencil"></i> Edit
                        </a>
                        <a asp-controller="Category" asp-action="Delete" class="btn btn-danger mx-2">
                            <i class="bi bi-trash-fill"></i> Delete
                        </a>
                    </div>
                </td>
            </tr>
        }
    </tbody>
</table>
```

### Ví dụ
- **Giao diện**:
  - Mỗi hàng trong bảng danh mục có thêm cột "Hành động" chứa hai nút:
    - **Edit**: Nút màu xanh (primary) với biểu tượng bút chì, liên kết đến hành động `Edit`.
    - **Delete**: Nút màu đỏ (danger) với biểu tượng thùng rác, liên kết đến hành động `Delete`.
  - Các nút được nhóm trong một `btn-group`, có chiều rộng 75% (`w-75`) và vai trò `group`.

- **Kiểm tra giao diện**:
  - Sau khi lưu và chạy ứng dụng, kiểm tra giao diện để đảm bảo các nút hiển thị đúng (màu sắc, biểu tượng, và khoảng cách).
  - Lưu ý: Các hành động `Edit` và `Delete` chưa được triển khai trong controller, sẽ được xử lý trong các video tiếp theo.

### Ghi chú thêm
- **Bootstrap Classes**:
  - `btn-group`: Nhóm các nút thành một khối thống nhất.
  - `btn-primary`: Nút màu xanh, thường dùng cho hành động tích cực (như Edit).
  - `btn-danger`: Nút màu đỏ, thường dùng cho hành động nguy hiểm (như Delete).
  - `mx-2`: Thêm khoảng cách ngang (margin) giữa các nút.
- **Bootstrap Icons**:
  - Sử dụng thẻ `<i>` với lớp `bi bi-pencil` (bút chì) cho Edit và `bi bi-trash-fill` (thùng rác) cho Delete.
  - Đảm bảo Bootstrap Icons được tích hợp trong dự án (thông qua CDN hoặc tải xuống).
- **Hành động trong Controller**:
  - Các hành động `Edit` và `Delete` cần được triển khai trong `CategoryController` để xử lý yêu cầu khi người dùng nhấn nút.
- **Tối ưu hóa**:
  - Đảm bảo tên controller và action trong `asp-controller` và `asp-action` khớp chính xác với định nghĩa trong `CategoryController`.
  - Kiểm tra giao diện trên các thiết bị khác nhau để đảm bảo hiển thị nhất quán.

--- 
*Ghi chú này được tối ưu hóa để lưu trữ trong Obsidian, hỗ trợ ôn tập và liên kết chéo với các ghi chú về ASP.NET Core, Bootstrap, và giao diện người dùng.*