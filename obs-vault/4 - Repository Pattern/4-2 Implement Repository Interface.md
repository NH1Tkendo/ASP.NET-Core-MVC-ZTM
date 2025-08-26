## Triển khai Generic Repository trong ASP.NET Core

### Mục tiêu
- Triển khai lớp generic repository (`Repository<T>`) trong dự án `Bulky.DataAccess` để thực hiện các thao tác CRUD theo giao diện `IRepository<T>`.
- Sử dụng generic để áp dụng repository cho bất kỳ mô hình nào (như `Category`, `Product`), tăng tính tái sử dụng và giảm lặp mã.

### Các bước triển khai

#### 1. Tạo lớp `Repository`
- Trong thư mục `Repository` của dự án `Bulky.DataAccess`, tạo lớp `Repository.cs`.
- Lớp này triển khai giao diện `IRepository<T>` với generic type `T` (phải là một class).
- Đảm bảo giao diện `IRepository` được đặt ở chế độ `public` để tránh lỗi truy cập.

#### Mã nguồn trong `Repository.cs`
```csharp
using Bulky.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bulky.DataAccess.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;
    internal DbSet<T> dbSet;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
        dbSet = _db.Set<T>();
    }

    public void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet;
        query = query.Where(filter);
        return query.FirstOrDefault();
    }

    public IEnumerable<T> GetAll()
    {
        IQueryable<T> query = dbSet;
        return query.ToList();
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        dbSet.RemoveRange(entities);
    }
}
```

#### Giải thích mã nguồn
- **Constructor và DbSet**:
  - Inject `ApplicationDbContext` thông qua constructor để truy cập cơ sở dữ liệu.
  - Tạo một `DbSet<T>` nội bộ (`dbSet`) bằng cách gọi `_db.Set<T>()`, cho phép truy cập tập hợp dữ liệu generic (ví dụ: `Categories` khi `T` là `Category`).
  - `dbSet` thay thế cho `_db.Categories`, giúp mã generic và tái sử dụng được.
- **Phương thức `Add`**:
  - Thêm một bản ghi (`entity`) vào `dbSet` bằng `dbSet.Add(entity)`.
- **Phương thức `GetFirstOrDefault`**:
  - Tạo một `IQueryable<T>` từ `dbSet`.
  - Áp dụng điều kiện lọc (`filter`) bằng `query.Where(filter)`.
  - Trả về bản ghi đầu tiên thỏa mãn điều kiện hoặc `null` nếu không tìm thấy (`query.FirstOrDefault()`).
- **Phương thức `GetAll`**:
  - Trả về tất cả bản ghi từ `dbSet` dưới dạng danh sách (`query.ToList()`).
- **Phương thức `Remove` và `RemoveRange`**:
  - `Remove`: Xóa một bản ghi (`entity`) khỏi `dbSet`.
  - `RemoveRange`: Xóa nhiều bản ghi (`entities`) khỏi `dbSet`.

### Lợi ích của triển khai Generic Repository
- **Tái sử dụng**: Có thể sử dụng cho bất kỳ mô hình nào (như `Category`, `Product`) mà không cần viết lại mã CRUD.
- **Tính module hóa**: Tách biệt logic truy cập dữ liệu khỏi Controller, giúp mã dễ bảo trì và kiểm thử.
- **Sử dụng LINQ linh hoạt**: Phương thức `GetFirstOrDefault` hỗ trợ các biểu thức LINQ để lọc dữ liệu theo nhiều điều kiện, thay vì chỉ dựa vào ID như `Find`.

### Ghi chú thêm
- Đảm bảo thêm using statement `using Bulky.DataAccess.Data` để truy cập `ApplicationDbContext`.
- Namespace của `IRepository` và `Repository` phải nhất quán (ví dụ: `Bulky.DataAccess.Repository`).
- Trong các bước tiếp theo, cần tạo repository cụ thể (như `CategoryRepository`) kế thừa từ `Repository<T>` để thêm các phương thức cụ thể (như `Update` và `SaveChanges`).
- Nếu gặp lỗi khi triển khai, kiểm tra:
  - Tham chiếu dự án (`Bulky.DataAccess` phải tham chiếu `Bulky.Models`).
  - Namespace trong các file liên quan.
  - Cài đặt các gói NuGet cần thiết (`Microsoft.EntityFrameworkCore`, `Microsoft.EntityFrameworkCore.SqlServer`).