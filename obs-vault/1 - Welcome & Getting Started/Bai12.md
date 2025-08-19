
## Kiến trúc MVC trong Ứng dụng Web

### Giới thiệu về MVC
- MVC là viết tắt của **Model–View–Controller (Mô hình–Giao diện–Bộ điều khiển)**, một mẫu thiết kế kiến trúc phổ biến trong phát triển ứng dụng web.
- Cấu trúc MVC chia ứng dụng thành ba thành phần chính: **Model**, **View**, và **Controller**, hoạt động phối hợp để xử lý yêu cầu người dùng và hiển thị giao diện.

### Thành phần của MVC
#### 1. Model (Mô hình)
- **Định nghĩa**: Đại diện cho cấu trúc và dữ liệu của ứng dụng.
- **Chức năng**:
  - Lưu trữ và quản lý dữ liệu (ví dụ: bảng cơ sở dữ liệu, lớp dữ liệu).
  - Ví dụ: Trong ứng dụng thương mại điện tử, Model bao gồm các lớp như **sản phẩm (Product)**, **đơn hàng (Order)**, **chi tiết đơn hàng (Order Details)**, **giỏ hàng (Shopping Cart)**.
- **Vai trò**: Đảm bảo dữ liệu được tổ chức và sẵn sàng để xử lý.

#### 2. View (Giao diện)
- **Định nghĩa**: Đại diện cho giao diện người dùng, hiển thị dữ liệu trên trình duyệt.
- **Chức năng**:
  - Chứa các thành phần HTML để hiển thị dữ liệu (ví dụ: biểu mẫu, bảng, biểu đồ).
  - Kết hợp dữ liệu từ Model để tạo giao diện trực quan.
- **Ví dụ**: Một bảng hiển thị danh sách sản phẩm hoặc biểu đồ doanh thu được định nghĩa trong View.

#### 3. Controller (Bộ điều khiển)
- **Định nghĩa**: Trung tâm xử lý của ứng dụng, đóng vai trò cầu nối giữa Model và View.
- **Chức năng**:
  - Xử lý yêu cầu từ người dùng (ví dụ: nhấp nút, truy cập trang web).
  - Lấy dữ liệu từ Model, xử lý (nếu cần), và truyền đến View.
  - Trả về kết quả cuối cùng (giao diện hoàn chỉnh) cho người dùng.
- **Ví dụ**: Khi người dùng truy cập trang web, Controller quyết định Model nào cần truy xuất, lấy dữ liệu và gửi đến View để hiển thị.

### Luồng hoạt động của MVC
1. **Yêu cầu từ người dùng**:
   - Người dùng truy cập trang web hoặc nhấp vào một nút.
   - Yêu cầu được gửi đến **Controller**.
2. **Xử lý trong Controller**:
   - Controller xác định dữ liệu cần lấy từ **Model**.
   - Thực hiện các xử lý cần thiết (ví dụ: chuyển đổi dữ liệu).
3. **Truyền dữ liệu đến View**:
   - Controller gửi dữ liệu đã xử lý đến **View**.
   - View kết hợp dữ liệu với định dạng HTML (ví dụ: hiển thị dữ liệu trong bảng).
4. **Hiển thị kết quả**:
   - View trả kết quả HTML về Controller.
   - Controller gửi giao diện hoàn chỉnh đến trình duyệt người dùng.

### Ví dụ minh họa
- Trong một ứng dụng web:
  - **Model**: Lớp `Product` chứa thông tin sản phẩm (tên, giá, số lượng).
  - **Controller**: Xử lý yêu cầu hiển thị danh sách sản phẩm, lấy dữ liệu từ `Product` và gửi đến View.
  - **View**: Hiển thị danh sách sản phẩm trong một bảng HTML.

### Ghi chú thêm
- **Controller là trung tâm của MVC**: Điều phối toàn bộ luồng dữ liệu và tương tác.
- **Action Methods (Phương thức hành động)**:
  - Là các điểm cuối (endpoints) trong Controller, xác định hành động cụ thể (ví dụ: hiển thị trang chủ).
  - Ví dụ: Trong tuyến mặc định (`default route`), nếu không chỉ định gì, ứng dụng sẽ gọi `HomeController` và phương thức `Index`.
- **Routing (Định tuyến)**: Quy định cách yêu cầu người dùng được ánh xạ đến Controller và Action Method (sẽ được trình bày chi tiết trong bài tiếp theo).

### Mã nguồn minh họa
```csharp
// Ví dụ tuyến mặc định trong Program.cs
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
```
- **Giải thích**:
  - Nếu không chỉ định, yêu cầu sẽ được chuyển đến `HomeController` và phương thức `Index`.
  - `id?` là tham số tùy chọn.

### Tổng kết
- MVC giúp tổ chức mã nguồn rõ ràng, dễ bảo trì:
  - **Model**: Quản lý dữ liệu.
  - **View**: Hiển thị giao diện.
  - **Controller**: Điều phối yêu cầu và dữ liệu.
- Hiểu rõ luồng hoạt động của MVC là nền tảng để phát triển ứng dụng web hiệu quả.