# School Administration & Management Dashboard

A professional, high-performance desktop application designed for educational institutions to streamline administrative operations, faculty tracking, student registries, automated attendance management, and real-time announcements.

This enterprise-grade system combines the rapid development capabilities of **Object-Relational Mapping (ORM)** with the high-performance control of **native, bare-metal database architecture**.

---

## 🛠️ Tech Stack & Architecture

* **Frontend UI:** Windows Presentation Foundation (WPF), XAML, Custom Styles & Component-Level Corner Customizations.
* **Backend Framework:** .NET Core / C# Core Framework.
* **Database Management:** Microsoft SQL Server (T-SQL Engine).
* **Primary Data Layer:** Entity Framework Core (Code-First Approach via DbContext).
* **Advanced Features Layer:** Bare-Metal **ADO.NET** Core Engine (`SqlConnection`, `SqlCommand`, `SqlDataReader`).

---

## 🚀 Key Features

### 1. Unified Control Dashboard
* **KPI Metric Analytics:** Real-time summary counters parsing total registered students, active faculty counts, class distributions, and automated calculations for overall **Daily Attendance Rates**.
* **Live System Logs:** Interactive background diagnostic tracker monitoring system processes and database refresh sequences with explicit timestamps.

### 2. Dual-Layer Data Access Layer (CRUD Architecture)
* **Enterprise Automation:** High-level entities (Students, Classes, Faculty Registry) are safely managed using Entity Framework Core, featuring cascading link-tracking (e.g., automatically unassigning students cleanly when a teacher profile is modified or deleted).
* **Bare-Metal Performance System:** The system includes a dedicated **Notice Board Engine** built entirely bypass-layer using raw **T-SQL strings and parameters**. This displays optimal query execution performance and explicit defensive coding mechanics.

### 3. Automated Attendance Management
* **Dynamic Registries:** Automatically builds attendance check sheets based on real-time database state.
* **Historical Data Filters:** Integrated date-picker engine parsing complex transactional records allowing administrators to fetch historical statistics seamlessly.

### 4. Advanced Security Framework
* **SQL Injection Defensive Layer:** Deep sanitization via explicit parameterized T-SQL placeholders (`@title`, `@content`, `@date`) preventing database manipulation attacks.
* **State Failure Handling:** Safe data transformation through strict asynchronous validation loops (`int.TryParse` conversions) ensuring immune states against application-level parsing failures.

---

## 📂 Project Structure Directory

```text
School_Management_System/
├── DatabaseAccess/
│   ├── EntityFramework/
│   │   ├── Entities/              # Data Models (Student, Teacher, Notice, ClassRecord)
│   │   └── SchoolDbContext.cs     # Automated ORM Database Context 
│   └── Repository/
│       ├── StudentRepo.cs         # EF-based CRUD Transactions
│       ├── AttendanceRepo.cs      # Complex Attendance Business Logic
│       └── NoticeRepo.cs          # Bare-Metal Native T-SQL ADO.NET Data Engine
├── Converters/
│   └── InverseBoolConverter.cs    # WPF Two-Way UI Data Binding Converters
├── Views/                         # Sub-Windows & Dynamic Modal Views
│   ├── LoginWindow.xaml           # Entry Gatekeeper Authentication View
│   ├── AddStudentWindow.xaml      # Registry Modal View
│   └── AddClassWindow.xaml
├── MainWindow.xaml                # Refactored High-Tier Layout Shell
└── MainWindow.xaml.cs             # Core Presentation Control Logic



🔌 T-SQL Database Schema Blueprint
To establish the bare-metal database layer alongside your Entity Framework schema, execute this optimized script inside your SQL Server Management Studio (SSMS) target instance:
USE SchoolManagementDB;
GO

IF OBJECT_ID('dbo.Notices', 'U') IS NOT NULL
    DROP TABLE dbo.Notices;
GO

CREATE TABLE Notices (
    Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Title NVARCHAR(100) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    DatePosted DATETIME DEFAULT GETDATE() NOT NULL
);
GO


⚙️ Connection Configurations
To safely synchronize your native data processing repositories with your Entity Framework structure, map your database engine instances uniformly using standard SQL authentication or local integrated Windows configurations:

C#
private readonly string _connectionString = @"Server=YOUR_SERVER_INSTANCE;Database=SchoolManagementDB;Trusted_Connection=True;TrustServerCertificate=True;";
🤝 Professional Implementation Notes
💡 Developer Architectural Insight:
This project intentionally presents two opposing architectural standards in enterprise software development. While Entity Framework Core handles heavy relationships, raw ADO.NET ensures total bare-metal hardware control, thread-safe memory handling (using blocks that clean up open connection resources automatically), and ultimate runtime speed optimization. Use this deployment model to show potential stakeholders your absolute mastery over high-level abstraction frameworks and raw database execution concepts alike.
