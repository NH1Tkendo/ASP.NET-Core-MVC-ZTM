## Tách biệt logic dự án BulkyWeb thành các dự án Class Library

### Mục tiêu
- Tách các thành phần trong dự án `BulkyWeb` (MVC) thành các dự án Class Library riêng biệt (`Bulky.DataAccess`, `Bulky.Models`, `Bulky.Utility`) để tăng tính module hóa và dễ bảo trì.
- Cập nhật namespace, tham chiếu dự án, và cài đặt các gói NuGet cần thiết để đảm bảo ứng dụng hoạt động đúng sau khi tách.

### Các bước thực hiện

#### 1. Di chuyển các thành phần sang dự án tương ứng
- **Di chuyển thư mục `Data` sang `

System: Bulky.DataAccess`**:
  - Chuyển thư mục `Data` (chứa `ApplicationDbContext`) từ `BulkyWeb` sang `Bulky.DataAccess`.
  - Xóa thư mục `Data` trong `BulkyWeb` sau khi di chuyển.
- **Di chuyển thư mục `Models` sang `Bulky.Models`**:
  - Chuyển thư mục `Models` (chứa các lớp như `Category`) sang `Bulky.Models`.
  - Xóa thư mục `Models` trong `BulkyWeb`.
- **Di chuyển thư mục `Migrations` sang `Bulky.DataAccess`**:
  - Chuyển thư mục `Migrations` sang `Bulky.DataAccess` để quản lý các migration liên quan đến cơ sở dữ liệu.
  - Xóa thư mục `Migrations` trong `BulkyWeb`.
- **Tạo lớp `SD` trong `Bulky.Utility`**:
  - Thêm một lớp tĩnh mới tên `SD` (Static Details) trong `Bulky.Utility` để lưu trữ các hằng số (constants) của ứng dụng.
  - Ví dụ:
    ```csharp
    namespace Bulky.Utility;
    public static class SD
    {
        // Định nghĩa các hằng số tại đây
    }
    ```

#### 2. Cài đặt gói NuGet trong `Bulky.DataAccess`
- Sau khi di chuyển `ApplicationDbContext` và `Migrations`, cần cài đặt các gói NuGet liên quan đến Entity Framework Core để giải quyết lỗi.
- Các gói cần cài:
  - `Microsoft.EntityFrameworkCore`
  - `Microsoft.EntityFrameworkCore.SqlServer`
  - `Microsoft.EntityFrameworkCore.Tools`
- Thao tác:
  1. Trong Visual Studio, vào **Manage NuGet Packages for Solution**.
  2. Cài đặt các gói trên cho dự án `Bulky.DataAccess`.
- Kết quả: Các lỗi liên quan đến Entity Framework Core trong `ApplicationDbContext` được giải quyết.

#### 3. Cập nhật Namespace
- Sau khi di chuyển, cần cập nhật namespace trong các file để phản ánh cấu trúc dự án mới:
  - Trong `Bulky.DataAccess`:
    - Cập nhật namespace của `ApplicationDbContext` từ `BulkyWeb.Data` thành `Bulky.DataAccess.Data`.
    - Cập nhật namespace trong các file migration (`.cs` và `.Designer.cs`) thành `Bulky.DataAccess.Migrations`.
  - Trong `Bulky.Models`:
    - Cập nhật namespace của các lớp (như `Category`) từ `BulkyWeb.Models` thành `Bulky.Models`.
  - Ví dụ:
    ```csharp
    // Trong ApplicationDbContext.cs
    namespace Bulky.DataAccess.Data;

    // Trong Category.cs
    namespace Bulky.Models;
    ```

#### 4. Thêm tham chiếu dự án
- Trong `BulkyWeb`, thêm tham chiếu (project references) đến:
  - `Bulky.DataAccess`
  - `Bulky.Models`
  - `Bulky.Utility`
- Thao tác:
  1. Nhấp chuột phải vào `BulkyWeb` trong Solution Explorer.
  2. Chọn **Add > Project Reference**.
  3. Chọn các dự án `Bulky.DataAccess`, `Bulky.Models`, và `Bulky.Utility`.
- Trong `Bulky.DataAccess`, thêm tham chiếu đến `Bulky.Models` để `ApplicationDbContext` có thể truy cập các lớp mô hình.

#### 5. Xử lý lỗi trong Views
- Các lỗi trong Views (như `ErrorViewModel`) xuất hiện do namespace `BulkyWeb/Models` không còn tồn tại.
- Giải pháp: Thêm namespace `Bulky.Models` vào file `_ViewImports.cshtml` để áp dụng global using cho tất cả các Views.
  ```csharp
  // Trong _ViewImports.cshtml
  @using Bulky.Models
  ```

#### 6. Xử lý lỗi trong Migrations
- Lỗi migration xảy ra do namespace không khớp giữa file `.cs` và `.Designer.cs`.
- Giải pháp: Cập nhật namespace trong cả hai file migration (`.cs` và `.Designer.cs`) thành `Bulky.DataAccess.Migrations`.
  ```csharp
  // Trong MigrationName.cs và MigrationName.Designer.cs
  namespace Bulky.DataAccess.Migrations;
  ```

#### 7. Cập nhật `Program.cs`
- Trong `BulkyWeb/Program.cs`, cập nhật namespace của `ApplicationDbContext` thành `Bulky.DataAccess.Data`.
- Ví dụ:
  ```csharp
  builder.Services.AddDbContext<Bulky.DataAccess.Data.ApplicationDbContext>(options =>
      options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
  ```

### Kết quả
- Các dự án `Bulky.DataAccess`, `Bulky.Models`, và `Bulky.Utility` hoạt động độc lập và được tổ chức hợp lý.
- Dự án `BulkyWeb` vẫn hoạt động bình thường với các trang CRUD (Create, Edit, Delete, Index).
- Ứng dụng trở nên "sạch" hơn với cấu trúc module hóa, dễ mở rộng và bảo trì.

### Ghi chú thêm
- **Tầm quan trọng của tách biệt**: Việc tách logic thành các dự án riêng giúp dễ dàng quản lý mã nguồn, đặc biệt trong các dự án lớn.
- **Xử lý lỗi namespace**: Khi di chuyển file, cần kiểm tra và cập nhật namespace trong cả file code và file liên quan (như `.Designer.cs`).
- **Thực hành thực tế**: Quá trình tách biệt này mô phỏng cách xử lý khi dự án phát triển phức tạp từ một cấu trúc đơn giản.
- Nếu lỗi vẫn xuất hiện, kiểm tra lại tham chiếu dự án, namespace, và các gói NuGet đã cài đặt.