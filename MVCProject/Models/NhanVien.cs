using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Models
{
    public class NhanVien
    {
        [Display(Name = "Mã Nhân Viên")]
        [Required]
        public string MaNhanVien { get; set; }
        [Display(Name = "Họ Tên")]
        [Required(ErrorMessage = "Không được bỏ trống tên")]  
        public string HoTen { get; set; }
        [Display(Name = "Ngày Sinh")]    [Required(ErrorMessage = "Điền Ngày Sinh")]
        public DateTime NgaySinh { get; set; }
        [Display(Name = "Số điện thoại")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Số điện Thoại sai")]
        public string SoDienThoai {  set; get; }
        [Display(Name = "Địa Chỉ")]
        public string DiaChi { get; set; }
        [Display(Name = "Chức Vụ")]
        [Required(ErrorMessage = "Hãy Điền Chức Vụ")]
        public string ChucVu { get; set; }
        [Display(Name = "Số Năm Công Tác")]
        [Required(ErrorMessage = "Hãy Điền Số Năm Công Tác")]
        public int SoNamCongTac { get; set; }
        [Display(Name = "Phòng Ban")]
        public int PhongBan_Id {get;set;}
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
