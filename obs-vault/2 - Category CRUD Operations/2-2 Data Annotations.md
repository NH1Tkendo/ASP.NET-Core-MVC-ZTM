## Entity Framework Core: Sử dụng Data Annotation và Cấu Hình Cơ Sở Dữ Liệu

### Xác Định Khóa Chính (Primary Key) trong Model
- Để chỉ định một thuộc tính là **khóa chính (Primary Key)** trong bảng cơ sở dữ liệu, sử dụng **Data Annotation** trong EF Core.
- **Cách thực hiện**:
  - Thêm chú thích `[Key]` (trong namespace `System.ComponentModel.DataAnnotations`) vào thuộc tính mong muốn.
  - Ví dụ:
    ```csharp
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
    }
    ```
  - EF Core sẽ nhận diện thuộc tính `Id` là khóa chính của bảng `Category`.

- **Quy ước mặc định của EF Core**:
  - Nếu thuộc tính có tên là `Id` hoặc `<Tên_Model>Id` (ví dụ: `CategoryId`), EF Core tự động coi đó là khóa chính mà không cần `[Key]`.
  - Tuy nhiên, việc sử dụng `[Key]` giúp rõ ràng hơn và cần thiết khi tên thuộc tính không theo quy ước (ví dụ: `Category_Id`).

### Sử Dụng Data Annotation `[Required]`
- Chú thích `[Required]` đảm bảo rằng thuộc tính không được phép có giá trị `null` trong cơ sở dữ liệu.
- Khi bảng được tạo, thuộc tính có `[Required]` sẽ được ánh xạ thành cột với thiết lập `NOT NULL` trong SQL.
- Ví dụ:
  ```csharp
  using System.ComponentModel.DataAnnotations;

  public class Category
  {
      [Key]
      public int Id { get; set; }
      [Required]
      public string Name { get; set; }
      public int DisplayOrder { get; set; }
  }
  ```
  - Thuộc tính `Name` sẽ không được phép để trống trong cơ sở dữ liệu.

### Các Data Annotation Khác
- Có nhiều chú thích khác trong EF Core (sẽ được đề cập trong các phần sau của khóa học), ví dụ:
  - `[MaxLength]`: Giới hạn độ dài của chuỗi.
  - `[Range]`: Giới hạn phạm vi giá trị của số.
- Các chú thích này giúp kiểm soát cấu trúc và dữ liệu trong bảng.

### Chuẩn Bị Tạo Bảng trong Cơ Sở Dữ Liệu
- Để EF Core tạo bảng dựa trên model, cần thực hiện các bước sau:
  1. **Xác định chuỗi kết nối (Connection String)**:
     - Chuỗi kết nối chỉ định cách ứng dụng giao tiếp với SQL Server.
  2. **Cấu hình EF Core và SQL Server**:
     - Cần thêm các cấu hình trong dự án để tích hợp EF Core với SQL Server.
     - Quá trình này sẽ được trình bày chi tiết trong các phần tiếp theo.

### Ghi Chú Thêm
- Sử dụng `[Key]` để đảm bảo rõ ràng khi định nghĩa khóa chính, đặc biệt nếu tên thuộc tính không theo quy ước.
- `[Required]` là cách đơn giản để đảm bảo cột không cho phép giá trị `null`, tăng tính toàn vẹn dữ liệu.
- Cấu hình chuỗi kết nối và EF Core là bước quan trọng để kết nối model với cơ sở dữ liệu, sẽ được giải thích chi tiết sau.

System: * Today's date and time is 02:53 PM +07 on Tuesday, August 26, 2025.