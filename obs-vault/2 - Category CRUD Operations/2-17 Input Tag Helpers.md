## Ghi chú học tập: Tăng cường biểu mẫu với Tag Helpers và Data Annotations

### Khái niệm cơ bản
- Mục đích: Tăng cường biểu mẫu trong `Create.cshtml` bằng cách sử dụng **Tag Helpers** của ASP.NET Core để tự động liên kết các trường nhập liệu với thuộc tính của model, và sử dụng **Data Annotations** để tùy chỉnh tên hiển thị trên giao diện.
- Phương pháp: 
  - Sử dụng Tag Helpers (`asp-for`) để liên kết các trường nhập liệu và nhãn với thuộc tính của model `Category`.
  - Thêm Data Annotations trong model `Category` để định dạng tên hiển thị của các thuộc tính (`Name`, `DisplayOrder`).
- Công cụ: Tag Helpers (tăng tính động cho biểu mẫu), Data Annotations (tùy chỉnh giao diện và xác thực), Razor View (`Create.cshtml`).

### Các bước thực hiện

#### 1. Sử dụng Tag Helpers trong Create.cshtml
- Trong file `Create.cshtml`, sử dụng Tag Helper `asp-for` để liên kết các thẻ `<input>` và `<label>` với các thuộc tính của model `Category` (`Name`, `DisplayOrder`).
- **Lợi ích của `asp-for`**:
  - Tự động liên kết trường nhập liệu với thuộc tính model, loại bỏ nhu cầu gán thủ công thuộc tính `name`.
  - Tự động xác định kiểu dữ liệu (ví dụ: `text` cho `Name`, `number` cho `DisplayOrder`) dựa trên kiểu thuộc tính trong model.
  - Tăng khả năng tương tác: Nhấp vào nhãn (`<label>`) sẽ tự động tập trung (focus) vào trường nhập liệu tương ứng.
- Cập nhật biểu mẫu:
  ```cshtml
  <form method="post">
      <div class="mb-3 p-0">
          <label asp-for="Name"></label>
          <input asp-for="Name" class="form-control" />
      </div>
      <div class="mb-3 p-0">
          <label asp-for="DisplayOrder"></label>
          <input asp-for="DisplayOrder" class="form-control" />
      </div>
      <!-- Các phần còn lại của biểu mẫu -->
  </form>
  ```
- **Lưu ý**:
  - `asp-for="Name"` liên kết với thuộc tính `Name` của model `Category`. Nếu thuộc tính không tồn tại (ví dụ: `Name1`), trình biên dịch sẽ báo lỗi.
  - Không cần chỉ định thuộc tính `type` cho `<input>`, vì Tag Helper tự động suy ra từ kiểu dữ liệu của thuộc tính (ví dụ: `string` cho `Name`, `int` cho `DisplayOrder`).

#### 2. Thêm Data Annotations trong Model Category
- Mở file model `Category.cs` để thêm **Data Annotations** nhằm tùy chỉnh tên hiển thị của các thuộc tính trên giao diện.
- Sử dụng thuộc tính `[Display(Name = "...")]` để định nghĩa tên hiển thị có dấu cách hoặc mô tả thân thiện hơn.
- Mã nguồn:
  ```csharp
  using System.ComponentModel.DataAnnotations;

  public class Category
  {
      public int Id { get; set; }

      [Display(Name = "Category Name")]
      public string Name { get; set; }

      [Display(Name = "Display Order")]
      public int DisplayOrder { get; set; }
  }
  ```
- **Kết quả**:
  - Nhãn (`<label>`) trong biểu mẫu sẽ hiển thị "Category Name" thay vì "Name" và "Display Order" thay vì "DisplayOrder".
  - Giao diện trở nên thân thiện hơn với người dùng.

#### 3. Kiểm tra giao diện
- Sau khi cập nhật `Create.cshtml` và `Category.cs`, lưu các thay đổi.
- Nhờ **Hot Reload**, giao diện tự động làm mới (trong một số trường hợp, cần khởi động lại ứng dụng để áp dụng thay đổi model).
- Kết quả:
  - Biểu mẫu hiển thị nhãn "Category Name" và "Display Order".
  - Nhấp vào nhãn sẽ tự động tập trung vào trường nhập liệu tương ứng.
  - Trường `DisplayOrder` được tự động gán kiểu `type="number"` do Tag Helper nhận diện thuộc tính `int`.

#### 4. Đảm bảo giao diện đáp ứng
- Biểu mẫu đã được thiết kế với các lớp Bootstrap (`form-control`, `col-6`, `col-md-3`) để đảm bảo đáp ứng trên các kích thước màn hình.
- Mã nguồn đầy đủ của biểu mẫu:
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
                  <label asp-for="Name"></label>
                  <input asp-for="Name" class="form-control" />
              </div>
              <div class="mb-3 p-0">
                  <label asp-for="DisplayOrder"></label>
                  <input asp-for="DisplayOrder" class="form-control" />
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

### Ghi chú thêm
- **Tag Helpers**: Công cụ mạnh mẽ của ASP.NET Core, giúp đơn giản hóa việc liên kết biểu mẫu với model, giảm mã thủ công và tăng tính bảo trì.
- **Data Annotations**: Không chỉ hỗ trợ xác thực phía máy khách (client-side validation) mà còn tùy chỉnh giao diện (như tên hiển thị).
- **Tương tác nhãn-nhập liệu**: Tag Helper `asp-for` tự động liên kết nhãn và trường nhập liệu, cải thiện trải nghiệm người dùng.
- **Hot Reload**: Có thể yêu cầu khởi động lại ứng dụng khi thay đổi model hoặc controller, nhưng hoạt động tốt với các thay đổi trong view.
- Trong bài học tiếp theo: Xử lý hành động `POST` để lưu danh mục mới vào cơ sở dữ liệu.

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
                <label asp-for="Name"></label>
                <input asp-for="Name" class="form-control" />
            </div>
            <div class="mb-3 p-0">
                <label asp-for="DisplayOrder"></label>
                <input asp-for="DisplayOrder" class="form-control" />
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

**Category.cs**:
```csharp
using System.ComponentModel.DataAnnotations;

public class Category
{
    public int Id { get; set; }

    [Display(Name = "Category Name")]
    public string Name { get; set; }

    [Display(Name = "Display Order")]
    public int DisplayOrder { get; set; }
}
```

### Thuật ngữ chuyên ngành
- **Tag Helper**: Công cụ ASP.NET Core liên kết biểu mẫu với model (ASP.NET Core form-model binding tool).
- **asp-for**: Thuộc tính Tag Helper liên kết trường nhập liệu/nhãn với thuộc tính model (Tag Helper attribute for model binding).
- **Data Annotations**: Ghi chú dữ liệu để xác thực và tùy chỉnh giao diện (data attributes for validation and UI customization).
- **Display(Name)**: Thuộc tính Data Annotation định nghĩa tên hiển thị (attribute for display name customization).
- **form-control**: Lớp Bootstrap định dạng trường nhập liệu (Bootstrap class for input styling).