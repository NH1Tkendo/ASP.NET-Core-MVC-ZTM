## Giới thiệu về Repository Pattern trong ASP.NET Core

### Mục tiêu
- Tạo một giao diện generic repository (`IRepository`) trong dự án `Bulky.DataAccess` để quản lý các thao tác CRUD (Create, Read, Update, Delete) cho các mô hình như `Category`, `Product`, v.v.
- Sử dụng generic để áp dụng repository cho nhiều loại mô hình, tăng tính tái sử dụng và giảm lặp mã.

### Tổng quan về Repository Pattern
- **Repository Pattern** là một mẫu thiết kế giúp tách biệt logic truy cập dữ liệu (data access logic) khỏi logic nghiệp vụ (business logic).
- Giao diện generic repository (`IRepository<T>`) cho phép áp dụng các thao tác CRUD trên bất kỳ mô hình nào (`T` là một class).
- Lợi ích:
  - Tăng tính module hóa và tái sử dụng mã.
  - Dễ dàng kiểm thử (test) và bảo trì.
  - Tách biệt logic truy cập cơ sở dữ liệu khỏi Controller.

### Tạo giao diện `IRepository`
- Trong dự án `Bulky.DataAccess`, tạo thư mục `Repository` để chứa các interface và triển khai liên quan.
- Tạo file `IRepository.cs` trong thư mục `Repository` với nội dung giao diện generic.

#### Mã nguồn trong `IRepository.cs`
```csharp
using System.Linq.Expressions;

namespace Bulky.DataAccess.Repository;

public interface IRepository<T> where T : class
{
    // T có thể là Category, Product, hoặc bất kỳ mô hình nào khác
    IEnumerable<T> GetAll(); // Lấy tất cả bản ghi
    T GetFirstOrDefault(Expression<Func<T, bool>> filter); // Lấy bản ghi đầu tiên theo điều kiện
    void Add(T entity); // Thêm bản ghi mới
    void Remove(T entity); // Xóa một bản ghi
    void RemoveRange(IEnumerable<T> entities); // Xóa nhiều bản ghi
}
```

#### Giải thích các phương thức
- **`GetAll()`**:
  - Trả về danh sách tất cả bản ghi của kiểu `T` (ví dụ: tất cả danh mục).
  - Kiểu trả về: `IEnumerable<T>`.
- **`GetFirstOrDefault(Expression<Func<T, bool>> filter)`**:
  - Lấy bản ghi đầu tiên thỏa mãn điều kiện LINQ (ví dụ: lấy danh mục theo ID hoặc điều kiện khác).
  - Sử dụng `Expression<Func<T, bool>>` để hỗ trợ các biểu thức LINQ linh hoạt, thay vì chỉ dựa vào `Find` (chỉ hoạt động với ID).
- **`Add(T entity)`**:
  - Thêm một bản ghi mới vào cơ sở dữ liệu.
- **`Remove(T entity)`**:
  - Xóa một bản ghi khỏi cơ sở dữ liệu.
- **`RemoveRange(IEnumerable<T> entities)`**:
  - Xóa nhiều bản ghi cùng lúc.

#### Lưu ý về `Update` và `SaveChanges`
- Không bao gồm phương thức `Update` trong giao diện generic vì logic cập nhật có thể khác nhau giữa các mô hình (ví dụ: cập nhật `Category` khác với cập nhật `Product`).
- `SaveChanges` cũng được giữ ngoài giao diện generic để triển khai cụ thể trong repository của từng mô hình (như `CategoryRepository`).
- Điều này đảm bảo tính linh hoạt khi xử lý các logic phức tạp trong phương thức `Update`.

### Cấu trúc thư mục
- Dự án `Bulky.DataAccess`:
  - Thư mục `Repository`:
    - `IRepository.cs`: Giao diện generic cho các thao tác CRUD.
  - Thư mục `Data`: Chứa `ApplicationDbContext`.
  - Thư mục `Migrations`: Chứa các file migration.

### Kế hoạch tiếp theo
- Trong các video tiếp theo, triển khai lớp repository cụ thể (như `CategoryRepository`) kế thừa từ `IRepository<T>` để thực hiện các thao tác CRUD cho `Category`.
- Thêm phương thức `Update` và gọi `SaveChanges` trong các repository cụ thể để hoàn thiện chức năng.

### Ghi chú thêm
- **Generic Repository** giúp giảm lặp mã khi làm việc với nhiều mô hình (như `Category`, `Product`, `OrderHeader`, `OrderDetail`).
- **Expression<Func<T, bool>>** cho phép sử dụng LINQ linh hoạt, hỗ trợ các điều kiện phức tạp hơn so với phương thức `Find`.
- Nếu bạn mới làm quen với Repository Pattern, có thể cảm thấy phức tạp ban đầu. Tuy nhiên, khi triển khai cụ thể (như `CategoryRepository`), các khái niệm sẽ trở nên rõ ràng hơn.
- Đảm bảo `Bulky.DataAccess` có tham chiếu đến `Bulky.Models` để truy cập các lớp mô hình như `Category`.