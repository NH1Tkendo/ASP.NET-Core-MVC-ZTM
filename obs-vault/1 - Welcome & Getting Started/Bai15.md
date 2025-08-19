## Tệp bố cục và cấu hình giao diện trong ứng dụng MVC

### Tổng quan về thư mục Views
- Thư mục `Views` chứa các tệp giao diện (views) và các tệp cấu hình liên quan:
  - **Thư mục con theo tên bộ điều khiển**: Ví dụ, `Views/Home` chứa các giao diện như `Index.cshtml` và `Privacy.cshtml` cho `HomeController`.
  - **Thư mục Shared**: Chứa các giao diện dùng chung (partial views) hoặc tệp bố cục (layout).
  - **Tệp cấu hình**:
    - `_ViewImports.cshtml`: Quản lý các câu lệnh `using` và tag helpers toàn cục.
    - `_ViewStart.cshtml`: Chỉ định tệp bố cục mặc định cho ứng dụng.
    - `_Layout.cshtml`: Tệp bố cục chính (master page) của ứng dụng.
    - `Error.cshtml`: Giao diện hiển thị thông báo lỗi.
    - `_ValidationScriptsPartial.cshtml`: Chứa các tập lệnh JavaScript dùng cho xác thực phía máy khách (client-side validation).

### Tệp bố cục (_Layout.cshtml)
- **Tệp bố cục (Layout)**:
  - Tương đương với "master page" trong các phiên bản .NET cũ.
  - Định nghĩa cấu trúc chung cho toàn bộ ứng dụng, bao gồm:
    - Phần `<head>`: Chứa các tệp CSS, JavaScript toàn cục.
    - Phần `<body>`: Bao gồm tiêu đề (header), nội dung chính, và chân trang (footer).
  - **RenderBody()**:
    - Là một phương thức trợ giúp (helper) trong MVC, hiển thị nội dung của giao diện được trả về từ bộ điều khiển.
    - Ví dụ: Nếu bộ điều khiển trả về `Index.cshtml`, nội dung của `Index.cshtml` sẽ được chèn vào vị trí `RenderBody()` trong `_Layout.cshtml`.
  - **Ví dụ cấu trúc _Layout.cshtml**:
    ```html
    <!DOCTYPE html>
    <html>
    <head>
        <title>Ứng dụng MVC</title>
        <link rel="stylesheet" href="~/css/site.css" />
    </head>
    <body>
        <header>
            <a asp-controller="Home" asp-action="Index">Home</a>
            <a asp-controller="Home" asp-action="Privacy">Privacy</a>
        </header>
        @RenderBody()
        <footer>
            <p>BulkyWeb &copy; 2025</p>
        </footer>
    </body>
    </html>
    ```
  - **Tag Helpers**:
    - Các thẻ như `asp-controller` và `asp-action` giúp tạo liên kết động tới bộ điều khiển và hành động.
    - Sẽ được giải thích chi tiết trong các video sau.

### Tệp _ViewStart.cshtml
- **Chức năng**:
  - Xác định tệp bố cục mặc định cho toàn bộ ứng dụng.
  - Nội dung điển hình:
    ```html
    @{
        Layout = "_Layout";
    }
    ```
  - Tệp này chỉ định rằng `_Layout.cshtml` là bố cục mặc định.
- **Lưu ý**:
  - Nếu đổi tên tệp bố cục (ví dụ: từ `_Layout.cshtml` thành `Layout.cshtml`), cần cập nhật lại trong `_ViewStart.cshtml` để tránh lỗi "Layout cannot be located".
  - Tên `_Layout.cshtml` là chuẩn thông dụng, nên giữ nguyên để dễ nhận diện.

### Tệp _ViewImports.cshtml
- **Chức năng**:
  - Định nghĩa các câu lệnh `using` và tag helpers áp dụng cho tất cả các giao diện.
  - Ví dụ:
    ```html
    @using BulkyWeb
    @using BulkyWeb.Models
    @addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
    ```
  - Giúp tránh lặp lại các câu lệnh `using` trong từng tệp giao diện.
- **Phạm vi áp dụng**:
  - Chỉ áp dụng cho các tệp trong thư mục `Views`, không ảnh hưởng đến `Controllers` hoặc `Models`.
- **Lợi ích**:
  - Tăng tính tái sử dụng và giảm mã lặp trong các giao diện.

### Giao diện dùng chung (Partial Views)
- **Định nghĩa**:
  - Là các giao diện không được hiển thị độc lập, mà được tích hợp vào giao diện chính.
  - Thường có tiền tố `_` trong tên tệp (ví dụ: `_ValidationScriptsPartial.cshtml`).
- **Ví dụ: _ValidationScriptsPartial.cshtml**:
  - Chứa các tập lệnh JavaScript dùng cho xác thực phía máy khách.
  - Chỉ được bao gồm trong các giao diện cần xác thực, giúp tối ưu hóa tài nguyên.
  - Ví dụ:
    ```html
    <script src="~/lib/jquery-validation/dist/jquery.validate.min.js"></script>
    <script src="~/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js"></script>
    ```
- **Quy ước đặt tên**:
  - Tiền tố `_` không bắt buộc, nhưng giúp dễ nhận biết rằng tệp là giao diện dùng chung.

### Tệp Error.cshtml
- **Chức năng**:
  - Hiển thị thông báo lỗi khi có sự cố trong ứng dụng.
  - Có thể tạm bỏ qua trong giai đoạn đầu học MVC.

### Quy trình hiển thị giao diện
1. Khi truy cập một URL (ví dụ: `localhost/home/index`):
   - Ứng dụng xác định bộ điều khiển (`HomeController`) và hành động (`Index`).
   - Phương thức hành động `Index()` trả về giao diện (ví dụ: `return View()`).
   - Giao diện `Index.cshtml` được chèn vào vị trí `@RenderBody()` trong `_Layout.cshtml`.
2. Tệp `_ViewStart.cshtml` đảm bảo `_Layout.cshtml` được áp dụng.
3. Các câu lệnh `using` và tag helpers từ `_ViewImports.cshtml` được áp dụng cho giao diện.

### Ghi chú thêm
- **Tầm quan trọng của _Layout.cshtml**:
  - Đóng vai trò là khung giao diện chính, đảm bảo tính nhất quán về thiết kế (header, footer, CSS, JavaScript) trên toàn ứng dụng.
- **Tính linh hoạt**:
  - Có thể tùy chỉnh bố cục hoặc giao diện trả về theo nhu cầu cụ thể.
  - Ví dụ: Trong `HomeController`, gọi `return View("Privacy")` để trả về giao diện khác mặc định.
- **Học tập dần tiến**:
  - MVC có thể phức tạp ban đầu, nhưng việc hiểu các thành phần như bố cục, giao diện dùng chung, và cấu hình sẽ rõ ràng hơn khi thực hành.

### Tổng kết
- Tệp `_Layout.cshtml` là bố cục chính, tích hợp nội dung giao diện thông qua `@RenderBody()`.
- `_ViewStart.cshtml` chỉ định bố cục mặc định, `_ViewImports.cshtml` quản lý các câu lệnh `using` toàn cục.
- Giao diện dùng chung (partial views) như `_ValidationScriptsPartial.cshtml` tăng tính tái sử dụng.
- Hiểu rõ cách các tệp này tương tác giúp xây dựng ứng dụng MVC hiệu quả và dễ bảo trì.