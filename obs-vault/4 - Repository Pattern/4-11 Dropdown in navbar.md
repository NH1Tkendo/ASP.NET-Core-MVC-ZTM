## Thêm Dropdown vào Thanh Điều Hướng (Navbar) trong Dự án .NET

### Mục tiêu
- Thêm một menu dropdown vào thanh điều hướng (navbar) để hỗ trợ quản lý nội dung (Content Management), bao gồm liên kết đến danh mục (Category).
- Cập nhật giao diện người dùng (UI) bằng cách sử dụng Bootstrap và điều chỉnh CSS để đảm bảo hiển thị đúng.

### 1. Thêm Dropdown vào Navbar
- **Mục đích**: Tích hợp một menu dropdown vào thanh điều hướng để chứa các liên kết như "Category" và các mục quản lý nội dung khác.
- **Nguồn tài nguyên**:
  - Sử dụng mẫu navbar từ [Bootstrap documentation](https://getbootstrap.com/docs) (phần Dropdown trong Navbar).
- **Các bước thực hiện**:
  - Truy cập Bootstrap documentation, tìm phần **Navbar** và sao chép mã HTML cho thành phần dropdown (`<li>` chứa dropdown).
  - Mở tệp `_Layout.cshtml` trong thư mục `Areas/[AreaName]/Views/Shared` (hoặc `Views/Shared` nếu chưa sử dụng Areas).
  - Dán mã dropdown vào sau các phần tử `<li>` hiện có trong thanh điều hướng:
    ```html
    <li class="nav-item dropdown">
        <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
            Content Management
        </a>
        <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
            <li><a class="dropdown-item" asp-area="Admin" asp-controller="Category" asp-action="Index">Category</a></li>
            <li><hr class="dropdown-divider"></li>
            <!-- Thêm các mục khác sau này -->
        </ul>
    </li>
    ```
  - **Giải thích**:
    - `Content Management`: Tên hiển thị của dropdown.
    - `<a asp-area="Admin" asp-controller="Category" asp-action="Index">`: Liên kết đến trang danh mục trong Admin Area.
    - `<hr class="dropdown-divider">`: Dòng phân cách trong dropdown.

### 2. Xử lý vấn đề giao diện (CSS)
- **Vấn đề**:
  - Sau khi thêm dropdown, giao diện có thể hiển thị màu không mong muốn (ví dụ: màu xanh lam) do CSS mặc định hoặc xung đột.
- **Cách khắc phục**:
  - Kiểm tra tệp CSS liên quan (ví dụ: `BulkyBook.styles.css` hoặc tệp CSS của Bootstrap).
  - Nếu màu sắc không phù hợp, có thể:
    - Bình luận (comment out) hoặc xóa đoạn CSS gây xung đột.
    - Thêm lớp `dropdown-item` vào liên kết trong dropdown để sử dụng kiểu dáng mặc định của Bootstrap:
      ```html
      <a class="dropdown-item" asp-area="Admin" asp-controller="Category" asp-action="Index">Category</a>
      ```
  - Lưu tệp và thực hiện **hard reload** (Ctrl + F5) trên trình duyệt để kiểm tra giao diện.
- **Kết quả**:
  - Dropdown hiển thị đúng, với văn bản màu tối (dark text) và kiểu dáng phù hợp.

### 3. Tinh chỉnh Dropdown
- **Sửa đổi nội dung**:
  - Đổi tên dropdown từ "Dropdown" thành "Content Management" để phù hợp với ngữ cảnh quản lý nội dung.
  - Di chuyển liên kết "Category" vào dropdown và đảm bảo các liên kết khác (nếu có) được tổ chức hợp lý.
  - Giữ dòng phân cách (`<hr class="dropdown-divider">`) để phân tách các mục trong dropdown.
- **Cập nhật giao diện**:
  - Nếu văn bản không hiển thị đúng màu, thêm lớp CSS như `text-dark` hoặc sử dụng `dropdown-item` để áp dụng kiểu dáng Bootstrap.
  - Ví dụ:
    ```html
    <a class="dropdown-item text-dark" asp-area="Admin" asp-controller="Category" asp-action="Index">Category</a>
    ```
- **Kết quả**: Dropdown hiển thị đẹp mắt, với các mục được tổ chức rõ ràng.

### 4. Kiểm tra hoạt động
- **Thử nghiệm**:
  - Chạy dự án và kiểm tra thanh điều hướng:
    - Dropdown "Content Management" hiển thị với mục "Category".
    - Nhấp vào "Category" dẫn đến trang danh mục trong Admin Area (`/Admin/Category/Index`).
  - **Kết quả**: Dropdown hoạt động đúng, giao diện hiển thị nhất quán.

### 5. Ghi chú thêm
- **Lợi ích**:
  - Dropdown giúp tổ chức các liên kết liên quan (như Category, Product trong tương lai) trong một menu gọn gàng.
  - Sử dụng Bootstrap đảm bảo giao diện thân thiện và dễ tùy chỉnh.
- **Lưu ý thực tế**:
  - Đảm bảo Bootstrap được tích hợp đúng trong dự án (thêm qua CDN hoặc NuGet package).
  - Kiểm tra CSS để tránh xung đột kiểu dáng, đặc biệt khi sử dụng CSS isolation trong .NET.
  - Khi thêm các mục mới (như Product), cập nhật dropdown với liên kết tương ứng và kiểm tra định tuyến.
- **Đề xuất**:
  - Liên kết ghi chú này với các ghi chú về **Bootstrap trong ASP.NET**, **ASP.NET Routing**, hoặc **Areas** trong Obsidian để tra cứu chéo.

---

