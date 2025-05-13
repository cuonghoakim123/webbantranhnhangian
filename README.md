# Website Bán Tranh Dân Gian

## Giới thiệu

Website Bán Tranh Dân Gian là một ứng dụng web được xây dựng bằng ASP.NET Core MVC, C# và SQL Server. Dự án này là một nền tảng thương mại điện tử chuyên dụng để bán các loại tranh dân gian Việt Nam như tranh Đông Hồ, tranh Hàng Trống, tranh Kim Hoàng và tranh làng Sình.

## Chức năng chính

1. **Phía người dùng:**

   - Đăng ký, đăng nhập tài khoản
   - Xem danh sách sản phẩm theo danh mục
   - Tìm kiếm sản phẩm
   - Xem chi tiết sản phẩm
   - Thêm sản phẩm vào giỏ hàng
   - Thanh toán đơn hàng
   - Quản lý thông tin cá nhân

2. **Phía quản trị:**
   - Quản lý danh mục sản phẩm
   - Quản lý sản phẩm
   - Quản lý đơn hàng
   - Quản lý người dùng

## Yêu cầu hệ thống

- .NET 9.0
- SQL Server
- Laragon/IIS

## Cài đặt

1. Clone repository về máy local
2. Mở dự án bằng Visual Studio
3. Cấu hình kết nối đến SQL Server trong file `appsettings.json`
4. Chạy ứng dụng

## Khởi chạy dự án

1. Đảm bảo SQL Server đã được cài đặt và đang chạy
2. Mở terminal hoặc command prompt tại thư mục gốc của dự án
3. Di chuyển đến thư mục WebBanTranhDanGian bằng lệnh: `cd WebBanTranhDanGian`
4. Khôi phục các package bằng lệnh: `dotnet restore`
5. Khởi chạy ứng dụng bằng lệnh: `dotnet run`
6. Mở trình duyệt và truy cập:
   - http://localhost:5065/

Lưu ý: Trên Windows PowerShell, sử dụng dấu chấm phẩy (`;`) thay vì `&&` để chạy nhiều lệnh, ví dụ: `cd WebBanTranhDanGian; dotnet run`

Khi ứng dụng chạy lần đầu, database sẽ tự động được tạo và seed dữ liệu mẫu.

## Cấu trúc dự án

- **Models:** Chứa các model cho dữ liệu
- **Controllers:** Chứa các controller xử lý logic
- **Views:** Chứa các view hiển thị giao diện người dùng
- **DbInitializer.cs:** Khởi tạo và seed dữ liệu cho database

## Công nghệ sử dụng

- ASP.NET Core MVC (.NET 9)
- Entity Framework Core
- C#
- SQL Server
- HTML/CSS/JavaScript
- Bootstrap

## Tài khoản mặc định

- **Admin:**
  - Email: admin@webbantranhdangian.com
  - Password: Admin123

## Người phát triển - Thông tin liên hệ và hỗ trợ

Nếu bạn cần hỗ trợ hoặc có câu hỏi, vui lòng liên hệ:

- Email: cuonghotran17022004@gmail.com
- zalo: 0355999141

---

- Phát triển bởi Trang Web Bán Tranh Dân Gian
