## Tùy chỉnh giao diện Bootstrap và thiết kế trang Create Category

### Mục tiêu
- Thay đổi giao diện dự án `BulkyWeb` bằng cách chuyển từ theme **Luxe** sang **Sandstone** của Bootswatch.
- Tùy chỉnh các lớp CSS của Bootstrap (ví dụ: nút, màu sắc) bằng cách ghi đè (override) trong file `site.css`.
- Cải thiện giao diện trang tạo danh mục (`Create Category`) sử dụng các lớp Bootstrap để có thiết kế đẹp và nhất quán.

### Thay đổi Theme Bootswatch
1. **Tải theme Sandstone**:
   - Truy cập Bootswatch, chọn theme **Sandstone**, tải file `bootstrap.css`.
   - Sao chép nội dung file `bootstrap.css` của theme Sandstone.
2. **Cập nhật file CSS**:
   - Trong dự án, vào thư mục `wwwroot/css`, mở file `bootstrap.css`.
   - Xóa nội dung theme Luxe hiện tại và dán nội dung theme Sandstone.
3. **Kết quả**:
   - Khi chạy ứng dụng, giao diện sẽ sử dụng theme Sandstone với bảng màu mới, thân thiện và hiện đại hơn.

### Tùy chỉnh CSS trong `site.css`
- Tùy chỉnh các lớp Bootstrap (như `btn-primary`, `btn-success`, `btn-outline`) để thay đổi màu sắc và kiểu dáng nút.
- Nội dung CSS được cung cấp trong file đính kèm từ trang `.NET match suite` (phần Section 4).
- Thao tác:
  - Mở file `wwwroot/css/site.css`.
  - Xóa nội dung hiện tại và dán các lớp CSS tùy chỉnh từ file đính kèm.
  - Đảm bảo file `site.css` được tải sau `bootstrap.css` trong `_Layout.cshtml` để ghi đè được áp dụng.

#### Mã nguồn trong `_Layout.cshtml`
```csharp
<link rel="stylesheet" href="~/css/bootstrap.css" />
<link rel="stylesheet" href="~/css/site.css" />
```

### Cải thiện giao diện trang Create Category
- Cập nhật giao diện trang `Views/Category/Create.cshtml` sử dụng các lớp Bootstrap để tạo bố cục thẻ (card) và biểu mẫu (form) chuyên nghiệp.
- Các lớp Bootstrap sử dụng: `card`, `card-header`, `card-body`, `row`, `form-floating`, `form-control`, `shadow`, `border-0`, v.v.

#### Mã nguồn trong `Create.cshtml`
```csharp
<div class="card shadow border-0">
    <div class="card-header">
        <div class="row">
            <div class="col-12 text-center">
                <h2 class="text-white py-2">Tạo danh mục</h2>
            </div>
        </div>
    </div>
    <div class="card-body p-4">
        <form asp-action="Create" class="row pt-2">
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
                <button type="submit" class="btn btn-outline-primary">Tạo</button>
            </div>
        </form>
    </div>
</div>
```

#### Giải thích các thay đổi
- **Card Layout**:
  - Sử dụng `card`, `shadow`, `border-0` để tạo giao diện thẻ hiện đại, không viền.
  - `card-header` chứa tiêu đề "Tạo danh mục" với kiểu chữ trắng (`text-white`) và đệm (`py-2`).
  - `card-body` chứa biểu mẫu với đệm (`p-4`) để bố cục rộng rãi.
- **Form Layout**:
  - Sử dụng `row` và `form-floating` để tạo bố cục biểu mẫu nổi (floating form).
  - Các trường nhập liệu (`input`) sử dụng `form-control`, `border-0`, `shadow`, `ms-2` để có giao diện mượt mà.
  - Nút gửi (`button`) sử dụng `btn-outline-primary` thay vì `btn-secondary` để phù hợp với bảng màu Sandstone.
- **Xóa các lớp không cần thiết**:
  - Loại bỏ thẻ `<hr>`, lớp `mt-4` (margin-top) và `mb-3` (margin-bottom) để tối ưu bố cục.
  - Sử dụng `pt-2` (padding-top) trong `row` để căn chỉnh khoảng cách.

### Kết quả
- Giao diện trang tạo danh mục trở nên hiện đại, nhất quán với theme Sandstone.
- Các nút và biểu mẫu có màu sắc và kiểu dáng được tùy chỉnh thông qua `site.css`.
- Biểu mẫu sử dụng bố cục `form-floating`, giúp nhãn (label) hiển thị động khi nhập liệu.

### Ghi chú thêm
- Đảm bảo file `site.css` được tải sau `bootstrap.css` trong `_Layout.cshtml` để ghi đè CSS hoạt động đúng.
- Các lớp CSS sử dụng đều là lớp Bootstrap tiêu chuẩn, không tạo lớp tùy chỉnh mới để duy trì tính nhất quán.
- Cần áp dụng các thay đổi tương tự cho các trang khác (ví dụ: Edit, Delete) trong các bước tiếp theo để đảm bảo giao diện đồng bộ.
- Nếu giao diện không cập nhật, thử làm mới cứng (hard reload) hoặc khởi động lại dự án để áp dụng CSS mới.