## Ghi chú học tập: Quản lý kết nối cơ sở dữ liệu và Migration trong Entity Framework Core

### Mục đích
- Hiểu cách cập nhật chuỗi kết nối (connection string) trong tệp `appsettings.json` và sử dụng lệnh `update-database` để áp dụng các migration trong Entity Framework Core, đảm bảo dự án hoạt động trên các môi trường khác nhau.

### Khái niệm
- **Chuỗi kết nối (Connection String)**: Cung cấp thông tin để kết nối với cơ sở dữ liệu, ví dụ: tên server, tên cơ sở dữ liệu, thông tin xác thực.
- **Migration trong Entity Framework Core**: Quá trình tạo và áp dụng các thay đổi cấu trúc cơ sở dữ liệu dựa trên mã nguồn (code-first approach).
- **Lệnh `update-database`**: Áp dụng tất cả các migration đã được định nghĩa trong mã nguồn lên cơ sở dữ liệu, đảm bảo cơ sở dữ liệu đồng bộ với mô hình.

### Cách thực hiện
- **Cập nhật chuỗi kết nối**:
  - Mở tệp `appsettings.json`.
  - Tìm và cập nhật trường `Server` trong chuỗi kết nối. Ví dụ:
    - Ban đầu: `Server=.` (không hoạt động do thay đổi cấu hình máy).
    - Cập nhật thành: `Server=(LocalDB)\\MSSQLLocalDB` (sử dụng tên server phù hợp).
  - Lưu ý: Entity Framework Core tự động thoát ký tự `\` thành `\\` trong chuỗi kết nối.
- **Áp dụng migration**:
  - Mở **Package Manager Console** trong Visual Studio.
  - Chạy lệnh:
    ```powershell
    update-database
    ```
  - Lệnh này sẽ áp dụng tất cả các migration đã định nghĩa trong dự án để tạo hoặc cập nhật cơ sở dữ liệu (ví dụ: cơ sở dữ liệu `Bulky`).
- **Kết quả**:
  - Cơ sở dữ liệu được tạo hoặc cập nhật với cấu trúc và dữ liệu khởi tạo (seeding data) như đã định nghĩa.
  - Các chức năng như hiển thị danh sách danh mục (`Category`) hoạt động bình thường.

### Mã nguồn (Chuỗi kết nối trong `appsettings.json`)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(LocalDB)\\MSSQLLocalDB;Database=Bulky;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### Ghi chú thêm
- **Tầm quan trọng của Migration**:
  - Migration cho phép đồng bộ cấu trúc cơ sở dữ liệu với mã nguồn, đảm bảo tính nhất quán trên các môi trường (local, Azure SQL,...).
  - Giúp các nhà phát triển mới dễ dàng thiết lập dự án chỉ với lệnh `update-database` sau khi sao chép mã nguồn.
- **Ưu điểm của Entity Framework Core**:
  - Giảm thời gian thiết lập dự án so với các phương pháp truyền thống (trước đây có thể mất cả ngày để cấu hình do nhiều phụ thuộc).
  - Chỉ cần cập nhật chuỗi kết nối và chạy `update-database` để dự án hoạt động.
- **Ứng dụng thực tế**:
  - Đối với môi trường khác (ví dụ: Azure SQL), chỉ cần thay đổi chuỗi kết nối trong `appsettings.json` và chạy lại `update-database`.
  - Dữ liệu khởi tạo (seeding data) như danh sách danh mục sẽ tự động được áp dụng.
