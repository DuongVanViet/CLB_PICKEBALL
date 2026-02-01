# 🎾 Pickleball Club Management – Vợt Thủ Phố Núi

> **Hệ thống quản lý CLB Pickleball hoàn chỉnh** | Backend (ASP.NET Core 8) + Frontend (Flutter) + Database (PostgreSQL)

---

## 👨‍🎓 Thông Tin Sinh Viên

| Thông tin | Chi tiết |
|-----------|---------|
| **Họ tên** | Dương Văn Việt |
| **MSSV** | 1771020743 |
| **Lớp** | CNTT 17-08 |
| **Năm học** | 2026 |

---

## 📋 Mô Tả Dự Án

Hệ thống quản lý toàn diện cho Câu lạc bộ Pickleball, cung cấp các chức năng:

- 👥 **Quản lý thành viên** - Cơ sở dữ liệu, hạng thành viên, lịch sử hoạt động
- 🏟️ **Đặt sân** - Lịch sân, thanh toán, đặt định kỳ (VIP/Diamond)
- 🏆 **Giải đấu** - Tạo giải (Round Robin/Knockout), cập nhật tỉ số real-time
- 💰 **Ví điện tử** - Nạp tiền, lịch sử giao dịch, tích điểm, xếp hạng
- 📊 **Dashboard Admin** - Thống kê tổng quỹ, biểu đồ doanh thu 12 tháng
- 🔔 **Thông báo real-time** - SignalR WebSocket, tự động hủy booking chưa thanh toán

---

## 🏗️ Cấu Trúc Dự Án

```
CLB_PICKEBALL-main/
├── Backend/                          # ASP.NET Core Web API 8.0
│   ├── Controllers/                  # API Controllers (Admin, Booking, Wallet, etc)
│   │   ├── AdminController.cs
│   │   ├── AuthController.cs
│   │   ├── BookingsController.cs
│   │   ├── CourtsController.cs
│   │   ├── MatchesController.cs
│   │   ├── MembersController.cs
│   │   ├── NewsController.cs
│   │   ├── NotificationsController.cs
│   │   ├── TournamentsController.cs
│   │   └── WalletController.cs
│   ├── Models/                       # Entity Models (prefix 734_)
│   ├── Data/
│   │   ├── ApplicationDbContext.cs   # DbContext
│   │   └── DbSeeder.cs              # Dữ liệu mẫu
│   ├── DTOs/                         # Data Transfer Objects
│   ├── Hubs/
│   │   └── PcmHub.cs               # SignalR Hub (Real-time)
│   ├── Services/                     # Background Services
│   ├── Migrations/                   # EF Core Migrations
│   ├── Program.cs                    # CORS, JWT, Swagger, DI
│   ├── appsettings.json             # Config
│   └── PcmBackend.csproj
│
├── Frontend/                         # Flutter Mobile / Web App
│   ├── lib/
│   │   ├── main.dart
│   │   ├── models/                  # Dart Models
│   │   ├── providers/               # State Management (Provider)
│   │   ├── screens/                 # UI Screens (Admin, Booking, Wallet)
│   │   ├── services/                # API Service (Dio) + SignalR Client
│   │   └── widgets/                 # Reusable Widgets & Charts
│   ├── assets/                      # Hình ảnh, icon
│   ├── android/                     # Android configuration
│   ├── ios/                         # iOS configuration
│   ├── web/                         # Web configuration
│   ├── windows/                     # Windows Desktop
│   ├── pubspec.yaml
│   └── README.md
│
├── Database/                        # PostgreSQL Schema
│   ├── database_schema.sql
│   └── POSTGRESQL_SETUP.md
│
└── Documentation/
    ├── README.md
    └── API Docs (Swagger)
```

---

## 🛠️ Tech Stack

### Backend
| Công nghệ | Phiên bản | Mục đích |
|----------|---------|---------|
| ASP.NET Core | 8.0 | Web API Framework |
| Entity Framework Core | 8.x | ORM & Database |
| PostgreSQL | - | Database |
| JWT | - | Authentication |
| SignalR | - | Real-time Communication |
| Swagger/OpenAPI | - | API Documentation |

