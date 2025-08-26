## Ghi chú học tập: Tạo trang thêm danh mục mới

### Khái niệm cơ bản
- Mục đích: Tạo một trang mới trong ứng dụng để người dùng có thể nhập thông tin danh mục mới (`Category`) thông qua biểu mẫu (form) và lưu vào cơ sở dữ liệu.
- Phương pháp: 
  - Tạo action method `Create` trong `CategoryController` để hiển thị view.
  - Tạo view `Create.cshtml` với biểu mẫu sử dụng Bootstrap và Razor để nhập `Name` và `DisplayOrder`.
  - Đảm bảo giao diện đáp ứng (responsive) và tích hợp nút quay lại danh sách danh mục.
- Công cụ: Bootstrap (định dạng biểu mẫu), Razor (xử lý logic giao diện), Bootstrap Icons (biểu tượng), Hot Reload (làm mới giao diện).

### Các bước thực hiện

#### 1. Tạo Action Method trong Controller
- Trong `CategoryController`, thêm action method `Create` để hiển thị view tạo danh mục.
- Action này trả về một `IActionResult` và gọi `View()` mà không cần truyền model (mặc định tạo đối tượng `Category` rỗng).
- Mã nguồn:
  ```csharp
  public IActionResult Create()
  {
      return View();
  }
  ```

#### 2. Tạo View Create.cshtml
- Tạo view `Create.cshtml` trong thư mục `Views/Category`:
  - Có thể tạo thủ công hoặc sử dụng tính năng "Add View" bằng cách nhấp chuột phải vào action method `Create` trong Visual Studio, chọn "Add View" và đặt tên là `Create` (phải khớp với tên action).
- Xóa nội dung mặc định và định nghĩa model cho view là `Category`:
  ```cshtml
  @model Category
  ```
- Lưu ý: Không cần truyền `new Category()` từ controller, vì Razor tự động tạo đối tượng `Category` rỗng với các giá trị mặc định.

#### 3. Thiết kế biểu mẫu trong Create.cshtml
- Tạo biểu mẫu (`<form>`) với phương thức `POST` để gửi dữ liệu.
- Sử dụng các lớp Bootstrap để định dạng:
  - `container`: Chứa nội dung chính.
  - `row`, `col-6`, `col-md-3`: Tạo bố cục lưới, đáp ứng trên các kích thước màn hình.
  - `form-control`: Định dạng trường nhập liệu.
  - `btn-primary`, `btn-outline-secondary`: Định dạng nút.
- Thêm các trường nhập liệu cho `Name` và `DisplayOrder`, nút "Create", và liên kết quay lại danh sách.
- Mã nguồn:
  ```cshtml
  @model Category

  <div class="container">
      <div class="row pt-4 pb-2">
          <h2 class="text-primary">Create Category</h2>
          <hr />
      </div>
      <div class="row mb-3">
          <form method="post">
              <div class="mb-3 p-0">
                  <label>Category Name</label>
                  <input type="text" class="form-control" />
              </div>
              <div class="mb-3 p-0">
                  <label>Display Order</label>
                  <input type="text" class="form-control" />
              </div>
              <div class="row">
                  <div class="col-6 col-md-3">
                      <button type="submit" class="btn btn-primary form-control">Create</button>
                  </div>
                  <div class="col-6 col-md-3">
                      <a asp-controller="Category" asp-action="Index" class="btn btn-outline-secondary form-control">Back to List</a>
                  </div>
              </div>
          </form>
      </div>
  </div>
  ```

#### 4. Cập nhật liên kết trong Index View
- Trong `Index.cshtml`, cập nhật liên kết của nút "Create New Category" để trỏ đến action `Create` trong `CategoryController`.
- Sử dụng thuộc tính `asp-controller` và `asp-action` để chỉ định rõ controller và action.
- Mã nguồn:
  ```cshtml
  <a asp-controller="Category" asp-action="Create" class="btn btn-primary">
      <i class="bi bi-plus-circle"></i> Create New Category
  </a>
  ```
