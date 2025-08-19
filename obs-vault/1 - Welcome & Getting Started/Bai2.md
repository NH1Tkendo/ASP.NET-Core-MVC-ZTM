## Ứng Dụng Thương Mại Điện Tử Với ASP.NET Core MVC

### Tổng Quan Ứng Dụng
- **Mục tiêu**: Xây dựng một ứng dụng thương mại điện tử (e-commerce) hoàn chỉnh sử dụng ASP.NET Core MVC.
- **Tính năng chính**:
  - Hiển thị sản phẩm, giỏ hàng, đặt hàng, thanh toán, và quản lý đơn hàng.
  - Phân quyền cho các loại người dùng: Khách hàng (Customer), Công ty (Company), Nhân viên (Employee), và Quản trị viên (Admin).
  - Tích hợp thanh toán qua Stripe và xác thực qua Facebook.
- **Môi trường phát triển**: Sử dụng các công cụ và tính năng như bảng dữ liệu (data table), trình soạn thảo văn bản phong phú (rich text editor), và thông báo (toaster notification).

### Tính Năng Dành Cho Khách Hàng
#### Trang Chủ
- **Hiển thị sản phẩm**:
  - Sản phẩm được hiển thị với hình ảnh, giá cả, và chi tiết.
  - Giá giảm (discounted price) dựa trên số lượng đặt hàng (ví dụ: 51 sản phẩm có giá 85).
- **Giỏ hàng**:
  - Hiển thị số lượng sản phẩm (mặc định: 0 nếu chưa đăng nhập).
  - Cho phép thêm sản phẩm vào giỏ hàng, chỉnh sửa số lượng, hoặc xóa sản phẩm.
  - Tổng giá trị đơn hàng (grand total) được hiển thị.

#### Quy Trình Đặt Hàng
- **Đăng nhập/Đăng ký**:
  - Yêu cầu đăng nhập để thêm sản phẩm vào giỏ hàng.
  - Hỗ trợ đăng ký tài khoản mới hoặc đăng nhập qua Facebook.
  - Tích hợp xác thực (validation) trong quá trình đăng ký.
- **Thêm vào giỏ hàng**:
  - Sau khi chọn số lượng và thêm vào giỏ hàng, hiển thị thông báo (toaster notification) xác nhận.
- **Thanh toán**:
  - Chuyển hướng đến Stripe để thanh toán (sử dụng thẻ tín dụng giả 424242 cho chế độ kiểm thử).
  - Sau khi thanh toán thành công, hiển thị thông báo đặt hàng thành công kèm mã đơn hàng (order ID).
- **Quản lý đơn hàng**:
  - Người dùng có thể xem trạng thái đơn hàng (order status) như “Đã duyệt” (Approved).
  - Cung cấp thông tin vận chuyển (carrier, tracking information) khi đơn hàng được gửi.

### Tính Năng Dành Cho Quản Trị Viên (Admin)
#### Quản Lý Nội Dung (Content Management)
- **Quản lý danh mục (Category)**:
  - Tạo, cập nhật, hoặc xóa danh mục.
- **Quản lý sản phẩm (Product)**:
  - **CRUD (Create, Read, Update, Delete)**:
    - Tạo, cập nhật, xóa sản phẩm với bảng dữ liệu (data table) hỗ trợ tìm kiếm, lọc, và sắp xếp.
    - Sử dụng trình soạn thảo văn bản phong phú (rich text editor) để chỉnh sửa mô tả sản phẩm.
    - Hỗ trợ tải lên hình ảnh mới hoặc thay thế hình ảnh hiện tại.
    - Xác nhận xóa sản phẩm bằng thông báo (sweet alert notification).
  - **Thông tin sản phẩm**:
    - Giá niêm yết (list price) và giá giảm dựa trên số lượng (1-50, >50, >100).
    - Gán danh mục cho sản phẩm qua danh sách thả xuống (dropdown).

#### Quản Lý Người Dùng
- **Loại tài khoản**:
  - Admin, Employee, Company, Customer.
  - Admin có thể tạo tài khoản và gán vai trò (role).
- **Tài khoản công ty (Company Account)**:
  - Cho phép đặt hàng mà không cần thanh toán ngay (thanh toán sau 30 ngày).
  - Khi tạo tài khoản công ty, admin chọn công ty từ danh sách thả xuống.

#### Quản Lý Đơn Hàng
- **Xem và xử lý đơn hàng**:
  - Hiển thị tất cả đơn hàng với trạng thái (Approved, Processing, Shipped).
  - Cập nhật trạng thái đơn hàng: Bắt đầu xử lý (Start Processing), Gửi hàng (Ship Order).
  - Nhập thông tin vận chuyển (carrier, tracking information) khi gửi hàng.
  - Đơn hàng đã gửi không thể hủy.
- **Thanh toán cho tài khoản công ty**:
  - Hiển thị nút “Thanh toán ngay” (Pay Now) sau khi đơn hàng được gửi.
  - Công ty hoặc admin có thể thanh toán qua Stripe.
  - Nếu thanh toán chưa được thực hiện, có thể thực hiện sau.

### Tính Năng Dành Cho Tài Khoản Công Ty
- **Đặt hàng**:
  - Thông tin giao hàng tự động điền dựa trên thông tin đăng ký.
  - Đặt hàng không cần thanh toán ngay, hiển thị trang xác nhận với mã đơn hàng.
- **Quản lý đơn hàng**:
  - Xem và chỉnh sửa đơn hàng.
  - Thanh toán đơn hàng qua Stripe khi cần.

### Quy Trình Phát Triển
- **Tích hợp Stripe**:
  - Sử dụng chế độ kiểm thử (test mode) với thẻ tín dụng giả (424242).
  - Chuyển hướng đến Stripe để thanh toán và quay lại website sau khi hoàn tất.
- **Thông báo**:
  - Sử dụng toaster notification để thông báo hành động thành công (thêm vào giỏ hàng, đặt hàng).
  - Sử dụng sweet alert để xác nhận xóa sản phẩm.
- **Bảng dữ liệu (Data Table)**:
  - Hỗ trợ tìm kiếm, lọc, và sắp xếp sản phẩm.
- **Xác thực qua Facebook**:
  - Đăng nhập nhanh bằng tài khoản Facebook.

### Ghi Chú Thêm
- **Độ dài khóa học**:
  - Khóa học dài, bao gồm nhiều chủ đề về .NET Core và ASP.NET Core MVC.
  - Tập trung vào thực hành và xây dựng ứng dụng thực tế.
- **Lợi ích**:
  - Học viên sẽ tự tin với ứng dụng thương mại điện tử hoàn chỉnh.
  - Hiểu cách triển khai các tính năng phức tạp và xử lý lỗi thực tế.
- **Khuyến nghị**:
  - Không nên lo lắng về độ phức tạp, vì khóa học cung cấp hướng dẫn chi tiết từng bước.
  - Thực hành thường xuyên để nắm vững các khái niệm và quy trình.

---

**Lưu ý**: Ghi chú này được tối ưu hóa cho Obsidian với định dạng Markdown, đảm bảo cấu trúc rõ ràng, dễ tra cứu, và phù hợp để ôn tập khóa học.