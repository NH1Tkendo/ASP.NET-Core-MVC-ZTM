## Ghi chú học tập: Quản lý danh mục trong Entity Framework Core

### Khái niệm cơ bản
- Mục đích: Tạo và khởi tạo dữ liệu ban đầu (seed data) cho bảng danh mục (Category) trong cơ sở dữ liệu sử dụng Entity Framework Core.
- Phương pháp: Sử dụng chức năng `OnModelCreating` trong `ApplicationDbContext` để thêm dữ liệu ban đầu vào bảng danh mục.
- Quy trình: Ghi đè (override) hàm `OnModelCreating`, sử dụng `ModelBuilder` để định nghĩa dữ liệu cần khởi tạo, sau đó tạo migration và cập nhật cơ sở dữ liệu.

### Các bước thực hiện

#### 1. Ghi đè hàm OnModelCreating
- Trong lớp `ApplicationDbContext`, ghi đè hàm `OnModelCreating` để cấu hình dữ liệu ban đầu.
- Hàm này nhận tham số kiểu `ModelBuilder` để định nghĩa cấu trúc và dữ liệu của các thực thể (entity).
- Cú pháp:
  ```csharp
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
      base.OnModelCreating(modelBuilder);
      // Cấu hình dữ liệu tại đây
  }
  ```

#### 2. Khởi tạo dữ liệu cho bảng danh mục
- Sử dụng `ModelBuilder` để chỉ định thực thể `Category` và thêm dữ liệu bằng phương thức `HasData`.
- Ví dụ: Thêm ba danh mục (`Action`, `SciFi`, `History`) với các thuộc tính `Id`, `Name`, và `DisplayOrder`.
- Mã nguồn:
  ```csharp
  modelBuilder.Entity<Category>().HasData(
      new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
      new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
      new Category { Id = 3, Name = "History", DisplayOrder = 3 }
  );
  ```

#### 3. Tạo và áp dụng migration
- Sau khi cấu hình dữ liệu, cần tạo migration để cập nhật cơ sở dữ liệu.
- Lệnh tạo migration:
  ```bash
  dotnet ef migrations add SeedCategoryTable
  ```
- Migration sẽ tạo lệnh `InsertData` để thêm ba bản ghi vào bảng `Category`.
- Áp dụng migration để cập nhật cơ sở dữ liệu:
  ```bash
  dotnet ef database update
  ```

#### 4. Kết quả
- Sau khi chạy lệnh `update database`, bảng `Category` sẽ chứa ba bản ghi:
  - Danh mục 1: `Action`, thứ tự hiển thị 1
  - Danh mục 2: `SciFi`, thứ tự hiển thị 2
  - Danh mục 3: `History`, thứ tự hiển thị 3
- Có thể kiểm tra bằng cách tải lại trang danh sách danh mục (category list page).

### Ghi chú thêm
- **Quan trọng**: Mỗi khi thay đổi cấu trúc hoặc dữ liệu trong cơ sở dữ liệu, luôn cần tạo migration và cập nhật cơ sở dữ liệu.
- Phương thức `HasData` trong `ModelBuilder` là cách chuẩn để khởi tạo dữ liệu ban đầu trong Entity Framework Core.
- Dữ liệu khởi tạo sẽ được áp dụng ngay khi migration được chạy, đảm bảo bảng danh mục không rỗng khi ứng dụng khởi động.
- Trong bài học tiếp theo: Tìm hiểu cách truy xuất (retrieve) các bản ghi từ bảng `Category`.

### Mã nguồn đầy đủ
```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    
    modelBuilder.Entity<Category>().HasData(
        new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
        new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
        new Category { Id = 3, Name = "History", DisplayOrder = 3 }
    );
}
```

### Thuật ngữ chuyên ngành
- **Entity Framework Core**: Framework quản lý cơ sở dữ liệu (database management framework).
- **ModelBuilder**: Công cụ cấu hình mô hình dữ liệu (data model configuration tool).
- **Migration**: Quá trình cập nhật cấu trúc cơ sở dữ liệu (database schema update process).
- **Seed data**: Dữ liệu khởi tạo ban đầu (initial data population).
- **OnModelCreating**: Phương thức cấu hình mô hình trong Entity Framework (model configuration method).