### Frontend
| Công nghệ | Phiên bản | Mục đích |
|----------|---------|---------|
| Flutter | 3.x | Cross-platform UI |
| Provider | - | State Management |
| Dio | - | HTTP Client |
| SignalR Client | - | Real-time Connection |
| FL Chart | - | Data Visualization |
| Flutter Secure Storage | - | Secure Token Storage |

### Database
| Công nghệ | Phiên bản | Mục đích |
|----------|---------|---------|
| PostgreSQL | 12+ | Relational Database |
| PgAdmin | - | Database Management |

---

## 🚀 Hướng Dẫn Cài Đặt & Chạy Dự Án

### 📋 Yêu Cầu Hệ Thống

- **OS**: Windows 10+ / macOS / Linux
- **.NET SDK**: 8.0+
- **Flutter SDK**: 3.x+
- **PostgreSQL**: 12+
- **Node.js** (optional, cho Frontend Web)

---

### 1️⃣ Cấu Hình Database (PostgreSQL)

#### Bước 1: Cài đặt PostgreSQL
```bash
# Windows - Download từ https://www.postgresql.org/download/windows/
# macOS
brew install postgresql@15

# Linux (Ubuntu)
sudo apt-get install postgresql postgresql-contrib
```

#### Bước 2: Kiểm tra thông tin kết nối
Mở file `Backend/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=5432;Database=Pcm734Database;User Id=postgres;Password=123456;"
  }
}
```

**Thông tin mặc định:**
- **Server**: localhost
- **Port**: 5432
- **User**: postgres
- **Password**: 123456 *(Thay đổi theo cài đặt của bạn)*

#### Bước 3: Tự động khởi tạo Database
Khi Backend chạy lần đầu, hệ thống sẽ tự động:
- ✅ Tạo database `Pcm734Database`
- ✅ Tạo toàn bộ bảng từ migrations
- ✅ Seed dữ liệu mẫu (Users, Members, Courts, Wallet, Tournaments, etc)

---

### 2️⃣ Chạy Backend API

```bash
# Cd vào thư mục Backend
cd Backend

# Restore dependencies
dotnet restore

# Set environment
$env:ASPNETCORE_ENVIRONMENT='Development'

# Run backend
dotnet run
```

**Kết quả:**
```
✅ API URL: http://localhost:5000
✅ Swagger UI: http://localhost:5000/swagger
```

---

### 3️⃣ Chạy Frontend Flutter

#### Bước 1: Cấu hình API URL
Mở file `Frontend/lib/services/api_service.dart`:

```dart
class ApiService {
  static const String baseUrl = 'http://localhost:5000/api';
  // Hoặc thay đổi IP nếu chạy trên máy khác
  // static const String baseUrl = 'http://192.168.x.x:5000/api';
}
```

#### Bước 2: Chạy ứng dụng
```bash
# Cd vào thư mục Frontend
cd Frontend

# Fetch dependencies
flutter pub get

# Run trên Chrome (Web)
flutter run -d chrome

# Hoặc chạy trên Android emulator
flutter run -d emulator-5554

# Hoặc Windows Desktop (cần Visual Studio C++ workload)
flutter run -d windows
```

---

## 👤 Tài Khoản Demo

### Admin & Roles

| Email | Password | Role | Chức năng |
|-------|----------|------|----------|
| `admin@pcm.com` | `Admin@123` | Admin | Dashboard, quản lý hệ thống |
| `treasurer@pcm.com` | `Treasurer@123` | Treasurer | Duyệt nạp tiền, thống kê |
| `referee@pcm.com` | `Referee@123` | Referee | Cập nhật kết quả trận |

### Member Accounts

```
member1@pcm.com → member17@pcm.com
Password: Member@123
```

---

## 💼 Chức Năng Chính

