## Tổ chức dự án ASP.NET Core theo mô hình phân tách (Separation of Concerns)

### Mục tiêu
- Tách biệt các thành phần của dự án MVC (`BulkyWeb`) thành các dự án riêng lẻ để tăng tính module hóa, dễ bảo trì và mở rộng trong ứng dụng thực tế.
- Loại bỏ dự án Razor Pages (`BulkyWebRazor_Temp`) vì chỉ phục vụ mục đích học tập.
- Tạo các dự án thư viện lớp (Class Library) để quản lý các thành phần như dữ liệu, mô hình và tiện ích.

### Xóa dự án Razor Pages
- Dự án `BulkyWebRazor_Temp` được tạo để tìm hiểu Razor Pages, không sử dụng trong dự án chính.
- Thao tác: Xóa dự án khỏi Solution trong Visual Studio để tập trung vào dự án MVC (`BulkyWeb`).

### Tạo các dự án Class Library
- Tạo ba dự án Class Library mới trong Solution để phân tách logic:
  1. **Bulky.DataAccess**: Chứa logic liên quan đến truy cập cơ sở dữ liệu (ví dụ: DbContext, migrations).
  2. **Bulky.Models**: Chứa các mô hình dữ liệu (Models) của ứng dụng.
  3. **Bulky.Utility**: Chứa các tiện ích (Utilities) như chức năng gửi email, hằng số (constants), hoặc các hàm hỗ trợ khác.

#### Thao tác tạo dự án
1. Trong Visual Studio, thêm dự án mới:
   - Chọn template **Class Library (.NET Core)**.
   - Đặt tên lần lượt: `Bulky.DataAccess`, `Bulky.Models`, `Bulky.Utility`.
   - Khung .NET: Sử dụng cùng phiên bản .NET của dự án chính (`BulkyWeb`).
2. Xóa các file mặc định (như `Class1.cs`) trong các dự án mới để chuẩn bị thêm các file cần thiết sau này.

### Cấu trúc Solution sau khi tạo
- **BulkyWeb**: Dự án MVC chính, chứa giao diện (Views), Controllers, và logic giao tiếp với người dùng.
- **Bulky.DataAccess**: Chứa DbContext và các migration để quản lý cơ sở dữ liệu.
- **Bulky.Models**: Chứa các lớp mô hình (Models) như `Category`, `Product`, v.v.
- **Bulky.Utility**: Chứa các tiện ích như gửi email, hằng số, hoặc các hàm hỗ trợ khác.

### Kế hoạch tiếp theo
- Chuyển logic từ dự án `BulkyWeb` sang các dự án Class Library tương ứng:
  - Chuyển `DbContext` và migrations sang `Bulky.DataAccess`.
  - Chuyển các lớp mô hình (Models) sang `Bulky.Models`.
  - Thêm các tiện ích (như gửi email, hằng số) vào `Bulky.Utility`.
- Cập nhật tham chiếu (references) giữa các dự án để đảm bảo `BulkyWeb` sử dụng được các thư viện mới.

### Lợi ích của việc phân tách
- **Tính module hóa**: Mỗi dự án chịu trách nhiệm cho một phần cụ thể của ứng dụng.
- **Dễ bảo trì**: Thay đổi trong một thành phần (ví dụ: Models) không ảnh hưởng trực tiếp đến các thành phần khác.
- **Tái sử dụng**: Các dự án Class Library có thể được sử dụng trong các dự án khác nếu cần.
- **Tổ chức rõ ràng**: Tránh việc tập trung tất cả logic (Models, DbContext, Utilities) trong một dự án duy nhất.

### Ghi chú thêm
- Đảm bảo các dự án Class Library sử dụng cùng phiên bản .NET với dự án chính để tránh xung đột.
- Các bước chi tiết để chuyển logic từ `BulkyWeb` sang các dự án Class Library sẽ được thực hiện trong các video tiếp theo, bao gồm cập nhật cấu hình và tham chiếu giữa các dự án.