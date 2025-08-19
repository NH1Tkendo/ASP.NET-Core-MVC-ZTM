## Định tuyến trong ứng dụng MVC

### Khái niệm cơ bản về định tuyến (Routing)
- Định tuyến (Routing) xác định cách một URL được ánh xạ tới một yêu cầu cụ thể trong ứng dụng MVC.
- Sau phần tên miền (domain name) như `localhost`, `google.com`, hoặc `netmastery.com`, phần tiếp theo của URL được xem là mẫu định tuyến (routing pattern).
- Mẫu định tuyến tiêu chuẩn trong MVC bao gồm:
  - **Tên bộ điều khiển (Controller)**: Thành phần đầu tiên sau tên miền.
  - **Tên hành động (Action)**: Thành phần tiếp theo sau dấu gạch chéo (`/`).
  - **ID (tùy chọn)**: Tham số bổ sung (nếu có) sau tên hành động.

### Mẫu định tuyến mặc định
- Mẫu định tuyến mặc định trong dự án .NET: `{controller}/{action}/{id?}`
  - `controller`: Tên của bộ điều khiển.
  - `action`: Tên của phương thức hành động trong bộ điều khiển.
  - `id?`: Tham số tùy chọn (có thể có hoặc không).
- Nếu không chỉ định `action`, hành động mặc định là `Index`.
- Nếu không chỉ định `controller`, bộ điều khiển mặc định là `Home`.

### Ví dụ phân tích URL
Dựa trên mẫu định tuyến `{controller}/{action}/{id?}`, phân tích các URL sau:

1. **URL**: `localhost/category/index/3`
   - **Bộ điều khiển (Controller)**: `category`
   - **Hành động (Action)**: `index`
   - **ID**: `3`

2. **URL**: `localhost/category`
   - **Bộ điều khiển (Controller)**: `category`
   - **Hành động (Action)**: `index` (mặc định)
   - **ID**: `null`

3. **URL**: `localhost/category/edit/3`
   - **Bộ điều khiển (Controller)**: `category`
   - **Hành động (Action)**: `edit`
   - **ID**: `3`

4. **URL**: `localhost/product/details/3`
   - **Bộ điều khiển (Controller)**: `product`
   - **Hành động (Action)**: `details`
   - **ID**: `3`

### Ý nghĩa của định tuyến
- Giúp xác định chính xác bộ điều khiển (controller) và hành động (action) được gọi khi người dùng truy cập một URL.
- Mẫu định tuyến mặc định (`Home/Index`) được cấu hình trong tệp `Program.cs`. Có thể tùy chỉnh mẫu này nếu cần.

### Ghi chú thêm
- Định tuyến là nền tảng của kiến trúc MVC, giúp ánh xạ URL tới các phương thức hành động cụ thể trong bộ điều khiển.
- Hiểu rõ cấu trúc URL giúp dễ dàng phân tích và dự đoán cách ứng dụng xử lý yêu cầu.
- Các video tiếp theo sẽ hướng dẫn cách tùy chỉnh định tuyến trong ứng dụng.