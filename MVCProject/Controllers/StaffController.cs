using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Helpers;
using MVCProject.Models;
using System;
using System.Collections.Generic;

namespace MVCProject.Controllers
{
    public class StaffController : Controller
    {
         
        public float LastStaffId;
        public DBHelper DBHelper = new DBHelper("HOST=127.0.0.1;Username=postgres;Password=220287;Database=MVCProject");
       [HttpGet]
        public IActionResult Index()
        {
            return View(DBHelper.Get());
        }
        [HttpPost]
        public JsonResult Search( string key = "")
        {

            return DBHelper.Get(key);

        }
        [HttpPost]
        public IActionResult Create(NhanVien newItem = null)
        {
            newItem.HoTen = XuLyTen(newItem.HoTen);
            newItem.DiaChi = XuLyTen(newItem.DiaChi);
            DBHelper.Create(newItem);
            return Redirect("/staff/index");
        }
        [HttpGet]
        public IActionResult Create()
        {
            LastStaffId = DBHelper.GetTheLastID();
            if (this.LastStaffId % 10 == 0) {
                ViewBag.LastStaffId = "NV-" + (this.LastStaffId / 10000).ToString().Substring(2) + "0";
            }
            else {
                ViewBag.LastStaffId = "NV-" + (this.LastStaffId / 10000).ToString().Substring(2);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Edit(NhanVien newItem = null)
        {
            newItem.HoTen = XuLyTen(newItem.HoTen);
            newItem.DiaChi = XuLyTen(newItem.DiaChi);
            DBHelper.Update(newItem);
            return Redirect("/staff/index");
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            
            return View(GetByMNV(id));
        }
        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public bool Delete(string MaNhanVien)
        {
            DBHelper.Delete(MaNhanVien);
            return true;
        }
        public IActionResult Report()
        {
            return Content("dang xay dung");
        }
       public StaffController()
        {
        }
        
        public NhanVien GetByMNV(string MNV)
        {
            NhanVien result = DBHelper.GetByMNV(MNV);

                return result; 
        }
        public string XuLyTen(string name)
        {
            string result = "";
            if (name != "" && name != null) {
                List<char> arrName = new List<char>(name.ToLower().Trim().ToCharArray());
                for (int i = 0; i < arrName.Count; i++) {
                    if (arrName[i] == ' ') {
                        int _i = i + 1;
                        if (arrName[_i] == ' ') {
                            arrName.RemoveAt(i);
                        }
                    }
                }
                arrName[0] = char.ToUpper(arrName[0]);
                for (int i = 0; i < arrName.Count; i++) {
                    if (arrName[i] == ' ') {
                        int _i = i + 1;
                        arrName[_i] = char.ToUpper(arrName[_i]);
                    }
                }
                
                for (int i = 0; i < arrName.Count; i++) {
                    result += arrName[i].ToString();
                }
            }
            return result;
        }

        public bool IsDuplicatedStaff(NhanVien pnv)
        {
            pnv.HoTen = XuLyTen(pnv.HoTen);
            bool daTonTai = false;
            List<NhanVien> DsNhanVien = DBHelper.Get();
            foreach (NhanVien nv in DsNhanVien) {
                if (nv.HoTen == pnv.HoTen) {
                    if (DateTime.Compare(nv.NgaySinh, pnv.NgaySinh) == 0) {
                        daTonTai = true;
                        break;

                    }
                    else { daTonTai = false; }
                }
                else { daTonTai = false; }
            }
            return daTonTai;
        }
        public bool EditValidate(NhanVien pnv)
        {
            bool daTonTai = false;
            List<NhanVien> DsNhanVien = DBHelper.Get();
            pnv.HoTen = XuLyTen(pnv.HoTen);
            for (int i = 0; i < DsNhanVien.Count; i++) {
                if (DsNhanVien[i].HoTen == pnv.HoTen && DateTime.Compare(DsNhanVien[i].NgaySinh, pnv.NgaySinh) == 0 && DsNhanVien[i].MaNhanVien != pnv.MaNhanVien) {
                    daTonTai = true;
                    break;
                }
                
            }
            return daTonTai;
        }
    }
}
