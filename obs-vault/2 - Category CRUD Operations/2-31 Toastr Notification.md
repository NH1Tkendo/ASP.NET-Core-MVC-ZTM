## Tích hợp Toastr để hiển thị thông báo nâng cao trong ASP.NET Core

### Giới thiệu về Toastr
- **Toastr** là một thư viện JavaScript dùng để hiển thị thông báo (notification) đẹp mắt và chuyên nghiệp, thay thế cho các thẻ HTML đơn giản như `<h2>`.
- Hỗ trợ các loại thông báo: thành công (success), cảnh báo (warning), lỗi (error), thông tin (info).
- Dễ tích hợp vào dự án ASP.NET Core thông qua CDN và sử dụng trong Partial View.

### Các bước tích hợp Toastr
#### 1. Thêm CDN cho CSS và JavaScript
- Thêm liên kết CSS của Toastr vào file `_Layout.cshtml` để áp dụng kiểu dáng toàn cục.
- Thêm liên kết JavaScript của Toastr vào Partial View `_Notification.cshtml` để hiển thị thông báo.
- Yêu cầu jQuery (đã có sẵn trong thư mục `lib/jquery/dist/jquery.min.js`).

##### Mã nguồn trong `_Layout.cshtml`
```csharp
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css" />
```

##### Mã nguồn trong `_Notification.cshtml` (JavaScript)
```csharp
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>
```

- Sử dụng phiên bản rút gọn (minified) của CSS và JavaScript để tối ưu hóa hiệu suất khi triển khai sản phẩm.

#### 2. Cập nhật Partial View để sử dụng Toastr
- Thay thế thẻ `<h2>` trong `_Notification.cshtml` bằng lệnh gọi hàm `toastr.success()` hoặc `toastr.error()` để hiển thị thông báo.
- Đảm bảo đặt lệnh gọi Toastr trong khối `<script>` với type là `text/javascript`.

##### Mã nguồn trong `_Notification.cshtml`
```csharp
@if (TempData["success"] != null)
{
    <script type="text/javascript">
        toastr.success("@TempData["success"]");
    </script>
}
@if (TempData["error"] != null)
{
    <script type="text/javascript">
        toastr.error("@TempData["error"]");
    </script>
}
```

#### 3. Tích hợp Partial View vào Layout
- Để thông báo Toastr có sẵn trên tất cả các trang, thêm thẻ `<partial>` vào `_Layout.cshtml` trước `@RenderBody()`.
- Điều này giúp tránh việc phải thêm Partial View vào từng trang riêng lẻ.

##### Mã nguồn trong `_Layout.cshtml`
```csharp
<partial name="_Notification" />
@RenderBody()
```

### Kết quả
- Sau khi thực hiện các thao tác CRUD (tạo, chỉnh sửa, xóa), thông báo Toastr sẽ xuất hiện với giao diện đẹp mắt.
- Ví dụ:
  - Chỉnh sửa danh mục: Hiển thị thông báo "Danh mục được cập nhật thành công" bằng `toastr.success`.
  - Xóa danh mục: Hiển thị thông báo "Danh mục được xóa thành công" bằng `toastr.success`.
- Thông báo tự động biến mất sau một lần hiển thị (do TempData) và có giao diện thân thiện hơn so với thẻ `<h2>`.

### Lợi ích của việc sử dụng Toastr với Partial View
- **Tính thẩm mỹ**: Toastr cung cấp giao diện thông báo hiện đại, dễ nhìn.
- **Tái sử dụng**: Partial View `_Notification.cshtml` đảm bảo mã thông báo được sử dụng trên toàn bộ ứng dụng mà không cần lặp lại.
- **Dễ bảo trì**: Nếu cần thay đổi thư viện thông báo (ví dụ: từ Toastr sang thư viện khác), chỉ cần cập nhật `_Notification.cshtml` mà không ảnh hưởng đến các trang khác.
- **Tính mở rộng**: Dễ dàng thêm các loại thông báo khác (như `toastr.warning` hoặc `toastr.info`) khi cần.

### Ghi chú thêm
- Đảm bảo các CDN được thêm chính xác và jQuery được tải trước Toastr để tránh lỗi.
- Khi thêm Partial View vào `_Layout.cshtml`, tất cả các trang trong ứng dụng sẽ tự động có chức năng thông báo mà không cần thêm mã bổ sung.
- Nếu không sử dụng Partial View, việc thêm logic Toastr vào từng trang sẽ gây khó khăn trong việc bảo trì và cập nhật sau này.