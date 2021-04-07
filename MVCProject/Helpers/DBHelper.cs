using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
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
        public static List<NhanVien> Get(string key = null,int phongban_id = 0)
        {
            IEnumerable<NhanVien> nhanvien = null;
            using (var connection = new NpgsqlConnection(connectionString)){
                connection.Open();
                if (key == null && phongban_id == 0)
                    nhanvien = connection.Query<NhanVien>("SELECT * from Nhan_Vien order by MaNhanVien ASC");
                else if (key == null ||key == "" )
                    nhanvien = connection.Query<NhanVien>("SELECT * from Nhan_Vien where phongban_id = @phongban_id order by MaNhanVien ASC",new { phongban_id = phongban_id});
                else if (phongban_id == 0)
                    nhanvien = connection.Query<NhanVien>("SELECT * from Nhan_Vien where lower(unaccent(hoten)) like '%' || lower(unaccent(@key)) || '%' OR lower(unaccent(diachi)) like '%' || lower(unaccent(@key)) || '%' order by MaNhanVien ASC", new { key = key });
                else
                     nhanvien = connection.Query<NhanVien>("SELECT * from Nhan_Vien where (lower(unaccent(hoten)) like '%' || lower(unaccent(@key)) || '%' OR lower(unaccent(diachi)) like '%' || lower(unaccent(@key)) || '%') AND phongban_id = @phongban_id order by MaNhanVien ASC", new { key = key,phongban_id = phongban_id });
            }
            return nhanvien.ToList();
        }
        public static List<NhanVien> GetStaffByDP(int key = 0)
        {
            IEnumerable<NhanVien> nhanvien = null;
            using (var connection = new NpgsqlConnection(connectionString)){
                connection.Open();
                if (key == 0)
                    nhanvien = connection.Query<NhanVien>("SELECT * from Nhan_Vien  order by MaNhanVien ASC");
                else
                    nhanvien = connection.Query<NhanVien>("SELECT * from Nhan_Vien where phongban_id = @key order by MaNhanVien ASC", new { key = key });
            }
            return nhanvien.ToList();
        }
        public static List<string> GetChucVu(){
            IEnumerable<string> dsChucVu = null;
            using (var connection = new NpgsqlConnection(connectionString)){
                connection.Open();
                dsChucVu = connection.Query<string>("SELECT DISTINCT chucvu	FROM nhan_vien");
            }
            return dsChucVu.ToList();
        }
        public static List<NhanVien> AdvandSearch(string keySearch,int phongBanId,string chucVu,int min,int max)
        {
            IEnumerable<NhanVien> nhanvien = null;
            using (var connection = new NpgsqlConnection(connectionString)){
                connection.Open();
                if ((keySearch == null || keySearch == "") && phongBanId == 0){
                    if (chucVu == null || chucVu == ""){
                        nhanvien = connection.Query<NhanVien>("SELECT * from Nhan_Vien where sonamcongtac BETWEEN @min AND @max  order by MaNhanVien ASC",new {min = min,max = max});
                    }
                    else
                        nhanvien = connection.Query<NhanVien>("SELECT * from Nhan_Vien where lower(unaccent(chucvu)) like '%' || lower(unaccent(@chucVu)) || '%' AND sonamcongtac BETWEEN @min AND @max  order by MaNhanVien ASC",new {chucVu = chucVu,min = min,max = max});
                }
                    
                else if(keySearch == null || keySearch == ""){
                    if (chucVu == null || chucVu == ""){
                        nhanvien = connection.Query<NhanVien>("SELECT * from Nhan_Vien where phongban_id = @phongban_id and sonamcongtac BETWEEN @min AND @max  order by MaNhanVien ASC", new {phongban_id = phongBanId,min = min,max = max});
                    }
                    else
                        nhanvien = connection.Query<NhanVien>("SELECT * from Nhan_Vien where phongban_id = @phongban_id and lower(unaccent(chucvu)) like '%' || lower(unaccent(@chucVu)) || '%' AND sonamcongtac BETWEEN @min AND @max  order by MaNhanVien ASC", new {phongban_id = phongBanId,chucVu = chucVu,min = min,max = max});
                }
                    
                else if(phongBanId == 0){
                    if (chucVu == null || chucVu == ""){
                        nhanvien = connection.Query<NhanVien>("SELECT * from Nhan_Vien where (lower(unaccent(hoten)) like '%' || lower(unaccent(@key)) || '%' OR lower(unaccent(diachi)) like '%' || lower(unaccent(@key)) || '%') AND sonamcongtac BETWEEN @min AND @max  order by MaNhanVien ASC", new {key = keySearch,min = min,max = max});
                    }
                    else
                        nhanvien = connection.Query<NhanVien>("SELECT * from Nhan_Vien where (lower(unaccent(hoten)) like '%' || lower(unaccent(@key)) || '%' OR lower(unaccent(diachi)) like '%' || lower(unaccent(@key)) || '%')  and lower(unaccent(chucvu)) like '%' || lower(unaccent(@chucVu)) || '%' AND sonamcongtac BETWEEN @min AND @max  order by MaNhanVien ASC", new {key = keySearch,chucVu = chucVu,min = min,max = max});
                }
                    
                else{
                    if (chucVu == null || chucVu == ""){
                        nhanvien = connection.Query<NhanVien>("SELECT * from Nhan_Vien where (lower(unaccent(hoten)) like '%' || lower(unaccent(@key)) || '%' OR lower(unaccent(diachi)) like '%' || lower(unaccent(@key)) || '%') and  phongban_id = @phongban_id AND sonamcongtac BETWEEN @min AND @max  order by MaNhanVien ASC", new {key = keySearch,phongban_id = phongBanId,min = min,max = max});
                    }
                    else
                        nhanvien = connection.Query<NhanVien>("SELECT * from Nhan_Vien where (lower(unaccent(hoten)) like '%' || lower(unaccent(@key)) || '%' OR lower(unaccent(diachi)) like '%' || lower(unaccent(@key)) || '%') and  phongban_id = @phongbanid and lower(unaccent(chucvu)) like '%' || lower(unaccent(@chucVu)) || '%' AND sonamcongtac BETWEEN @min AND @max  order by MaNhanVien ASC", new {key = keySearch,phongban_id = phongBanId,chucVu = chucVu,min = min,max = max});
                }
                    
            }
            return nhanvien.ToList();
        }
        public static List<PhongBan> GetDP(string key = null){
            IEnumerable<PhongBan> dsPhongBan = null;
            using (var connection = new NpgsqlConnection(connectionString)){
                connection.Open();
                if (key == null)
                    dsPhongBan = connection.Query<PhongBan>("SELECT * from Phong_ban order by id ASC");
                else    
                    dsPhongBan = connection.Query<PhongBan>("SELECT * from Phong_ban where lower(unaccent(Ten_Phong_Ban)) like '%' || lower(unaccent(@key)) || '%' ", new { key = key });
            }
            return dsPhongBan.ToList();
        }
        public static NhanVien GetByMNV(string key = "")
        {
            IEnumerable<NhanVien> nhanvien = null;
            
            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();
                    nhanvien = connection.Query<NhanVien>("SELECT * from Nhan_Vien where MaNhanVien = @key ", new { key = key });
            }
            return nhanvien.First();
        }
        public static PhongBan GetbyMDP(int key = 0)
        {
            IEnumerable<PhongBan> phongBan = null;
            
            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();
                    phongBan = connection.Query<PhongBan>("SELECT * from phong_ban where id = @key ", new { key = key });
            }
            return phongBan.First();
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
                connection.Execute("Insert into Nhan_Vien(MaNhanVien,HoTen,NgaySinh, SoDienThoai,DiaChi,ChucVu,SoNamCongTac,phongban_id) values(@maNhanVien,@hoTen,@ngaySinh,@soDienThoai,@diaChi,@chucVu,@soNamCongTac,@phongban_id)",   new { nv.MaNhanVien,nv.HoTen,nv.NgaySinh,nv.SoDienThoai,nv.DiaChi,nv.ChucVu,nv.SoNamCongTac,nv.PhongBan_Id });

            }
        }
        public static void CreatePB(PhongBan pb)
        {
            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();
               connection.Execute("INSERT INTO phong_ban(TenPhongBan) VALUES (@tenPhongBan);",   new { pb.TenPhongBan });

            }
        }
        public static void Update(NhanVien nv)
        {
            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();
                connection.Execute("Update Nhan_Vien SET  HoTen = @hoTen ,NgaySinh = @ngaySinh, SoDienThoai = @soDienThoai,DiaChi = @diaChi,ChucVu = @chucVu,SoNamCongTac = @soNamCongTac,phongban_id = (Select id from Phong_ban where id = @PhongBan_Id) WHERE MaNhanVien = @MaNhanVien", new { nv.HoTen, nv.NgaySinh, nv.SoDienThoai, nv.DiaChi, nv.ChucVu, nv.SoNamCongTac,nv.PhongBan_Id, nv.MaNhanVien });

            }
        }
        public static void UpdateDP(PhongBan pb)
        {
            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();
                connection.Execute("Update phong_ban SET  TenPhongBan = @TenPhongBan where id = @id", new {pb.id, pb.TenPhongBan});
            }
        }
        public static void Delete(string maNhanVien)
        {
            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();
                connection.Execute("Delete FROM  Nhan_Vien where MaNhanVien = @maNhanVien", new { maNhanVien = maNhanVien});

            }
        }
        public static void DeleteDP(int id)
        {
            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();
                connection.Execute("Delete FROM  phong_ban where id = @id", new { id = id});

            }
        }

    }
}
