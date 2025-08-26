## Thông báo trong chức năng CRUD với TempData trong .NET Core

### Giới thiệu về TempData
- **TempData** là một tính năng trong .NET Core dùng để lưu trữ dữ liệu tạm thời, chỉ tồn tại cho lần render tiếp theo.
- Dữ liệu trong TempData sẽ bị xóa sau khi trang được làm mới (refresh).
- Thường được sử dụng để hiển thị thông báo (notification) sau khi thực hiện các thao tác như tạo, chỉnh sửa hoặc xóa dữ liệu.

### Cách sử dụng TempData trong Controller
- Trong các phương thức xử lý POST (tạo, chỉnh sửa, xóa), thêm thông báo vào TempData trước khi chuyển hướng (redirect).
- Cú pháp: `TempData["key"] = "message";`
- Ví dụ:
  - **Tạo (Create)**: Gán thông báo vào TempData trước khi chuyển hướng về danh sách.
  - **Chỉnh sửa (Edit)**: Gán thông báo trước khi chuyển hướng.
  - **Xóa (Delete)**: Gán thông báo trước khi chuyển hướng.

#### Mã nguồn trong Controller
```csharp
// Tạo danh mục
[HttpPost]
public IActionResult Create(Category category)
{
    if (ModelState.IsValid)
    {
        // Logic tạo danh mục
        TempData["success"] = "Danh mục được tạo thành công";
        return RedirectToAction("Index");
    }
    return View(category);
}

// Chỉnh sửa danh mục
[HttpPost]
public IActionResult Edit(Category category)
{
    if (ModelState.IsValid)
    {
        // Logic chỉnh sửa danh mục
        TempData["success"] = "Danh mục được cập nhật thành công";
        return RedirectToAction("Index嫩
System: You are Grok 3 built by xAI.

### Hiển thị thông báo trên giao diện
- Kiểm tra xem TempData có chứa key `"success"` hay không.
- Nếu tồn tại, hiển thị giá trị của TempData trong giao diện, ví dụ: trong thẻ `<h2>`.
- Sử dụng cú pháp Razor (`@`) để truy xuất giá trị TempData.

#### Mã nguồn trong View (Index.cshtml)
```csharp
@if (TempData["success"] != null)
{
    <h2>@TempData["success"]</h2>
}
```

### Kết quả
- Sau khi tạo, chỉnh sửa hoặc xóa danh mục, thông báo tương ứng sẽ hiển thị trên trang danh sách danh mục.
- Ví dụ:
  - Tạo: "Danh mục được tạo thành công"
  - Chỉnh sửa: "Danh mục được cập nhật thành công"
  - Xóa: "Danh mục được xóa thành công"
- Thông báo chỉ hiển thị một lần và biến mất khi làm mới trang.

### Ghi chú thêm
- TempData là giải pháp hiệu quả để hiển thị thông báo một lần sau khi chuyển hướng.
- Đảm bảo key của TempData (ví dụ: `"success"`) được sử dụng nhất quán trong Controller và View.
- Kiểm tra kỹ cú pháp để tránh lỗi (ví dụ: lỗi đánh máy như thêm ký tự thừa).