## Validation Summary trong ASP.NET

### Mục tiêu
- Hiểu cách sử dụng `Validation Summary` trong ASP.NET để hiển thị thông báo lỗi.
- Phân biệt các chế độ hiển thị lỗi: `All`, `ModelOnly`, và `None`.
- Ứng dụng thực tế trên trang đăng nhập (login page) và các tình huống khác.

### Khái niệm
- **Validation Summary**: Hiển thị tất cả thông báo lỗi (error messages) trong một khu vực tổng hợp trên giao diện.
- **Các chế độ hiển thị**:
  - `All`: Hiển thị tất cả lỗi, bao gồm lỗi liên quan đến thuộc tính (properties) và lỗi tùy chỉnh (custom errors).
  - `ModelOnly`: Chỉ hiển thị lỗi liên quan đến mô hình (model), không bao gồm lỗi của thuộc tính cụ thể.
  - `None`: Không hiển thị bất kỳ thông báo lỗi nào trong validation summary.

### Cách thực hiện
1. **Cấu hình Validation Summary**:
   - Trong mã giao diện (view), sử dụng thẻ `<asp:ValidationSummary>` với thuộc tính `ValidationGroup` để xác định nhóm kiểm tra.
   - Thiết lập chế độ hiển thị thông qua thuộc tính `DisplayMode` (`All`, `ModelOnly`, hoặc `None`).

2. **Thêm lỗi tùy chỉnh trong Controller**:
   - Trong controller, sử dụng `ModelState.AddModelError` để thêm thông báo lỗi.
   - Nếu lỗi không liên quan đến thuộc tính cụ thể, để trống tham số `key` trong `AddModelError`.

   ```csharp
   // Ví dụ: Thêm lỗi tùy chỉnh trong CategoryController
   if (obj.Name.ToLower() == "test")
   {
       ModelState.AddModelError("", "Test là giá trị không hợp lệ.");
   }
   ```

3. **Chuyển đổi chế độ hiển thị**:
   - Chuyển đổi giữa `All`, `ModelOnly`, và `None` trong thẻ `<asp:ValidationSummary>` để kiểm tra hành vi hiển thị lỗi.
   - Ví dụ: 
     ```html
     <asp:ValidationSummary DisplayMode="ModelOnly" runat="server" />
     ```

### Ví dụ
1. **Chế độ `All`**:
   - Hiển thị tất cả lỗi, bao gồm lỗi thuộc tính (ví dụ: `Name`, `DisplayOrder`) và lỗi tùy chỉnh.
   - Trường hợp: Nhập `Name = "test"` và `DisplayOrder = 0`:
     - Hiển thị lỗi thuộc tính (property-related errors) và lỗi tùy chỉnh ("Test là giá trị không hợp lệ").

2. **Chế độ `ModelOnly`**:
   - Chỉ hiển thị lỗi không liên quan đến thuộc tính cụ thể.
   - Trường hợp: Nhập `Name = "test"` và `DisplayOrder = 0`:
     - Chỉ hiển thị thông báo: "Test là giá trị không hợp lệ".
     - Lỗi thuộc tính như `Name` hoặc `DisplayOrder` không xuất hiện trong validation summary.

3. **Chế độ `None`**:
   - Không hiển thị bất kỳ thông báo lỗi nào trong validation summary.
   - Trường hợp: Nhập `Name = "test"` và `DisplayOrder = 0`:
     - Không có thông báo lỗi nào được hiển thị, dù lỗi vẫn tồn tại.

### Ứng dụng thực tế
- **Trang đăng nhập (Login Page)**:
  - Sử dụng các trường bắt buộc (required fields) như `Username` và `Password`.
  - Nếu người dùng không nhập, hiển thị lỗi thuộc tính ngay tại trường nhập liệu.
  - Nếu thông tin không hợp lệ (ví dụ: sai mật khẩu), thêm lỗi tùy chỉnh và hiển thị bằng `ModelOnly` trong validation summary.

### Mã nguồn
```csharp
// Trong CategoryController
public IActionResult Create(Category obj)
{
    if (obj.Name != null && obj.Name.ToLower() == "test")
    {
        ModelState.AddModelError("", "Test là giá trị không hợp lệ.");
    }
    if (!ModelState.IsValid)
    {
        return View(obj);
    }
    // Xử lý tạo mới...
}
```

```html
<!-- Trong View -->
<asp:ValidationSummary DisplayMode="ModelOnly" runat="server" />
<asp:TextBox ID="Name" runat="server" />
<asp:ValidationMessage For="Name" runat="server" />
<asp:TextBox ID="DisplayOrder" runat="server" />
<asp:ValidationMessage For="DisplayOrder" runat="server" />
```

### Ghi chú thêm
- **Validation cá nhân (Individual Validation)**: Các thông báo lỗi liên quan đến thuộc tính cụ thể (như `Name`, `DisplayOrder`) được hiển thị ngay bên cạnh trường nhập liệu.
- **Tạm tắt Validation Summary**: Nếu chỉ cần validation cá nhân, có thể comment hoặc xóa thẻ `<asp:ValidationSummary>`.
- **Kiểm tra null**: Tránh lỗi ngoại lệ (exception) bằng cách kiểm tra `obj.Name != null` trước khi thực hiện so sánh hoặc thao tác trên thuộc tính.
- **Tính linh hoạt**: Chọn chế độ hiển thị (`All`, `ModelOnly`, `None`) tùy theo yêu cầu giao diện và trải nghiệm người dùng.

--- 
*Ghi chú này được tối ưu hóa để lưu trữ trong Obsidian, hỗ trợ ôn tập và liên kết chéo với các ghi chú khác về ASP.NET.*