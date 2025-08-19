## Định tuyến và cấu trúc MVC trong ứng dụng ASP.NET Core

### Cấu trúc thư mục trong dự án MVC
- Dự án MVC có ba thư mục chính:
  - **Thư mục Controllers**: Chứa các bộ điều khiển (controllers).
  - **Thư mục Models**: Chứa các mô hình (models) định nghĩa dữ liệu.
  - **Thư mục Views**: Chứa các tệp giao diện (views) để hiển thị nội dung.
- **Quy tắc đặt tên bộ điều khiển**:
  - Tên bộ điều khiển phải kết thúc bằng từ khóa `Controller` (ví dụ: `HomeController`).
  - Các bộ điều khiển phải được đặt trong thư mục `Controllers`, nếu không ứng dụng sẽ không nhận diện được.
- **Quy tắc đặt tên thư mục giao diện (Views)**:
  - Mỗi bộ điều khiển có một thư mục con tương ứng trong thư mục `Views`, với tên trùng với tên bộ điều khiển (không bao gồm từ `Controller`).
  - Ví dụ: Bộ điều khiển `HomeController` sẽ có thư mục `Views/Home` chứa các tệp giao diện như `Index.cshtml` và `Privacy.cshtml`.

### Cách hoạt động của định tuyến (Routing)
- **Định tuyến mặc định**:
  - Mẫu định tuyến mặc định: `{controller}/{action}/{id?}`.
  - Nếu không chỉ định `controller` hoặc `action` trong URL, ứng dụng sẽ sử dụng:
    - Bộ điều khiển mặc định: `HomeController`.
    - Hành động mặc định: `Index`.
  - Ví dụ: URL `localhost/` sẽ gọi `HomeController` và hành động `Index`.
- **Phân tích URL**:
  - URL: `localhost/home/privacy`
    - **Bộ điều khiển (Controller)**: `Home`
    - **Hành động (Action)**: `Privacy`
    - **ID**: `null` (vì không có tham số ID).
  - URL: `localhost/home/index`
    - **Bộ điều khiển (Controller)**: `Home`
    - **Hành động (Action)**: `Index`
    - **ID**: `null`.

### Mối quan hệ giữa Controller, Action và View
- **Bộ điều khiển (Controller)**:
  - Chứa các phương thức hành động (action methods) trả về kiểu `IActionResult`.
  - Ví dụ: Trong `HomeController`, các phương thức `Index()` và `Privacy()` là các hành động.
- **Phương thức hành động (Action Method)**:
  - Mỗi hành động trả về một giao diện (view) tương ứng.
  - Nếu không chỉ định tên giao diện trong phương thức (ví dụ: `return View();`), hệ thống sẽ tìm tệp giao diện trong thư mục `Views/[ControllerName]` với tên trùng với hành động.
  - Ví dụ:
    - `Index()` trả về `Views/Home/Index.cshtml`.
    - `Privacy()` trả về `Views/Home/Privacy.cshtml`.
- **Tùy chỉnh giao diện trả về**:
  - Có thể chỉ định giao diện cụ thể bằng cách truyền tên giao diện vào `return View("TênGiaoDiện")`.
  - Ví dụ: Trong phương thức `Index()`, nếu gọi `return View("Privacy")`, hệ thống sẽ trả về `Views/Home/Privacy.cshtml` thay vì `Index.cshtml`.

### Ví dụ mã nguồn
#### HomeController.cs
```csharp
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View(); // Trả về Views/Home/Index.cshtml
    }

    public IActionResult Privacy()
    {
        return View(); // Trả về Views/Home/Privacy.cshtml
    }
}
```

#### Tùy chỉnh giao diện
```csharp
public IActionResult Index()
{
    return View("Privacy"); // Trả về Views/Home/Privacy.cshtml thay vì Index.cshtml
}
```

### Gỡ lỗi (Debugging) trong Visual Studio
- **Thêm điểm dừng (Breakpoint)**:
  - Nhấp vào lề trái của dòng mã trong Visual Studio để đặt điểm dừng.
  - Khi chạy ứng dụng, chương trình sẽ tạm dừng tại điểm dừng, cho phép kiểm tra luồng thực thi.
- **Ví dụ**:
  - Đặt điểm dừng tại phương thức `Index()` trong `HomeController`.
  - Truy cập URL `localhost/home/index`, ứng dụng sẽ dừng tại dòng `return View();`, chứng minh rằng hành động `Index` được gọi.

### Ghi chú thêm
- **Mô hình (Model) không bắt buộc**:
  - Một số hành động chỉ trả về giao diện tĩnh (static view) mà không cần mô hình.
  - Ví dụ: `ErrorViewModel` trong thư mục `Models` không bắt buộc phải sử dụng trong mọi trường hợp.
- **Tầm quan trọng của định tuyến**:
  - Giúp hệ thống ánh xạ chính xác URL tới bộ điều khiển và hành động tương ứng.
  - Hiểu rõ định tuyến giúp dự đoán và kiểm soát luồng xử lý yêu cầu trong ứng dụng.
- **Tùy chỉnh định tuyến**:
  - Mẫu định tuyến mặc định có thể được thay đổi trong tệp `Program.cs`.
  - Ví dụ: Đặt hành động mặc định là `Privacy` thay vì `Index`:
    ```csharp
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Privacy}/{id?}");
    ```
- MVC là một mô hình phức tạp, nhưng việc hiểu 50-70% nội dung ở giai đoạn này là tiến bộ đáng kể. Thực hành trong các phần tiếp theo sẽ giúp làm rõ hơn.

### Tổng kết
- Định tuyến trong MVC ánh xạ URL tới bộ điều khiển, hành động và tham số tùy chọn.
- Cấu trúc thư mục và quy tắc đặt tên trong MVC đảm bảo tính tổ chức và dễ bảo trì.
- Các phương thức hành động trong bộ điều khiển quyết định giao diện nào sẽ được trả về, với khả năng tùy chỉnh linh hoạt.