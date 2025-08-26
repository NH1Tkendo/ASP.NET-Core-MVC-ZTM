## Entity Framework Core: Cấu Hình DbContext và Kết Nối Cơ Sở Dữ Liệu

### Tổng Quan
- Để kết nối **Entity Framework Core (EF Core)** với cơ sở dữ liệu, cần tạo một lớp **DbContext** và cấu hình nó trong dự án.
- Lớp **DbContext** đóng vai trò cầu nối giữa mã nguồn và cơ sở dữ liệu, cho phép EF Core thực hiện các thao tác như tạo bảng, truy vấn dữ liệu, v.v.

### Tạo Lớp `ApplicationDbContext`
1. **Tạo thư mục và lớp**:
   - Trong dự án, tạo một thư mục mới tên `Data`.
   - Thêm một lớp mới trong thư mục `Data` với tên `ApplicationDbContext.cs` (tên lớp có thể tùy chỉnh, nhưng `ApplicationDbContext` là quy ước phổ biến).

2. **Cấu trúc lớp `ApplicationDbContext`**:
   - Lớp này phải kế thừa từ `DbContext` (có trong `Microsoft.EntityFrameworkCore`).
   - Thêm constructor để nhận thông tin cấu hình (options) và chuyển nó cho lớp cơ sở `DbContext`.
   - Ví dụ:
     ```csharp
     using Microsoft.EntityFrameworkCore;

     public class ApplicationDbContext : DbContext
     {
         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
         {
         }
     }
     ```
   - **Giải thích**:
     - `DbContextOptions<ApplicationDbContext>`: Đối tượng chứa thông tin cấu hình (như chuỗi kết nối) cho `DbContext`.
     - `base(options)`: Chuyển các tùy chọn cấu hình đến lớp cơ sở `DbContext`.

### Cấu Hình `DbContext` trong `Program.cs`
1. **Đăng ký `DbContext`**:
   - Trong tệp `Program.cs`, thêm dịch vụ `DbContext` vào container dịch vụ (services) của ứng dụng.
   - Sử dụng phương thức `AddDbContext` để chỉ định lớp `ApplicationDbContext` và cấu hình kết nối tới SQL Server.

2. **Ví dụ mã trong `Program.cs`**:
   ```csharp
   var builder = WebApplication.CreateBuilder(args);

   // Thêm dịch vụ vào container
   builder.Services.AddDbContext<ApplicationDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

   // Các cấu hình khác
   builder.Services.AddControllersWithViews();

   var app = builder.Build();
   // ...
   app.Run();
   ```
   - **Giải thích**:
     - `AddDbContext<ApplicationDbContext>`: Đăng ký `ApplicationDbContext` làm dịch vụ trong ứng dụng.
     - `options.UseSqlServer(...)`: Chỉ định rằng `DbContext` sử dụng SQL Server làm cơ sở dữ liệu.
     - `builder.Configuration.GetConnectionString("DefaultConnection")`: Lấy chuỗi kết nối từ section `ConnectionStrings` trong `appsettings.json` với khóa `DefaultConnection`.

3. **Lưu ý về `ConnectionStrings`**:
   - Section `ConnectionStrings` trong `appsettings.json` là một section đặc biệt, được EF Core nhận diện thông qua phương thức `GetConnectionString`.
   - Ví dụ `appsettings.json`:
     ```json
     {
         "ConnectionStrings": {
             "DefaultConnection": "Server=.;Database=Bulky;Trusted_Connection=True;TrustServerCertificate=True"
         }
     }
     ```
   - Nếu khóa (ví dụ: `DefaultConnection`) không tồn tại hoặc sai tên (như `DefaultConnection1`), ứng dụng sẽ báo lỗi khi chạy.

### Lỗi Thường Gặp
- **Sai tên khóa trong `GetConnectionString`**:
  - Ví dụ: Nếu dùng `GetConnectionString("DefaultConnection1")` trong khi `appsettings.json` chỉ có `DefaultConnection`, ứng dụng sẽ không tìm thấy chuỗi kết nối.
  - Luôn kiểm tra chính xác tên khóa trong `appsettings.json`.

### Ghi Chú Thêm
- Lớp `ApplicationDbContext` là thành phần cốt lõi để EF Core tương tác với cơ sở dữ liệu.
- Cấu hình trong `Program.cs` đảm bảo ứng dụng biết cách sử dụng EF Core và kết nối tới SQL Server.
- Việc sử dụng `GetConnectionString` giúp truy xuất chuỗi kết nối một cách an toàn và dễ bảo trì.
- Các bước tiếp theo (như migration) sẽ sử dụng cấu hình này để tạo bảng trong cơ sở dữ liệu, sẽ được trình bày trong các phần sau.