### 📊 Admin Dashboard
- Thống kê tổng quỹ & doanh thu hiện tại
- Biểu đồ thu/chi 12 tháng
- Duyệt yêu cầu nạp tiền từ thành viên
- Thống kê thành viên, booking, giải đấu
- Quản lý tin tức & thông báo

### 🏟️ Booking & Giải Đấu
- Xem danh sách sân theo lịch
- Đặt sân và thanh toán qua ví
- Đặt sân định kỳ (VIP: 4 sân/tháng, Diamond: 8 sân/tháng)
- Tạo giải đấu (Round Robin / Knockout)
- Cập nhật tỉ số trận đấu real-time

### 💰 Ví Điện Tử
- Nạp tiền (upload ảnh chứng minh)
- Lịch sử giao dịch chi tiết
- Tích điểm & xếp hạng thành viên
- Ưu đãi theo Tier (Member, VIP, Diamond)

### 🔔 Real-time & Automation
- Thông báo real-time qua SignalR
- Tự động hủy booking chưa thanh toán trong 2 giờ
- Nhắc lịch trước 24 giờ
- Cập nhật kết quả giải đấu tức thì

---

## 🔧 Lỗi Thường Gặp & Cách Xử Lý

### ❌ Backend không chạy (Connection refused on port 5000)

**Nguyên nhân:** Port 5000 đã bị chiếm hoặc firewall chặn

**Giải pháp:**
```bash
# Windows: Tìm process chiếm port 5000
netstat -ano | findstr :5000

# Tắt process (thay <PID> bằng số lấy được)
taskkill /PID <PID> /F

# Hoặc đổi port trong Program.cs
app.Run("http://localhost:5001");
```

### ❌ Flutter lỗi Connection Refused

**Nguyên nhân:** Backend không chạy hoặc baseUrl sai

**Giải pháp:**
1. Kiểm tra Backend đang chạy: `http://localhost:5000/swagger`
2. Kiểm tra baseUrl trong `Frontend/lib/services/api_service.dart`
3. Nếu chạy trên thiết bị khác, dùng IP thực: `http://192.168.x.x:5000/api`

### ❌ Lỗi CORS (Cross-Origin)

**Nguyên nhân:** Frontend và Backend khác domain

**Giải pháp:** Backend đã cấu hình AllowAll cho development (xem `Program.cs`)

### ❌ PostgreSQL connection failed

**Nguyên nhân:** Service PostgreSQL chưa chạy

**Giải pháp:**
```bash
# Windows
net start postgresql-x64-15

# macOS
brew services start postgresql@15

# Linux
sudo service postgresql start
```

---

## 📚 Tài Liệu Bổ Sung

- [Backend Setup & API Docs](Backend/README.md)
- [Frontend Setup](Frontend/README.md)
- [PostgreSQL Setup](Backend/POSTGRESQL_SETUP.md)
- [API Swagger](http://localhost:5000/swagger) - Khi backend đang chạy
- [Database Schema](Backend/database_schema.sql)

---

## 🔐 Security Notes

- ⚠️ **JWT Secret** được định nghĩa trong `appsettings.json` - thay đổi trước production
- ⚠️ **Database password** trong config - sử dụng Environment Variables trên production
- ⚠️ **CORS** được cấu hình AllowAll cho development - giới hạn domain trên production
- ⚠️ **Token secure storage** sử dụng Flutter Secure Storage

---

## 📝 License

Dự án học tập - Câu lạc bộ Pickleball CLB 2026

---

## 📧 Contact & Support

**Sinh viên:**
- Dương Văn Việt (1771020743)
- Email: viet.duong@student.edu.vn

**Giáo viên hướng dẫn:**
- [Tên giáo viên] - [Email]

---

## ✨ Ghi Chú

Dự án này được phát triển như một bài kiểm tra nâng cao, áp dụng các kỹ thuật hiện đại:
- ✅ Clean Architecture (Backend)
- ✅ State Management (Frontend)
- ✅ Real-time Communication (SignalR)
- ✅ JWT Authentication
- ✅ Database Migrations & Seeding
- ✅ API Documentation (Swagger)


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
