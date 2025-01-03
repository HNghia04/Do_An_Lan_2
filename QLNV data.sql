USE [master]
GO
/****** Object:  Database [QuanLy_NhanVien]    Script Date: 11/1/2024 8:11:43 PM ******/
CREATE DATABASE [QuanLy_NhanVien]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuanLy_NhanVien', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\QuanLy_NhanVien.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'QuanLy_NhanVien_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\QuanLy_NhanVien_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [QuanLy_NhanVien] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuanLy_NhanVien].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuanLy_NhanVien] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuanLy_NhanVien] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuanLy_NhanVien] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuanLy_NhanVien] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuanLy_NhanVien] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuanLy_NhanVien] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QuanLy_NhanVien] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuanLy_NhanVien] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuanLy_NhanVien] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuanLy_NhanVien] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuanLy_NhanVien] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuanLy_NhanVien] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuanLy_NhanVien] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuanLy_NhanVien] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuanLy_NhanVien] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QuanLy_NhanVien] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuanLy_NhanVien] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuanLy_NhanVien] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuanLy_NhanVien] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuanLy_NhanVien] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuanLy_NhanVien] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuanLy_NhanVien] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuanLy_NhanVien] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QuanLy_NhanVien] SET  MULTI_USER 
GO
ALTER DATABASE [QuanLy_NhanVien] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuanLy_NhanVien] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuanLy_NhanVien] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuanLy_NhanVien] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [QuanLy_NhanVien] SET DELAYED_DURABILITY = DISABLED 
GO
USE [QuanLy_NhanVien]
GO
/****** Object:  Table [dbo].[Bang_cap]    Script Date: 11/1/2024 8:11:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bang_cap](
	[Ma_bang_cap] [int] NOT NULL,
	[Ten_bang_cap] [nvarchar](50) NULL,
	[Ten_co_so_giao_duc] [nvarchar](50) NULL,
	[Ngay_cap_bang] [datetime] NULL,
	[Hang_bang] [nvarchar](50) NULL,
	[Chuyen_nganh_dao_tao] [nvarchar](50) NULL,
	[Ma_nhan_vien] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Ma_bang_cap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bang_luong]    Script Date: 11/1/2024 8:11:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bang_luong](
	[Ma_bang_luong] [int] NOT NULL,
	[Thang_tinh_luong] [datetime] NULL,
	[Luong_co_ban] [float] NULL,
	[Phu_cap] [float] NULL,
	[Luong_thuc_nhan] [float] NULL,
	[Ma_nhan_vien] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Ma_bang_luong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cham_cong]    Script Date: 11/1/2024 8:11:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cham_cong](
	[Ma_cham_cong] [int] NOT NULL,
	[Ngay_lam_viec] [datetime] NULL,
	[So_gio_lam_viec_trong_ngay] [float] NULL,
	[Ma_nhan_vien] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Ma_cham_cong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Chuc_vu]    Script Date: 11/1/2024 8:11:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chuc_vu](
	[Ma_chuc_vu] [int] NOT NULL,
	[Ten_chuc_vu] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Ma_chuc_vu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Don_xin_thoi_viec]    Script Date: 11/1/2024 8:11:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Don_xin_thoi_viec](
	[Ma_don_xin_thoi_viec] [int] NOT NULL,
	[Ho_ten] [nvarchar](50) NULL,
	[Chuc_vu] [nvarchar](50) NULL,
	[Ngay_nop_don] [datetime] NULL,
	[Ngay_du_kien_thoi_viec] [datetime] NULL,
	[Ly_do] [nvarchar](100) NULL,
	[Trang_thai] [nvarchar](50) NULL,
	[Ma_nhan_vien] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Ma_don_xin_thoi_viec] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hop_dong]    Script Date: 11/1/2024 8:11:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hop_dong](
	[Ma_hop_dong] [int] NOT NULL,
	[Ngay_bat_dau] [datetime] NULL,
	[Ngay_ket_thuc] [datetime] NULL,
	[Muc_luong_co_ban] [float] NULL,
	[Ma_nhan_vien] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Ma_hop_dong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Loai_phu_cap]    Script Date: 11/1/2024 8:11:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Loai_phu_cap](
	[Ma_loai_phu_cap] [int] IDENTITY(1,1) NOT NULL,
	[Ten_loai_phu_cap] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Ma_loai_phu_cap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nghi_phep]    Script Date: 11/1/2024 8:11:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nghi_phep](
	[Ma_nghi_phep] [int] NOT NULL,
	[Ngay_bat_dau_nghi] [datetime] NULL,
	[Ngay_ket_thuc_nghi] [datetime] NULL,
	[Trang_thai] [nvarchar](50) NULL,
	[Ma_nhan_vien] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Ma_nghi_phep] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nhan_vien]    Script Date: 11/1/2024 8:11:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nhan_vien](
	[Ma_nhan_vien] [int] IDENTITY(1,1) NOT NULL,
	[Ho_va_ten] [nvarchar](50) NULL,
	[Gioi_tinh] [nvarchar](3) NULL,
	[So_dien_thoai] [char](11) NULL,
	[Ten_chuc_vu] [nvarchar](50) NULL,
	[Ngay_bat_dau_lam_viec] [datetime] NULL,
	[Ma_phong_ban] [int] NOT NULL,
	[Ma_chuc_vu] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Ma_nhan_vien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phong_ban]    Script Date: 11/1/2024 8:11:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phong_ban](
	[Ma_phong_ban] [int] NOT NULL,
	[Ten_phong_ban] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Ma_phong_ban] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phu_cap]    Script Date: 11/1/2024 8:11:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phu_cap](
	[Ma_phu_cap] [int] NOT NULL,
	[Loai_phu_cap] [nvarchar](50) NULL,
	[So_tien_phu_cap] [float] NULL,
	[Ma_nhan_vien] [int] NOT NULL,
	[Ma_loai_phu_cap] [int] NOT NULL,
 CONSTRAINT [PK__Phu_cap__46FD6023190D5C8F] PRIMARY KEY CLUSTERED 
(
	[Ma_phu_cap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quyen_han]    Script Date: 11/1/2024 8:11:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quyen_han](
	[Ma_quyen_han] [int] NOT NULL,
	[Ten_quyen_han] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Ma_quyen_han] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tai_khoan]    Script Date: 11/1/2024 8:11:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tai_khoan](
	[Ma_tai_khoan] [int] IDENTITY(1,1) NOT NULL,
	[Ten_tai_khoan] [nvarchar](50) NOT NULL,
	[Mat_khau] [nvarchar](255) NOT NULL,
	[Ma_quyen_han] [int] NOT NULL,
	[Ma_nhan_vien] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Ma_tai_khoan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Bang_cap] ([Ma_bang_cap], [Ten_bang_cap], [Ten_co_so_giao_duc], [Ngay_cap_bang], [Hang_bang], [Chuyen_nganh_dao_tao], [Ma_nhan_vien]) VALUES (1, N'Cử nhân', N'Đại học Bách Khoa', CAST(N'2018-07-15T00:00:00.000' AS DateTime), N'Giỏi', N'Kỹ thuật phần mềm', 6)
INSERT [dbo].[Bang_cap] ([Ma_bang_cap], [Ten_bang_cap], [Ten_co_so_giao_duc], [Ngay_cap_bang], [Hang_bang], [Chuyen_nganh_dao_tao], [Ma_nhan_vien]) VALUES (2, N'Kỹ sư', N'Đại học Quốc Gia', CAST(N'2019-09-01T00:00:00.000' AS DateTime), N'Khá', N'Công nghệ thông tin', 2)
INSERT [dbo].[Bang_cap] ([Ma_bang_cap], [Ten_bang_cap], [Ten_co_so_giao_duc], [Ngay_cap_bang], [Hang_bang], [Chuyen_nganh_dao_tao], [Ma_nhan_vien]) VALUES (3, N'Cao đẳng', N'Cao đẳng Kinh tế', CAST(N'2020-11-10T00:00:00.000' AS DateTime), N'Khá', N'Quản trị kinh doanh', 3)
INSERT [dbo].[Bang_cap] ([Ma_bang_cap], [Ten_bang_cap], [Ten_co_so_giao_duc], [Ngay_cap_bang], [Hang_bang], [Chuyen_nganh_dao_tao], [Ma_nhan_vien]) VALUES (4, N'Thạc sĩ', N'Đại học Kinh tế', CAST(N'2021-05-20T00:00:00.000' AS DateTime), N'Xuất sắc', N'Tài chính', 4)
INSERT [dbo].[Bang_cap] ([Ma_bang_cap], [Ten_bang_cap], [Ten_co_so_giao_duc], [Ngay_cap_bang], [Hang_bang], [Chuyen_nganh_dao_tao], [Ma_nhan_vien]) VALUES (5, N'Cử nhân', N'Đại học Bách Khoa', CAST(N'2022-03-30T00:00:00.000' AS DateTime), N'Khá', N'Triết học', 5)
INSERT [dbo].[Bang_cap] ([Ma_bang_cap], [Ten_bang_cap], [Ten_co_so_giao_duc], [Ngay_cap_bang], [Hang_bang], [Chuyen_nganh_dao_tao], [Ma_nhan_vien]) VALUES (6, N'Tiến sĩ', N'Viện Hàn Lâm', CAST(N'2024-03-30T00:00:00.000' AS DateTime), N'Giỏi', N'Triết học', 7)
INSERT [dbo].[Bang_cap] ([Ma_bang_cap], [Ten_bang_cap], [Ten_co_so_giao_duc], [Ngay_cap_bang], [Hang_bang], [Chuyen_nganh_dao_tao], [Ma_nhan_vien]) VALUES (7, N'Kỹ sư', N'Đại học Quốc Gia', CAST(N'2021-03-30T00:00:00.000' AS DateTime), N'Khá', N'Kỹ thuật phần mềm', 7)
INSERT [dbo].[Bang_cap] ([Ma_bang_cap], [Ten_bang_cap], [Ten_co_so_giao_duc], [Ngay_cap_bang], [Hang_bang], [Chuyen_nganh_dao_tao], [Ma_nhan_vien]) VALUES (8, N'Cử nhân', N'Đại học Bách Khoa', CAST(N'2023-03-30T00:00:00.000' AS DateTime), N'Khá', N'Kỹ thuật phần mềm', 9)
INSERT [dbo].[Bang_cap] ([Ma_bang_cap], [Ten_bang_cap], [Ten_co_so_giao_duc], [Ngay_cap_bang], [Hang_bang], [Chuyen_nganh_dao_tao], [Ma_nhan_vien]) VALUES (9, N'Kỹ sư', N'Đại học Quốc Gia', CAST(N'2022-03-30T00:00:00.000' AS DateTime), N'Giỏi', N'Công nghệ thông tin', 5)
GO
INSERT [dbo].[Bang_luong] ([Ma_bang_luong], [Thang_tinh_luong], [Luong_co_ban], [Phu_cap], [Luong_thuc_nhan], [Ma_nhan_vien]) VALUES (1, CAST(N'2023-09-01T00:00:00.000' AS DateTime), 12000000, 1000000, 13000000, 6)
INSERT [dbo].[Bang_luong] ([Ma_bang_luong], [Thang_tinh_luong], [Luong_co_ban], [Phu_cap], [Luong_thuc_nhan], [Ma_nhan_vien]) VALUES (2, CAST(N'2024-08-01T00:00:00.000' AS DateTime), 11000000, 800000, 11800000, 2)
INSERT [dbo].[Bang_luong] ([Ma_bang_luong], [Thang_tinh_luong], [Luong_co_ban], [Phu_cap], [Luong_thuc_nhan], [Ma_nhan_vien]) VALUES (3, CAST(N'2023-09-01T00:00:00.000' AS DateTime), 12500000, 1200000, 13700000, 3)
INSERT [dbo].[Bang_luong] ([Ma_bang_luong], [Thang_tinh_luong], [Luong_co_ban], [Phu_cap], [Luong_thuc_nhan], [Ma_nhan_vien]) VALUES (4, CAST(N'2024-10-01T00:00:00.000' AS DateTime), 13000000, 1500000, 14500000, 4)
INSERT [dbo].[Bang_luong] ([Ma_bang_luong], [Thang_tinh_luong], [Luong_co_ban], [Phu_cap], [Luong_thuc_nhan], [Ma_nhan_vien]) VALUES (5, CAST(N'2024-09-01T00:00:00.000' AS DateTime), 15000000, 500000, 15500000, 5)
GO
INSERT [dbo].[Cham_cong] ([Ma_cham_cong], [Ngay_lam_viec], [So_gio_lam_viec_trong_ngay], [Ma_nhan_vien]) VALUES (1, CAST(N'2024-10-01T00:00:00.000' AS DateTime), 8, 6)
INSERT [dbo].[Cham_cong] ([Ma_cham_cong], [Ngay_lam_viec], [So_gio_lam_viec_trong_ngay], [Ma_nhan_vien]) VALUES (2, CAST(N'2024-10-02T00:00:00.000' AS DateTime), 7.5, 2)
INSERT [dbo].[Cham_cong] ([Ma_cham_cong], [Ngay_lam_viec], [So_gio_lam_viec_trong_ngay], [Ma_nhan_vien]) VALUES (3, CAST(N'2024-10-03T00:00:00.000' AS DateTime), 8, 3)
INSERT [dbo].[Cham_cong] ([Ma_cham_cong], [Ngay_lam_viec], [So_gio_lam_viec_trong_ngay], [Ma_nhan_vien]) VALUES (4, CAST(N'2024-10-04T00:00:00.000' AS DateTime), 8.5, 4)
INSERT [dbo].[Cham_cong] ([Ma_cham_cong], [Ngay_lam_viec], [So_gio_lam_viec_trong_ngay], [Ma_nhan_vien]) VALUES (5, CAST(N'2024-10-05T00:00:00.000' AS DateTime), 7, 5)
INSERT [dbo].[Cham_cong] ([Ma_cham_cong], [Ngay_lam_viec], [So_gio_lam_viec_trong_ngay], [Ma_nhan_vien]) VALUES (6, CAST(N'2024-10-28T21:46:43.193' AS DateTime), 2, 5)
INSERT [dbo].[Cham_cong] ([Ma_cham_cong], [Ngay_lam_viec], [So_gio_lam_viec_trong_ngay], [Ma_nhan_vien]) VALUES (7, CAST(N'2024-10-28T21:53:58.307' AS DateTime), 9, 3)
INSERT [dbo].[Cham_cong] ([Ma_cham_cong], [Ngay_lam_viec], [So_gio_lam_viec_trong_ngay], [Ma_nhan_vien]) VALUES (8, CAST(N'2024-10-28T21:53:58.307' AS DateTime), 9, 7)
INSERT [dbo].[Cham_cong] ([Ma_cham_cong], [Ngay_lam_viec], [So_gio_lam_viec_trong_ngay], [Ma_nhan_vien]) VALUES (9, CAST(N'2024-10-28T21:58:32.163' AS DateTime), 8, 8)
INSERT [dbo].[Cham_cong] ([Ma_cham_cong], [Ngay_lam_viec], [So_gio_lam_viec_trong_ngay], [Ma_nhan_vien]) VALUES (10, CAST(N'2024-10-30T00:00:20.383' AS DateTime), 9, 4)
GO
INSERT [dbo].[Chuc_vu] ([Ma_chuc_vu], [Ten_chuc_vu]) VALUES (1, N'Nhân viên')
INSERT [dbo].[Chuc_vu] ([Ma_chuc_vu], [Ten_chuc_vu]) VALUES (2, N'Quản lý')
INSERT [dbo].[Chuc_vu] ([Ma_chuc_vu], [Ten_chuc_vu]) VALUES (3, N'Trưởng phòng')
INSERT [dbo].[Chuc_vu] ([Ma_chuc_vu], [Ten_chuc_vu]) VALUES (4, N'Giám đốc')
INSERT [dbo].[Chuc_vu] ([Ma_chuc_vu], [Ten_chuc_vu]) VALUES (5, N'Phó giám đốc')
GO
INSERT [dbo].[Don_xin_thoi_viec] ([Ma_don_xin_thoi_viec], [Ho_ten], [Chuc_vu], [Ngay_nop_don], [Ngay_du_kien_thoi_viec], [Ly_do], [Trang_thai], [Ma_nhan_vien]) VALUES (1, N'Nguyễn Văn A', N'Nhân viên', CAST(N'2024-09-01T00:00:00.000' AS DateTime), CAST(N'2024-10-01T00:00:00.000' AS DateTime), N'Lý do cá nhân', N'Đã duyệt', 4)
INSERT [dbo].[Don_xin_thoi_viec] ([Ma_don_xin_thoi_viec], [Ho_ten], [Chuc_vu], [Ngay_nop_don], [Ngay_du_kien_thoi_viec], [Ly_do], [Trang_thai], [Ma_nhan_vien]) VALUES (2, N'Trần Thị B', N'Nhân viên', CAST(N'2024-08-15T00:00:00.000' AS DateTime), CAST(N'2024-09-15T00:00:00.000' AS DateTime), N'Chuyển công tác', N'Đã duyệt', 2)
INSERT [dbo].[Don_xin_thoi_viec] ([Ma_don_xin_thoi_viec], [Ho_ten], [Chuc_vu], [Ngay_nop_don], [Ngay_du_kien_thoi_viec], [Ly_do], [Trang_thai], [Ma_nhan_vien]) VALUES (3, N'Phạm Văn C', N'Nhân viên', CAST(N'2024-07-20T00:00:00.000' AS DateTime), CAST(N'2024-08-20T00:00:00.000' AS DateTime), N'Không hợp với công việc', N'Đã duyệt', 3)
INSERT [dbo].[Don_xin_thoi_viec] ([Ma_don_xin_thoi_viec], [Ho_ten], [Chuc_vu], [Ngay_nop_don], [Ngay_du_kien_thoi_viec], [Ly_do], [Trang_thai], [Ma_nhan_vien]) VALUES (4, N'Lê Thị H', N'Nhân viên', CAST(N'2024-07-20T00:00:00.000' AS DateTime), CAST(N'2024-08-20T00:00:00.000' AS DateTime), N'Không hợp với công việc', N'Đã duyệt', 9)
INSERT [dbo].[Don_xin_thoi_viec] ([Ma_don_xin_thoi_viec], [Ho_ten], [Chuc_vu], [Ngay_nop_don], [Ngay_du_kien_thoi_viec], [Ly_do], [Trang_thai], [Ma_nhan_vien]) VALUES (5, N'Lê Thị H', N'Nhân viên', CAST(N'2024-07-20T00:00:00.000' AS DateTime), CAST(N'2024-08-20T00:00:00.000' AS DateTime), N'Không hợp với công việc', N'Đã duyệt', 9)
INSERT [dbo].[Don_xin_thoi_viec] ([Ma_don_xin_thoi_viec], [Ho_ten], [Chuc_vu], [Ngay_nop_don], [Ngay_du_kien_thoi_viec], [Ly_do], [Trang_thai], [Ma_nhan_vien]) VALUES (6, N'Lê Thị H', N'Nhân viên', CAST(N'2024-07-20T00:00:00.000' AS DateTime), CAST(N'2024-08-20T00:00:00.000' AS DateTime), N'Không hợp với công việc', N'Chờ duyệt', 9)
INSERT [dbo].[Don_xin_thoi_viec] ([Ma_don_xin_thoi_viec], [Ho_ten], [Chuc_vu], [Ngay_nop_don], [Ngay_du_kien_thoi_viec], [Ly_do], [Trang_thai], [Ma_nhan_vien]) VALUES (7, N'Phạm Văn G', N'Nhân viên', CAST(N'2024-07-20T00:00:00.000' AS DateTime), CAST(N'2024-08-20T00:00:00.000' AS DateTime), N'Không hợp với công việc', N'Chờ duyệt', 8)
INSERT [dbo].[Don_xin_thoi_viec] ([Ma_don_xin_thoi_viec], [Ho_ten], [Chuc_vu], [Ngay_nop_don], [Ngay_du_kien_thoi_viec], [Ly_do], [Trang_thai], [Ma_nhan_vien]) VALUES (8, N'Phạm Văn C', N'Nhân viên', CAST(N'2024-07-20T00:00:00.000' AS DateTime), CAST(N'2024-08-20T00:00:00.000' AS DateTime), N'Không hợp với công việc', N'Chờ duyệt', 4)
INSERT [dbo].[Don_xin_thoi_viec] ([Ma_don_xin_thoi_viec], [Ho_ten], [Chuc_vu], [Ngay_nop_don], [Ngay_du_kien_thoi_viec], [Ly_do], [Trang_thai], [Ma_nhan_vien]) VALUES (9, N'Phạm Văn C', N'Trưởng phòng', CAST(N'2024-10-29T11:20:19.000' AS DateTime), CAST(N'2024-11-02T11:20:19.000' AS DateTime), N'bệnh', N'Chờ duyệt', 4)
GO
INSERT [dbo].[Hop_dong] ([Ma_hop_dong], [Ngay_bat_dau], [Ngay_ket_thuc], [Muc_luong_co_ban], [Ma_nhan_vien]) VALUES (1, CAST(N'2023-01-10T00:00:00.000' AS DateTime), CAST(N'2024-01-10T00:00:00.000' AS DateTime), 12000000, 6)
INSERT [dbo].[Hop_dong] ([Ma_hop_dong], [Ngay_bat_dau], [Ngay_ket_thuc], [Muc_luong_co_ban], [Ma_nhan_vien]) VALUES (2, CAST(N'2022-12-20T00:00:00.000' AS DateTime), CAST(N'2023-12-20T00:00:00.000' AS DateTime), 11000000, 2)
INSERT [dbo].[Hop_dong] ([Ma_hop_dong], [Ngay_bat_dau], [Ngay_ket_thuc], [Muc_luong_co_ban], [Ma_nhan_vien]) VALUES (3, CAST(N'2023-02-15T00:00:00.000' AS DateTime), CAST(N'2024-02-15T00:00:00.000' AS DateTime), 12500000, 3)
INSERT [dbo].[Hop_dong] ([Ma_hop_dong], [Ngay_bat_dau], [Ngay_ket_thuc], [Muc_luong_co_ban], [Ma_nhan_vien]) VALUES (4, CAST(N'2023-03-10T00:00:00.000' AS DateTime), CAST(N'2024-03-10T00:00:00.000' AS DateTime), 13000000, 4)
INSERT [dbo].[Hop_dong] ([Ma_hop_dong], [Ngay_bat_dau], [Ngay_ket_thuc], [Muc_luong_co_ban], [Ma_nhan_vien]) VALUES (5, CAST(N'2021-07-22T00:00:00.000' AS DateTime), CAST(N'2022-07-22T00:00:00.000' AS DateTime), 15000000, 5)
INSERT [dbo].[Hop_dong] ([Ma_hop_dong], [Ngay_bat_dau], [Ngay_ket_thuc], [Muc_luong_co_ban], [Ma_nhan_vien]) VALUES (6, CAST(N'2021-07-22T00:00:00.000' AS DateTime), CAST(N'2029-12-28T00:00:00.000' AS DateTime), 15000000, 8)
INSERT [dbo].[Hop_dong] ([Ma_hop_dong], [Ngay_bat_dau], [Ngay_ket_thuc], [Muc_luong_co_ban], [Ma_nhan_vien]) VALUES (7, CAST(N'2021-07-22T00:00:00.000' AS DateTime), CAST(N'2029-12-28T00:00:00.000' AS DateTime), 15000000, 9)
GO
SET IDENTITY_INSERT [dbo].[Loai_phu_cap] ON 

INSERT [dbo].[Loai_phu_cap] ([Ma_loai_phu_cap], [Ten_loai_phu_cap]) VALUES (1, N'Phụ cấp xăng xe')
INSERT [dbo].[Loai_phu_cap] ([Ma_loai_phu_cap], [Ten_loai_phu_cap]) VALUES (2, N'Phụ cấp ăn trưa')
INSERT [dbo].[Loai_phu_cap] ([Ma_loai_phu_cap], [Ten_loai_phu_cap]) VALUES (3, N'Phụ cấp điện thoại')
INSERT [dbo].[Loai_phu_cap] ([Ma_loai_phu_cap], [Ten_loai_phu_cap]) VALUES (4, N'Phụ cấp đi lại')
INSERT [dbo].[Loai_phu_cap] ([Ma_loai_phu_cap], [Ten_loai_phu_cap]) VALUES (5, N'Phụ cấp nhà ở')
SET IDENTITY_INSERT [dbo].[Loai_phu_cap] OFF
GO
INSERT [dbo].[Nghi_phep] ([Ma_nghi_phep], [Ngay_bat_dau_nghi], [Ngay_ket_thuc_nghi], [Trang_thai], [Ma_nhan_vien]) VALUES (1, CAST(N'2024-10-15T00:00:00.000' AS DateTime), CAST(N'2024-10-20T00:00:00.000' AS DateTime), N'Chờ duyệt', 6)
INSERT [dbo].[Nghi_phep] ([Ma_nghi_phep], [Ngay_bat_dau_nghi], [Ngay_ket_thuc_nghi], [Trang_thai], [Ma_nhan_vien]) VALUES (2, CAST(N'2024-11-01T00:00:00.000' AS DateTime), CAST(N'2024-11-05T00:00:00.000' AS DateTime), N'Đã duyệt', 2)
INSERT [dbo].[Nghi_phep] ([Ma_nghi_phep], [Ngay_bat_dau_nghi], [Ngay_ket_thuc_nghi], [Trang_thai], [Ma_nhan_vien]) VALUES (3, CAST(N'2024-12-01T00:00:00.000' AS DateTime), CAST(N'2024-12-10T00:00:00.000' AS DateTime), N'Từ chối', 3)
INSERT [dbo].[Nghi_phep] ([Ma_nghi_phep], [Ngay_bat_dau_nghi], [Ngay_ket_thuc_nghi], [Trang_thai], [Ma_nhan_vien]) VALUES (5, CAST(N'2025-02-15T00:00:00.000' AS DateTime), CAST(N'2025-02-20T00:00:00.000' AS DateTime), N'Đã duyệt', 5)
INSERT [dbo].[Nghi_phep] ([Ma_nghi_phep], [Ngay_bat_dau_nghi], [Ngay_ket_thuc_nghi], [Trang_thai], [Ma_nhan_vien]) VALUES (6, CAST(N'2025-02-15T00:00:00.000' AS DateTime), CAST(N'2025-02-20T00:00:00.000' AS DateTime), N'Chờ duyệt', 15)
INSERT [dbo].[Nghi_phep] ([Ma_nghi_phep], [Ngay_bat_dau_nghi], [Ngay_ket_thuc_nghi], [Trang_thai], [Ma_nhan_vien]) VALUES (7, CAST(N'2024-10-28T22:30:16.000' AS DateTime), CAST(N'2024-11-09T22:30:16.000' AS DateTime), N'Chờ duyệt', 2)
GO
SET IDENTITY_INSERT [dbo].[Nhan_vien] ON 

INSERT [dbo].[Nhan_vien] ([Ma_nhan_vien], [Ho_va_ten], [Gioi_tinh], [So_dien_thoai], [Ten_chuc_vu], [Ngay_bat_dau_lam_viec], [Ma_phong_ban], [Ma_chuc_vu]) VALUES (2, N'Nguyễn Văn A', N'Nam', N'0912345678 ', N'Nhân viên', CAST(N'2023-01-10T00:00:00.000' AS DateTime), 1, 1)
INSERT [dbo].[Nhan_vien] ([Ma_nhan_vien], [Ho_va_ten], [Gioi_tinh], [So_dien_thoai], [Ten_chuc_vu], [Ngay_bat_dau_lam_viec], [Ma_phong_ban], [Ma_chuc_vu]) VALUES (3, N'Trần Thị B', N'Nữ', N'0923456789 ', N'Nhân viên', CAST(N'2022-12-20T00:00:00.000' AS DateTime), 2, 1)
INSERT [dbo].[Nhan_vien] ([Ma_nhan_vien], [Ho_va_ten], [Gioi_tinh], [So_dien_thoai], [Ten_chuc_vu], [Ngay_bat_dau_lam_viec], [Ma_phong_ban], [Ma_chuc_vu]) VALUES (4, N'Phạm Văn C', N'Nam', N'0934567890 ', N'Nhân viên', CAST(N'2023-02-15T00:00:00.000' AS DateTime), 3, 1)
INSERT [dbo].[Nhan_vien] ([Ma_nhan_vien], [Ho_va_ten], [Gioi_tinh], [So_dien_thoai], [Ten_chuc_vu], [Ngay_bat_dau_lam_viec], [Ma_phong_ban], [Ma_chuc_vu]) VALUES (5, N'Lê Thị D', N'Nữ', N'0945678901 ', N'Nhân viên', CAST(N'2023-03-10T00:00:00.000' AS DateTime), 4, 1)
INSERT [dbo].[Nhan_vien] ([Ma_nhan_vien], [Ho_va_ten], [Gioi_tinh], [So_dien_thoai], [Ten_chuc_vu], [Ngay_bat_dau_lam_viec], [Ma_phong_ban], [Ma_chuc_vu]) VALUES (6, N'Nguyễn Văn E', N'Nam', N'0956789012 ', N'Quản lý', CAST(N'2021-07-22T00:00:00.000' AS DateTime), 1, 2)
INSERT [dbo].[Nhan_vien] ([Ma_nhan_vien], [Ho_va_ten], [Gioi_tinh], [So_dien_thoai], [Ten_chuc_vu], [Ngay_bat_dau_lam_viec], [Ma_phong_ban], [Ma_chuc_vu]) VALUES (7, N'Trần Thị F', N'Nữ', N'0967890123 ', N'Quản lý', CAST(N'2021-09-15T00:00:00.000' AS DateTime), 2, 2)
INSERT [dbo].[Nhan_vien] ([Ma_nhan_vien], [Ho_va_ten], [Gioi_tinh], [So_dien_thoai], [Ten_chuc_vu], [Ngay_bat_dau_lam_viec], [Ma_phong_ban], [Ma_chuc_vu]) VALUES (8, N'Phạm Văn G', N'Nam', N'0978901234 ', N'Trưởng phòng', CAST(N'2020-11-05T00:00:00.000' AS DateTime), 3, 3)
INSERT [dbo].[Nhan_vien] ([Ma_nhan_vien], [Ho_va_ten], [Gioi_tinh], [So_dien_thoai], [Ten_chuc_vu], [Ngay_bat_dau_lam_viec], [Ma_phong_ban], [Ma_chuc_vu]) VALUES (9, N'Lê Thị H', N'Nữ', N'0989012345 ', N'Trưởng phòng', CAST(N'2020-01-20T00:00:00.000' AS DateTime), 4, 3)
INSERT [dbo].[Nhan_vien] ([Ma_nhan_vien], [Ho_va_ten], [Gioi_tinh], [So_dien_thoai], [Ten_chuc_vu], [Ngay_bat_dau_lam_viec], [Ma_phong_ban], [Ma_chuc_vu]) VALUES (10, N'Nguyễn Văn I', N'Nam', N'0990123456 ', N'Giám đốc', CAST(N'2019-06-10T00:00:00.000' AS DateTime), 5, 4)
INSERT [dbo].[Nhan_vien] ([Ma_nhan_vien], [Ho_va_ten], [Gioi_tinh], [So_dien_thoai], [Ten_chuc_vu], [Ngay_bat_dau_lam_viec], [Ma_phong_ban], [Ma_chuc_vu]) VALUES (11, N'Trần Thị K', N'Nữ', N'0901234567 ', N'Phó giám đốc', CAST(N'2018-03-15T00:00:00.000' AS DateTime), 5, 5)
INSERT [dbo].[Nhan_vien] ([Ma_nhan_vien], [Ho_va_ten], [Gioi_tinh], [So_dien_thoai], [Ten_chuc_vu], [Ngay_bat_dau_lam_viec], [Ma_phong_ban], [Ma_chuc_vu]) VALUES (15, N'Nguyễn Thị L', N'Nam', N'0912632405 ', N'Nhân viên', CAST(N'2018-03-16T00:00:00.000' AS DateTime), 5, 1)
SET IDENTITY_INSERT [dbo].[Nhan_vien] OFF
GO
INSERT [dbo].[Phong_ban] ([Ma_phong_ban], [Ten_phong_ban]) VALUES (1, N'Phòng Kế Toán')
INSERT [dbo].[Phong_ban] ([Ma_phong_ban], [Ten_phong_ban]) VALUES (2, N'Phòng Nhân Sự')
INSERT [dbo].[Phong_ban] ([Ma_phong_ban], [Ten_phong_ban]) VALUES (3, N'Phòng IT')
INSERT [dbo].[Phong_ban] ([Ma_phong_ban], [Ten_phong_ban]) VALUES (4, N'Phòng Kinh Doanh')
INSERT [dbo].[Phong_ban] ([Ma_phong_ban], [Ten_phong_ban]) VALUES (5, N'Phòng Hành Chính')
GO
INSERT [dbo].[Phu_cap] ([Ma_phu_cap], [Loai_phu_cap], [So_tien_phu_cap], [Ma_nhan_vien], [Ma_loai_phu_cap]) VALUES (1, N'Tiền ăn', 300000, 2, 1)
INSERT [dbo].[Phu_cap] ([Ma_phu_cap], [Loai_phu_cap], [So_tien_phu_cap], [Ma_nhan_vien], [Ma_loai_phu_cap]) VALUES (2, N'Tiền xăng', 150000, 3, 2)
INSERT [dbo].[Phu_cap] ([Ma_phu_cap], [Loai_phu_cap], [So_tien_phu_cap], [Ma_nhan_vien], [Ma_loai_phu_cap]) VALUES (3, N'Tiền thưởng', 500000, 4, 3)
INSERT [dbo].[Phu_cap] ([Ma_phu_cap], [Loai_phu_cap], [So_tien_phu_cap], [Ma_nhan_vien], [Ma_loai_phu_cap]) VALUES (4, N'Tiền điện thoại', 200000, 2, 4)
INSERT [dbo].[Phu_cap] ([Ma_phu_cap], [Loai_phu_cap], [So_tien_phu_cap], [Ma_nhan_vien], [Ma_loai_phu_cap]) VALUES (5, N'Tiền công tác', 400000, 2, 5)
INSERT [dbo].[Phu_cap] ([Ma_phu_cap], [Loai_phu_cap], [So_tien_phu_cap], [Ma_nhan_vien], [Ma_loai_phu_cap]) VALUES (6, N'Tiền ăn', 350000, 3, 1)
INSERT [dbo].[Phu_cap] ([Ma_phu_cap], [Loai_phu_cap], [So_tien_phu_cap], [Ma_nhan_vien], [Ma_loai_phu_cap]) VALUES (7, N'Tiền thưởng', 300000, 4, 2)
INSERT [dbo].[Phu_cap] ([Ma_phu_cap], [Loai_phu_cap], [So_tien_phu_cap], [Ma_nhan_vien], [Ma_loai_phu_cap]) VALUES (8, N'Tiền xăng', 300000, 4, 2)
INSERT [dbo].[Phu_cap] ([Ma_phu_cap], [Loai_phu_cap], [So_tien_phu_cap], [Ma_nhan_vien], [Ma_loai_phu_cap]) VALUES (9, N'Tiền thưởng', 300000, 4, 2)
GO
INSERT [dbo].[Quyen_han] ([Ma_quyen_han], [Ten_quyen_han]) VALUES (1, N'Admin')
INSERT [dbo].[Quyen_han] ([Ma_quyen_han], [Ten_quyen_han]) VALUES (2, N'Quản lý')
INSERT [dbo].[Quyen_han] ([Ma_quyen_han], [Ten_quyen_han]) VALUES (3, N'Nhân viên')
GO
SET IDENTITY_INSERT [dbo].[Tai_khoan] ON 

INSERT [dbo].[Tai_khoan] ([Ma_tai_khoan], [Ten_tai_khoan], [Mat_khau], [Ma_quyen_han], [Ma_nhan_vien]) VALUES (7, N'quanly', N'quanly', 2, 2)
INSERT [dbo].[Tai_khoan] ([Ma_tai_khoan], [Ten_tai_khoan], [Mat_khau], [Ma_quyen_han], [Ma_nhan_vien]) VALUES (8, N'nhanvien', N'nhanvien1', 3, 3)
INSERT [dbo].[Tai_khoan] ([Ma_tai_khoan], [Ten_tai_khoan], [Mat_khau], [Ma_quyen_han], [Ma_nhan_vien]) VALUES (15, N'admin', N'admin', 1, NULL)
SET IDENTITY_INSERT [dbo].[Tai_khoan] OFF
GO
ALTER TABLE [dbo].[Bang_cap]  WITH CHECK ADD FOREIGN KEY([Ma_nhan_vien])
REFERENCES [dbo].[Nhan_vien] ([Ma_nhan_vien])
GO
ALTER TABLE [dbo].[Bang_luong]  WITH CHECK ADD FOREIGN KEY([Ma_nhan_vien])
REFERENCES [dbo].[Nhan_vien] ([Ma_nhan_vien])
GO
ALTER TABLE [dbo].[Cham_cong]  WITH CHECK ADD FOREIGN KEY([Ma_nhan_vien])
REFERENCES [dbo].[Nhan_vien] ([Ma_nhan_vien])
GO
ALTER TABLE [dbo].[Don_xin_thoi_viec]  WITH CHECK ADD FOREIGN KEY([Ma_nhan_vien])
REFERENCES [dbo].[Nhan_vien] ([Ma_nhan_vien])
GO
ALTER TABLE [dbo].[Hop_dong]  WITH CHECK ADD FOREIGN KEY([Ma_nhan_vien])
REFERENCES [dbo].[Nhan_vien] ([Ma_nhan_vien])
GO
ALTER TABLE [dbo].[Nghi_phep]  WITH CHECK ADD FOREIGN KEY([Ma_nhan_vien])
REFERENCES [dbo].[Nhan_vien] ([Ma_nhan_vien])
GO
ALTER TABLE [dbo].[Nhan_vien]  WITH CHECK ADD FOREIGN KEY([Ma_chuc_vu])
REFERENCES [dbo].[Chuc_vu] ([Ma_chuc_vu])
GO
ALTER TABLE [dbo].[Nhan_vien]  WITH CHECK ADD FOREIGN KEY([Ma_phong_ban])
REFERENCES [dbo].[Phong_ban] ([Ma_phong_ban])
GO
ALTER TABLE [dbo].[Phu_cap]  WITH CHECK ADD  CONSTRAINT [FK__Phu_cap__Ma_loai__403A8C7D] FOREIGN KEY([Ma_loai_phu_cap])
REFERENCES [dbo].[Loai_phu_cap] ([Ma_loai_phu_cap])
GO
ALTER TABLE [dbo].[Phu_cap] CHECK CONSTRAINT [FK__Phu_cap__Ma_loai__403A8C7D]
GO
ALTER TABLE [dbo].[Phu_cap]  WITH CHECK ADD  CONSTRAINT [FK__Phu_cap__Ma_nhan__29572725] FOREIGN KEY([Ma_nhan_vien])
REFERENCES [dbo].[Nhan_vien] ([Ma_nhan_vien])
GO
ALTER TABLE [dbo].[Phu_cap] CHECK CONSTRAINT [FK__Phu_cap__Ma_nhan__29572725]
GO
ALTER TABLE [dbo].[Tai_khoan]  WITH CHECK ADD FOREIGN KEY([Ma_nhan_vien])
REFERENCES [dbo].[Nhan_vien] ([Ma_nhan_vien])
GO
ALTER TABLE [dbo].[Tai_khoan]  WITH CHECK ADD FOREIGN KEY([Ma_quyen_han])
REFERENCES [dbo].[Quyen_han] ([Ma_quyen_han])
GO
USE [master]
GO
ALTER DATABASE [QuanLy_NhanVien] SET  READ_WRITE 
GO
