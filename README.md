🎾 Pickleball Club Management – Vợt Thủ Phố Núi

Sinh viên: Dương Văn Việt
MSSV: 1771020743
Lớp: CNTT 17-08
Năm học: 2026

Hệ thống quản lý CLB Pickleball hoàn chỉnh gồm Backend (ASP.NET Core 8 Web API), Frontend (Flutter Mobile/Web) và Database (PostgreSQL).
Dự án phục vụ quản lý thành viên, đặt sân, giải đấu, ví điện tử và dashboard thống kê cho CLB.

📁 Cấu trúc dự án
bai_kiem_tra_nang_cao/
├── Backend/                      # ASP.NET Core Web API 8.0
│   ├── Controllers/              # Các API Controller (Admin, Booking, Wallet...)
│   ├── Models/                   # Entity Models (prefix 734_)
│   ├── Data/                     # DbContext + Seeder (PostgreSQL)
│   ├── DTOs/                     # Data Transfer Objects
│   ├── Hubs/                     # SignalR Hub (Real-time)
│   ├── Services/                 # Background Services
│   └── Program.cs                # CORS, JWT, Swagger, DI
│
└── Frontend/                     # Flutter Mobile / Web App
    ├── lib/
    │   ├── models/               # Dart Models
    │   ├── providers/            # State Management (Provider)
    │   ├── screens/              # UI Screens (Admin, Booking, Wallet...)
    │   ├── services/             # API (Dio) + SignalR Client
    │   └── widgets/              # Reusable Widgets & Charts
    └── pubspec.yaml

🛠️ Tech Stack
🔧 Backend

Framework: ASP.NET Core 8 Web API

Database: PostgreSQL (Entity Framework Core – Code First)

Authentication: JWT Bearer Token

Real-time: SignalR (WebSocket)

API Docs: Swagger / OpenAPI

📱 Frontend

Framework: Flutter 3.x (Mobile & Web)

State Management: Provider

HTTP Client: Dio

Real-time: SignalR Client

Charts: FL Chart

Secure Storage: Flutter Secure Storage

🚀 Hướng dẫn cài đặt & chạy dự án
1️⃣ Database (PostgreSQL)

Cài đặt PostgreSQL và đảm bảo service đang chạy

Kiểm tra Backend/appsettings.json (mặc định):

User: postgres

Password: 123456

📌 Khi chạy Backend lần đầu, hệ thống sẽ tự động:

Tạo database Pcm734Database

Tạo toàn bộ bảng

Seed dữ liệu mẫu (Users, Members, Courts, Wallet, Tournaments…)

2️⃣ Chạy Backend API
cd bai_kiem_tra_nang_cao/Backend

dotnet restore
$env:ASPNETCORE_ENVIRONMENT='Development'
dotnet run


✅ API URL: http://localhost:5000
✅ Swagger UI: http://localhost:5000/swagger

3️⃣ Chạy Frontend Flutter
🔧 Cấu hình API URL

File: Frontend/lib/services/api_service.dart

static const String baseUrl = 'http://localhost:5000/api';

▶️ Run App
cd bai_kiem_tra_nang_cao/Frontend

flutter pub get
flutter run -d chrome


(Hoặc chạy Windows Desktop nếu đã cài Visual Studio C++ workload)

👤 Tài khoản Demo
Email	Password	Role	Chức năng
admin@pcm.com
	Admin@123	Admin	Dashboard, quản lý hệ thống
treasurer@pcm.com
	Treasurer@123	Treasurer	Duyệt nạp tiền, thống kê
referee@pcm.com
	Referee@123	Referee	Cập nhật kết quả
member1@pcm.com
	Member@123	Member	Đặt sân, ví cá nhân

📌 Có 17 tài khoản Member từ member1 → member17

📱 Chức năng chính
💼 Admin Dashboard

Thống kê tổng quỹ & doanh thu

Biểu đồ thu/chi 12 tháng

Duyệt yêu cầu nạp tiền

Thống kê thành viên, booking, giải đấu

🏆 Booking & Giải đấu

Đặt sân theo lịch

Thanh toán bằng ví

Đặt sân định kỳ (VIP/Diamond)

Tạo giải đấu (Round Robin / Knockout)

Cập nhật tỉ số real-time

💰 Ví điện tử (Wallet)

Nạp tiền (upload ảnh)

Lịch sử giao dịch

Tích điểm & xếp hạng thành viên

Ưu đãi theo Tier

🔔 Real-time & Tự động

Thông báo real-time

Tự hủy booking chưa thanh toán

Nhắc lịch trước 24h

🔧 Lỗi thường gặp & cách xử lý
❌ Backend không chạy (port 5000)

Nguyên nhân: Port đã bị chiếm

Fix: Tắt instance cũ hoặc đổi port

❌ Flutter lỗi Connection Refused

Kiểm tra Backend đang chạy

Kiểm tra baseUrl

❌ Lỗi CORS

Backend đã cấu hình AllowAll (dev)

🎓 Thông tin sinh viên

Họ tên: Dương Văn Việt

MSSV: 1771020743

Lớp: CNTT 17-08

Năm: 2026
