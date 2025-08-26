## Ghi chú học tập: Hiển thị danh sách danh mục trong Index View

### Khái niệm cơ bản
- Mục đích: Truyền danh sách danh mục (`objCategoryList`) từ controller sang `Index View` và hiển thị dưới dạng bảng HTML sử dụng các lớp Bootstrap.
- Phương pháp: Cập nhật `Index View` để tạo bảng HTML với các cột tiêu đề (heading) và sử dụng tính năng **Hot Reload** của .NET 7 để tự động làm mới giao diện khi thay đổi view.
- Công cụ: Bootstrap để định dạng bảng, Hot Reload để cải thiện hiệu quả phát triển.

### Các bước thực hiện

#### 1. Cấu hình bảng HTML trong Index View
- Trong file `Index.cshtml` (thuộc thư mục `Views/Category`), thêm một bảng HTML để hiển thị danh sách danh mục.
- Sử dụng các lớp Bootstrap (`table`, `table-bordered`, `table-striped`) để định dạng bảng.
- Thêm tiêu đề cột (heading) cho bảng, bao gồm:
  - Tên danh mục (`Category Name`)
  - Thứ tự hiển thị (`Display Order`)
- Mã nguồn:
  ```html
  <table class="table table-bordered table-striped">
      <tr>
          <th>Category Name</th>
          <th>Display Order</th>
      </tr>
  </table>
  ```

#### 2. Sử dụng Hot Reload trong .NET 7
- **Hot Reload** là tính năng của .NET 7, cho phép tự động làm mới giao diện khi thay đổi các file view (HTML, CSS, JavaScript) mà không cần build lại toàn bộ dự án.
- Lưu ý:
  - Hot Reload chỉ hoạt động với các thay đổi trong view, không áp dụng cho thay đổi trong controller (cần build lại dự án).
  - Cần kích hoạt Hot Reload trong môi trường phát triển:
    - Kiểm tra biểu tượng Hot Reload trong IDE (ví dụ: Visual Studio).
    - Đảm bảo tùy chọn **Hot Reload on File Save** được bật.
    - Nếu không thấy thay đổi, khởi động lại ứng dụng sau khi bật Hot Reload.
- Quy trình:
  - Thay đổi mã trong `Index.cshtml` (ví dụ: thêm bảng HTML).
  - Lưu file, giao diện sẽ tự động làm mới để hiển thị bảng.

#### 3. Kết quả
- Sau khi lưu file `Index.cshtml`, bảng HTML với các tiêu đề cột (`Category Name`, `Display Order`) sẽ hiển thị trên giao diện.
- Hiện tại, bảng chỉ có phần tiêu đề (header). Phần nội dung (danh sách danh mục) sẽ được thêm trong bước tiếp theo.

### Ghi chú thêm
- **Bootstrap**: Thư viện CSS dùng để định dạng bảng, giúp giao diện trực quan và thân thiện hơn (`table-bordered` thêm viền, `table-striped` thêm hiệu ứng xen kẽ màu).
- **Hot Reload**: Tăng hiệu quả phát triển bằng cách loại bỏ nhu cầu khởi động lại ứng dụng khi chỉnh sửa view.
- Trong bài học tiếp theo: Thêm mã để hiển thị chi tiết danh sách danh mục trong bảng (lặp qua `objCategoryList` để hiển thị dữ liệu).

### Mã nguồn đầy đủ
```html
<table class="table table-bordered table-striped">
    <tr>
        <th>Category Name</th>
        <th>Display Order</th>
    </tr>
</table>
```

### Thuật ngữ chuyên ngành
- **Hot Reload**: Tính năng tự động làm mới giao diện (automatic interface refresh feature).
- **Bootstrap**: Thư viện định dạng giao diện (UI styling framework).
- **Index View**: Giao diện hiển thị danh sách chính (primary list display interface).
- **Table-bordered**: Lớp Bootstrap thêm viền cho bảng (Bootstrap class for table borders).
- **Table-striped**: Lớp Bootstrap thêm hiệu ứng xen kẽ màu cho các hàng (Bootstrap class for striped rows).