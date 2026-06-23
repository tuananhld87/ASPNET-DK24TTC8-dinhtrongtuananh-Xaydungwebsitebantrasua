Báo cáo tiến độ Tuần 3 26/05/2026: Dự án Website Bán Trà Sữa
1. Phát triển chức năng quản trị và thống kê
Xây dựng trang Thống kê hệ thống: Phát triển giao diện thống kê dữ liệu trực quan dành cho Admin bao gồm:

Thống kê tổng số lượng đơn hàng và tổng doanh thu trên toàn hệ thống.

Phân loại chi tiết số lượng đơn hàng theo trạng thái: Đang chờ, Đang giao và Đã hoàn thành.

Bảng tổng hợp số lượng món trà sữa bán chạy theo từng danh mục (Categories).

Tối ưu điều hướng: Tích hợp nút điều hướng quay lại trang Quản lý của Admin (AdminDashboard) giúp luồng thao tác của quản trị viên được liền mạch.

2. Hoàn thiện giao diện hệ thống
Thanh điều hướng (Navbar): Cấu hình thuộc tính sticky-top, giúp thanh menu cố định trên đầu trang khi người dùng cuộn xem danh sách trà sữa, tối ưu hóa trải nghiệm người dùng.

Thiết kế lại footer với bố cục cân đối: Hiển thị thông tin cửa hàng, địa chỉ và các liên kết hỗ trợ khách hàng.

Tối ưu hiển thị: Điều chỉnh khoảng cách (padding, margin) để footer hiển thị gọn gàng, không bị chồng lên các thành phần phân trang (pagination) sản phẩm.

3. Cập nhật dữ liệu và kiểm thử
Bổ sung dữ liệu sản phẩm: Đã thêm mới danh sách trà sữa mẫu vào hệ thống, đảm bảo dữ liệu đầy đủ bao gồm hình ảnh, giá bán, mô tả topping và các tùy chọn mức đường/đá.

Kiểm tra tính nhất quán: Đảm bảo các dữ liệu mẫu mới hoạt động ổn định với các bảng Products, Categories và liên kết chính xác với thông tin đơn hàng trong Orders.

4. Tài liệu và Đóng gói
Hướng dẫn cài đặt: Cập nhật chi tiết nội dung file HUONG_DAN_CAI_DAT.md để hỗ trợ quá trình thiết lập môi trường cho website bán trà sữa.

Đồng bộ hóa Database: Cập nhật file SQL script chứa toàn bộ dữ liệu mẫu về thực đơn và khách hàng hiện có của dự án.