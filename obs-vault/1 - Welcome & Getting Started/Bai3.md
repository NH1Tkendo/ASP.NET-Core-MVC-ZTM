## Tổng Quan Về ASP.NET Core và Sự Phát Triển Của .NET

### Lịch Sử Phát Triển .NET
- **2002: Ra mắt Web Forms**:
  - Microsoft giới thiệu Web Forms (Biểu mẫu Web), một bước đột phá trong lập trình .NET.
  - **Hạn chế**: Web Forms có nhiều nhược điểm, cần được khắc phục.
- **Sự ra đời của .NET MVC**:
  - Để giải quyết các hạn chế của Web Forms, nhóm .NET phát triển kiến trúc MVC (Model-View-Controller).
  - **Hạn chế của MVC**:
    - Được xây dựng dựa trên các thành phần của Web Forms.
    - Gắn chặt với IIS (Internet Information Services) và hệ điều hành Windows.
- **2016: Ra mắt ASP.NET Core**:
  - Tháng 6/2016, Microsoft phát hành ASP.NET Core, phiên bản đầu tiên của .NET Core.
  - **Đặc điểm**:
    - Được viết lại hoàn toàn, không phụ thuộc vào Windows, hỗ trợ đa nền tảng (cross-platform).
    - Được thiết kế với kiến trúc đám mây (cloud architecture), mạnh mẽ và linh hoạt.
- **Các phiên bản tiếp theo**:
  - **Tháng 8/2018**: Ra mắt .NET Core 2.0.
  - **Hàng năm**: Microsoft cam kết phát hành phiên bản mới vào tháng 11 mỗi năm.
    - **2022**: .NET Core 7.
    - **2023**: .NET Core 8 (bản xem trước có sẵn qua Visual Studio Preview).
- **Khóa học này**: Sử dụng phiên bản xem trước của .NET Core 8.

### Lý Do Nên Học ASP.NET Core
- **Hiệu năng cao**:
  - .NET Core nhanh hơn đáng kể so với Web Forms và .NET MVC truyền thống, theo các bài kiểm tra hiệu suất (benchmark).
- **Mã nguồn mở (Open Source)**:
  - .NET Core là mã nguồn mở, cho phép cộng đồng đóng góp và tùy chỉnh.
- **Hỗ trợ đa nền tảng (Cross-Platform)**:
  - Không còn phụ thuộc vào IIS hoặc Windows, có thể chạy trên Linux, macOS, và các nền tảng khác.
- **Tích hợp Dependency Injection**:
  - Tích hợp sẵn tính năng Dependency Injection (Tiêm phụ thuộc), giúp quản lý phụ thuộc hiệu quả.
  - Tiết kiệm thời gian và tăng hiệu suất phát triển.
- **Dễ dàng nâng cấp**:
  - Các bản cập nhật .NET Core được thiết kế để dễ dàng nâng cấp, đảm bảo ứng dụng luôn tương thích với phiên bản mới.
- **Thân thiện với đám mây (Cloud-Friendly)**:
  - Được xây dựng với kiến trúc đám mây, phù hợp cho các ứng dụng hiện đại triển khai trên đám mây.
- **Hiệu suất vượt trội**:
  - Mỗi phiên bản .NET Core mới đều cải thiện hiệu suất so với phiên bản trước.
- **Cam kết dài hạn từ Microsoft**:
  - Microsoft đầu tư mạnh vào .NET Core, đảm bảo hỗ trợ lâu dài cho công nghệ này.

### Ghi Chú Thêm
- **Tầm quan trọng của Dependency Injection**:
  - Giúp quản lý phụ thuộc dễ dàng, đặc biệt trong các dự án lớn.
  - Sau khóa học, học viên sẽ thấy Dependency Injection là công cụ không thể thiếu.
- **Tính linh hoạt**:
  - .NET Core hỗ trợ tích hợp dễ dàng với các công nghệ đám mây và các nền tảng khác.
- **Khuyến nghị**:
  - Làm quen với các tính năng của .NET Core qua thực hành để hiểu rõ lợi ích.
  - Sử dụng Visual Studio Preview để trải nghiệm các tính năng mới nhất của .NET Core 8.

---

**Lưu ý**: Ghi chú này được tối ưu hóa cho Obsidian, sử dụng định dạng Markdown với cấu trúc rõ ràng, dễ tra cứu, và phù hợp để ôn tập khóa học về ASP.NET Core MVC.