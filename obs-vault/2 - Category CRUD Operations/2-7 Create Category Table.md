```markdown
## Tạo Bảng Dữ Liệu với Entity Framework Core

### Tổng quan
- Bài học giới thiệu cách sử dụng **Entity Framework Core** để tạo bảng **Categories** trong cơ sở dữ liệu (database).
- Quy trình bao gồm: định nghĩa mô hình (model), cấu hình **DbContext**, tạo migration và áp dụng lên cơ sở dữ liệu.
- Mục tiêu: Tạo bảng **Categories** với 3 cột: **ID**, **Name**, **DisplayOrder**.

### Khái niệm cơ bản
- **Entity Framework Core (EF Core)**: Công cụ ORM (Object-Relational Mapping) giúp ánh xạ các lớp (class) trong mã nguồn thành bảng trong cơ sở dữ liệu.
- **Primary Key (Khóa chính)**:
  - Cột được đánh dấu bằng thuộc tính `[Key]` hoặc có tên là `ID` hoặc `<Tên_Model>ID` (ví dụ: `CategoryID`) sẽ được EF Core tự động nhận diện là khóa chính.
  - Nếu không định nghĩa khóa chính, EF Core sẽ báo lỗi khi tạo migration.
- **DbContext**: Lớp trung tâm quản lý kết nối cơ sở dữ liệu và các bảng (thông qua **DbSet**).
- **Migration**: Công cụ tự động tạo và cập nhật lược đồ cơ sở dữ liệu dựa trên mã nguồn.

### Quy trình tạo bảng Categories

#### 1. Định nghĩa mô hình (Model)
- Tạo lớp `Category` với các thuộc tính:
  - `ID`: Khóa chính.
  - `Name`: Tên danh mục.
  - `DisplayOrder`: Thứ tự hiển thị.
- Ví dụ mã nguồn:
```csharp
public class Category
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int DisplayOrder { get; set; }
}
```
- **Lưu ý**: Nếu đặt tên cột khác (ví dụ: `Category21ID`) và không có `[Key]`, EF Core sẽ báo lỗi do không tìm thấy khóa chính.

#### 2. Cấu hình DbContext
- Trong lớp `ApplicationDbContext`, thêm thuộc tính `DbSet` để ánh xạ bảng:
```csharp
public DbSet<Category> Categories { get; set; }
```
- Tên thuộc tính (`Categories`) sẽ được sử dụng làm tên bảng trong cơ sở dữ liệu.

#### 3. Tạo Migration
- **Công cụ**: Sử dụng **Package Manager Console** trong Visual Studio.
- **Lệnh**:
  - `Add-Migration <Tên_Migration>`: Tạo tệp migration.
  - Ví dụ: `Add-Migration AddCategoryTableToDB`.
- **Kết quả**:
  - EF Core tạo thư mục `Migrations` với tệp migration (ví dụ: `<Timestamp>_AddCategoryTableToDB.cs`).
  - Tệp migration chứa hai phương thức:
    - `Up()`: Mã lệnh để tạo bảng (với cột `ID`, `Name`, `DisplayOrder` và ràng buộc khóa chính).
    - `Down()`: Mã lệnh để xóa bảng (rollback nếu có lỗi).
  - Mã ví dụ trong tệp migration:
```csharp
migrationBuilder.CreateTable(
    name: "Categories",
    columns: table => new
    {
        ID = table.Column<int>(nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
        Name = table.Column<string>(nullable: true),
        DisplayOrder = table.Column<int>(nullable: false)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_Categories", x => x.ID);
    });
```

#### 4. Áp dụng Migration vào cơ sở dữ liệu
- **Lệnh**: `Update-Database`
- **Quy trình**:
  - EF Core kiểm tra bảng `__EFMigrationsHistory` để xác định các migration chưa được áp dụng.
  - Nếu có migration mới (như `AddCategoryTableToDB`), EF Core chuyển mã migration thành lệnh SQL để tạo bảng.
- **Kết quả**:
  - Bảng `Categories` được tạo trong cơ sở dữ liệu với 3 cột: `ID` (khóa chính, tự động tăng), `Name`, `DisplayOrder`.
  - Bảng `__EFMigrationsHistory` ghi nhận migration đã áp dụng.

#### 5. Xử lý lỗi
- Nếu thiếu khóa chính (primary key):
  - Lỗi: `The entity type 'Category' requires a primary key to be defined`.
  - Cách khắc phục: Đảm bảo có cột `ID` hoặc `<Tên_Model>ID`, hoặc sử dụng `[Key]` để chỉ định khóa chính.
- Để xóa migration (nếu cần): `Remove-Migration` (chỉ hoạt động nếu migration chưa được áp dụng).

### Tóm tắt quy trình tạo bảng
1. Tạo lớp mô hình (`Category`) với các thuộc tính cần thiết.
2. Thêm `DbSet` trong `ApplicationDbContext`.
3. Chạy lệnh `Add-Migration <Tên_Migration>` để tạo tệp migration.
4. Chạy lệnh `Update-Database` để áp dụng migration vào cơ sở dữ liệu.

### Ghi chú thêm
- EF Core tự động xử lý các lệnh SQL, giúp lập trình viên tập trung vào viết mã thay vì quản lý cơ sở dữ liệu trực tiếp.
- Migration giúp theo dõi và quản lý các thay đổi lược đồ cơ sở dữ liệu một cách dễ dàng.
- Để kiểm tra bảng đã tạo:
  - Mở SQL Server Management Studio, làm mới danh sách bảng.
  - Kiểm tra bảng `__EFMigrationsHistory` để xem lịch sử migration.

### Tài liệu tham khảo
- Nội dung bài học sẽ tiếp tục được mở rộng trong các video tiếp theo, giải thích chi tiết hơn về migrations và các tính năng nâng cao của EF Core.
```