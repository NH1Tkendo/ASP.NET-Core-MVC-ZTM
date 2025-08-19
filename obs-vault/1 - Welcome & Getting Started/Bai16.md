## Tiêm phụ thuộc (Dependency Injection) trong lập trình .NET

### Khái niệm về Tiêm phụ thuộc (Dependency Injection)
- **Định nghĩa**:
  - Tiêm phụ thuộc (Dependency Injection - DI) là một mẫu thiết kế (design pattern) trong đó một lớp hoặc đối tượng nhận các phụ thuộc (dependent classes) từ bên ngoài thay vì tự tạo chúng.
  - Mục tiêu:
    - Loại bỏ việc tạo, quản lý và hủy đối tượng bên trong lớp.
    - Tăng tính liên kết lỏng (loose coupling) giữa các lớp.
- **Lợi ích**:
  - Giảm mã lặp (duplicate code).
  - Dễ dàng thay đổi hoặc nâng cấp triển khai (implementation) mà không cần sửa đổi nhiều nơi.
  - Tăng tính bảo trì và khả năng mở rộng của ứng dụng.

### Ví dụ thực tế về Tiêm phụ thuộc
- **Tình huống thực tế**:
  - Bob đi leo núi và cần các vật dụng như bản đồ, đèn pin, thanh protein.
  - Thay vì tự chuẩn bị từng món, Bob đặt tất cả vào một chiếc ba lô (container).
  - Khi cần, Bob chỉ cần lấy vật dụng từ ba lô mà không cần lo lắng về việc chuẩn bị hay quản lý chúng.
- **Áp dụng vào lập trình**:
  - Ba lô tương đương với **thùng chứa tiêm phụ thuộc (DI container)**.
  - Các vật dụng (bản đồ, đèn pin,...) tương đương với các dịch vụ (services) như gửi email hoặc truy cập cơ sở dữ liệu.
  - Thay vì tạo đối tượng trực tiếp trong mã, ứng dụng yêu cầu DI container cung cấp các dịch vụ cần thiết.

### Tình huống không sử dụng Tiêm phụ thuộc
- **Ví dụ**:
  - Ứng dụng có 3 trang, mỗi trang cần gửi email và truy cập cơ sở dữ liệu.
  - Cách tiếp cận truyền thống:
    - Trong mỗi trang, tạo đối tượng `Db` (truy cập cơ sở dữ liệu) và `Email` (gửi email).
    - Gọi phương thức từ các đối tượng này và hủy chúng sau khi sử dụng.
- **Vấn đề**:
  - **Mã lặp**: Logic tạo, sử dụng và hủy đối tượng được lặp lại ở mỗi trang.
  - **Khó bảo trì**: Nếu cần thay đổi lớp `Db` hoặc `Email` (ví dụ: chuyển sang `Db_new` hoặc `Email_new`), phải sửa mã ở tất cả các trang.
  - **Tốn thời gian**: Với ứng dụng lớn (30 hoặc 300 trang), việc thay đổi trở nên rất phức tạp.
  - **Kết nối chặt (tight coupling)**: Các trang phụ thuộc trực tiếp vào triển khai cụ thể của `Db` và `Email`.

### Tình huống sử dụng Tiêm phụ thuộc
- **Cách tiếp cận**:
  - Sử dụng một **thùng chứa tiêm phụ thuộc (DI container)** để quản lý các dịch vụ.
  - Đăng ký các dịch vụ (services) trong thùng chứa:
    - Định nghĩa giao diện (`IEmail`, `IDb`) và triển khai cụ thể (`Email`, `Db`).
    - Đăng ký ánh xạ: `IEmail -> Email`, `IDb -> Db`.
  - Khi một trang cần dịch vụ, nó yêu cầu giao diện (`IEmail`, `IDb`) thay vì tạo đối tượng cụ thể.
  - DI container tự động tạo và cung cấp đối tượng triển khai.
