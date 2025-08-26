## Ghi chú học tập: Triển khai CategoryRepository

### Mục đích
- Hiểu cách triển khai lớp `CategoryRepository` để thực hiện các phương thức của giao diện `ICategoryRepository`, đồng thời tái sử dụng chức năng từ lớp `Repository` thông qua kế thừa và tiêm phụ thuộc (Dependency Injection).

### Khái niệm
- **Triển khai giao diện**: Lớp `CategoryRepository` triển khai `ICategoryRepository` để cung cấp logic cụ thể cho các phương thức như `Update` và `Save`.
- **Kế thừa lớp cơ sở**: `CategoryRepository` kế thừa từ `Repository<Category>` để tái sử dụng các phương thức cơ bản như `Add`, `Get`, `GetAll`, `Remove`, `RemoveRange`.
- **Tiêm phụ thuộc (Dependency Injection)**: Sử dụng `ApplicationDbContext` để tương tác với cơ sở dữ liệu, được truyền vào qua constructor.

### Cách thực hiện
- **Tạo lớp `CategoryRepository`**:
  - Tạo một lớp công khai (`public class`) có tên `CategoryRepository`.
  - Lớp này kế thừa từ `Repository<Category>` và triển khai `ICategoryRepository`.
  - Sử dụng phím tắt (Ctrl + .) để tự động triển khai các phương thức của giao diện, nhưng các phương thức cơ bản đã được xử lý bởi lớp cơ sở `Repository<Category>`.
- **Xử lý lỗi thiếu tham số**:
  - Lớp `Repository<Category>` yêu cầu một tham số `ApplicationDbContext` trong constructor.
  - Thêm constructor vào `CategoryRepository` để nhận `ApplicationDbContext` thông qua tiêm phụ thuộc và truyền nó cho lớp cơ sở (`base(db)`).
- **Triển khai phương thức**:
  - `Save`: Gọi `_db.SaveChanges()` để lưu các thay đổi vào cơ sở dữ liệu.
  - `Update`: Sử dụng `_db.Categories.Update(obj)` để cập nhật thông tin của đối tượng `Category`.

### Mã nguồn
```csharp
public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _db;

    public CategoryRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Save()
    {
        _db.SaveChanges();
    }

    public void Update(Category obj)
    {
        _db.Categories.Update(obj);
    }
}
```

### Ghi chú thêm
- **Tái sử dụng mã**: Kế thừa từ `Repository<Category>` giúp tránh lặp lại mã cho các phương thức cơ bản như `Add`, `Get`, `GetAll`,...
- **Tiêm phụ thuộc**: `ApplicationDbContext` được truyền vào constructor thông qua cơ chế tiêm phụ thuộc, giúp quản lý kết nối cơ sở dữ liệu một cách hiệu quả.
- **Ứng dụng tiếp theo**: Cần thay thế việc sử dụng trực tiếp `ApplicationDbContext` trong mã hiện tại bằng `CategoryRepository` để tăng tính trừu tượng và dễ bảo trì.
