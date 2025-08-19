## Bài học: Tìm hiểu Connected Services, Dependencies và LaunchSettings.json trong Dự án MVC

### Mục tiêu
- Hiểu vai trò của **Connected Services**, **Dependencies**, và tệp **launchSettings.json** trong dự án ASP.NET Core MVC.
- Tìm hiểu cách cấu hình và sử dụng các thiết lập để chạy ứng dụng.

### Nội dung chính

#### 1. Connected Services
- Hiện tại: **Connected Services** trống.
- Mục đích: Quản lý các dịch vụ bên ngoài (ví dụ: dịch vụ đám mây, cơ sở dữ liệu).
- Ghi chú: Sẽ bỏ qua vì chưa sử dụng trong dự án này.

#### 2. Dependencies
- **Dependencies** (Phụ thuộc):
  - Liệt kê các gói (packages) hoặc dự án mà dự án hiện tại phụ thuộc.
  - Ví dụ: Các gói NuGet để kết nối cơ sở dữ liệu, tích hợp thanh toán (như Stripe).
- Hiện tại: Không có gói NuGet hoặc dự án phụ thuộc nào.
- Trong tương lai:
  - Khi thêm **NuGet packages** hoặc dự án khác, **Dependencies** sẽ tự động cập nhật.
  - Sẽ được hướng dẫn chi tiết trong các bài sau.

#### 3. Tệp launchSettings.json
- Vị trí: Nằm trong thư mục **Properties**.
- Mục đích: Xác định các thiết lập khi chạy hoặc gỡ lỗi (debug) ứng dụng.
- Nội dung chính:
  - **IIS Settings**:
    - Xác định URL và số cổng cho HTTP/HTTPS.
    - Ví dụ: 
      - HTTP: `http://localhost:<port>`
      - HTTPS: `https://localhost:<port>`
  - **Profiles**:
    - Bao gồm các cấu hình như **HTTP**, **HTTPS**, và **IIS Express**.
    - Mỗi profile chỉ định:
      - URL ứng dụng.
      - Biến môi trường (environment variables).
  - **Environment Variables**:
    - Hoạt động như các biến toàn cục.
    - Ví dụ: `ASPNETCORE_ENVIRONMENT`:
      - `Development`: Sử dụng cơ sở dữ liệu hoặc khóa API dành cho phát triển (không dùng thẻ tín dụng thật).
      - `Production`: Sử dụng cơ sở dữ liệu hoặc khóa API sản xuất (hỗ trợ thanh toán thực).
  - Cách sử dụng:
    - Chọn profile để chạy ứng dụng (mặc định: HTTPS).
    - Có thể thay đổi cổng (port) trong `applicationUrl`.
      - Ví dụ: Thay đổi cổng từ `7169` sang `7001` → Ứng dụng chạy trên `https://localhost:7001`.
- Lưu ý:
  - Thay đổi cổng được lưu trong `launchSettings.json`.
  - Có thể hoàn tác (undo) thay đổi qua Git để trở về cấu hình gốc.
  - Thông thường, không cần chỉnh sửa profile thường xuyên.

### Ví dụ thực tế
- Thay đổi cổng trong `launchSettings.json`:
  ```json
  "applicationUrl": "https://localhost:7001;http://localhost:5000"
  ```
  - Sau khi thay đổi và chạy, ứng dụng khởi động trên cổng `7001`.

### Ghi chú thêm
- **Connected Services** và **Dependencies** sẽ được cập nhật khi tích hợp các dịch vụ hoặc gói mới.
- **launchSettings.json** quan trọng trong việc kiểm soát cách ứng dụng chạy trong môi trường phát triển.
- Để hoàn tác thay đổi (như cổng), sử dụng Git để khôi phục tệp.

```markdown
**Lưu ý**: Đảm bảo kiểm tra cổng và biến môi trường trong `launchSettings.json` khi chạy ứng dụng để tránh xung đột cổng. Sử dụng Git để quản lý thay đổi an toàn.
```