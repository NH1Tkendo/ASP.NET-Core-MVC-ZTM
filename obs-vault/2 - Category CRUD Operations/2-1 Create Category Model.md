## Entity Framework Core và MVC: Tạo Model và Tự Động Tạo Bảng Cơ Sở Dữ Liệu

### Khái niệm cơ bản
- Trong ứng dụng MVC (Model-View-Controller), dữ liệu thường được lưu trữ trong cơ sở dữ liệu (database) với nhiều bảng (tables).
- Khi sử dụng **Entity Framework Core (EF Core)**, thay vì tạo bảng trực tiếp trong cơ sở dữ liệu, chúng ta định nghĩa các **model** trong dự án.
- EF Core sẽ tự động tạo các bảng trong cơ sở dữ liệu dựa trên các model này, giúp đơn giản hóa quá trình phát triển.

### Tạo Model trong Dự Án
- **Model** là các lớp (class) đại diện cho cấu trúc của bảng trong cơ sở dữ liệu.
- Thông thường, các model được đặt trong thư mục **Models**, nhưng không bắt buộc:
  - Thư mục **Models** có thể đổi tên, không giống như thư mục **Controllers** hoặc **Views** (phải giữ nguyên tên).
- Mỗi thuộc tính (property) trong model sẽ ánh xạ thành một cột (column) trong bảng cơ sở dữ liệu.

### Ví dụ: Tạo Model `Category`
1. **Tạo lớp `Category`**:
   - Trong thư mục **Models**, thêm một lớp mới với tên `Category.cs`.
   - Lớp này sẽ định nghĩa các thuộc tính tương ứng với các cột trong bảng `Category`.

2. **Thêm thuộc tính cho lớp `Category`**:
   ```csharp
   public class Category
   {
       public int Id { get; set; } // Khóa chính (Primary Key)
       public string Name { get; set; } // Tên danh mục
       public int DisplayOrder { get; set; } // Thứ tự hiển thị
   }
   ```
   - `Id`: Thuộc tính kiểu `int`, đại diện cho khóa chính (Primary Key) của bảng.
   - `Name`: Thuộc tính kiểu `string`, lưu tên của danh mục.
   - `DisplayOrder`: Thuộc tính kiểu `int`, xác định thứ tự hiển thị của danh mục trên giao diện.

3. **Sử dụng code snippet**:
   - Trong Visual Studio, gõ `prop` và nhấn `Tab` hai lần để tạo nhanh một thuộc tính.

### Xác Định Khóa Chính (Primary Key)
- Để chỉ định một thuộc tính là khóa chính, cần sử dụng **Data Annotation** (chú thích dữ liệu).
- Chi tiết về cách sử dụng Data Annotation sẽ được trình bày trong các phần tiếp theo.

### Ghi chú thêm
- Các thuộc tính trong model sẽ ánh xạ trực tiếp thành các cột trong bảng cơ sở dữ liệu.
- EF Core tự động xử lý việc tạo bảng dựa trên cấu trúc model, giúp tiết kiệm thời gian và giảm lỗi khi làm việc với cơ sở dữ liệu.
- Việc đặt model trong thư mục **Models** là một quy ước (convention), không phải yêu cầu bắt buộc, giúp tổ chức mã nguồn rõ ràng hơn.