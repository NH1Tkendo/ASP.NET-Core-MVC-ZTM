## Thư Mục wwwroot và Appsettings.json trong Ứng Dụng .NET Core

### Thư Mục wwwroot

#### Khái niệm
- Thư mục `wwwroot` là nơi lưu trữ tất cả **nội dung tĩnh (static content)** của ứng dụng .NET Core.
- Nội dung tĩnh bao gồm:
  - Tệp CSS (ví dụ: `site.css`).
  - Tệp JavaScript (ví dụ: `site.js`).
  - Gói NuGet hoặc thư viện bên thứ ba (ví dụ: Bootstrap, jQuery, jQuery Validation).
  - Tệp hình ảnh, PDF, PowerPoint hoặc bất kỳ tệp nào không chứa mã HTML.

#### Đặc điểm
- Được tạo mặc định khi khởi tạo ứng dụng MVC.
- Là nơi duy nhất để thêm nội dung tĩnh trong dự án.
- Ví dụ: Tệp `site.css` được sử dụng để định dạng giao diện ứng dụng, trong khi `site.js` hiện là một mẫu trống.

#### Lưu ý
- Khi cần thêm hình ảnh hoặc tệp tĩnh khác, luôn đặt trong thư mục `wwwroot` để đảm bảo tính tổ chức.

### Appsettings.json

#### Khái niệm
- Tệp `appsettings.json` là nơi lưu trữ **chuỗi kết nối (connection strings)** và **khóa bí mật (secret keys)** của ứng dụng.
- Các loại thông tin được lưu trữ:
  - Chuỗi kết nối cơ sở dữ liệu.
  - Khóa bí mật cho dịch vụ email (ví dụ: SendGrid).
  - Khóa cho Azure Blob Storage hoặc Azure Storage Account.

#### Đặc điểm
- Tệp `appsettings.json` giúp tập trung tất cả thông tin nhạy cảm, tránh việc phải tìm kiếm trong mã nguồn.
- Hỗ trợ nhiều môi trường (environment) thông qua các tệp riêng biệt:
  - `appsettings.json`: Cấu hình mặc định.
  - `appsettings.Development.json`: Cấu hình cho môi trường phát triển.
  - `appsettings.Production.json`: Cấu hình cho môi trường sản xuất (nếu có).

#### Cách hoạt động
- Ứng dụng .NET Core tự động chọn tệp cấu hình dựa trên biến môi trường `ASPNETCORE_ENVIRONMENT` (được định nghĩa trong `launchSettings.json`).
  - Ví dụ: Nếu môi trường là `Production`, ứng dụng sẽ sử dụng `appsettings.Production.json`.

#### Lưu ý
- Chỉ lưu trữ chuỗi kết nối và khóa bí mật trong `appsettings.json` hoặc các tệp liên quan.
- Khi triển khai ứng dụng, cần tạo và cấu hình tệp `appsettings.Production.json` phù hợp.

### Ghi chú thêm
- Trong các video tiếp theo, các tệp quan trọng khác như `Program.cs` sẽ được phân tích chi tiết.
- Thư mục `Controllers`, `Models`, và `Views` sẽ được đề cập trong các phần sau.
- Khi triển khai mã nguồn, cấu hình môi trường sản xuất sẽ được điều chỉnh để minh họa cách hoạt động.

---

**Ghi chú**: Nội dung được tổ chức theo cấu trúc logic, tập trung vào ý chính và sử dụng thuật ngữ tiếng Việt kèm thuật ngữ gốc tiếng Anh theo yêu cầu. Nội dung được tối ưu để lưu trữ trong Obsidian và dễ đọc trên thiết bị di động.