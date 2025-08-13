#Các khó khăn trong quá trình làm bài và cách giải quyết

### Trong thư mục Views tạo View Index mới sử dụng Layout mặc định là Shared/\_Layout.cshtml

B1: Chuột phải vào thư mục Views -> add -> Views

B2: Chọn và đặt tên view

B3: Chọn phần template ở cuối hộp thoại và sau đó chọn như hình

![md_assets/Cau2_Lab3.png]

### Chạy thử trang Index xem kết quả (Chạy lần đầu)

B1: Kéo file Index.cshtml vào trong folder HelloWord trong Views

B2: Đổi tên HelloWord.cs thành HelloWordController.cs

B3: Chạy lại trang

### Chỉnh trong tập tin RouteConfig.cs để có thể chạy đường dẫn

http://localhost:xx/HelloWorld/Welcome/Scott/4

Thêm cấu hình Route trong App\_Start/RouteConfig.cs. Thêm ngay trước route mặc định

``` C#
routes.MapRoute(
  name: "HelloWord",
  url: "HelloWord/Welcome/{name}/{numTimes}",
  defaults: new { controller = "HelloWord", action = "Welcome", name = UrlParameter.Optional, numTimes = UrlParameter.Optional }
);

```

