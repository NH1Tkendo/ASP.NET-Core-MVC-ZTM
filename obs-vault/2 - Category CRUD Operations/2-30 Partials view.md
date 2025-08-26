## Sử dụng Partial View để hiển thị thông báo TempData trên nhiều trang

### Giới thiệu về Partial View
- **Partial View** là một thành phần giao diện có thể tái sử dụng trong ASP.NET Core, giúp tránh lặp lại mã code trên nhiều trang.
- Phù hợp để hiển thị thông báo (notification) như thành công (success) hoặc lỗi (error) trên các trang khác nhau (ví dụ: quản lý danh mục, loại sản phẩm, sản phẩm).
- Trong trường hợp này, Partial View được sử dụng để quản lý logic hiển thị TempData (thông báo thành công hoặc lỗi).

### Tạo Partial View cho thông báo
- Tạo một Partial View mới trong thư mục `Shared` để tái sử dụng trên nhiều trang.
- Quy ước đặt tên: Sử dụng dấu gạch dưới (`_`) ở đầu tên file để dễ nhận biết đây là Partial View.
- Ví dụ: `_Notification.cshtml`

#### Mã nguồn trong Partial View (_Notification.cshtml)
```csharp
@if (TempData["success"] != null)
{
    <h2>@TempData["success"]</h2>
}
@if (TempData["error"] != null)
{
    <h2>@TempData["error"]</h2>
}
```
- Kiểm tra cả hai key `success` và `error` trong TempData.
- Nếu key tồn tại (không null), hiển thị thông báo tương ứng trong thẻ `<h2>`.

### Sử dụng Partial View trong trang Index
- Thêm Partial View vào trang cần hiển thị thông báo (ví dụ: `Index.cshtml`) bằng thẻ `<partial>`.
- Cú pháp: `<partial name="_Notification" />`
- Đảm bảo tên Partial View được viết chính xác, nếu không sẽ không hoạt động.

#### Mã nguồn trong Index.cshtml
```csharp
<partial name="_Notification" />
```
- Chỉ cần một dòng code để tích hợp Partial View vào trang.

### Lợi ích của Partial View
- **Tái sử dụng**: Logic hiển thị thông báo được viết một lần và sử dụng ở nhiều trang.
- **Dễ bảo trì**: Khi cần thay đổi giao diện hoặc logic của thông báo, chỉ cần chỉnh sửa trong `_Notification.cshtml`, các trang sử dụng Partial View sẽ tự động cập nhật.
- **Tính nhất quán**: Đảm bảo giao diện và chức năng thông báo đồng bộ trên toàn bộ ứng dụng.
- **Giảm lặp code**: Tránh việc sao chép logic hiển thị TempData trên nhiều trang.

### Kết quả
- Sau khi tích hợp Partial View, thông báo từ TempData (ví dụ: "Danh mục được cập nhật thành công") vẫn hiển thị đúng như trước.
- Thông báo chỉ xuất hiện một lần và biến mất khi làm mới trang (do đặc tính của TempData).
- Có thể dễ dàng mở rộng để hiển thị thông báo lỗi bằng cách thêm `TempData["error"]` trong Controller.

### Ghi chú thêm
- Đảm bảo tên Partial View được khai báo chính xác trong thẻ `<partial>`.
- Partial View là lựa chọn lý tưởng cho các thành phần giao diện tái sử dụng, không chỉ giới hạn ở thông báo mà còn áp dụng cho các thành phần như menu, footer, hoặc script xác thực.
- Nếu cần hiển thị thông báo lỗi, chỉ cần gán `TempData["error"] = "Thông báo lỗi";` trong Controller tương tự như cách gán thông báo thành công.