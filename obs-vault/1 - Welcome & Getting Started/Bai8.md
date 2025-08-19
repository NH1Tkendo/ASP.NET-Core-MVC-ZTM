## Bài học: Khám phá dự án MVC và tệp cấu hình trong Visual Studio 2022

### Mục tiêu
- Hiểu cấu trúc cơ bản của dự án ASP.NET Core MVC.
- Tìm hiểu tệp cấu hình dự án (Project File) và các thuộc tính quan trọng.

### Cách thực hiện

#### 1. Chạy ứng dụng
- Nhấn nút **HTTPS** trong Visual Studio 2022 để:
  - Biên dịch (build) dự án.
  - Mở ứng dụng trong trình duyệt.
- Kết quả:
  - Ứng dụng mặc định hiển thị với:
    - **Header**: Chứa menu điều hướng với hai mục:
      - **Home**: Trang chào mừng.
      - **Privacy**: Trang thông tin bảo mật.
    - **Footer**: Phần chân trang.
    - **Body**: Nội dung chính.
  - Điều hướng (navigation) đã được cấu hình sẵn bởi mẫu dự án .NET.

#### 2. Khám phá tệp dự án (Project File)
- **Phân biệt Solution và Project**:
  - **Solution (`Bulky`)**: Có thể chứa nhiều dự án.
  - **Project (`BulkyWeb`)**: Dự án MVC hiện tại.
- Cách xem tệp dự án:
  - Nhấp chuột phải vào **BulkyWeb** (dự án) trong Solution Explorer.
  - Chọn **Edit Project File**.
- Nội dung tệp dự án:
  - **Target Framework**:
    - Chỉ định sử dụng **.NET 8.0** (`net8.0`).
  - **Nullable**:
    - Bật tính năng kiểm tra giá trị null (sẽ được giải thích chi tiết trong các bài sau).
  - **Implicit Usings**:
    - Tự động bao gồm các câu lệnh `using` mặc định của .NET.
    - Giúp giảm việc viết các câu lệnh `using` thủ công.
    - Nếu tắt, phải thêm các câu lệnh `using` rõ ràng.
- Tệp dự án chứa các thuộc tính quan trọng và được cập nhật khi:
  - Thêm **NuGet packages** hoặc **NPM packages**.
  - Thay đổi cấu hình dự án.

### Ghi chú thêm
- Tệp dự án trong .NET Core mới (như .NET 8) được đơn giản hóa so với các phiên bản cũ.
- Các tính năng như **Nullable** (từ .NET 6) và **Implicit Usings** giúp tăng hiệu quả phát triển.
- Trong các bài tiếp theo:
  - Sẽ giải thích chi tiết về **Nullable**.
  - Hướng dẫn cách tệp dự án được cập nhật khi thêm gói NuGet hoặc NPM.

```markdown
**Lưu ý**: Đảm bảo hiểu rõ sự khác biệt giữa Solution và Project để quản lý tốt các dự án trong Visual Studio. Khi thêm gói NuGet, luôn kiểm tra tệp dự án để xác nhận các thay đổi.
```