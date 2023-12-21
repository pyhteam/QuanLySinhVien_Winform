USE [QLDSV1]
GO

ALTER TABLE [dbo].[SinhVien] DROP CONSTRAINT [FK_SinhVien_QueQuan]
GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 08/12/2021 19:24:07 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaiKhoan]') AND type in (N'U'))
DROP TABLE [dbo].[TaiKhoan]
GO
/****** Object:  Table [dbo].[QueQuan]    Script Date: 08/12/2021 19:24:07 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QueQuan]') AND type in (N'U'))
DROP TABLE [dbo].[QueQuan]
GO

/****** Object:  Table [dbo].[LopHoc]    Script Date: 08/12/2021 19:24:07 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LopHoc]') AND type in (N'U'))
DROP TABLE [dbo].[LopHoc]
GO
/****** Object:  Table [dbo].[SinhVien]   Script Date: 08/12/2021 19:24:07 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SinhVien]') AND type in (N'U'))
DROP TABLE [dbo].[SinhVien]
GO
/*DIEM HP */
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DiemHP]') AND type in (N'U'))
DROP TABLE [dbo].[DiemHP]
GO
/*Mon Học */
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MonHoc]') AND type in (N'U'))
DROP TABLE [dbo].[MonHoc]
GO
/*Khoa */
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Khoa]') AND type in (N'U'))
DROP TABLE [dbo].[Khoa]
GO
/*GiangVien */
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GiangVien]') AND type in (N'U'))
DROP TABLE [dbo].[GiangVien]
GO
/****** Object:  Table [dbo].[SinhVien]    Script Date: 08/12/2021 19:24:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SinhVien](
	[MaSinhVien] [nvarchar](10) NOT NULL,
	[MaLop] [nvarchar](5) NOT NULL,
	[HoVaTen] [nvarchar](50) NOT NULL,
	[GioiTinh] [tinyint] NOT NULL,
	[NgaySinh] [date] NOT NULL,
	[DiaChi] [nvarchar](max) NULL,
	[MaQueQuan] [nvarchar](2) NOT NULL,
 CONSTRAINT [PK_SinhVien] PRIMARY KEY CLUSTERED 
(
	[MaSinhVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LopHoc]    Script Date: 08/12/2021 19:24:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LopHoc](
	[MaLop] [nvarchar](5) NOT NULL,
	[TenLop] [nvarchar](50) NOT NULL,
	[MaKhoa] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_LopHoc] PRIMARY KEY CLUSTERED 
(
	[MaLop] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[QueQuan]    Script Date: 08/12/2021 19:24:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QueQuan](
	[MaQueQuan] [nvarchar](2) NOT NULL,
	[TenQueQuan] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_QueQuan] PRIMARY KEY CLUSTERED 
(
	[MaQueQuan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 08/12/2021 19:24:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoan](
	[MaTaiKhoan] [nvarchar](4) NOT NULL,
	[HoVaTen] [nvarchar](50) NOT NULL,
	[TenDangNhap] [nvarchar](20) NOT NULL,
	[MatKhau] [nvarchar](100) NOT NULL,
	[QuyenHan] [nvarchar](10) NOT NULL,
	[GhiChu] [nvarchar](max) NULL,
 CONSTRAINT [PK_TaiKhoan] PRIMARY KEY CLUSTERED 
(
	[MaTaiKhoan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/*** Bảng điểm **/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiemHP](
	[MaSinhVien] [nvarchar](10) NOT NULL,
	[HoVaTen] [nvarchar](50) NOT NULL,
	[MaLop] [nvarchar](5) NOT NULL,
	[MaHocPhan] [nvarchar](10) NOT NULL,
	[HocKy] [nvarchar](5) NULL,
	[DiemQuaTrinh] [nvarchar](5) NULL,
	[DiemLan1] [nvarchar](5) NOT NULL,
	[DiemLan2] [nvarchar](5),
	[DiemTongKet] [nvarchar](5) NULL,
	
 CONSTRAINT [PK_DiemHP] PRIMARY KEY CLUSTERED 
(
	[MaSinhVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 
GO
/* MonHoc */
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MonHoc](
	[MaHocPhan] [nvarchar](10) NOT NULL,
	[TenMonHoc] [nvarchar](50) NOT NULL,

	[MaKhoa] [nvarchar](10) NOT NULL,
	[MaGV] [nvarchar](5) NOT NULL,
	[SoTC] [nvarchar](5) NOT NULL,
	[HocKy] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_MonHoc] PRIMARY KEY CLUSTERED 
(
	[MaHocPhan] ASC
	
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Khoa]    Script Date: 08/12/2021 19:24:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Khoa](
	[MaKhoa] [nvarchar](10) NOT NULL,
	[TenKhoa] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Khoa] PRIMARY KEY CLUSTERED 
(
	[MaKhoa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GiangVien]    Script Date: 08/12/2021 19:24:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GiangVien](
	[MaGV] [nvarchar](5) NOT NULL,
	[HoVaTen] [nvarchar](50) NOT NULL,
	[GioiTinh] [tinyint] NOT NULL,
	[Email] [nvarchar] (50)NOT NULL,
 CONSTRAINT [PK_MaGV] PRIMARY KEY CLUSTERED 
(
	[MaGV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[SinhVien] ([MaSinhVien],[MaLop], [HoVaTen], [GioiTinh], [NgaySinh], [DiaChi], [MaQueQuan]) VALUES (N'DTH215736',N'22TH1', N'Trần Tuấn Anh', 0, CAST(N'2003-11-12' AS Date), N'Long Xuyên', N'89')
INSERT [dbo].[SinhVien] ([MaSinhVien],[MaLop], [HoVaTen], [GioiTinh], [NgaySinh], [DiaChi], [MaQueQuan]) VALUES (N'DTH215792',N'22TH1', N'Nguyễn Thị Ngọc Tuyền', 1, CAST(N'2003-11-22' AS Date), N'Long Xuyên', N'89')
INSERT [dbo].[SinhVien] ([MaSinhVien],[MaLop], [HoVaTen], [GioiTinh], [NgaySinh], [DiaChi], [MaQueQuan]) VALUES (N'DTH215779',N'22TH1', N'Nguyễn Cao Thành Phúc', 0, CAST(N'2003-04-19' AS Date), N'Long Xuyên', N'89')
INSERT [dbo].[SinhVien] ([MaSinhVien],[MaLop], [HoVaTen], [GioiTinh], [NgaySinh], [DiaChi], [MaQueQuan]) VALUES (N'DTH215738',N'22TH1', N'Đặng Phước Cảnh', 0, CAST(N'2003-04-16' AS Date), N'Long Xuyên', N'89')
INSERT [dbo].[SinhVien] ([MaSinhVien],[MaLop], [HoVaTen], [GioiTinh], [NgaySinh], [DiaChi], [MaQueQuan]) VALUES (N'DTH215758',N'22TH1', N'Lê Hoàng Khang', 0, CAST(N'2003-08-04' AS Date), N'Long Xuyên', N'89')
INSERT [dbo].[SinhVien] ([MaSinhVien],[MaLop], [HoVaTen], [GioiTinh], [NgaySinh], [DiaChi], [MaQueQuan]) VALUES (N'DTH216222',N'22TH1', N'Huỳnh Thanh Trúc', 1, CAST(N'2003-05-15' AS Date), N'Long Xuyên', N'89')
INSERT [dbo].[SinhVien] ([MaSinhVien],[MaLop], [HoVaTen], [GioiTinh], [NgaySinh], [DiaChi], [MaQueQuan]) VALUES (N'DTH216032',N'22TH1', N'Bùi Kim Ngân', 1, CAST(N'2003-09-10' AS Date), N'Long Xuyên', N'89')
INSERT [dbo].[SinhVien] ([MaSinhVien],[MaLop], [HoVaTen], [GioiTinh], [NgaySinh], [DiaChi], [MaQueQuan]) VALUES (N'DTH216124',N'22TH1', N'Phan Phước Sang', 0, CAST(N'2003-03-11' AS Date), N'Long Xuyên', N'89')
INSERT [dbo].[SinhVien] ([MaSinhVien],[MaLop], [HoVaTen], [GioiTinh], [NgaySinh], [DiaChi], [MaQueQuan]) VALUES (N'DTH215764',N'22TH1', N'Võ Văn Luận', 0, CAST(N'2002-02-03' AS Date), N'Long Xuyên', N'89')
INSERT [dbo].[SinhVien] ([MaSinhVien],[MaLop], [HoVaTen], [GioiTinh], [NgaySinh], [DiaChi], [MaQueQuan]) VALUES (N'DTH215776',N'22TH1', N'Lý Thị Thúy Oanh', 1, CAST(N'2003-11-11' AS Date), N'Long Xuyên', N'89')
INSERT [dbo].[SinhVien] ([MaSinhVien],[MaLop], [HoVaTen], [GioiTinh], [NgaySinh], [DiaChi], [MaQueQuan]) VALUES (N'DTH215734',N'22TH1', N'Bùi Thị Kim', 1, CAST(N'2003-11-25' AS Date), N'Long Xuyên', N'89')
INSERT [dbo].[SinhVien] ([MaSinhVien],[MaLop], [HoVaTen], [GioiTinh], [NgaySinh], [DiaChi], [MaQueQuan]) VALUES (N'DTH215799',N'22TH1', N'Nguyễn Ngọc Minh Thư', 1, CAST(N'2003-09-02' AS Date), N'Long Xuyên', N'89')
INSERT [dbo].[SinhVien] ([MaSinhVien],[MaLop], [HoVaTen], [GioiTinh], [NgaySinh], [DiaChi], [MaQueQuan]) VALUES (N'DTH215800',N'22TH1', N'Nguyễn Thị Anh Thư', 1, CAST(N'2003-10-14' AS Date), N'Long Xuyên', N'89')
INSERT [dbo].[SinhVien] ([MaSinhVien],[MaLop], [HoVaTen], [GioiTinh], [NgaySinh], [DiaChi], [MaQueQuan]) VALUES (N'DTH215791',N'22TH1', N'Lê Thị Ngọc Tuyền', 1, CAST(N'2003-08-06' AS Date), N'Long Xuyên', N'89')
INSERT [dbo].[SinhVien] ([MaSinhVien],[MaLop], [HoVaTen], [GioiTinh], [NgaySinh], [DiaChi], [MaQueQuan]) VALUES (N'DTH215678',N'22TH2', N'Nguyễn Thảo Huỳnh', 1, CAST(N'2003-01-27' AS Date), N'Long Xuyên', N'89')
GO
INSERT [dbo].[LopHoc] ([MaLop], [TenLop],[MaKhoa]) VALUES (N'22TH1', N'Lớp DH22TH1',N'CNTT')
INSERT [dbo].[LopHoc] ([MaLop], [TenLop],[MaKhoa]) VALUES (N'22TH2', N'Lớp DH22TH2',N'CNTT')
INSERT [dbo].[LopHoc] ([MaLop], [TenLop],[MaKhoa]) VALUES (N'22TH3', N'Lớp DH22TH3',N'CNTT')
INSERT [dbo].[LopHoc] ([MaLop], [TenLop],[MaKhoa]) VALUES (N'22TH4', N'Lớp DH22TH4',N'CNTT')
INSERT [dbo].[LopHoc] ([MaLop], [TenLop],[MaKhoa]) VALUES (N'22PM', N'Lớp DH22PM',N'CNTT')
INSERT [dbo].[LopHoc] ([MaLop], [TenLop],[MaKhoa]) VALUES (N'22MN', N'Lớp DH22MN',N'CNTT')
INSERT [dbo].[LopHoc] ([MaLop], [TenLop],[MaKhoa]) VALUES (N'22VN', N'Lớp DH22VN',N'CNTT')
INSERT [dbo].[LopHoc] ([MaLop], [TenLop],[MaKhoa]) VALUES (N'22KT', N'Lớp DH22KT',N'CNTT')
INSERT [dbo].[LopHoc] ([MaLop], [TenLop],[MaKhoa]) VALUES (N'22NN', N'Lớp DH22NN',N'CNTT')
INSERT [dbo].[LopHoc] ([MaLop], [TenLop],[MaKhoa]) VALUES (N'22LU', N'Lớp DH22LU',N'CNTT')
INSERT [dbo].[LopHoc] ([MaLop], [TenLop],[MaKhoa]) VALUES (N'22DL', N'Lớp DH22DL',N'CNTT')
INSERT [dbo].[LopHoc] ([MaLop], [TenLop],[MaKhoa]) VALUES (N'22BT', N'Lớp DH22BT',N'CNTT')
INSERT [dbo].[LopHoc] ([MaLop], [TenLop],[MaKhoa]) VALUES (N'22TA', N'Lớp DH22TA',N'CNTT')
INSERT [dbo].[LopHoc] ([MaLop], [TenLop],[MaKhoa]) VALUES (N'22MK', N'Lớp DH22MK',N'CNTT')
INSERT [dbo].[LopHoc] ([MaLop], [TenLop],[MaKhoa]) VALUES (N'22NH', N'Lớp DH22NH',N'CNTT')
GO

INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'01', N'Hà Nội')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'02', N'Hà Giang')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'04', N'Cao Bằng')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'06', N'Bắc Kạn')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'08', N'Tuyên Quang')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'10', N'Lào Cai')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'11', N'Điện Biên')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'12', N'Lai Châu')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'14', N'Sơn La')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'15', N'Yên Bái')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'17', N'Hoà Bình')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'19', N'Thái Nguyên')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'20', N'Lạng Sơn')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'22', N'Quảng Ninh')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'24', N'Bắc Giang')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'25', N'Phú Thọ')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'26', N'Vĩnh Phúc')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'27', N'Bắc Ninh')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'30', N'Hải Dương')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'31', N'Hải Phòng')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'33', N'Hưng Yên')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'34', N'Thái Bình')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'35', N'Hà Nam')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'36', N'Nam Định')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'37', N'Ninh Bình')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'38', N'Thanh Hóa')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'40', N'Nghệ An')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'42', N'Hà Tĩnh')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'44', N'Quảng Bình')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'45', N'Quảng Trị')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'46', N'Thừa Thiên Huế')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'48', N'Đà Nẵng')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'49', N'Quảng Nam')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'51', N'Quảng Ngãi')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'52', N'Bình Định')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'54', N'Phú Yên')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'56', N'Khánh Hòa')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'58', N'Ninh Thuận')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'60', N'Bình Thuận')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'62', N'Kon Tum')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'64', N'Gia Lai')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'66', N'Đắk Lắk')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'67', N'Đắk Nông')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'68', N'Lâm Đồng')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'70', N'Bình Phước')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'72', N'Tây Ninh')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'74', N'Bình Dương')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'75', N'Đồng Nai')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'77', N'Bà Rịa - Vũng Tàu')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'79', N'Hồ Chí Minh')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'80', N'Long An')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'82', N'Tiền Giang')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'83', N'Bến Tre')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'84', N'Trà Vinh')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'86', N'Vĩnh Long')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'87', N'Đồng Tháp')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'89', N'An Giang')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'91', N'Kiên Giang')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'92', N'Cần Thơ')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'93', N'Hậu Giang')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'94', N'Sóc Trăng')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'95', N'Bạc Liêu')
INSERT [dbo].[QueQuan] ([MaQueQuan], [TenQueQuan]) VALUES (N'96', N'Cà Mau')
GO
INSERT [dbo].[TaiKhoan] ([MaTaiKhoan], [HoVaTen], [TenDangNhap], [MatKhau], [QuyenHan], [GhiChu]) VALUES (N'TK01', N'Nguyễn Thị Ngọc Tuyền', N'ntntuyen', N'123456', N'admin', N'Quản trị viên')
INSERT [dbo].[TaiKhoan] ([MaTaiKhoan], [HoVaTen], [TenDangNhap], [MatKhau], [QuyenHan], [GhiChu]) VALUES (N'TK02', N'Trần Tuấn Anh', N'ttanh', N'123456', N'user', N'Giáo vụ')
GO
INSERT [dbo].DiemHP ([MaSinhVien],[HoVaTen],[MaLop],[MaHocPhan],[HocKy],[DiemQuaTrinh],[DiemLan1],[DiemLan2],[DiemTongKet]) VALUES(N'DTH215736', N'Trần Tuấn Anh',N'22TH1',N'COS111',N'5',N'5',N'5',N'5',N'5')

GO
INSERT [dbo].MonHoc ([MaHocPhan],[TenMonHoc],[MaKhoa],[MaGV],[SoTC],[HocKy] ) VALUES(N'COS111', N'Lập trình',N'CNTT',N'0511',N'4',N'1')

GO
INSERT [dbo].[Khoa] ([MaKhoa], [TenKhoa]) VALUES (N'CNTT', N'Công nghệ thông tin')

GO
INSERT [dbo].[GiangVien] ([MaGV], [HoVaTen],[GioiTinh],[Email]) VALUES (N'0511', N'Nguyễn Thị A',1,N'A@gmail.com')

ALTER TABLE [dbo].[SinhVien]  WITH CHECK ADD  CONSTRAINT [FK_SinhVien_QueQuan] FOREIGN KEY([MaQueQuan])
REFERENCES [dbo].[QueQuan] ([MaQueQuan])
GO
ALTER TABLE [dbo].[SinhVien] CHECK CONSTRAINT [FK_SinhVien_QueQuan]
GO

