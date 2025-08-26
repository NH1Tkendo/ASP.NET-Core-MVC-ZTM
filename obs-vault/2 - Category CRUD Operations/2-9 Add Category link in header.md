
## Hiển thị Danh sách Danh mục và Điều hướng Giao diện

### Tổng quan
- Bài học hướng dẫn cách hiển thị danh sách danh mục trên trang **Category List** và thêm liên kết điều hướng trong giao diện để truy cập trang này.
- Mục tiêu:
  - Thêm mục điều hướng (navigation item) vào thanh menu để truy cập `/Category/Index` mà không cần nhập URL thủ công.
  - Sử dụng **Tag Helpers** trong ASP.NET để tạo liên kết điều hướng.

### Khái niệm cơ bản
- **_Layout.cshtml**: Tệp giao diện chung (shared layout) định nghĩa cấu trúc giao diện cho toàn bộ ứng dụng, bao gồm thanh điều hướng (navigation bar).
- **Tag Helpers**: Công cụ trong ASP.NET Core giúp tạo HTML động một cách dễ dàng, thay thế cho việc viết URL tĩnh trong thuộc tính `href`.
  - Ví dụ: `asp-controller` và `asp-action` chỉ định controller và action tương ứng.
- **Điều hướng (Navigation)**: Tạo liên kết trong thanh menu để chuyển đến trang danh sách danh mục (`/Category/Index`).

### Quy trình thực hiện

#### 1. Thêm liên kết điều hướng trong _Layout.cshtml
- **Vị trí**: Tệp `/Views/Shared/_Layout.cshtml` chứa thanh điều hướng với các thẻ `<ul>` và `<li>` để hiển thị menu.
- **Cấu trúc hiện tại**:
  - Thanh menu có liên kết đến `Home` (controller: `Home`, action: `Index`) và `Privacy`.
  - Sử dụng **Tag Helpers**:
```html
<li><a asp-controller="Home" asp-action="Index">Home</a></li>
```
- **Thêm liên kết đến Category**:
  - Sao chép và sửa đổi thẻ `<li>` để thêm liên kết đến `CategoryController` và hành động `Index`:
```html
<li><a asp-controller="Category" asp-action="Index">Category</a></li>
```
- **Giải thích**:
  - `asp-controller="Category"`: Chỉ định controller là `CategoryController`.
  - `asp-action="Index"`: Chỉ định hành động là `Index`.
  - Văn bản hiển thị: `Category` (tên hiển thị trên menu).
  - Các lớp Bootstrap (nếu có) được giữ nguyên để đảm bảo giao diện đồng bộ.

#### 2. Kiểm tra điều hướng
- Chạy ứng dụng và kiểm tra thanh điều hướng.
- **Kết quả**:
  - Mục `Category` xuất hiện trong menu.
  - Khi nhấp vào `Category`, ứng dụng chuyển hướng đến `/Category/Index` và hiển thị trang danh sách danh mục (`Index.cshtml`).
- **Gỡ lỗi**:
  - Đặt điểm dừng (breakpoint) trong phương thức `Index` của `CategoryController` để xác nhận luồng thực thi.
  - Xóa điểm dừng: Nhấp lại vào điểm dừng trong Visual Studio để xóa.

#### 3. Lợi ích của Tag Helpers
- **Rõ ràng**: `asp-controller` và `asp-action` chỉ định đích đến (controller và action) một cách minh bạch, thay vì viết URL tĩnh (ví dụ: `href="/Category/Index"`).
- **Dễ bảo trì**: Nếu cấu trúc URL thay đổi, Tag Helpers tự động cập nhật mà không cần sửa đổi mã HTML.
- **Tích hợp ASP.NET**: Hỗ trợ các tính năng động của ASP.NET Core, giảm nguy cơ lỗi khi điều hướng.

### Tóm tắt quy trình
1. Mở tệp `/Views/Shared/_Layout.cshtml`.
2. Thêm mục điều hướng mới trong thẻ `<ul>`:
```html
<li><a asp-controller="Category" asp-action="Index">Category</a></li>
```
3. Chạy ứng dụng, kiểm tra menu và xác nhận điều hướng đến `/Category/Index`.

### Ghi chú thêm
- Hiện tại, trang `Category/Index` chỉ hiển thị tiêu đề tĩnh (`<h1>Danh sách Danh mục</h1>`).
- Các bước tiếp theo sẽ tích hợp **Entity Framework Core** để lấy danh sách danh mục từ cơ sở dữ liệu và hiển thị trên trang.
- **Tag Helpers** là công cụ mạnh mẽ, đặc biệt khi làm việc với các ứng dụng ASP.NET Core, giúp đơn giản hóa việc tạo liên kết và biểu mẫu.

### Tài liệu tham khảo
- Nội dung bài học sẽ tiếp tục với việc tích hợp danh sách danh mục từ cơ sở dữ liệu vào trang `Category/Index`.
