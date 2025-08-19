## Tổng quan khóa học: Xây dựng ứng dụng thực tế với .NET Core

### Mục tiêu khóa học
- Xây dựng một ứng dụng thực tế với kiến trúc hoàn chỉnh.
- Tìm hiểu các công nghệ và kỹ thuật tiên tiến trong .NET Core.
- Triển khai ứng dụng lên Azure để hiểu quy trình triển khai.

### Nội dung chính

#### 1. Kiến trúc ứng dụng
- **Mô hình kho lưu trữ (Repository Pattern)** và **Đơn vị công việc (Unit of Work)**:
  - Sử dụng với **Entity Framework** để quản lý cơ sở dữ liệu.
  - Tăng tính module hóa và dễ bảo trì.

#### 2. Quản lý dữ liệu giao diện
- **TempData**, **ViewBag**, **ViewData** trong .NET Core:
  - Phân biệt sự khác nhau giữa các phương pháp.
  - Hướng dẫn khi nào nên sử dụng từng loại.

#### 3. Phát triển API và giao diện
- **API Controllers**: Xây dựng các API để xử lý yêu cầu.
- **Razor Pages**: Tạo giao diện người dùng động.
- Tích hợp các tính năng nâng cao:
  - **Sweet Alerts**: Hiển thị thông báo thân thiện.
  - **Rich Text Editor**: Tạo nội dung văn bản phong phú.
  - **DataTables**: Quản lý và hiển thị dữ liệu dạng bảng với JavaScript.

#### 4. Bảo mật ứng dụng
- Sử dụng **.NET Identity** để:
  - Quản lý vai trò (roles) và phân quyền (authorization).
  - Xây dựng hệ thống quản lý người dùng:
    - Chuyển đổi vai trò người dùng linh hoạt.
- Tích hợp **đăng nhập xã hội (Social Login)** với Facebook:
  - Cho phép người dùng đăng ký/đăng nhập nhanh mà không cần nhập thông tin.

#### 5. Xử lý thanh toán
- Tích hợp **Stripe Payment**:
  - Xử lý thanh toán trực tuyến.
  - Thực hiện hoàn tiền (refund) qua Stripe.

#### 6. Quản lý phiên và gửi email
- **Sessions** trong .NET Core:
  - Quản lý trạng thái người dùng.
- Gửi email với **SendGrid**:
  - Tích hợp dịch vụ gửi email tự động.

#### 7. Các khái niệm nâng cao
- **View Components** trong .NET Core:
  - Tạo các thành phần giao diện tái sử dụng.
- **Khởi tạo cơ sở dữ liệu (DB Initializer)**:
  - Thiết lập và quản lý dữ liệu ban đầu cho ứng dụng.

#### 8. Triển khai ứng dụng
- Đưa ứng dụng hoàn chỉnh lên **Azure**:
  - Hướng dẫn quy trình triển khai.
  - Cung cấp kinh nghiệm thực tế về triển khai ứng dụng.

### Ghi chú thêm
- Đây là tổng quan cấp cao (10,000 ft) về nội dung khóa học.
- Các bài học tiếp theo sẽ đi sâu vào từng phần cụ thể.
- Tập trung vào việc áp dụng thực tế, sử dụng các công cụ và thư viện phổ biến trong phát triển ứng dụng .NET Core.