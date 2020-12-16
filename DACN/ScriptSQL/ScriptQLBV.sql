create database QLBV
go

use QLBV
go

create table TaiKhoan (
	idTK int IDENTITY(1,1) primary key,
	RoleTK varchar(20),
	Username varchar(50),
	Pass varchar(50),
	Email varchar(50),
	SDT varchar(11),
	NgayTao date,
	GioiTinh varchar(10),
	HoTen nvarchar(50)
)
go

create table KieuBDS(
	idKieuBDS int IDENTITY(1,1) primary key,
	TenKieu nvarchar(50),
)
go

create table LoaiBDS(
	idBDS int IDENTITY(1,1) primary key,
	TenLoai nvarchar(50),
)
go
create table ThanhPho(
	idTP int IDENTITY(1,1) primary key,
	TenTP nvarchar(50),
)
go
create table Quan(
	idQuan int IDENTITY(1,1) primary key,
	TenQuan nvarchar(50),
	idTP int references ThanhPho(idTP)
)
go

create table NhaTro (
	idNT int IDENTITY(1,1) primary key,
	DienTich int,
	PhongNgu int,
	Lau int,
	NhaTam int,
	NgayDang date,
	Gia int,
	idBDS int, 
	idKieuBDS int,
	idQuan int,
)
go

create table BaiViet (
	idBV int IDENTITY(1,1) primary key,
	TieuDe nvarchar(255),
	TieuDePhu nvarchar(255),
	MoTa text,
	idTK int references TaiKhoan(idTK),
	idNT int references NhaTro(idNT),
)
go

create table BaoCao (
	idBC int IDENTITY(1,1) primary key,
	TieuDe nvarchar(255),
	TenBaoCao nvarchar(100),
	idBV int references BaiViet(idBV)
)
go

create table HinhAnh (
	idHA int IDENTITY(1,1) primary key,
	Link varchar(255),
	idNT int references NhaTro(idNT),
	idTK int references TaiKhoan(idTK)
)
go


insert into TaiKhoan values ('admin','thanhnguyen1234' ,'e10adc3949ba59abbe56e057f20f883e','wh1.knightz@gmail.com','0123456789',	'2011-11-30', 'nam', N'Nguyễn Trường Thành')
insert into TaiKhoan values ('user','thanhson1234' ,'e10adc3949ba59abbe56e057f20f883e','wh.knightz@gmail.com','0123456789',	'2011-11-30', 'nam', N'Đỗ Thanh Sơn')


--update data 25-11
alter table BaiViet
add TrangThai bit

alter table TaiKhoan
add TrangThai bit

--update data 27-11

--Chỉnh sửa lại cột mô tả từ text thành nvarchar(max)
ALTER TABLE BaiViet 
ALTER COLUMN MoTa NVARCHAR(MAX) 
--Xóa cột ngày đăng Nhà trọ và thêm vào bài viết
ALTER TABLE NhaTro
DROP COLUMN NgayDang

ALTER TABLE BaiViet 
add  NgayDang date 

insert into Quan values (N'Quận 1')
insert into NhaTro values (40, NULL, NULL, NULL, 5500000, NULL, NULL, 1)
insert into BaiViet values (N'Phòng Studio, 1PN đầy đủ tiện nghi có ban công ngay CV Lê Văn Tám Q1', N'Thạch Thị Thanh, Phường Tân Định, Quận 1, TPHCM', N'Vị trí đắt địa trung tâm Quận 3, dễ dàng di chuyển qua các khu vực nội thành Q1, Q10, Q11, Tân Bình, Phú Nhuận Nhà xây đẹp, đẳng cấp, khu vực an ninh, dân trí cao. Trang bị đầy đủ nội thất sịn sò : Tivi internet, tủ lạnh, bếp hút mùi...', 5, 1, 1, '2020-11-27')

-- updata 28-11
create table Phuong(
	idPhuong int IDENTITY(1,1) primary key,
	TenPhuong nvarchar(50),
	idQuan int references Quan(idQuan)
)
go
--update 09-12
ALTER TABLE BaoCao 
add  NgayBC date 
ALTER TABLE BaoCao 
add TrangThai bit

--update 15-12
alter table ThanhPho
add Alias varchar(50)

alter table Quan
add Alias varchar(50)

alter table Phuong
add Alias varchar(50)

alter table LoaiBDS
add Alias varchar(50)