## Ghi chú học tập: Thêm nút tạo danh mục mới trong trang danh sách danh mục

### Khái niệm cơ bản
- Mục đích: Thêm nút "Create New Category" vào trang danh sách danh mục (`Index View`) để cho phép người dùng chuyển hướng đến trang tạo danh mục mới.
- Phương pháp: Sử dụng các lớp Bootstrap để định dạng giao diện, tạo bố cục lưới (grid layout) và thêm nút với biểu tượng Bootstrap Icons.
- Công cụ: Bootstrap (định dạng lưới và nút), Bootstrap Icons (biểu tượng), Razor View (`Index.cshtml`) để cấu hình giao diện.

### Các bước thực hiện

#### 1. Cấu hình bố cục trong Index View
- Mở file `Index.cshtml` trong thư mục `Views/Category`.
- Thay thế tiêu đề `<h1>` đơn giản bằng một bố cục sử dụng hệ thống lưới của Bootstrap:
  - Thêm thẻ `<div>` với lớp `container` để chứa nội dung.
  - Bên trong, thêm thẻ `<div>` với lớp `row` và khoảng cách đệm trên (`pt-4`).
- Chia bố cục thành 12 cột (theo hệ thống lưới Bootstrap), sử dụng 6 cột cho tiêu đề và 6 cột cho nút.
- Mã nguồn:
  ```cshtml
  <div class="container">
      <div class="row pt-4 pb-3">
          <div class="col-6">
              <h2 class="text-primary">Category List</h2>
          </div>
          <div class="col-6 text-end">
              <a class="btn btn-primary" href="#">
                  <i class="bi bi-plus-circle"></i> Create New Category
              </a>
          </div>
      </div>
      <!-- Bảng danh mục hiện tại -->
  </div>
  ```

#### 2. Giải thích bố cục
- **Container**: Lớp `container` đảm bảo nội dung được căn giữa và có khoảng cách phù hợp.
- **Row**: Lớp `row` tạo một hàng chứa 12 cột theo hệ thống lưới Bootstrap.
- **Col-6**: Mỗi `<div class="col-6">` chiếm 6 cột, chia đôi không gian hàng.
  - Cột trái: Hiển thị tiêu đề `<h2>` với lớp `text-primary` (màu chính của chủ đề).
  - Cột phải: Sử dụng lớp `text-end` để căn chỉnh nút sang bên phải.
- **Nút (Button)**:
  - Sử dụng thẻ `<a>` với lớp `btn btn-primary` để tạo nút có kiểu dáng Bootstrap.
  - Thêm biểu tượng `plus-circle` từ Bootstrap Icons để tăng tính trực quan.
  - Tạm thời, thuộc tính `href="#"` được sử dụng làm placeholder; liên kết sẽ được cập nhật sau khi tạo action trong controller.

#### 3. Tích hợp Bootstrap Icons
- Đảm bảo rằng Bootstrap Icons đã được tích hợp trong file `_Layout.cshtml` thông qua CDN:
  ```cshtml
  <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css">
  ```
- Sử dụng biểu tượng `bi-plus-circle` trong nút:
  ```cshtml
  <i class="bi bi-plus-circle"></i>
  ```

#### 4. Kiểm tra và sử dụng Hot Reload
- Sau khi lưu file `Index.cshtml`, **Hot Reload** tự động làm mới giao diện.
- Kết quả:
  - Tiêu đề "Category List" hiển thị ở bên trái với màu `text-primary`.
  - Nút "Create New Category" hiển thị ở bên phải với biểu tượng dấu cộng (`+`) và kiểu dáng `btn-primary`.
  - Khoảng cách đệm trên (`pt-4`) và dưới (`pb-3`) được áp dụng để bố cục gọn gàng.

### Ghi chú thêm
- **Bootstrap Grid System**: Hệ thống lưới Bootstrap chia một hàng thành 12 cột, cho phép phân chia không gian linh hoạt (`col-6` chiếm nửa chiều rộng).
- **text-end**: Lớp Bootstrap căn chỉnh nội dung sang bên phải.
- **btn-primary**: Lớp Bootstrap tạo nút với màu nền chính của chủ đề.
- Nút hiện tại có `href="#"` (chưa liên kết đến action). Trong bài học tiếp theo, cần tạo action trong controller để xử lý khi nhấp vào nút "Create New Category".
- Trong bài học tiếp theo: Tạo action và view để xử lý chức năng tạo danh mục mới.

### Mã nguồn đầy đủ
```cshtml
@model List<Category>

<div class="container">
    <div class="row pt-4 pb-3">
        <div class="col-6">
            <h2 class="text-primary">Category List</h2>
        </div>
        <div class="col-6 text-end">
            <a class="btn btn-primary" href="#">
                <i class="bi bi-plus-circle"></i> Create New Category
            </a>
        </div>
    </div>
    <table class="table table-bordered table-striped">
        <tr>
            <th>Category Name</th>
            <th>Display Order</th>
        </tr>
        <tbody>
            @foreach (var obj in Model.OrderBy(u => u.DisplayOrder))
            {
                <tr>
                    <td>@obj.Name</td>
                    <td>@obj.DisplayOrder</td>
                </tr>
            }
        </tbody>
    </table>
</div>
```

### Thuật ngữ chuyên ngành
- **Bootstrap Grid System**: Hệ thống lưới Bootstrap (Bootstrap layout grid system).
- **Container**: Lớp chứa nội dung chính (content container class).
- **Row**: Hàng trong hệ thống lưới Bootstrap (grid row).
- **Col-6**: Lớp chiếm 6 cột trong lưới Bootstrap (six-column grid class).
- **text-primary**: Lớp Bootstrap áp dụng màu chính của chủ đề (primary theme color class).