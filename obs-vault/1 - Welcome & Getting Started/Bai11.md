## Tệp Program.cs trong Ứng Dụng .NET Core

### Khái niệm
- Tệp `Program.cs` là tệp chính trong ứng dụng .NET Core, chịu trách nhiệm cấu hình ứng dụng.
- Trong các phiên bản cũ, .NET Core sử dụng hai tệp: `Program.cs` và `Startup.cs`. Từ phiên bản mới hơn, cả hai được gộp lại thành `Program.cs`.

### Nhiệm vụ chính
- **Thêm dịch vụ vào container (Adding services to the container)**: Đăng ký các dịch vụ cần thiết cho ứng dụng.
- **Cấu hình pipeline xử lý yêu cầu (Configuring the request pipeline)**: Xác định cách ứng dụng xử lý các yêu cầu HTTP.

### Cấu trúc chi tiết

#### 1. Thêm dịch vụ vào container
- Sử dụng `builder.WebApplication` để tạo một đối tượng `builder`.
- Thêm các dịch vụ vào `builder.Services`:
  - Ví dụ: `AddControllersWithViews()` được sử dụng để kích hoạt kiến trúc MVC (Model-View-Controller), cho phép ứng dụng sử dụng controllers và views.
- Trong tương lai, các dịch vụ khác (như dependency injection) sẽ được thêm vào đây.

#### 2. Cấu hình pipeline xử lý yêu cầu
- Pipeline xác định cách xử lý các yêu cầu HTTP khi chúng đến ứng dụng.
- Các thành phần chính trong pipeline:
  - **Kiểm tra môi trường (Environment check)**:
    - Dùng `app.Environment.IsDevelopment()` để kiểm tra xem ứng dụng đang chạy trong môi trường phát triển (Development) hay không.
    - Nếu **không phải môi trường phát triển**:
      - Sử dụng `app.UseExceptionHandler("/Home/Error")` để chuyển hướng đến trang lỗi khi có ngoại lệ.
    - Nếu **là môi trường phát triển**:
      - Hiển thị chi tiết ngoại lệ để hỗ trợ debug.
    - Biến môi trường được định nghĩa trong `launchSettings.json`. Có thể kiểm tra các môi trường khác như `Production`, `Staging`, hoặc môi trường tùy chỉnh với `IsEnvironment("Tên_môi_trường")`.
  - **Chuyển hướng HTTPS (HTTPS Redirection)**:
    - Sử dụng `app.UseHttpsRedirection()` để đảm bảo yêu cầu sử dụng HTTPS.
  - **Sử dụng tệp tĩnh (Static Files)**:
    - `app.UseStaticFiles()` cho phép truy cập các tệp tĩnh trong thư mục `wwwroot` (CSS, JavaScript, hình ảnh, v.v.).
  - **Định tuyến (Routing)**:
    - `app.UseRouting()` kích hoạt cơ chế định tuyến cho ứng dụng.
  - **Ủy quyền (Authorization)**:
    - `app.UseAuthorization()` được thêm mặc định vào pipeline (sẽ được giải thích chi tiết khi học về xác thực và ủy quyền).
  - **Cấu hình định tuyến mặc định (Default Route)**:
    - Sử dụng `app.MapControllerRoute()` để định nghĩa mẫu định tuyến mặc định:
      ```csharp
      app.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}"
      );
      ```
      - **Giải thích**: Nếu không chỉ định route, ứng dụng sẽ tìm đến `HomeController`, gọi hành động `Index`, với tham số `id` là tùy chọn (`?` biểu thị `id` có thể là `null`).
  - **Chạy ứng dụng**:
    - `app.Run()` khởi chạy ứng dụng, hoàn tất cấu hình pipeline.

### Ghi chú thêm
- **Tính quan trọng của Program.cs**:
  - Là nơi duy nhất để thêm dịch vụ vào container hoặc cấu hình middleware pipeline.
  - Mọi thay đổi liên quan đến dịch vụ hoặc cách xử lý yêu cầu đều được thực hiện trong tệp này.
- **Đối với người mới học**:
  - Nội dung trong `Program.cs` (như middleware, pipeline) có thể phức tạp ban đầu.
  - Chỉ cần nhớ: `Program.cs` là nơi cấu hình dịch vụ và pipeline.
  - Các khái niệm sẽ trở nên rõ ràng hơn khi tiếp tục khóa học.

---

**Ghi chú**: Nội dung được tổ chức logic, lược bỏ các phần không cần thiết, giữ lại ví dụ mã nguồn và diễn giải súc tích bằng tiếng Việt theo chuẩn Markdown, phù hợp để lưu trữ trong Obsidian và dễ đọc trên thiết bị di động.