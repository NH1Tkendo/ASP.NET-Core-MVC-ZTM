## Ghi chú học tập: Truyền và hiển thị danh sách danh mục trong Index View

### Khái niệm cơ bản
- Mục đích: Truyền danh sách danh mục (`List<Category>`) từ controller sang `Index View` và hiển thị dữ liệu trong bảng HTML bằng cách lặp qua danh sách.
- Phương pháp: Sử dụng từ khóa `@model` để định nghĩa kiểu dữ liệu của model trong view, kết hợp mã C# trong HTML để hiển thị danh sách danh mục.
- Công cụ: Tính năng Hot Reload của .NET 7 để tự động cập nhật giao diện, cú pháp Razor để nhúng mã C# trong view.

### Các bước thực hiện

#### 1. Truyền danh sách danh mục từ Controller sang View
- Trong action `Index` của controller, danh sách danh mục (`objCategoryList`) đã được truy xuất bằng `_db.Categories.ToList()`.
- Truyền danh sách này sang view bằng cách đưa vào phương thức `View`:
  ```csharp
  public IActionResult Index()
  {
      List<Category> objCategoryList = _db.Categories.ToList();
      return View(objCategoryList);
  }
  ```

#### 2. Định nghĩa Model trong Index View
- Trong file `Index.cshtml`, sử dụng từ khóa `@model` (chữ thường) để khai báo kiểu dữ liệu của model được truyền từ controller.
- Kiểu dữ liệu là `List<Category>` (danh sách các đối tượng `Category`).
- Cú pháp:
  ```cshtml
  @model List<Category>
  ```
- Lưu ý: Khi khai báo `@model`, sử dụng chữ thường. Khi truy cập model trong mã Razor, sử dụng `Model` (chữ `M` in hoa).

#### 3. Hiển thị danh sách danh mục trong bảng HTML
- Sử dụng vòng lặp `foreach` trong Razor để lặp qua `Model` và hiển thị các thuộc tính (`Name`, `DisplayOrder`) trong bảng HTML.
- Thêm thẻ `<tbody>` vào bảng và sử dụng vòng lặp để tạo các hàng (`<tr>`) và cột (`<td>`).
- Mã nguồn:
  ```cshtml
  @model List<Category>

  <table class="table table-bordered table-striped">
      <tr>
          <th>Category Name</th>
          <th>Display Order</th>
      </tr>
      <tbody>
          @foreach (var obj in Model)
          {
              <tr>
                  <td>@obj.Name</td>
                  <td>@obj.DisplayOrder</td>
              </tr>
          }
      </tbody>
  </table>
  ```

#### 4. Sắp xếp danh sách danh mục (Tùy chọn)
- Để sắp xếp danh sách theo `DisplayOrder`, sử dụng LINQ trong controller hoặc trong view.
- Ví dụ: Sắp xếp trong controller trước khi truyền sang view:
  ```csharp
  List<Category> objCategoryList = _db.Categories.OrderBy(u => u.DisplayOrder).ToList();
  ```
- Hoặc, sắp xếp trực tiếp trong view bằng Razor:
  ```cshtml
  @foreach (var obj in Model.OrderBy(u => u.DisplayOrder))
  {
      <tr>
          <td>@obj.Name</td>
          <td>@obj.DisplayOrder</td>
      </tr>
  }
  ```

#### 5. Sử dụng Hot Reload
- Khi chỉnh sửa file `Index.cshtml`, tính năng **Hot Reload** tự động làm mới giao diện mà không cần khởi động lại ứng dụng.
- Kết quả: Bảng hiển thị danh sách danh mục với các cột `Name` và `DisplayOrder`, ví dụ:
  - Action, 1
  - SciFi, 2
  - History, 3

### Ghi chú thêm
- **Razor**: Cú pháp của ASP.NET Core cho phép nhúng mã C# trong HTML, hỗ trợ các cấu trúc như vòng lặp (`foreach`), điều kiện (`if`), và truy cập thuộc tính đối tượng.
- **Hot Reload**: Tiết kiệm thời gian phát triển bằng cách tự động cập nhật giao diện khi thay đổi view.
- **Model vs. @model**:
  - `@model` (chữ thường): Khai báo kiểu dữ liệu của model ở đầu view.
  - `Model` (chữ `M` in hoa): Truy cập model trong mã Razor.
- Trong bài học tiếp theo: Có thể tìm hiểu cách thêm các tính năng như chỉnh sửa hoặc xóa danh mục.

### Mã nguồn đầy đủ
```cshtml
@model List<Category>

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
```

### Thuật ngữ chuyên ngành
- **Razor**: Cú pháp nhúng C# trong HTML (C# embedded HTML syntax).
- **Hot Reload**: Tính năng tự động làm mới giao diện (automatic interface refresh feature).
- **Model**: Đối tượng dữ liệu được truyền từ controller sang view (data object passed to view).
- **@model**: Từ khóa khai báo kiểu dữ liệu model trong Razor (model type declaration keyword).
- **OrderBy**: Phương thức LINQ sắp xếp dữ liệu (LINQ method for sorting data).