using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Models
{
    public class NhanVien
    {
        public string maNhanVien { get; set; }
        public string hoTen { get; set; }

       
        public DateTime ngaySinh { get; set; }
        public string soDienThoai { get; set; }
        public string diaChi { get; set; }
        public string chucVu { get; set; }
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
