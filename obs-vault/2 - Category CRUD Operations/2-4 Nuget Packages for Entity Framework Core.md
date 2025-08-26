## Entity Framework Core: Cài Đặt Gói NuGet và Chuẩn Bị Tạo Cơ Sở Dữ Liệu

### Tổng Quan
- Để sử dụng **Entity Framework Core (EF Core)** tạo và quản lý cơ sở dữ liệu từ mã nguồn, cần cài đặt các gói **NuGet** cần thiết vào dự án.
- EF Core cho phép thao tác với cơ sở dữ liệu (tạo bảng, thêm dữ liệu, v.v.) trực tiếp từ mã nguồn, thay vì sử dụng công cụ quản lý cơ sở dữ liệu.

### Cài Đặt Gói NuGet
1. **Mở Quản Lý NuGet**:
   - Trong dự án (ví dụ: `BulkyWeb`), nhấp chuột phải và chọn **Manage NuGet Packages**.

2. **Cài đặt các gói NuGet cần thiết**:
   - **Microsoft.EntityFrameworkCore**:
     - Gói chính của EF Core, cung cấp các chức năng cốt lõi để làm việc với cơ sở dữ liệu.
     - Ví dụ: Cài đặt phiên bản preview cho .NET 8 (nếu đang dùng .NET 8 preview):
       - Bật tùy chọn **Include prerelease** trong NuGet Package Manager.
       - Chọn phiên bản phù hợp (ví dụ: 8.0.0-preview).
   - **Microsoft.EntityFrameworkCore.SqlServer**:
     - Gói hỗ trợ kết nối và làm việc với SQL Server.
     - Cài đặt phiên bản tương ứng với `Microsoft.EntityFrameworkCore` để tránh xung đột.
   - **Microsoft.EntityFrameworkCore.Tools**:
     - Cung cấp các công cụ dòng lệnh để thực hiện **migration** (di chuyển cơ sở dữ liệu).
     - Migration là quá trình tạo hoặc cập nhật lược đồ cơ sở dữ liệu dựa trên các model.

3. **Lưu ý khi cài đặt**:
   - Đảm bảo tất cả các gói NuGet của Microsoft (liên quan đến EF Core) có **cùng phiên bản** để tránh lỗi tương thích.
   - Nếu dự án sử dụng .NET 8 preview, chọn các gói preview tương ứng.
   - Có thể cài đặt phiên bản cụ thể thay vì phiên bản mới nhất nếu cần.

4. **Kiểm tra cài đặt**:
   - Sau khi cài đặt, các gói sẽ được thêm vào tệp dự án (`.csproj`). Ví dụ:
     ```xml
     <ItemGroup>
         <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.0-preview" />
         <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.0-preview" />
         <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.0-preview" />
     </ItemGroup>
     ```
   - Tệp `.csproj` lưu trữ thông tin về các gói NuGet, giúp dự án tải và sử dụng chúng.

### Migration Là Gì?
- **Migration** là quá trình EF Core sử dụng để tạo hoặc cập nhật lược đồ cơ sở dữ liệu (schema) dựa trên các model trong dự án.
- Gói `Microsoft.EntityFrameworkCore.Tools` cung cấp các lệnh (như `Add-Migration`, `Update-Database`) để thực hiện migration.
- Chi tiết về migration sẽ được giải thích trong các phần tiếp theo.

### Ghi Chú Thêm
- Việc cài đặt đúng phiên bản các gói NuGet là rất quan trọng để đảm bảo dự án hoạt động ổn định.
- Nếu có phiên bản mới của các gói, có thể cập nhật thông qua tab **Updates** trong NuGet Package Manager.
- Sau khi cài đặt các gói, dự án đã sẵn sàng để cấu hình EF Core và tạo cơ sở dữ liệu, sẽ được trình bày trong các bước tiếp theo.