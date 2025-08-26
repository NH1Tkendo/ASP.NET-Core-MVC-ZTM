## Xử lý và làm mới Migrations trong Entity Framework Core

### Mục tiêu
- Hướng dẫn cách làm mới (reset) migrations trong Entity Framework Core khi migrations bị lỗi hoặc hỏng (corrupted).
- Xóa cơ sở dữ liệu và migrations hiện tại, sau đó tạo lại migration mới để bắt đầu từ đầu.

### Các bước làm mới Migrations
#### 1. Xóa cơ sở dữ liệu
- Truy cập SQL Server Management Studio (hoặc công cụ quản lý cơ sở dữ liệu).
- Xóa cơ sở dữ liệu hiện tại (ví dụ: `Bulky`) để bắt đầu từ đầu.
  - Thao tác: Nhấp chuột phải vào cơ sở dữ liệu `Bulky` > **Delete**.

#### 2. Xóa thư mục Migrations
- Trong dự án `Bulky.DataAccess`, xóa toàn bộ thư mục `Migrations` để loại bỏ các file migration cũ.
- Kết quả: Không còn migrations nào trong dự án.

#### 3. Cấu hình Package Manager Console
- Mở **Package Manager Console** trong Visual Studio (Tools > NuGet Package Manager > Package Manager Console).
- Đặt **Default Project** trong Package Manager Console thành `Bulky.DataAccess` (vì `ApplicationDbContext` và migrations giờ nằm trong dự án này).
  - Lưu ý: **Startup Project** vẫn là `BulkyWeb`, nhưng migrations được quản lý trong `Bulky.DataAccess`.

#### 4. Tạo Migration mới
- Trong Package Manager Console, chạy lệnh để tạo migration mới:
  ```powershell
  Add-Migration AddCategoryToDbAndSeedTable
  ```
- Mô tả:
  - Tên migration: `AddCategoryToDbAndSeedTable` (phản ánh việc tạo bảng `Categories` và seeding dữ liệu).
  - Lệnh này sẽ tạo một file migration mới trong thư mục `Migrations` của `Bulky.DataAccess`, bao gồm các thay đổi để tạo bảng `Categories` và chèn dữ liệu khởi tạo (seeding).

#### 5. Cập nhật cơ sở dữ liệu
- Chạy lệnh sau trong Package Manager Console để áp dụng migration và tạo lại cơ sở dữ liệu:
  ```powershell
  Update-Database
  ```
- Kết quả:
  - Cơ sở dữ liệu mới (`Bulky`) được tạo.
  - Bảng `Categories` được tạo và dữ liệu khởi tạo (nếu có) được chèn vào.

#### 6. Kiểm tra kết quả
- Mở SQL Server Management Studio, làm mới danh sách cơ sở dữ liệu.
- Kiểm tra:
  - Cơ sở dữ liệu `Bulky` được tạo lại.
  - Bảng `Categories` tồn tại và chứa dữ liệu khởi tạo (nếu được cấu hình trong migration).

### Lưu ý quan trọng
- **Mất dữ liệu**: Khi xóa cơ sở dữ liệu và migrations, tất cả dữ liệu hiện tại (như danh mục đã thêm) sẽ bị mất. Phương pháp này chỉ phù hợp khi muốn làm mới hoàn toàn, đặc biệt trong giai đoạn học tập hoặc phát triển.
- **Cấu hình đúng dự án**: Đảm bảo chọn đúng `Default Project` (`Bulky.DataAccess`) trong Package Manager Console để tránh lỗi như "The target project is BulkyWeb".
- **Namespace chính xác**: Sau khi di chuyển `ApplicationDbContext` sang `Bulky.DataAccess`, đảm bảo namespace trong các file migration và code-behind được cập nhật (ví dụ: `Bulky.DataAccess.Data`).

### Ưu điểm và nhược điểm
- **Ưu điểm**:
  - Giúp làm mới migrations khi gặp lỗi nghiêm trọng hoặc hỏng.
  - Đơn giản hóa việc bắt đầu lại từ đầu trong môi trường phát triển.
- **Nhược điểm**:
  - Xóa toàn bộ dữ liệu hiện có, không phù hợp cho môi trường sản xuất (production) trừ khi có kế hoạch sao lưu và khôi phục.

### Ghi chú thêm
- Nếu migrations tiếp tục gặp lỗi, kiểm tra:
  - Namespace trong `ApplicationDbContext` và các file migration.
  - Tham chiếu dự án giữa `Bulky.DataAccess` và `Bulky.Models`.
  - Các gói NuGet (`Microsoft.EntityFrameworkCore`, `Microsoft.EntityFrameworkCore.SqlServer`, `Microsoft.EntityFrameworkCore.Tools`) đã được cài đặt trong `Bulky.DataAccess`.
- Đây là quy trình hữu ích khi học hoặc thử nghiệm với Entity Framework Core. Trong môi trường thực tế, cần cẩn thận hơn và có thể sử dụng các công cụ như rollback migrations (`Remove-Migration`) thay vì xóa toàn bộ.