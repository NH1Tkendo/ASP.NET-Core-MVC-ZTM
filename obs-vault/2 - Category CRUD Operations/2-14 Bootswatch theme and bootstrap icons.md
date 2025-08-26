## Ghi chú học tập: Tùy chỉnh giao diện với Bootstrap và Bootswatch

### Khái niệm cơ bản
- Mục đích: Cải thiện giao diện ứng dụng bằng cách sử dụng các chủ đề (theme) từ Bootswatch và tích hợp biểu tượng (icons) từ Bootstrap để làm cho ứng dụng trực quan và hấp dẫn hơn.
- Phương pháp: Thay thế file CSS mặc định của Bootstrap bằng CSS từ Bootswatch và thêm Bootstrap Icons thông qua CDN.
- Công cụ: Bootswatch (cung cấp chủ đề miễn phí cho Bootstrap), Bootstrap Icons (thư viện biểu tượng), Razor View (`_Layout.cshtml`) để cấu hình giao diện.

### Các bước thực hiện

#### 1. Tích hợp chủ đề Bootswatch
- Truy cập [Bootswatch.com](https://bootswatch.com/) để chọn và tải chủ đề (theme). Trong ví dụ, sử dụng chủ đề **Lux**.
- Tải file `bootstrap.css` của chủ đề Lux:
  - Nhấp vào nút **Download** trên trang Bootswatch để lấy file CSS.
  - Sao chép toàn bộ nội dung của file `bootstrap.css` đã tải.
- Thay thế file CSS mặc định:
  - Trong dự án, mở thư mục `wwwroot/lib/bootstrap/css`.
  - Mở file `bootstrap.css` và thay thế nội dung bằng nội dung từ file Lux `bootstrap.css`.
- Cập nhật file `_Layout.cshtml`:
  - Đảm bảo rằng file `_Layout.cshtml` tham chiếu đến `bootstrap.css` thay vì `bootstrap.min.css`.
  - Ví dụ:
    ```cshtml
    <link rel="stylesheet" href="~/lib/bootstrap/css/bootstrap.css" />
    ```

#### 2. Tùy chỉnh Header và Footer
- **Header (Navbar)**:
  - Trong file `_Layout.cshtml`, chỉnh sửa lớp của thẻ `<nav>` để sử dụng giao diện tối (`navbar-dark` và `bg-primary`).
  - Xóa lớp `navbar-light` và các lớp không cần thiết như `text-dark` để phù hợp với chủ đề Lux.
  - Mã nguồn:
    ```cshtml
    <nav class="navbar navbar-dark bg-primary">
        <!-- Nội dung navbar -->
    </nav>
    ```
- **Footer**:
  - Thêm lớp `bg-primary` để làm tối footer.
  - Căn giữa nội dung bằng lớp `text-center`.
  - Mã nguồn:
    ```cshtml
    <footer class="bg-primary text-center">
        <!-- Nội dung footer -->
    </footer>
    ```

#### 3. Tích hợp Bootstrap Icons
- Truy cập [icons.getbootstrap.com](https://icons.getbootstrap.com/) và vào tab **Install**.
- Sao chép liên kết CDN của Bootstrap Icons:
  ```html
  <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css">
  ```
- Thêm liên kết CDN vào phần `<head>` của file `_Layout.cshtml`:
  ```cshtml
  <head>
      <link rel="stylesheet" href="~/lib/bootstrap/css/bootstrap.css" />
      <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css">
      <!-- Các thẻ khác -->
  </head>
  ```
- Sử dụng biểu tượng trong footer:
  - Tìm biểu tượng trên trang Bootstrap Icons (ví dụ: `heart-fill`).
  - Thêm biểu tượng vào footer bằng thẻ `<i>` với lớp tương ứng.
  - Mã nguồn ví dụ:
    ```cshtml
    <footer class="bg-primary text-center">
        Made with <i class="bi bi-heart-fill"></i> by .NET Mastery
    </footer>
    ```

#### 4. Kiểm tra kết quả
- Sau khi lưu các thay đổi, nhờ **Hot Reload**, giao diện sẽ tự động làm mới.
- Kết quả:
  - Giao diện sử dụng chủ đề Lux với màu sắc hiện đại.
  - Navbar và footer có nền tối (`bg-primary`), footer căn giữa với biểu tượng trái tim (`heart-fill`).

### Ghi chú thêm
- **Bootswatch**: Cung cấp các chủ đề miễn phí để tùy chỉnh Bootstrap, giúp giao diện đẹp hơn mà không cần thiết kế từ đầu.
- **Bootstrap Icons**: Thư viện biểu tượng dễ sử dụng, tích hợp qua CDN hoặc tải về cục bộ.
- **Hot Reload**: Tự động cập nhật giao diện khi chỉnh sửa file `_Layout.cshtml` hoặc các file view khác.
- File `_Layout.cshtml` là nơi cấu hình giao diện chung cho toàn bộ ứng dụng, bao gồm CSS, JS, và các thành phần như navbar, footer.
- Trong bài học tiếp theo: Có thể tìm hiểu cách thêm các tính năng tương tác hoặc cải tiến giao diện khác.

### Mã nguồn đầy đủ
```cshtml
<head>
    <link rel="stylesheet" href="~/lib/bootstrap/css/bootstrap.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css">
</head>
<body>
    <nav class="navbar navbar-dark bg-primary">
        <!-- Nội dung navbar -->
    </nav>
    <!-- Nội dung chính của trang -->
    <footer class="bg-primary text-center">
        Made with <i class="bi bi-heart-fill"></i> by .NET Mastery
    </footer>
</body>
```

### Thuật ngữ chuyên ngành
- **Bootswatch**: Thư viện cung cấp chủ đề Bootstrap (Bootstrap theme library).
- **Bootstrap Icons**: Thư viện biểu tượng của Bootstrap (Bootstrap icon library).
- **CDN (Content Delivery Network)**: Mạng phân phối nội dung để tải tài nguyên từ xa (content delivery network).
- **_Layout.cshtml**: File bố cục chính trong ASP.NET Core (main layout file in ASP.NET Core).
- **bg-primary**: Lớp Bootstrap cho màu nền chính (Bootstrap class for primary background color).