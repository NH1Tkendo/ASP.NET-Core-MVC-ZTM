## Sử dụng Areas trong Dự án .NET để Phân chia Chức năng

### Mục tiêu
- Hiểu khái niệm **Areas** trong ASP.NET và cách sử dụng để phân chia các chức năng trong dự án.
- Tìm hiểu cách thêm Areas (Admin và Customer) vào dự án.
- Cập nhật định tuyến (routing), controllers, views và các tệp liên quan để hỗ trợ Areas.
- Xử lý các lỗi phát sinh khi triển khai Areas.

### 1. Khái niệm Areas
- **Areas**:
  - Là một tính năng trong ASP.NET cho phép chia dự án thành các khu vực logic riêng biệt (ví dụ: Admin và Customer).
  - Mỗi Area có thể chứa các thư mục riêng cho **Controllers**, **Models**, và **Views**, giúp tổ chức mã nguồn rõ ràng hơn.
- **Ứng dụng**:
  - Phân tách chức năng giữa giao diện người dùng (Customer) và giao diện quản trị (Admin).
  - Ví dụ: Admin Area quản lý danh mục (Category), Customer Area quản lý trang chủ (Home).

### 2. Thêm Areas vào dự án
- **Các bước thực hiện**:
  - Nhấp chuột phải vào dự án `BulkyBookWeb`, chọn `Add > New Scaffolded Item`.
  - Chọn `MVC Area` và đặt tên:
    - Thêm Area `Admin` (cho chức năng quản trị).
    - Thêm Area `Customer` (cho giao diện người dùng).
  - **Kết quả**:
    - Thư mục `Areas` được tạo trong dự án, chứa:
      - `Areas/Admin` (với các thư mục `Controllers`, `Models`, `Views`).
      - `Areas/Customer` (tương tự).
  - **Lưu ý**:
    - Xóa các thư mục `Models` và `Data` trong Areas nếu đã được chuyển sang dự án riêng (ví dụ: `BulkyBook.Models`, `BulkyBook.DataAccess`).

### 3. Cập nhật Controllers và Views
- **Di chuyển Controllers**:
  - Chuyển `CategoryController` vào `Areas/Admin/Controllers` (vì liên quan đến quản trị).
  - Chuyển `HomeController` vào `Areas/Customer/Controllers` (vì liên quan đến giao diện người dùng).
- **Cập nhật Namespace**:
  - Đảm bảo namespace được cập nhật theo đúng vị trí mới. Ví dụ:
    - `CategoryController`: `BulkyBookWeb.Areas.Admin.Controllers`.
    - `HomeController`: `BulkyBookWeb.Areas.Customer.Controllers`.
  - Nếu namespace không tự động cập nhật, chỉnh sửa thủ công.
- **Thêm Area Attribute**:
  - Thêm thuộc tính `[Area]` vào controller để chỉ định Area:
    ```csharp
    [Area("Admin")]
    public class CategoryController : Controller
    {
        // Code
    }

    [Area("Customer")]
    public class HomeController : Controller
    {
        // Code
    }
    ```
- **Di chuyển Views**:
  - Chuyển thư mục `Views/Category` vào `Areas/Admin/Views`.
  - Chuyển thư mục `Views/Home` vào `Areas/Customer/Views`.
  - Di chuyển các tệp `_ViewStart.cshtml` và `_ViewImports.cshtml` vào thư mục `Views` của mỗi Area:
    - `Areas/Admin/Views/_ViewImports.cshtml`.
    - `Areas/Customer/Views/_ViewImports.cshtml`.
  - **Mục đích**: Đảm bảo views sử dụng đúng namespace và mô hình (models).

### 4. Cập nhật Định tuyến (Routing)
- **Mục đích**: Cấu hình định tuyến để hỗ trợ Areas.
- **Các bước thực hiện**:
  - Mở tệp `Program.cs`.
  - Cập nhật định tuyến mặc định để bao gồm Area:
    ```csharp
    app.MapControllerRoute(
        name: "default",
        pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
    ```
  - **Giải thích**:
    - `area=Customer`: Đặt `Customer` làm Area mặc định.
    - `{controller=Home}/{action=Index}`: Định nghĩa controller và action mặc định.
    - `{id?}`: Tham số tùy chọn cho ID.
- **Lưu ý**:
  - Nếu định tuyến sai (ví dụ: sử dụng dấu `:` thay vì `=`), sẽ gây lỗi. Cần sửa thành `area=Customer`.

### 5. Xử lý lỗi View không tìm thấy
- **Vấn đề**:
  - Sau khi di chuyển controllers và views, ứng dụng có thể báo lỗi "View không tìm thấy" do không xác định được vị trí views mới.
- **Cách khắc phục**:
  - Đảm bảo views được di chuyển đúng vào thư mục `Areas/[AreaName]/Views`.
  - Sao chép `_ViewImports.cshtml` và `_ViewStart.cshtml` vào mỗi Area để định nghĩa namespace và layout.
  - Kiểm tra namespace trong `_ViewImports.cshtml` để đảm bảo tham chiếu đúng đến mô hình (ví dụ: `BulkyBook.Models`).

### 6. Cập nhật liên kết trong Views
- **Vấn đề**:
  - Các liên kết (links) trong views có thể trỏ sai Area nếu không chỉ định `asp-area`.
  - Ví dụ: Khi nhấp vào liên kết "Category" từ Customer Area, ứng dụng tìm `CategoryController` trong Customer Area thay vì Admin Area.
- **Cách khắc phục**:
  - Thêm thuộc tính `asp-area` vào các thẻ liên kết trong views:
    ```html
    <a asp-area="Admin" asp-controller="Category" asp-action="Index">Category</a>
    <a asp-area="Customer" asp-controller="Home" asp-action="Index">Home</a>
    ```
  - **Lưu ý**:
    - Nếu không chỉ định `asp-area`, ứng dụng sẽ tìm controller trong Area hiện tại của trang.
    - Trong cùng một Area (ví dụ: CRUD trong `Category` trong Admin Area), không cần chỉ định `asp-area` vì mặc định sẽ tìm trong cùng Area.

### 7. Kiểm tra hoạt động
- **Thử nghiệm**:
  - Chạy dự án và kiểm tra:
    - Trang chủ (Home) trong Customer Area: Hoạt động bình thường.
    - Danh mục (Category) trong Admin Area: Hoạt động bình thường (Index, Create, Edit, Delete).
- **Kết quả**:
  - Tất cả chức năng hoạt động đúng sau khi cấu hình Areas, di chuyển controllers/views, và cập nhật định tuyến.

### 8. Ghi chú thêm
- **Lợi ích của Areas**:
  - Tăng tính tổ chức cho dự án bằng cách phân chia rõ ràng giữa các chức năng (Admin vs. Customer).
  - Dễ dàng mở rộng khi thêm các Area mới (ví dụ: Seller Area, Support Area).
- **Lưu ý thực tế**:
  - Luôn kiểm tra namespace và định tuyến sau khi di chuyển controllers hoặc views.
  - Đảm bảo các liên kết trong views chỉ định đúng `asp-area` để tránh lỗi định tuyến.
  - Có thể bỏ qua `asp-area` trong các thao tác CRUD trong cùng Area để đơn giản hóa.
- **Đề xuất**:
  - Liên kết ghi chú này với các ghi chú về **ASP.NET Routing**, **MVC Architecture**, hoặc **Dependency Injection** trong Obsidian để tra cứu chéo.

---
