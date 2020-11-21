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


insert into TaiKhoan values ('admin','thanhnguyen1234' ,'1234','wh.knightz@gmail.com','0123456789',	'2011-11-30', 'nam', N'Nguyễn Trường Thành')
insert into TaiKhoan values ('user','thanhson1234' ,'1234','wh.knightz@gmail.com','0123456789',	'2011-11-30', 'nam', N'Đỗ Thanh Sơn')
insert into TaiKhoan values ('user','thanhson12345' ,'e10adc3949ba59abbe56e057f20f883e','wh1.knightz@gmail.com','0123456789',	'2011-11-30', 'nam', N'Đỗ Thanh Sơn')