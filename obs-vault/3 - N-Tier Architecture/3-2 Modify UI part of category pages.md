## Tùy chỉnh giao diện các trang CRUD trong ASP.NET Core

### Mục tiêu
- Áp dụng thiết kế giao diện đã sử dụng trong trang `Create Category` cho các trang khác (`Edit`, `Delete`, `Index`) để đảm bảo tính đồng bộ.
- Sử dụng bố cục thẻ (card) và các lớp Bootstrap (như `card`, `card-header`, `card-body`, `form-floating`) để tạo giao diện hiện đại.
- Loại bỏ các thành phần không cần thiết (như tiêu đề dư thừa, lớp không sử dụng) để tối ưu hóa giao diện.

### Các bước thực hiện
#### 1. Áp dụng thiết kế cho trang Edit và Delete
- Sao chép bố cục từ `Create.cshtml` làm mẫu (template) để áp dụng cho `Edit.cshtml` và `Delete.cshtml`.
- Cấu trúc chung:
  - Thẻ `<div class="card shadow border-0">` để tạo thẻ chính.
  - `card-header` chứa tiêu đề (ví dụ: "Chỉnh sửa danh mục" hoặc "Xóa danh mục").
  - `card-body` chứa biểu mẫu (form) với các trường nhập liệu sử dụng `form-floating`.

##### Mã nguồn trong `Edit.cshtml`
```csharp
<div class="card shadow border-0">
    <div class="card-header">
        <div class="row">
            <div class="col-12 text-center">
                <h2 class="text-white py-2">Chỉnh sửa danh mục</h2>
            </div>
        </div>
    </div>
    <div class="card-body p-4">
        <form asp-action="Edit" class="row pt-2">
            <div class="form-floating py-2 col-12">
                <input asp-for="Name" class="form-control border-0 shadow ms-2" />
                <label asp-for="Name">Tên danh mục</label>
                <span asp-validation-for="Name" class="text-danger"></span>
            </div>
            <div class="form-floating py-2 col-12">
                <input asp-for="DisplayOrder" class="form-control border-0 shadow ms-2" />
                <label asp-for="DisplayOrder">Thứ tự hiển thị</label>
                <span asp-validation-for="DisplayOrder" class="text-danger"></span>
            </div>
            <div class="col-12">
                <button type="submit" class="btn btn-outline-primary">Cập nhật</button>
            </div>
        </form>
    </div>
</div>
```

##### Mã nguồn trong `Delete.cshtml`
```csharp
<div class="card shadow border-0">
    <div class="card-header">
        <div class="row">
            <div class="col-12 text-center">
                <h2 class="text-white py-2">Xóa danh mục</h2>
            </div>
        </div>
    </div>
    <div class="card-body p-4">
        <form asp-action="Delete" class="row pt-2">
            <div class="form-floating py-2 col-12">
                <input asp-for="Name" class="form-control border-0 shadow ms-2" disabled />
                <label asp-for="Name">Tên danh mục</label>
            </div>
            <div class="form-floating py-2 col-12">
                <input asp-for="DisplayOrder" class="form-control border-0 shadow ms-2" disabled />
                <label asp-for="DisplayPOWER

System: DisplayOrder">Thứ tự hiển thị</label>
            </div>
            <div class="col-12">
                <button type="submit" class="btn btn-outline-primary">Xóa</button>
            </div>
        </form>
    </div>
</div>
```

#### Giải thích thay đổi
- **Biểu mẫu (Form)**:
  - Sử dụng `form-floating` cho các trường nhập liệu (`Name`, `DisplayOrder`) để tạo hiệu ứng nhãn động.
  - Các trường trong trang `Delete` được thêm thuộc tính `disabled` để ngăn chỉnh sửa.
  - Nút gửi sử dụng lớp `btn-outline-primary` để đồng bộ với theme Sandstone.
- **Loại bỏ các lớp không cần thiết**:
  - Xóa các thẻ tiêu đề dư thừa như `<h2>Edit Category</h2>` hoặc `<h2>Category List</h2>` để tránh lặp lại với tiêu đề trong `card-header`.
  - Xóa lớp `mt-4` (margin-top) và `mb-3` (margin-bottom) để tối ưu bố cục.
- **Bố cục thẻ**:
  - Sử dụng `card`, `shadow`, `border-0` để tạo giao diện thẻ hiện đại.
  - `card-header` chứa tiêu đề trắng (`text-white`) với đệm (`py-2`).
  - `card-body` có đệm (`p-4`) để bố cục rộng rãi.

#### 2. Cải thiện giao diện trang Index (Danh sách danh mục)
- Áp dụng bố cục thẻ tương tự cho trang `Index.cshtml`.
- Di chuyển nội dung danh sách danh mục vào `card-body`, loại bỏ tiêu đề dư thừa (`Category List`) và các lớp không cần thiết như `container`.

##### Mã nguồn trong `Index.cshtml`
```csharp
<div class="card shadow border-0">
    <div class="card-header">
        <div class="row">
            <div class="col-12 text-center">
                <h2 class="text-white py-2">Danh sách danh mục</h2>
            </div>
        </div>
    </div>
    <div class="card-body p-4">
        <!-- Nội dung danh sách danh mục -->
        <table class="table">
            <thead>
                <tr>
                    <th>Tên danh mục</th>
                    <th>Thứ tự hiển thị</th>
                    <th>Thao tác</th>
                </tr>
            </thead>
            <tbody>
                <!-- Danh sách danh mục từ Model -->
            </tbody>
        </table>
    </div>
</div>
```

#### Thay đổi bổ sung
- Thêm lớp `pt-2` (padding-top) vào `row` trong biểu mẫu để căn chỉnh khoảng cách.
- Loại bỏ lớp `container` trong `Index.cshtml` nếu không cần thiết, đảm bảo bố cục gọn gàng.
- Thêm lớp `pb-4` (padding-bottom) trong `card-body` nếu cần khoảng cách dưới cùng.

### Kết quả
- Các trang `Create`, `Edit`, `Delete`, và `Index` có giao diện đồng bộ với theme Sandstone và bố cục thẻ (card).
- Biểu mẫu sử dụng `form-floating` tạo hiệu ứng nhãn động, tăng tính thẩm mỹ.
- Các nút sử dụng lớp `btn-outline-primary` để phù hợp với bảng màu của theme.
- Giao diện trở nên hiện đại, nhất quán và dễ sử dụng trên tất cả các trang CRUD.

### Ghi chú thêm
- Đảm bảo các lớp Bootstrap được sử dụng chính xác để tránh lỗi hiển thị.
- Nếu giao diện không cập nhật, thử làm mới cứng (hard reload) hoặc khởi động lại ứng dụng.
- Các thay đổi này tập trung vào tính đồng bộ và tối ưu hóa giao diện, sử dụng các lớp Bootstrap tiêu chuẩn để dễ bảo trì.
- Trong các bước tiếp theo, có thể áp dụng thêm các cải tiến như phân trang (pagination) hoặc lọc (filtering) cho trang `Index` để nâng cao trải nghiệm người dùng.