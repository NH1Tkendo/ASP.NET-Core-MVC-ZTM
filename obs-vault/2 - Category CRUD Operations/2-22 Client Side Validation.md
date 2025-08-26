## Client-Side Validation trong ASP.NET Core

### Mục tiêu
- Hiểu cách triển khai **client-side validation** (kiểm tra hợp lệ phía máy khách) sử dụng JavaScript trong ASP.NET Core.
- Kết hợp **server-side validation** (kiểm tra hợp lệ phía máy chủ) và client-side validation để cải thiện trải nghiệm người dùng.
- Sử dụng **partial view** để tích hợp các script cần thiết cho client-side validation.

### Khái niệm
- **Client-Side Validation**:
  - Xử lý kiểm tra hợp lệ trực tiếp trên trình duyệt (browser) trước khi gửi yêu cầu đến máy chủ.
  - Giảm tải cho máy chủ, cải thiện tốc độ phản hồi và tránh làm mới trang (page reload).
  - Trong ASP.NET Core, được hỗ trợ thông qua jQuery Validation và các script tích hợp sẵn.
- **Server-Side Validation**:
  - Xử lý kiểm tra hợp lệ trên máy chủ, thường dùng cho các logic phức tạp hoặc kiểm tra tùy chỉnh (custom validation).
  - Kích hoạt khi người dùng nhấn nút (ví dụ: Create), gửi yêu cầu đến controller, và trả về thông báo lỗi.
- **Partial View**:
  - Là các tệp giao diện (view) tái sử dụng, thường chứa mã HTML hoặc script, được nhúng vào các view khác.
  - Trong trường hợp này, partial view `_ValidationScriptsPartial` chứa các script JavaScript cần thiết cho client-side validation.

### Cách thực hiện
1. **Tích hợp Partial View cho Client-Side Validation**:
   - Thêm partial view `_ValidationScriptsPartial` vào view chính (ví dụ: `Create.cshtml`).
   - Đảm bảo partial view được đặt trong thư mục `Shared` (vị trí mặc định).
   - Sử dụng **tag helper** `<partial>` trong Razor để nhúng partial view.

   ```html
   @section Scripts {
       <partial name="_ValidationScriptsPartial" />
   }
   ```

   - Lưu ý: Tên partial view (`_ValidationScriptsPartial`) phải khớp chính xác với tên tệp trong thư mục `Shared`.

2. **Vị trí nhúng Partial View**:
   - Nếu partial view chỉ chứa script, nhúng trong section `@section Scripts {}`.
   - Nếu partial view chứa nội dung giao diện (UI), nhúng trực tiếp vào phần thân của view.

3. **Kiểm tra hoạt động**:
   - Client-side validation: Kích hoạt ngay khi người dùng nhập liệu, hiển thị lỗi mà không cần gửi yêu cầu đến máy chủ (không có spinner hoặc reload trang).
   - Server-side validation: Kích hoạt khi nhấn nút (ví dụ: Create) và xử lý các kiểm tra tùy chỉnh phức tạp.

### Ví dụ
1. **Cấu hình Partial View**:
   - Trong `Create.cshtml`, thêm script section và nhúng partial view:

   ```html
   @section Scripts {
       <partial name="_ValidationScriptsPartial" />
   }
   ```

2. **Kiểm tra Client-Side Validation**:
   - Khi người dùng nhấn nút Create mà không nhập dữ liệu:
     - Lỗi hiển thị ngay lập tức (ví dụ: trường `Name` hoặc `DisplayOrder` trống).
     - Không có yêu cầu gửi đến máy chủ, không reload trang, không chạm breakpoint trong controller.
   - Khi nhập `DisplayOrder` vượt quá giới hạn (ví dụ: >100):
     - Lỗi hiển thị tự động trong quá trình nhập liệu.

3. **Kiểm tra Server-Side Validation**:
   - Với kiểm tra tùy chỉnh (ví dụ: `Name` và `DisplayOrder` không được trùng nhau):
     - Client-side validation không xử lý được, yêu cầu gửi đến máy chủ.
     - Breakpoint trong controller được kích hoạt, trả về thông báo lỗi.

   ```csharp
   // Trong CategoryController
   if (obj.Name == obj.DisplayOrder.ToString())
   {
       ModelState.AddModelError("", "Tên danh mục và thứ tự hiển thị không được trùng nhau.");
   }
   ```

### Mã nguồn
```html
<!-- Trong Create.cshtml -->
<form asp-action="Create">
    <asp:TextBox ID="Name" runat="server" />
    <asp:ValidationMessage For="Name" runat="server" />
    <asp:TextBox ID="DisplayOrder" runat="server" />
    <asp:ValidationMessage For="DisplayOrder" runat="server" />
    <button type="submit">Create</button>
</form>

@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}
```

```csharp
// Trong CategoryController
public IActionResult Create(Category obj)
{
    if (obj.Name != null && obj.Name.ToLower() == "test")
    {
        ModelState.AddModelError("", "Test là giá trị không hợp lệ.");
    }
    if (obj.Name == obj.DisplayOrder.ToString())
    {
        ModelState.AddModelError("", "Tên danh mục và thứ tự hiển thị không được trùng nhau.");
    }
    if (!ModelState.IsValid)
    {
        return View(obj);
    }
    // Xử lý tạo mới...
}
```

### Ghi chú thêm
- **Ưu điểm của Client-Side Validation**:
  - Phản hồi nhanh, không cần reload trang, cải thiện trải nghiệm người dùng.
  - Tích hợp sẵn trong ASP.NET Core thông qua jQuery Validation.
- **Hạn chế của Client-Side Validation**:
  - Không xử lý được các logic kiểm tra phức tạp (ví dụ: kiểm tra tùy chỉnh như so sánh `Name` và `DisplayOrder`).
  - Cần kết hợp với server-side validation để đảm bảo tính toàn vẹn dữ liệu.
- **Partial View**:
  - Tên partial view phải chính xác, nếu không sẽ gây lỗi không tìm thấy tệp.
  - Thư mục `Shared` là nơi mặc định để lưu trữ partial view.
- **Tối ưu hóa**:
  - Chỉ nhúng `_ValidationScriptsPartial` trong các view cần client-side validation để giảm tải tài nguyên.
  - Kiểm tra cẩn thận các logic tùy chỉnh trong controller để đảm bảo không có lỗi ngoại lệ (exception).

--- 
*Ghi chú này được tối ưu hóa để lưu trữ trong Obsidian, hỗ trợ ôn tập và liên kết chéo với các ghi chú về ASP.NET Core và Validation.*