USE [QuanLyThuVien]
GO
/****** Object:  View [dbo].[View_Borrowers]    Script Date: 11/4/2014 8:01:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[View_Borrowers] as
select * from DOCGIA where ID in 
(select IDDocGia from THONGTINMUONTRA where NgayTra > GETDATE())
GO
