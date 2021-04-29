using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DACN.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<BaiViet> BaiViets { get; set; }
        public virtual DbSet<BaoCao> BaoCaos { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<DanhSachYeuThich> DanhSachYeuThiches { get; set; }
        public virtual DbSet<HinhAnh> HinhAnhs { get; set; }
        public virtual DbSet<KieuBD> KieuBDS { get; set; }
        public virtual DbSet<LoaiBaiViet> LoaiBaiViets { get; set; }
        public virtual DbSet<LoaiBD> LoaiBDS { get; set; }
        public virtual DbSet<NhaTro> NhaTroes { get; set; }
        public virtual DbSet<Phuong> Phuongs { get; set; }
        public virtual DbSet<Quan> Quans { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<ThanhPho> ThanhPhoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HinhAnh>()
                .Property(e => e.Link)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiBD>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<NhaTro>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Phuong>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<Quan>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.RoleTK)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.Pass)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.GioiTinh)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.CMND)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.QuyenHan)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.Avatar)
                .IsUnicode(false);

            modelBuilder.Entity<ThanhPho>()
                .Property(e => e.Alias)
                .IsUnicode(false);
        }
    }
}
