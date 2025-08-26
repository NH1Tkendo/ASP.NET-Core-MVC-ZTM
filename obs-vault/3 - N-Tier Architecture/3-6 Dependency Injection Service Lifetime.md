## Hiểu về Service Lifetimes trong Dependency Injection của .NET Core

### Mục tiêu
- Giải thích các loại vòng đời dịch vụ (service lifetimes) trong Dependency Injection (DI) của .NET Core: **Transient**, **Scoped**, và **Singleton**.
- Minh họa sự khác biệt giữa các vòng đời này thông qua ví dụ thực tế trong một ứng dụng MVC.

### Tổng quan về Service Lifetimes
- **Transient**:
  - Tạo một instance mới mỗi khi dịch vụ được yêu cầu.
  - An toàn và đơn giản, không tái sử dụng instance.
  - Ví dụ: Mỗi lần gọi dịch vụ, một đối tượng mới được tạo.
- **Scoped**:
  - Tạo một instance duy nhất cho mỗi HTTP request.
  - Trong cùng một request, tất cả các lần gọi dịch vụ sử dụng cùng một instance.
  - Phù hợp cho ứng dụng web, đặc biệt khi cần đồng bộ dữ liệu trong một request.
- **Singleton**:
  - Tạo một instance duy nhất cho toàn bộ vòng đời của ứng dụng.
  - Instance được tái sử dụng cho tất cả các request cho đến khi ứng dụng khởi động lại.
  - Cần cẩn thận khi sử dụng để tránh vấn đề về trạng thái (state).

### Ví dụ thực tế
#### 1. Tạo dự án MVC
- Tạo dự án MVC mới với tên `DI_Service_Lifetime` trong Visual Studio.
- Thêm thư mục `Services` để chứa các interface và triển khai.

#### 2. Tạo Interface
- Tạo ba interface tương ứng với ba vòng đời:
  - `IScopedGuidService`
  - `ISingletonGuidService`
  - `ITransientGuidService`
- Mỗi interface có một phương thức `GetGuid` trả về chuỗi GUID ngẫu nhiên.
```csharp
namespace DI_Service_Lifetime.Services;

public interface IScopedGuidService
{
    string GetGuid();
}

public interface ISingletonGuidService
{
    string GetGuid();
}

public interface ITransientGuidService
{
    string GetGuid();
}
```

#### 3. Tạo triển khai (Implementation)
- Tạo ba lớp triển khai tương ứng:
  - `ScopedGuidService`, `SingletonGuidService`, `TransientGuidService`.
- Mỗi lớp tạo một GUID ngẫu nhiên trong constructor và trả về qua phương thức `GetGuid`.
```csharp
namespace DI_Service_Lifetime.Services;

public class SingletonGuidService : ISingletonGuidService
{
    private readonly string _id;
    public SingletonGuidService()
    {
        _id = Guid.NewGuid().ToString();
    }
    public string GetGuid() => _id;
}

public class ScopedGuidService : IScopedGuidService
{
    private readonly string _id;
    public ScopedGuidService()
    {
        _id = Guid.NewGuid().ToString();
    }
    public string GetGuid() => _id;
}

public class TransientGuidService : ITransientGuidService
{
    private readonly string _id;
    public TransientGuidService()
    {
        _id = Guid.NewGuid().ToString();
    }
    public string GetGuid() => _id;
}
```

#### 4. Đăng ký dịch vụ trong `Program.cs`
- Đăng ký các dịch vụ với vòng đời tương ứng trong `Program.cs`.
```csharp
builder.Services.AddSingleton<ISingletonGuidService, SingletonGuidService>();
builder.Services.AddScoped<IScopedGuidService, ScopedGuidService>();
builder.Services.AddTransient<ITransientGuidService, TransientGuidService>();
```

#### 5. Sử dụng dịch vụ trong Controller
- Trong `HomeController`, inject các dịch vụ và kiểm tra GUID được trả về.
- Tạo sáu trường để lưu các instance của dịch vụ:
  - Hai instance cho `Transient`, hai cho `Scoped`, và hai cho `Singleton`.
- Hiển thị GUID của từng instance trong action `Index`.
```csharp
using DI_Service_Lifetime.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace DI_Service_Lifetime.Controllers;

public class HomeController : Controller
{
    private readonly IScopedGuidService _scoped1;
    private readonly IScopedGuidService _scoped2;
    private readonly ISingletonGuidService _singleton1;
    private readonly ISingletonGuidService _singleton2;
    private readonly ITransientGuidService _transient1;
    private readonly ITransientGuidService _transient2;

    public HomeController(
        IScopedGuidService scoped1, IScopedGuidService scoped2,
        ISingletonGuidService singleton1, ISingletonGuidService singleton2,
        ITransientGuidService transient1, ITransientGuidService transient2)
    {
        _scoped1 = scoped1;
        _scoped2 = scoped2;
        _singleton1 = singleton1;
        _singleton2 = singleton2;
        _transient1 = transient1;
        _transient2 = transient2;
    }

    public IActionResult Index()
    {
        StringBuilder messages = new();
        messages.AppendLine($"Transient 1: {_transient1.GetGuid()}");
        messages.AppendLine($"Transient 2: {_transient2.GetGuid()}");
        messages.AppendLine();
        messages.AppendLine($"Scoped 1: {_scoped1.GetGuid()}");
        messages.AppendLine($"Scoped 2: {_scoped2.GetGuid()}");
        messages.AppendLine();
        messages.AppendLine($"Singleton 1: {_singleton1.GetGuid()}");
        messages.AppendLine($"Singleton 2: {_singleton2.GetGuid()}");

        return Ok(messages.ToString());
    }
}
```

#### 6. Kết quả khi chạy ứng dụng
- **Transient**:
  - Mỗi lần gọi `GetGuid`, một GUID mới được tạo ( Transient 1 và Transient 2 luôn khác nhau).
- **Scoped**:
  - Trong cùng một HTTP request, cả hai instance (`Scoped1` và `Scoped2`) trả về cùng một GUID.
  - Khi làm mới trang (new request), GUID mới được tạo.
- **Singleton**:
  - Cả hai instance (`Singleton1` và `Singleton2`) trả về cùng một GUID trong toàn bộ vòng đời ứng dụng.
  - GUID không thay đổi khi làm mới trang.

### Phân tích kết quả
- **Transient**: Mỗi yêu cầu tạo một instance mới, phù hợp khi không cần duy trì trạng thái.
- **Scoped**: Một instance được sử dụng trong suốt một HTTP request, lý tưởng cho ứng dụng web để đảm bảo tính nhất quán trong request.
- **Singleton**: Một instance duy nhất được sử dụng cho toàn bộ ứng dụng, cần cẩn thận để tránh vấn đề về trạng thái (state) hoặc đồng bộ hóa.

### Ghi chú thêm
- **Scoped là lựa chọn phổ biến** cho ứng dụng web vì nó cân bằng giữa hiệu suất và tính an toàn.
- **Singleton** cần được sử dụng cẩn thận, đặc biệt khi dịch vụ lưu trữ trạng thái, để tránh xung đột dữ liệu.
- Ví dụ này minh họa rõ sự khác biệt giữa các vòng đời thông qua việc tạo GUID, giúp dễ dàng hình dung cách Dependency Injection quản lý instance của dịch vụ.