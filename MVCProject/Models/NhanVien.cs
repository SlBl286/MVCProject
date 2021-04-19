using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Models
{
    [Table("nhan_vien")]
    public class NhanVien : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id {get;}
        [Display(Name = "Mã Nhân Viên")]
        [Required]
        [Column("manhanvien")]
        public string MaNhanVien { get; set; }
        [Display(Name = "Họ Tên")]
        [Required(ErrorMessage = "Không được bỏ trống tên")]  
         [Column("hoten")]
        public string HoTen { get; set; }
        [Display(Name = "Ngày Sinh")]    [Required(ErrorMessage = "Điền Ngày Sinh")]
         [Column("ngaysinh")]
        public DateTime NgaySinh { get; set; }
        [Display(Name = "Số điện thoại")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Số điện Thoại sai")]
         [Column("sodienthoai")]
        public string SoDienThoai {  set; get; }
        [Display(Name = "Địa Chỉ")]
         [Column("diachi")]
        public string DiaChi { get; set; }
        [Display(Name = "Chức Vụ")]
        [Required(ErrorMessage = "Hãy Điền Chức Vụ")]
         [Column("chucvu")]
        public string ChucVu { get; set; }
        [Display(Name = "Số Năm Công Tác")]
        [Required(ErrorMessage = "Hãy Điền Số Năm Công Tác")]
         [Column("sonamcongtac")]
        public int SoNamCongTac { get; set; }
        [Display(Name = "Phòng Ban")]
        [Column("phongban_id")]
        public int PhongBan_Id {get;set;}
        [NotMappedAttribute]
        public string PhongBan {get;set;}

        public NhanVien() { }
        public NhanVien(string maNhanVien, string hoTen, DateTime ngaySinh, string soDienThoai, string diaChi, string chucVu, int soNamCongTac)
        {
            this.MaNhanVien = maNhanVien;
            this.HoTen = hoTen;
            this.NgaySinh = ngaySinh;
            this.SoDienThoai = soDienThoai;
            this.DiaChi = diaChi;
            this.ChucVu = chucVu;
            this.SoNamCongTac = soNamCongTac;
        }
    }
}
