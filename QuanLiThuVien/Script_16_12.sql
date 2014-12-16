USE [QuanLyThuVien]
GO
/****** Object:  StoredProcedure [dbo].[proc_layDSMuonTra]    Script Date: 12/16/2014 11:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[proc_layDSMuonTra] @maDocGia varchar(10), @dieukien int
as --DK: 0: tat ca, 1: sach chua tra, 2: sach qua han
if(@dieukien = 1)
	 select tt.*, s.* from THONGTINMUONTRA as tt, DOCGIA as dg, SACH as s
	 where tt.IDDocGia = dg.ID and dg.MHV_MSSV = @maDocGia and s.ID = tt.IDSach and tt.NgayTra is null and tt.HanTra >= GETDATE()
else if(@dieukien = 2)
	select tt.*, s.*from THONGTINMUONTRA as tt, DOCGIA as dg, SACH as s
	where tt.IDDocGia = dg.ID and dg.MHV_MSSV = @maDocGia and s.ID = tt.IDSach and tt.NgayTra is null  and DATEDIFF(M, tt.HanTra, GETDATE()) <= 6
else
	select tt.*, s.* from THONGTINMUONTRA as tt, DOCGIA as dg, SACH as s
	where tt.IDDocGia = dg.ID and dg.MHV_MSSV = @maDocGia and s.ID = tt.IDSach

GO
/****** Object:  StoredProcedure [dbo].[proc_layTTMuonSach]    Script Date: 12/16/2014 11:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[proc_layTTMuonSach] @maSach varchar(10)
as
begin
	select DATEDIFF(d, tt.HanTra, GETDATE())*1000 as TienPhat, tt.* , s.TenSach from THONGTINMUONTRA as tt, SACH as s where tt.NgayTra is null
	or DATEDIFF(M, GETDATE(), tt.HanTra) < 0 and tt.IDSach = @maSach and s.ID = tt.IDSach
end

GO
/****** Object:  Table [dbo].[BANPHAT]    Script Date: 12/16/2014 11:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BANPHAT](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LiDo] [nvarchar](200) NULL,
	[PhiPhat] [money] NULL,
	[IDThongTinMuonTra] [int] NULL,
 CONSTRAINT [PK_BanPhat] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CHUDE]    Script Date: 12/16/2014 11:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHUDE](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenChuDe] [nvarchar](50) NULL,
 CONSTRAINT [PK_CHUDE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DOCGIA]    Script Date: 12/16/2014 11:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DOCGIA](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MHV_MSSV] [varchar](10) NULL,
	[Hoten] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](200) NULL,
	[CMND] [varchar](10) NULL,
	[Email] [nvarchar](50) NULL,
	[Truong] [nvarchar](50) NULL,
	[Khoa] [nvarchar](50) NULL,
	[Avatar] [nvarchar](200) NULL,
	[LoaiDocGia] [nvarchar](50) NULL,
 CONSTRAINT [PK_DOCGIA] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LICHSUMUONPHONG]    Script Date: 12/16/2014 11:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LICHSUMUONPHONG](
	[IDDocGia] [int] NULL,
	[IDPhong] [int] NULL,
	[ThoiGianMuon] [datetime] NULL,
	[ThoiGianTra] [datetime] NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[delete_flag] [char](1) NULL,
 CONSTRAINT [PK_LICHSUMUONPHONG_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LOPTAPHUAN]    Script Date: 12/16/2014 11:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LOPTAPHUAN](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Ngay] [date] NULL,
	[Phong] [nchar](10) NULL,
	[ThoiGian] [time](7) NULL,
 CONSTRAINT [PK_LOP_TAP_HUAN] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NGUOIDANGKY]    Script Date: 12/16/2014 11:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NGUOIDANGKY](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MSSV_MaHocVien] [varchar](10) NULL,
	[Hoten] [nchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Truong] [nvarchar](100) NULL,
	[XacNhan] [bit] NULL,
	[IDLopTapHuan] [int] NULL,
 CONSTRAINT [PK_NguoiDangKy] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NHANVIEN]    Script Date: 12/16/2014 11:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NHANVIEN](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenNhanVien] [nvarchar](50) NULL,
	[ChucVu] [nvarchar](50) NULL,
	[Email] [varchar](50) NULL,
	[SDT] [varchar](20) NULL,
 CONSTRAINT [PK_NhanVien] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NHAXUATBAN]    Script Date: 12/16/2014 11:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NHAXUATBAN](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenNXB] [nvarchar](50) NULL,
	[QuocGia] [nvarchar](50) NULL,
 CONSTRAINT [PK_NHAXUATBAN] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NHOMSACH]    Script Date: 12/16/2014 11:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NHOMSACH](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenNhom] [nvarchar](50) NULL,
 CONSTRAINT [PK_NHOMSACH] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PHONG]    Script Date: 12/16/2014 11:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PHONG](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TrangThai] [nvarchar](20) NULL,
 CONSTRAINT [PK_PHONG] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SACH]    Script Date: 12/16/2014 11:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SACH](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenSach] [nvarchar](100) NULL,
	[NamXB] [date] NULL,
	[Tap] [int] NULL,
	[Cuon] [int] NULL,
	[TrangThai] [nvarchar](30) NULL,
	[NoiDungTomTat] [nvarchar](max) NULL,
	[MucLuc] [varchar](300) NULL,
	[IDNhaXuatBan] [int] NULL,
	[IDNhomSach] [int] NULL,
	[IDViTri] [int] NULL,
	[IDChuDe] [int] NULL,
 CONSTRAINT [PK_SACH] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TACGIA]    Script Date: 12/16/2014 11:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TACGIA](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TenTacGia] [nvarchar](50) NULL,
	[QuocTich] [nvarchar](50) NULL,
 CONSTRAINT [PK_TACGIA] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TACGIA_SACH]    Script Date: 12/16/2014 11:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TACGIA_SACH](
	[IDTacGia] [int] NOT NULL,
	[IDSach] [int] NOT NULL,
 CONSTRAINT [PK_TACGIA_SACH] PRIMARY KEY CLUSTERED 
(
	[IDTacGia] ASC,
	[IDSach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[THONGTINMUONTRA]    Script Date: 12/16/2014 11:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[THONGTINMUONTRA](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NgayMuon] [date] NULL,
	[NgayTra] [date] NULL,
	[HanTra] [date] NULL,
	[GiaHan] [bit] NULL,
	[IDNhanVienNhan] [int] NULL,
	[IDNhanVienTra] [int] NULL,
	[IDDocGia] [int] NULL,
	[IDSach] [int] NULL,
 CONSTRAINT [PK_ThongTinMuonTra] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[THUGOPY]    Script Date: 12/16/2014 11:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[THUGOPY](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NoiDung] [nvarchar](50) NULL,
	[NgayGopY] [date] NULL,
	[DaXem] [bit] NULL,
 CONSTRAINT [PK_ThuGopY] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[USER]    Script Date: 12/16/2014 11:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USER](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[USERNAME] [int] NOT NULL,
	[PASSWORD] [nvarchar](30) NOT NULL,
	[PASSWORD_SALT] [nvarchar](50) NULL,
	[ROLE] [nvarchar](10) NULL,
 CONSTRAINT [PK_USER] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VITRI]    Script Date: 12/16/2014 11:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VITRI](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Ke] [int] NULL,
	[Tang] [int] NULL,
	[Ngan] [int] NULL,
 CONSTRAINT [PK_VITRI] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[VIEW_BORROWERS]    Script Date: 12/16/2014 11:10:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VIEW_BORROWERS]
AS
SELECT        ID, MHV_MSSV, Hoten, DiaChi, CMND, Email, Truong, Khoa, Avatar
FROM            dbo.DOCGIA
WHERE        (ID IN
                             (SELECT        IDDocGia
                               FROM            dbo.THONGTINMUONTRA
                               WHERE        (NgayTra > GETDATE())))




GO
SET IDENTITY_INSERT [dbo].[BANPHAT] ON 

INSERT [dbo].[BANPHAT] ([ID], [LiDo], [PhiPhat], [IDThongTinMuonTra]) VALUES (1, N'Sách bẩn', 10000.0000, 1)
SET IDENTITY_INSERT [dbo].[BANPHAT] OFF
SET IDENTITY_INSERT [dbo].[CHUDE] ON 

INSERT [dbo].[CHUDE] ([ID], [TenChuDe]) VALUES (1, N'Vô cơ')
INSERT [dbo].[CHUDE] ([ID], [TenChuDe]) VALUES (2, N'Hữu Cơ')
SET IDENTITY_INSERT [dbo].[CHUDE] OFF
SET IDENTITY_INSERT [dbo].[DOCGIA] ON 

INSERT [dbo].[DOCGIA] ([ID], [MHV_MSSV], [Hoten], [DiaChi], [CMND], [Email], [Truong], [Khoa], [Avatar], [LoaiDocGia]) VALUES (1, N'0944873', N'Phan Huy Ích', N'21 Nguyễn Chí Thanh', N'091112343', N'Phuong@gmail.com', N'Đh KHTN', N'CNTT', NULL, NULL)
INSERT [dbo].[DOCGIA] ([ID], [MHV_MSSV], [Hoten], [DiaChi], [CMND], [Email], [Truong], [Khoa], [Avatar], [LoaiDocGia]) VALUES (2, N'1112223', N'Phương Huỳnh', N'54 Phan Văn Trị', N'034524553', N'thuong@gmail.com', N'Đh Y Được', N'Nội', NULL, NULL)
INSERT [dbo].[DOCGIA] ([ID], [MHV_MSSV], [Hoten], [DiaChi], [CMND], [Email], [Truong], [Khoa], [Avatar], [LoaiDocGia]) VALUES (8, NULL, N'Phương Trang', N'111, Phan Huy Ích', N'066544343', N'tuyen@gmail.com', N'Đh KHTN', N'CNTT', NULL, NULL)
SET IDENTITY_INSERT [dbo].[DOCGIA] OFF
SET IDENTITY_INSERT [dbo].[LICHSUMUONPHONG] ON 

INSERT [dbo].[LICHSUMUONPHONG] ([IDDocGia], [IDPhong], [ThoiGianMuon], [ThoiGianTra], [ID], [delete_flag]) VALUES (1, 1, CAST(0x0000A3CF00F73140 AS DateTime), CAST(0x0000A3CF0107AC00 AS DateTime), 1, N'0')
INSERT [dbo].[LICHSUMUONPHONG] ([IDDocGia], [IDPhong], [ThoiGianMuon], [ThoiGianTra], [ID], [delete_flag]) VALUES (1, 1, CAST(0x0000A3F700000000 AS DateTime), CAST(0x0000A3F700000000 AS DateTime), 2, N'0')
INSERT [dbo].[LICHSUMUONPHONG] ([IDDocGia], [IDPhong], [ThoiGianMuon], [ThoiGianTra], [ID], [delete_flag]) VALUES (1, 1, CAST(0x0000A40D0083D600 AS DateTime), CAST(0x0000A40D00B54640 AS DateTime), 3, N'0')
INSERT [dbo].[LICHSUMUONPHONG] ([IDDocGia], [IDPhong], [ThoiGianMuon], [ThoiGianTra], [ID], [delete_flag]) VALUES (2, 1, CAST(0x0000A3CF00C5C100 AS DateTime), CAST(0x0000A3CF00F73140 AS DateTime), 4, N'0')
INSERT [dbo].[LICHSUMUONPHONG] ([IDDocGia], [IDPhong], [ThoiGianMuon], [ThoiGianTra], [ID], [delete_flag]) VALUES (1, 1, CAST(0x0000A4060083D600 AS DateTime), CAST(0x0000A40600C5C100 AS DateTime), 7, NULL)
SET IDENTITY_INSERT [dbo].[LICHSUMUONPHONG] OFF
SET IDENTITY_INSERT [dbo].[LOPTAPHUAN] ON 

INSERT [dbo].[LOPTAPHUAN] ([ID], [Ngay], [Phong], [ThoiGian]) VALUES (1, CAST(0x2D390B00 AS Date), N'1         ', CAST(0x0700E03495640000 AS Time))
INSERT [dbo].[LOPTAPHUAN] ([ID], [Ngay], [Phong], [ThoiGian]) VALUES (2, CAST(0x2E390B00 AS Date), N'2         ', CAST(0x070040230E430000 AS Time))
SET IDENTITY_INSERT [dbo].[LOPTAPHUAN] OFF
SET IDENTITY_INSERT [dbo].[NHANVIEN] ON 

INSERT [dbo].[NHANVIEN] ([ID], [TenNhanVien], [ChucVu], [Email], [SDT]) VALUES (1, N'Phương Trang', N'Thủ Thư', N'Thuong@gmail.com', N'0922133313')
INSERT [dbo].[NHANVIEN] ([ID], [TenNhanVien], [ChucVu], [Email], [SDT]) VALUES (2, N'Ngọc Tuyền', N'Thủ Thư', N'Vuong@gmail.com', N'0933245535')
SET IDENTITY_INSERT [dbo].[NHANVIEN] OFF
SET IDENTITY_INSERT [dbo].[NHAXUATBAN] ON 

INSERT [dbo].[NHAXUATBAN] ([ID], [TenNXB], [QuocGia]) VALUES (1, N'Nhà xuất bản trẻ', N'Việt Nam')
SET IDENTITY_INSERT [dbo].[NHAXUATBAN] OFF
SET IDENTITY_INSERT [dbo].[NHOMSACH] ON 

INSERT [dbo].[NHOMSACH] ([ID], [TenNhom]) VALUES (1, N'Nghiên cứu')
INSERT [dbo].[NHOMSACH] ([ID], [TenNhom]) VALUES (2, N'Luận văn')
SET IDENTITY_INSERT [dbo].[NHOMSACH] OFF
SET IDENTITY_INSERT [dbo].[PHONG] ON 

INSERT [dbo].[PHONG] ([ID], [TrangThai]) VALUES (1, N'Sẵn sàng')
SET IDENTITY_INSERT [dbo].[PHONG] OFF
SET IDENTITY_INSERT [dbo].[SACH] ON 

INSERT [dbo].[SACH] ([ID], [TenSach], [NamXB], [Tap], [Cuon], [TrangThai], [NoiDungTomTat], [MucLuc], [IDNhaXuatBan], [IDNhomSach], [IDViTri], [IDChuDe]) VALUES (1, N'Phương thức hóa', CAST(0x79360B00 AS Date), 1, 1, N'Đang mượn', NULL, NULL, 1, 1, 1, 1)
INSERT [dbo].[SACH] ([ID], [TenSach], [NamXB], [Tap], [Cuon], [TrangThai], [NoiDungTomTat], [MucLuc], [IDNhaXuatBan], [IDNhomSach], [IDViTri], [IDChuDe]) VALUES (2, N'Công thức hóa', CAST(0xE2350B00 AS Date), 1, 1, N'Đặt', NULL, NULL, 1, 2, 2, 2)
SET IDENTITY_INSERT [dbo].[SACH] OFF
SET IDENTITY_INSERT [dbo].[TACGIA] ON 

INSERT [dbo].[TACGIA] ([ID], [TenTacGia], [QuocTich]) VALUES (1, N'Ngô Ngọc An', N'Việt Nam')
INSERT [dbo].[TACGIA] ([ID], [TenTacGia], [QuocTich]) VALUES (2, N'Phan Huy Chương', N'Việt nam')
SET IDENTITY_INSERT [dbo].[TACGIA] OFF
SET IDENTITY_INSERT [dbo].[THONGTINMUONTRA] ON 

INSERT [dbo].[THONGTINMUONTRA] ([ID], [NgayMuon], [NgayTra], [HanTra], [GiaHan], [IDNhanVienNhan], [IDNhanVienTra], [IDDocGia], [IDSach]) VALUES (1, CAST(0x25390B00 AS Date), CAST(0x27390B00 AS Date), NULL, NULL, 1, 2, 1, 1)
INSERT [dbo].[THONGTINMUONTRA] ([ID], [NgayMuon], [NgayTra], [HanTra], [GiaHan], [IDNhanVienNhan], [IDNhanVienTra], [IDDocGia], [IDSach]) VALUES (2, CAST(0x2F390B00 AS Date), CAST(0x62390B00 AS Date), NULL, NULL, 1, NULL, 1, 1)
INSERT [dbo].[THONGTINMUONTRA] ([ID], [NgayMuon], [NgayTra], [HanTra], [GiaHan], [IDNhanVienNhan], [IDNhanVienTra], [IDDocGia], [IDSach]) VALUES (3, CAST(0x67390B00 AS Date), CAST(0x69390B00 AS Date), NULL, NULL, 1, NULL, 1, 1)
SET IDENTITY_INSERT [dbo].[THONGTINMUONTRA] OFF
SET IDENTITY_INSERT [dbo].[USER] ON 

INSERT [dbo].[USER] ([ID], [USERNAME], [PASSWORD], [PASSWORD_SALT], [ROLE]) VALUES (1, 1, N'123456', N'1234567', N'admin')
INSERT [dbo].[USER] ([ID], [USERNAME], [PASSWORD], [PASSWORD_SALT], [ROLE]) VALUES (2, 2, N'123456', N'2356', N'nv')
INSERT [dbo].[USER] ([ID], [USERNAME], [PASSWORD], [PASSWORD_SALT], [ROLE]) VALUES (5, 1, N'123456', N'1234567', N'dg')
SET IDENTITY_INSERT [dbo].[USER] OFF
SET IDENTITY_INSERT [dbo].[VITRI] ON 

INSERT [dbo].[VITRI] ([ID], [Ke], [Tang], [Ngan]) VALUES (1, 1, 1, 1)
INSERT [dbo].[VITRI] ([ID], [Ke], [Tang], [Ngan]) VALUES (2, 1, 1, 2)
SET IDENTITY_INSERT [dbo].[VITRI] OFF
ALTER TABLE [dbo].[LICHSUMUONPHONG] ADD  CONSTRAINT [DF_LICHSUMUONPHONG_delete_flag]  DEFAULT ((0)) FOR [delete_flag]
GO
ALTER TABLE [dbo].[THUGOPY] ADD  CONSTRAINT [DF_THUGOPY_DaXem]  DEFAULT ((0)) FOR [DaXem]
GO
ALTER TABLE [dbo].[BANPHAT]  WITH CHECK ADD  CONSTRAINT [FK_BANPHAT_THONGTINMUONTRA] FOREIGN KEY([IDThongTinMuonTra])
REFERENCES [dbo].[THONGTINMUONTRA] ([ID])
GO
ALTER TABLE [dbo].[BANPHAT] CHECK CONSTRAINT [FK_BANPHAT_THONGTINMUONTRA]
GO
ALTER TABLE [dbo].[LICHSUMUONPHONG]  WITH CHECK ADD  CONSTRAINT [FK_LICHSUMUONPHONG_DOCGIA] FOREIGN KEY([IDDocGia])
REFERENCES [dbo].[DOCGIA] ([ID])
GO
ALTER TABLE [dbo].[LICHSUMUONPHONG] CHECK CONSTRAINT [FK_LICHSUMUONPHONG_DOCGIA]
GO
ALTER TABLE [dbo].[LICHSUMUONPHONG]  WITH CHECK ADD  CONSTRAINT [FK_LICHSUMUONPHONG_PHONG] FOREIGN KEY([IDPhong])
REFERENCES [dbo].[PHONG] ([ID])
GO
ALTER TABLE [dbo].[LICHSUMUONPHONG] CHECK CONSTRAINT [FK_LICHSUMUONPHONG_PHONG]
GO
ALTER TABLE [dbo].[NGUOIDANGKY]  WITH CHECK ADD  CONSTRAINT [FK_NGUOIDANGKY_LOPTAPHUAN] FOREIGN KEY([IDLopTapHuan])
REFERENCES [dbo].[LOPTAPHUAN] ([ID])
GO
ALTER TABLE [dbo].[NGUOIDANGKY] CHECK CONSTRAINT [FK_NGUOIDANGKY_LOPTAPHUAN]
GO
ALTER TABLE [dbo].[SACH]  WITH CHECK ADD  CONSTRAINT [FK_SACH_CHUDE] FOREIGN KEY([IDChuDe])
REFERENCES [dbo].[CHUDE] ([ID])
GO
ALTER TABLE [dbo].[SACH] CHECK CONSTRAINT [FK_SACH_CHUDE]
GO
ALTER TABLE [dbo].[SACH]  WITH CHECK ADD  CONSTRAINT [FK_SACH_NHAXUATBAN] FOREIGN KEY([IDNhaXuatBan])
REFERENCES [dbo].[NHAXUATBAN] ([ID])
GO
ALTER TABLE [dbo].[SACH] CHECK CONSTRAINT [FK_SACH_NHAXUATBAN]
GO
ALTER TABLE [dbo].[SACH]  WITH CHECK ADD  CONSTRAINT [FK_SACH_NHOMSACH] FOREIGN KEY([IDNhomSach])
REFERENCES [dbo].[NHOMSACH] ([ID])
GO
ALTER TABLE [dbo].[SACH] CHECK CONSTRAINT [FK_SACH_NHOMSACH]
GO
ALTER TABLE [dbo].[SACH]  WITH CHECK ADD  CONSTRAINT [FK_SACH_VITRI] FOREIGN KEY([IDViTri])
REFERENCES [dbo].[VITRI] ([ID])
GO
ALTER TABLE [dbo].[SACH] CHECK CONSTRAINT [FK_SACH_VITRI]
GO
ALTER TABLE [dbo].[TACGIA_SACH]  WITH CHECK ADD  CONSTRAINT [FK_TACGIA_SACH_SACH] FOREIGN KEY([IDSach])
REFERENCES [dbo].[SACH] ([ID])
GO
ALTER TABLE [dbo].[TACGIA_SACH] CHECK CONSTRAINT [FK_TACGIA_SACH_SACH]
GO
ALTER TABLE [dbo].[TACGIA_SACH]  WITH CHECK ADD  CONSTRAINT [FK_TACGIA_SACH_TACGIA] FOREIGN KEY([IDTacGia])
REFERENCES [dbo].[TACGIA] ([ID])
GO
ALTER TABLE [dbo].[TACGIA_SACH] CHECK CONSTRAINT [FK_TACGIA_SACH_TACGIA]
GO
ALTER TABLE [dbo].[THONGTINMUONTRA]  WITH CHECK ADD  CONSTRAINT [FK_THONGTINMUONTRA_DOCGIA] FOREIGN KEY([IDDocGia])
REFERENCES [dbo].[DOCGIA] ([ID])
GO
ALTER TABLE [dbo].[THONGTINMUONTRA] CHECK CONSTRAINT [FK_THONGTINMUONTRA_DOCGIA]
GO
ALTER TABLE [dbo].[THONGTINMUONTRA]  WITH CHECK ADD  CONSTRAINT [FK_THONGTINMUONTRA_NHANVIEN] FOREIGN KEY([IDNhanVienNhan])
REFERENCES [dbo].[NHANVIEN] ([ID])
GO
ALTER TABLE [dbo].[THONGTINMUONTRA] CHECK CONSTRAINT [FK_THONGTINMUONTRA_NHANVIEN]
GO
ALTER TABLE [dbo].[THONGTINMUONTRA]  WITH CHECK ADD  CONSTRAINT [FK_THONGTINMUONTRA_NHANVIEN1] FOREIGN KEY([IDNhanVienTra])
REFERENCES [dbo].[NHANVIEN] ([ID])
GO
ALTER TABLE [dbo].[THONGTINMUONTRA] CHECK CONSTRAINT [FK_THONGTINMUONTRA_NHANVIEN1]
GO
ALTER TABLE [dbo].[THONGTINMUONTRA]  WITH CHECK ADD  CONSTRAINT [FK_THONGTINMUONTRA_SACH] FOREIGN KEY([IDSach])
REFERENCES [dbo].[SACH] ([ID])
GO
ALTER TABLE [dbo].[THONGTINMUONTRA] CHECK CONSTRAINT [FK_THONGTINMUONTRA_SACH]
GO
ALTER TABLE [dbo].[USER]  WITH CHECK ADD  CONSTRAINT [FK_USER_DOCGIA] FOREIGN KEY([USERNAME])
REFERENCES [dbo].[DOCGIA] ([ID])
GO
ALTER TABLE [dbo].[USER] CHECK CONSTRAINT [FK_USER_DOCGIA]
GO
ALTER TABLE [dbo].[USER]  WITH CHECK ADD  CONSTRAINT [FK_USER_NHANVIEN] FOREIGN KEY([USERNAME])
REFERENCES [dbo].[NHANVIEN] ([ID])
GO
ALTER TABLE [dbo].[USER] CHECK CONSTRAINT [FK_USER_NHANVIEN]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "DOCGIA"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 220
               Right = 254
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 1410
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VIEW_BORROWERS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VIEW_BORROWERS'
GO
