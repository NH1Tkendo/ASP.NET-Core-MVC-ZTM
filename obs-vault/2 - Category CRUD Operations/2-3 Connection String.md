## Entity Framework Core: Cấu Hình Chuỗi Kết Nối (Connection String) trong Dự Án

### Tổng Quan
- Để kết nối ứng dụng với cơ sở dữ liệu SQL Server, cần định nghĩa **chuỗi kết nối (Connection String)** trong dự án.
- Chuỗi kết nối được lưu trữ trong tệp `appsettings.json`, giúp quản lý thông tin kết nối một cách an toàn và tập trung.

### Yêu Cầu Cài Đặt
- Cần cài đặt **SQL Server** và **SQL Server Management Studio (SSMS)** để quản lý cơ sở dữ liệu.
- Trong SSMS, kết nối tới server bằng:
  - `localhost`: Máy chủ cục bộ.
  - `(localdb)\MSSQLLocalDB`: Máy chủ cục bộ LocalDB.
  - `.` (dấu chấm): Máy chủ mặc định trên máy cục bộ.
- Chọn máy chủ phù hợp dựa trên cấu hình của bạn.

### Thêm Chuỗi Kết Nối vào `appsettings.json`
1. **Vị trí lưu trữ**:
   - Chuỗi kết nối nên được định nghĩa trong tệp `appsettings.json`, không nên hardcode trong `Program.cs`.
   - Tệp `appsettings.json` là tệp JSON chứa các cặp key-value để lưu trữ thông tin cấu hình.

2. **Cấu trúc chuỗi kết nối**:
   - Sử dụng section `ConnectionStrings` (tên cố định, cần viết chính xác để EF Core nhận diện).
   - Trong section này, định nghĩa một cặp key-value với:
     - **Key**: Tên chuỗi kết nối (thường là `DefaultConnection`).
     - **Value**: Chuỗi kết nối tới SQL Server.

3. **Ví dụ mã trong `appsettings.json`**:
   ```json
   {
       "ConnectionStrings": {
           "DefaultConnection": "Server=.;Database=Bulky;Trusted_Connection=True;TrustServerCertificate=True"
       }
   }
   ```
   - **Giải thích**:
     - `Server=.`: Chỉ định máy chủ SQL (`.` là máy chủ cục bộ).
     - `Database=Bulky`: Tên cơ sở dữ liệu (sẽ được tạo nếu chưa tồn tại).
     - `Trusted_Connection=True`: Sử dụng xác thực Windows (không cần tài khoản/mật khẩu).
     - `TrustServerCertificate=True`: Bỏ qua kiểm tra chứng chỉ SSL (cần thiết với một số cài đặt SQL Server).

4. **Lưu ý**:
   - Tên section phải là `ConnectionStrings` (có chữ "s") để EF Core nhận diện.
   - Đảm bảo không có lỗi chính tả trong các thuộc tính (`Trusted_Connection` có dấu gạch dưới, `TrustServerCertificate` không có dấu cách hoặc gạch dưới).
   - Nếu không đặt `TrustServerCertificate=True`, kết nối có thể thất bại trên một số cấu hình SQL Server.

### Tiếp Theo
- Sau khi định nghĩa chuỗi kết nối, cần cấu hình EF Core trong dự án để sử dụng chuỗi này và tạo bảng trong cơ sở dữ liệu.
- Các bước cấu hình này sẽ được trình bày chi tiết trong các phần tiếp theo.

### Ghi Chú Thêm
- Sử dụng `appsettings.json` giúp tách biệt cấu hình khỏi mã nguồn, dễ dàng bảo trì và thay đổi.
- Việc đặt tên `DefaultConnection` là quy ước, nhưng có thể sử dụng tên khác nếu cần, miễn là tham chiếu đúng trong mã nguồn.
- Chuỗi kết nối phải được kiểm tra kỹ để đảm bảo kết nối thành công với SQL Server.