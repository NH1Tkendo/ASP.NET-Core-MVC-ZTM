## Entity Framework Core: Tạo Cơ Sở Dữ Liệu và Xử Lý Lỗi Chuỗi Kết Nối

### Tổng Quan
- Sau khi cấu hình **ApplicationDbContext** và chuỗi kết nối trong dự án, chúng ta có thể sử dụng **Entity Framework Core (EF Core)** để tạo cơ sở dữ liệu.
- Quá trình cấu hình EF Core bao gồm:
  - Thêm các gói NuGet cần thiết.
  - Tạo lớp `ApplicationDbContext` kế thừa từ `DbContext`.
  - Đăng ký `DbContext` trong `Program.cs` với chuỗi kết nối từ `appsettings.json`.
- Bước tiếp theo là sử dụng lệnh trong **Package Manager Console** để tạo cơ sở dữ liệu.

### Tạo Cơ Sở Dữ Liệu
1. **Kiểm tra trước khi tạo**:
   - Trong **SQL Server Management Studio (SSMS)**, kết nối tới máy chủ (ví dụ: `.` cho máy cục bộ) và xác nhận rằng cơ sở dữ liệu (ví dụ: `Bulky`) chưa tồn tại.

2. **Sử dụng Package Manager Console**:
   - Mở **Tools > NuGet Package Manager > Package Manager Console** trong Visual Studio.
   - Chạy lệnh:
     ```
     Update-Database
     ```
   - Lệnh này sẽ:
     - Tạo cơ sở dữ liệu dựa trên chuỗi kết nối trong `appsettings.json` (ví dụ: `Database=Bulky`).
     - Tạo bảng hệ thống `__EFMigrationsHistory` để theo dõi các migration.

3. **Kết quả**:
   - Sau khi chạy lệnh thành công, cơ sở dữ liệu `Bulky` sẽ xuất hiện trong SSMS.
   - Bảng `__EFMigrationsHistory` được tạo tự động để lưu lịch sử các migration.
   - Nếu không có migration nào được áp dụng, thông báo sẽ hiển thị: *“No migrations were applied. Database is already up to date.”*

### Xử Lý Lỗi Thường Gặp
1. **Lỗi: "Connection string property has not been initialized"**:
   - **Nguyên nhân**: Tên khóa chuỗi kết nối trong `Program.cs` không khớp với tên trong `appsettings.json`.
     - Ví dụ: Trong `Program.cs` sử dụng `GetConnectionString("DefaultConnection1")`, nhưng `appsettings.json` chỉ có `DefaultConnection`.
   - **Khắc phục**:
     - Kiểm tra và sửa tên khóa trong `Program.cs`:
       ```csharp
       builder.Services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
       ```
     - Đảm bảo `appsettings.json` có đúng khóa:
       ```json
       {
           "ConnectionStrings": {
               "DefaultConnection": "Server=.;Database=Bulky;Trusted_Connection=True;TrustServerCertificate=True"
           }
       }
       ```

2. **Lỗi: "Keyword not supported: server"**:
   - **Nguyên nhân**: Cú pháp chuỗi kết nối sai (ví dụ: sử dụng dấu `:` thay vì `=`).
     - Sai: `"Server:.;Database=Bulky;..."`.
     - Đúng: `"Server=.;Database=Bulky;..."`.
   - **Khắc phục**:
     - Sửa chuỗi kết nối trong `appsettings.json`:
       ```json
       {
           "ConnectionStrings": {
               "DefaultConnection": "Server=.;Database=Bulky;Trusted_Connection=True;TrustServerCertificate=True"
           }
       }
       ```
   - Sau khi sửa, chạy lại lệnh `Update-Database`.

### Tiếp Theo
- Cơ sở dữ liệu đã được tạo, nhưng chưa có bảng nào ngoài `__EFMigrationsHistory`.
- Để tạo bảng dựa trên model (ví dụ: bảng `Category` với các cột `Id`, `Name`, `DisplayOrder`), cần thực hiện **migration**:
  - Thêm model vào `ApplicationDbContext`.
  - Chạy lệnh `Add-Migration` và `Update-Database`.
- Các bước này sẽ được trình bày chi tiết trong các phần tiếp theo.

### Ghi Chú Thêm
- Lệnh `Update-Database` chỉ tạo cơ sở dữ liệu nếu nó chưa tồn tại và áp dụng các migration (nếu có).
- Bảng `__EFMigrationsHistory` là bảng hệ thống, không cần chỉnh sửa thủ công.
- Đảm bảo chuỗi kết nối trong `appsettings.json` và cấu hình trong `Program.cs` chính xác để tránh lỗi khi tạo cơ sở dữ liệu.
- Migration sẽ giúp ánh xạ các model (như `Category`) thành các bảng trong cơ sở dữ liệu, sẽ được giải thích chi tiết sau.