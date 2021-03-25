﻿using Dapper;
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
    public class DBHelper
    {
        private readonly string connectionString;
        public List<NhanVien> Get(string key = "")
        {
            IEnumerable<NhanVien> nhanvien = null;
            using (var connection = new NpgsqlConnection(this.connectionString)){
                connection.Open();
                if (key == "")
                    nhanvien = connection.Query<NhanVien>("SELECT * from NhanVien order by MaNhanVien ASC");
                else
                    nhanvien = connection.Query<NhanVien>("SELECT * from NhanVien where HoTen like '%' || @key || '%' OR DiaChi LIKE '%' || @key || '%'", new { key = key });
            }
            return nhanvien.ToList();
        }
        public NhanVien GetByMNV(string key = "")
        {
            IEnumerable<NhanVien> nhanvien = null;
            
            using (var connection = new NpgsqlConnection(this.connectionString)) {
                connection.Open();
                    nhanvien = connection.Query<NhanVien>("SELECT * from NhanVien where MaNhanVien = @key ", new { key = key });
            }
            return nhanvien.First();
        }
        public int GetTheLastID()
        {
            
            
            using (var connection = new NpgsqlConnection(this.connectionString)) {
                connection.Open();
                var id = connection.Query<int>("select nextval('nhanvien_id_seq'::regclass); ");
                if ((int)id.Count() == 0) return 0;
                return id.First();
            }
            
        }
        public void Create(NhanVien nv)
        {
            using (var connection = new NpgsqlConnection(this.connectionString)) {
                connection.Open();
                connection.Execute("Insert into NhanVien(MaNhanVien,HoTen,NgaySinh, SoDienThoai,DiaChi,ChucVu,SoNamCongTac) values(@maNhanVien,@hoTen,@ngaySinh,@soDienThoai,@diaChi,@chucVu,@soNamCongTac)",   new { nv.MaNhanVien,nv.HoTen,nv.NgaySinh,nv.SoDienThoai,nv.DiaChi,nv.ChucVu,nv.SoNamCongTac });

            }
        }
        public void Update(NhanVien nv)
        {
            using (var connection = new NpgsqlConnection(this.connectionString)) {
                connection.Open();
                connection.Execute("Update NhanVien SET  HoTen = @hoTen ,NgaySinh = @ngaySinh, SoDienThoai = @soDienThoai,DiaChi = @diaChi,ChucVu = @chucVu,SoNamCongTac = @soNamCongTac WHERE MaNhanVien = @maNhanVien", new { nv.MaNhanVien, nv.HoTen, nv.NgaySinh, nv.SoDienThoai, nv.DiaChi, nv.ChucVu, nv.SoNamCongTac });

            }
        }
        public void Delete(string maNhanVien)
        {
            using (var connection = new NpgsqlConnection(this.connectionString)) {
                connection.Open();
                connection.Execute("Delete FROM  NhanVien where MaNhanVien = @maNhanVien", new { maNhanVien = maNhanVien});

            }
        }
        public DBHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }


    }
}
