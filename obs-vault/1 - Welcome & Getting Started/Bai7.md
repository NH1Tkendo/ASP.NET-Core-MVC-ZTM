## Bài học: Tạo dự án MVC với Visual Studio 2022

### Mục tiêu
- Tạo một dự án ASP.NET Core MVC sử dụng Visual Studio 2022.
- Thiết lập cấu hình cơ bản và tích hợp với kho lưu trữ Git.

### Hướng dẫn thực hiện

#### 1. Tạo dự án mới
- Mở **Visual Studio 2022**.
- Chọn **Create a new project** từ giao diện chính.
- Tìm kiếm **MVC** trong thanh tìm kiếm để lọc các mẫu dự án.
- Chọn **ASP.NET Core Web App (Model-View-Controller)**:
  - Đảm bảo chọn mẫu có **Model-View-Controller (MVC)**, không chọn mẫu Razor Pages.
- Đặt tên dự án:
  - **Project name**: `BulkyWeb`
  - **Solution name**: `Bulky`
  - **Location**: Chọn thư mục lưu trữ dự án.
- Nhấn **Next** để tiếp tục.

#### 2. Cấu hình dự án
- **Framework**: Chọn **.NET 8**.
- **Authentication Type**: Chọn **None** để giữ đơn giản (sẽ thêm xác thực sau).
- **Configure for HTTPS**: Bật tùy chọn này.
- **Do not use top-level statements**: Bỏ chọn (không quan trọng, chỉ liên quan đến `using` statements).
- Nhấn **Create** để tạo dự án.

#### 3. Kết quả
- Visual Studio sẽ tạo dự án với các tệp và thư mục mặc định.
- Các tệp/thư mục này sẽ được khám phá chi tiết trong các bước tiếp theo.

#### 4. Tích hợp với Git
- Thêm dự án vào **Source Control**:
  - Đăng nhập vào tài khoản Git (nếu chưa đăng nhập).
  - Tạo kho lưu trữ mới:
    - **Repository name**: `Bulky_MVC`
    - **Private repository**: Đặt là riêng tư (có thể chuyển sang công khai sau).
- Nhấn **Create and Push** để tạo kho lưu trữ và đẩy mã lên Git.
- Kiểm tra: Xác nhận có **2 outgoing changes** và thực hiện **Push** để hoàn tất.

### Ghi chú thêm
- Dự án `BulkyWeb` là một phần của solution `Bulky`, cho phép thêm nhiều dự án khác trong tương lai.
- Cấu trúc tệp/thư mục mặc định của dự án MVC sẽ được khám phá ở các bước sau.
- Tích hợp Git giúp quản lý mã nguồn và theo dõi thay đổi hiệu quả.

```markdown
**Lưu ý**: Đảm bảo đã đăng nhập vào tài khoản Git trước khi đẩy mã. Nếu gặp lỗi, kiểm tra kết nối mạng hoặc thông tin xác thực.
```