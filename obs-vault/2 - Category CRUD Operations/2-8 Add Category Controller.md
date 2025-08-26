
## Quản lý CRUD cho Danh mục (Category) với Entity Framework Core

### Tổng quan
- Bài học hướng dẫn cách thực hiện các thao tác **CRUD** (Create, Read, Update, Delete) trên bảng **Categories** sử dụng **Entity Framework Core**.
- Mục tiêu: Tạo **CategoryController** để quản lý danh mục, bắt đầu bằng việc thiết lập controller và view cơ bản để hiển thị danh sách danh mục.
- Nội dung chính:
  - Tạo **CategoryController** và hành động mặc định (`Index`).
  - Thiết lập view tương ứng để hiển thị danh sách danh mục.
  - Xử lý lỗi liên quan đến view không tìm thấy.

### Khái niệm cơ bản
- **Controller**: Lớp xử lý các yêu cầu HTTP, điều hướng logic nghiệp vụ và trả về view hoặc dữ liệu.
  - Quy ước đặt tên: Tên controller phải kết thúc bằng từ `Controller` (ví dụ: `CategoryController`).
- **Action Method (Phương thức hành động)**: Phương thức trong controller xử lý yêu cầu cụ thể, ví dụ: `Index` để hiển thị danh sách.
- **View**: Tệp giao diện người dùng (thường là `.cshtml`) hiển thị dữ liệu từ controller.
  - Quy ước: View phải nằm trong thư mục `/Views/<Tên_Controller>/` hoặc `/Views/Shared/`.
- **CRUD**:
  - **Create**: Tạo mới danh mục.
  - **Read**: Lấy danh sách hoặc chi tiết danh mục.
  - **Update**: Cập nhật thông tin danh mục.
  - **Delete**: Xóa danh mục.

### Quy trình thiết lập CategoryController

#### 1. Tạo Controller
- Trong thư mục `Controllers`, thêm controller mới:
  - **Loại**: MVC Controller - Empty.
  - **Tên**: `CategoryController`.
- Mã mặc định được tạo:
```csharp
public class CategoryController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
```
- **Giải thích**:
  - `Index`: Phương thức hành động mặc định, trả về view `Index.cshtml`.
  - URL truy cập: `/Category/Index`.

#### 2. Kiểm tra hành vi Controller
- Thêm điểm dừng (breakpoint) trong phương thức `Index` để kiểm tra luồng thực thi.
- Chạy ứng dụng và truy cập URL: `/Category/Index`.
- **Kết quả mong đợi**: Ứng dụng dừng tại breakpoint, xác nhận controller hoạt động.
- **Lỗi gặp phải**: 
  - Thông báo: `The view 'Index' was not found`.
  - Nguyên nhân: View `Index.cshtml` chưa được tạo trong thư mục `/Views/Category/`.

#### 3. Tạo View
- Tạo thư mục `/Views/Category/` (tên phải khớp với tên controller).
- Thêm view mới:
  - **Loại**: Razor View - Empty.
  - **Tên**: `Index.cshtml`.
- Nội dung cơ bản của `Index.cshtml`:
```html
<h1>Danh sách Danh mục</h1>
```
- **Giải thích**:
  - View hiển thị tiêu đề "Danh sách Danh mục".
  - EF Core sẽ được sử dụng sau để lấy dữ liệu danh mục từ cơ sở dữ liệu và hiển thị trong view này.

#### 4. Kiểm tra lại ứng dụng
- Chạy lại ứng dụng, truy cập `/Category/Index`.
- **Kết quả**: View hiển thị thành công với nội dung `<h1>Danh sách Danh mục</h1>`.

### Xử lý lỗi
- **Lỗi view không tìm thấy**:
  - EF Core tìm kiếm view theo thứ tự:
    1. `/Views/<Tên_Controller>/<Tên_Action>.cshtml` (ví dụ: `/Views/Category/Index.cshtml`).
    2. `/Views/Shared/<Tên_Action>.cshtml`.
  - Nếu không tìm thấy, lỗi `The view 'Index' was not found` sẽ xuất hiện.
- **Khắc phục**: Đảm bảo tạo đúng thư mục và view theo quy ước.

### Tóm tắt quy trình
1. Tạo `CategoryController` với hành động `Index`.
2. Tạo thư mục `/Views/Category/` và thêm tệp `Index.cshtml`.
3. Kiểm tra ứng dụng bằng cách truy cập URL `/Category/Index`.
4. Chuẩn bị tích hợp EF Core để thực hiện các thao tác CRUD trong các bước tiếp theo.

### Ghi chú thêm
- **CategoryController** hiện chỉ hiển thị view tĩnh. Các bước tiếp theo sẽ tích hợp logic CRUD để:
  - Lấy danh sách danh mục từ cơ sở dữ liệu (`Read`).
  - Thêm mới danh mục (`Create`).
  - Cập nhật danh mục (`Update`).
  - Xóa danh mục (`Delete`).
- Quy ước đặt tên controller và view rất quan trọng để tránh lỗi không tìm thấy tài nguyên.

### Tài liệu tham khảo
- Nội dung bài học sẽ tiếp tục với việc tích hợp EF Core vào `CategoryController` để thực hiện các thao tác CRUD.