- Lưu ý: 
  - Việc chỉ định rõ `asp-controller="Category"` giúp mã dễ đọc và tránh nhầm lẫn, đặc biệt trong dự án lớn.
  - Có thể bỏ `asp-controller` nếu action nằm trong cùng controller, nhưng chỉ định rõ ràng là cách làm tốt hơn.

#### 5. Đảm bảo giao diện đáp ứng
- Sử dụng hệ thống lưới Bootstrap để đảm bảo giao diện đáp ứng:
  - `col-6`: Mỗi nút chiếm 6 cột trên màn hình nhỏ.
  - `col-md-3`: Mỗi nút chiếm 3 cột trên màn hình trung bình trở lên.
- Thêm lớp `p-0` (padding zero) cho nhãn và trường nhập liệu để giảm khoảng cách thừa.
- Thêm lớp `pt-4`, `pb-2`, `mb-3` để điều chỉnh khoảng cách tổng thể.

#### 6. Kiểm tra và sử dụng Hot Reload
- Sau khi lưu `Index.cshtml` và `Create.cshtml`, **Hot Reload** tự động làm mới giao diện.
- Kết quả:
  - Nhấp vào nút "Create New Category" trong trang danh sách dẫn đến trang tạo danh mục.
  - Trang tạo danh mục hiển thị biểu mẫu với các trường `Category Name`, `Display Order`, nút "Create", và liên kết "Back to List".
  - Giao diện đáp ứng, với các nút thu nhỏ trên màn hình lớn và xếp chồng trên màn hình nhỏ.

### Ghi chú thêm
- **Razor View**: Hỗ trợ định nghĩa model (`@model`) và sử dụng cú pháp `asp-controller`, `asp-action` để tạo liên kết động.
- **Bootstrap Grid**: Hệ thống lưới linh hoạt, cho phép bố cục thay đổi theo kích thước màn hình (`col-6` cho nhỏ, `col-md-3` cho trung bình/lớn).
- **Hot Reload**: Tiết kiệm thời gian bằng cách tự động cập nhật giao diện khi chỉnh sửa view.
- **Nút Back to List**: Liên kết đến action `Index` của `CategoryController` để quay lại danh sách danh mục.
- Trong bài học tiếp theo: Xử lý logic gửi biểu mẫu (`POST`) để lưu danh mục mới vào cơ sở dữ liệu.

### Mã nguồn đầy đủ
**Create.cshtml**:
```cshtml
@model Category

<div class="container">
    <div class="row pt-4 pb-2">
        <h2 class="text-primary">Create Category</h2>
        <hr />
    </div>
    <div class="row mb-3">
        <form method="post">
            <div class="mb-3 p-0">
                <label>Category Name</label>
                <input type="text" class="form-control" />
            </div>
            <div class="mb-3 p-0">
                <label>Display Order</label>
                <input type="text" class="form-control" />
            </div>
            <div class="row">
                <div class="col-6 col-md-3">
                    <button type="submit" class="btn btn-primary form-control">Create</button>
                </div>
                <div class="col-6 col-md-3">
                    <a asp-controller="Category" asp-action="Index" class="btn btn-outline-secondary form-control">Back to List</a>
                </div>
            </div>
        </form>
    </div>
</div>
```

**Cập nhật nút trong Index.cshtml**:
```cshtml
<div class="col-6 text-end">
    <a asp-controller="Category" asp-action="Create" class="btn btn-primary">
        <i class="bi bi-plus-circle"></i> Create New Category
    </a>
</div>
```

### Thuật ngữ chuyên ngành
- **Action Method**: Phương thức trong controller xử lý yêu cầu (controller method handling requests).
- **Razor View**: Giao diện sử dụng cú pháp Razor để nhúng C# trong HTML (C# embedded HTML view).
- **asp-controller, asp-action**: Thuộc tính Razor tạo liên kết đến controller/action (Razor attributes for routing).
- **Responsive Design**: Thiết kế giao diện thích ứng với các kích thước màn hình (adaptive UI design).
- **form-control**: Lớp Bootstrap định dạng trường nhập liệu (Bootstrap class for input fields).