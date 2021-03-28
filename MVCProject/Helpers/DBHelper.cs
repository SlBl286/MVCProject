using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Helpers
{
    public static class DBHelper
    {
        private static readonly string connectionString = "HOST=127.0.0.1;Username=postgres;Password=220287;Database=MVCProject";
        public static List<NhanVien> Get(string key = null)
        {
            IEnumerable<NhanVien> nhanvien = null;
            using (var connection = new NpgsqlConnection(connectionString)){
                connection.Open();
                if (key == null)
                    nhanvien = connection.Query<NhanVien>("SELECT * from NhanVien order by MaNhanVien ASC");
                else
                    nhanvien = connection.Query<NhanVien>("SELECT * from NhanVien where lower(unaccent(hoten)) like '%' || lower(unaccent(@key)) || '%' OR lower(unaccent(diachi)) like '%' || lower(unaccent(@key)) || '%' ", new { key = key });
            }
            return nhanvien.ToList();
        }
        public static NhanVien GetByMNV(string key = "")
        {
            IEnumerable<NhanVien> nhanvien = null;
            
            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();
                    nhanvien = connection.Query<NhanVien>("SELECT * from NhanVien where MaNhanVien = @key ", new { key = key });
            }
            return nhanvien.First();
        }
        public static int GetTheLastID()
        {
            
            
            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();
                var id = connection.Query<int>("select nextval('nhanvien_id_seq'::regclass); ");
                if ((int)id.Count() == 0) return 0;
                return id.First();
            }
            
        }
        public static void Create(NhanVien nv)
        {
            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();
                connection.Execute("Insert into NhanVien(MaNhanVien,HoTen,NgaySinh, SoDienThoai,DiaChi,ChucVu,SoNamCongTac) values(@maNhanVien,@hoTen,@ngaySinh,@soDienThoai,@diaChi,@chucVu,@soNamCongTac)",   new { nv.MaNhanVien,nv.HoTen,nv.NgaySinh,nv.SoDienThoai,nv.DiaChi,nv.ChucVu,nv.SoNamCongTac });

            }
        }
        public static void Update(NhanVien nv)
        {
            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();
                connection.Execute("Update NhanVien SET  HoTen = @hoTen ,NgaySinh = @ngaySinh, SoDienThoai = @soDienThoai,DiaChi = @diaChi,ChucVu = @chucVu,SoNamCongTac = @soNamCongTac WHERE MaNhanVien = @maNhanVien", new { nv.MaNhanVien, nv.HoTen, nv.NgaySinh, nv.SoDienThoai, nv.DiaChi, nv.ChucVu, nv.SoNamCongTac });

            }
        }
        public static void Delete(string maNhanVien)
        {
            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();
                connection.Execute("Delete FROM  NhanVien where MaNhanVien = @maNhanVien", new { maNhanVien = maNhanVien});

            }
        }

    }
}
