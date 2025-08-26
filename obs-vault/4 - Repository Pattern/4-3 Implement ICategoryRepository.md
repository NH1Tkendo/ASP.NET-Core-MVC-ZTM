## Ghi chú học tập: Tạo và triển khai ICategoryRepository

### Mục đích
- Hiểu cách tạo và triển khai giao diện `ICategoryRepository` kế thừa từ `IRepository` để quản lý các thao tác liên quan đến mô hình `Category` trong hệ thống.

### Khái niệm
- **Giao diện (Interface)**: Định nghĩa các phương thức mà một lớp phải triển khai. Trong trường hợp này, `ICategoryRepository` sẽ kế thừa từ `IRepository` để tái sử dụng các chức năng cơ bản và bổ sung các phương thức cụ thể cho `Category`.
- **Mô hình (Model)**: `Category` là đối tượng chính được sử dụng trong giao diện này.
- **Kế thừa giao diện**: `ICategoryRepository` sẽ mở rộng `IRepository` với tham số là `Category` để đảm bảo các phương thức cơ bản được áp dụng cho `Category`.

### Cách thực hiện
- **Tạo giao diện `ICategoryRepository`**:
  - Tạo một giao diện công khai (`public interface`) có tên `ICategoryRepository`.
  - Giao diện này kế thừa từ `IRepository<Category>`, đảm bảo tất cả các phương thức cơ bản từ `IRepository` được áp dụng cho mô hình `Category`.
  - Bổ sung hai phương thức đặc thù:
    - `Update`: Cập nhật thông tin của một đối tượng `Category`.
    - `Save`: Lưu các thay đổi vào cơ sở dữ liệu.
- **Cấu trúc giao diện**:
  - Các phương thức từ `IRepository` được kế thừa tự động.
  - Thêm các phương thức `Update` và `Save` để đáp ứng yêu cầu cụ thể của `Category`.

### Mã nguồn
```csharp
public interface ICategoryRepository : IRepository<Category>
{
    void Update(Category obj);
    void Save();
}
```

### Ghi chú thêm
- **Tính kế thừa**: Việc kế thừa từ `IRepository<Category>` giúp tái sử dụng các phương thức chung (như `Add`, `Delete`, `Get`,...) mà không cần định nghĩa lại.
- **Triển khai tiếp theo**: Trong bước tiếp theo, cần tạo lớp `CategoryRepository` để triển khai giao diện `ICategoryRepository`, cung cấp chi tiết logic cho các phương thức đã định nghĩa.
- **Ứng dụng**: Giao diện này đảm bảo tính thống nhất trong việc quản lý các thao tác liên quan đến `Category`, đồng thời cho phép mở rộng dễ dàng khi cần thêm chức năng mới.