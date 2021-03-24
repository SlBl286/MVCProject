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
        public string maNhanVien { get; set; }
        [Display(Name = "Họ Tên")]
        [Required(ErrorMessage = "Không được bỏ trống tên")]  
        public string hoTen { get; set; }
        [Display(Name = "Ngày Sinh")]    [Required(ErrorMessage = "Điền Ngày Sinh")]
        public DateTime ngaySinh { get; set; }
        [Display(Name = "Số điện thoại")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Số điện Thoại sai")]
        public string soDienThoai {  set; get; }
        [Display(Name = "Địa Chỉ")]
        public string diaChi { get; set; }
        [Display(Name = "Chức Vụ")]
        [Required(ErrorMessage = "Hãy Điền Chức Vụ")]
        public string chucVu { get; set; }
        [Display(Name = "Số Năm Công Tác")]
        [Required(ErrorMessage = "Hãy Điền Số Năm Công Tác")]
        public int soNamCongTac { get; set; }

        public NhanVien() { }
        public NhanVien(string maNhanVien, string hoTen, DateTime ngaySinh, string soDienThoai, string diaChi, string chucVu, int soNamCongTac)
        {
            this.maNhanVien = maNhanVien;
            this.hoTen = hoTen;
            this.ngaySinh = ngaySinh;
            this.soDienThoai = soDienThoai;
            this.diaChi = diaChi;
            this.chucVu = chucVu;
            this.soNamCongTac = soNamCongTac;
        }
    }
}
