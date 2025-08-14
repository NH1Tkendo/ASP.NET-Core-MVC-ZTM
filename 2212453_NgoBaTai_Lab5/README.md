#Các khó khăn trong quá trình làm bài và cách giải quyết

## Câu 1

B1: Copy project MvcBlog từ Lab 3 và đặt ở vị trí cùng cấp với solution hiện tại

Trong Visual Studio 
* Chuột phải vào solution và chọn **Add existing project -> Chọn file .csjpro của dự án vừa mới copy vào**
* Chuột phải vào solution chọn **Restore nuget packages**
* Build lại toàn bộ solution

B2: Vào SSMS thêm các thuộc tính như yêu cầu

B3: Mở file EF.edmx trong Entity Framework Designer.
* Ở khu vực trống → Right-click → chọn Update Model from Database….
* Trong tab Refresh: Chọn bảng Blog và Post.
* Nhấn Finish.
* Build lại EntityModel để tạo lại các file .Designer.cs.

B4: Cập nhật lại Controller và View
* Xóa Controller và View liên quan đến Blog và Post trong dự án MvcBlog.
* Scaffold lại:
 * Right-click thư mục Controllers → Add → Controller.
 * Chọn MVC 5 Controller with views, using Entity Framework.
 * Chọn Model (Blog hoặc Post) và DbContext trong EntityModel.
 * Tạo mới Views.