- **Lợi ích**:
  - **Mã sạch hơn**: Các trang chỉ sử dụng giao diện, không cần biết chi tiết triển khai.
  - **Dễ dàng nâng cấp**: Nếu cần thay đổi triển khai (ví dụ: từ `Db` sang `Db_new`), chỉ cần cập nhật đăng ký trong DI container.
  - **Tính liên kết lỏng**: Các trang không phụ thuộc trực tiếp vào triển khai cụ thể, giảm sự phụ thuộc giữa các lớp.

### Ví dụ mã nguồn
#### Không sử dụng DI
```csharp
public class Page1
{
    public void GetDataAndSendEmail()
    {
        var db = new Db(); // Tạo đối tượng Db
        db.GetData();
        db.Dispose();

        var email = new Email(); // Tạo đối tượng Email
        email.Send();
        email.Dispose();
    }
}
```

#### Sử dụng DI

![[Dependency_Injection.png]]


```csharp
public interface IDb
{
    void GetData();
}

public interface IEmail
{
    void Send();
}

public class Db : IDb
{
    public void GetData() { /* Triển khai */ }
}

public class Email : IEmail
{
    public void Send() { /* Triển khai */ }
}

public class Page1
{
    private readonly IDb _db;
    private readonly IEmail _email;

    // Tiêm phụ thuộc qua constructor
    public Page1(IDb db, IEmail email)
    {
        _db = db;
        _email = email;
    }

    public void GetDataAndSendEmail()
    {
        _db.GetData();
        _email.Send();
    }
}
```

#### Đăng ký dịch vụ trong DI Container (Program.cs)
```csharp
builder.Services.AddScoped<IDb, Db>();
builder.Services.AddScoped<IEmail, Email>();
```

#### Thay đổi triển khai
- Nếu cần thay đổi sang `Db_new` hoặc `Email_new`:
```csharp
builder.Services.AddScoped<IDb, Db_new>();
builder.Services.AddScoped<IEmail, Email_new>();
```
- Các trang sử dụng `IDb` và `IEmail` sẽ tự động nhận triển khai mới mà không cần sửa mã.

### Tích hợp Tiêm phụ thuộc trong .NET
- **Tích hợp sẵn**:
  - .NET Framework cung cấp thùng chứa DI tích hợp (Microsoft.Extensions.DependencyInjection).
  - Chỉ cần đăng ký dịch vụ trong `Program.cs`, .NET sẽ tự động quản lý vòng đời và cung cấp các đối tượng.
- **Các loại vòng đời dịch vụ**:
  - **Transient**: Tạo mới mỗi lần yêu cầu.
  - **Scoped**: Tạo một lần cho mỗi yêu cầu HTTP.
  - **Singleton**: Tạo một lần cho toàn bộ ứng dụng.
- **Ví dụ đăng ký dịch vụ**:
  ```csharp
  builder.Services.AddScoped<IDb, Db>();
  builder.Services.AddSingleton<IEmail, Email>();
  ```

### Ghi chú thêm
- **Lợi ích chính**:
  - Giảm mã lặp, tăng tính bảo trì và khả năng mở rộng.
  - Tăng tính linh hoạt khi thay đổi triển khai.
  - Thúc đẩy thiết kế theo nguyên tắc liên kết lỏng (loose coupling).
- **Ứng dụng thực tế**:
  - DI thường được sử dụng trong các ứng dụng MVC để cung cấp các dịch vụ như truy cập cơ sở dữ liệu, gửi email, hoặc ghi log.
- **Học tập dần tiến**:
  - Hiểu DI là bước quan trọng để thiết kế các ứng dụng .NET hiện đại, đặc biệt trong ASP.NET Core.

### Tổng kết
- Tiêm phụ thuộc (Dependency Injection) là một mẫu thiết kế giúp quản lý các phụ thuộc của lớp thông qua thùng chứa (DI container).
- Giảm mã lặp, tăng tính linh hoạt và bảo trì bằng cách tách biệt triển khai khỏi giao diện.
- Trong .NET, DI được tích hợp sẵn, chỉ cần đăng ký dịch vụ và để framework xử lý việc tạo và cung cấp đối tượng.
- Ví dụ thực tế (ba lô của Bob) giúp hình dung cách DI hoạt động trong lập trình, giúp mã trở nên gọn gàng và dễ bảo trì.