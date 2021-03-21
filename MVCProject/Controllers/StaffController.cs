﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Helpers;
using MVCProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MVCProject.Controllers
{
    public class StaffController : Controller
    {
        public float LastStaffId;
        public List<NhanVien> DsNhanVien = new List<NhanVien>();
        public SessionHelper SessionHelper = new SessionHelper();
       [HttpGet]
        public IActionResult Index()
        {

            byte[] json;
            if (HttpContext.Session.TryGetValue("StaffList", out json)) {
                DsNhanVien = JsonConvert.DeserializeObject<List<NhanVien>>(HttpContext.Session.GetString("StaffList"));
                LastStaffId = JsonConvert.DeserializeObject<float>(HttpContext.Session.GetString("LastStaffId"));
            }
            else {

                HttpContext.Session.SetString("StaffList", JsonConvert.SerializeObject(DsNhanVien));
                HttpContext.Session.SetString("LastStaffId", JsonConvert.SerializeObject(LastStaffId));
            }

            return View(DsNhanVien);
        }
        [HttpPost]
        public IActionResult index( string key = "")
        {

            byte[] json;
            if (HttpContext.Session.TryGetValue("StaffList", out json)) {
                DsNhanVien = JsonConvert.DeserializeObject<List<NhanVien>>(HttpContext.Session.GetString("StaffList"));
                LastStaffId = JsonConvert.DeserializeObject<float>(HttpContext.Session.GetString("LastStaffId"));
            }
            else {

                HttpContext.Session.SetString("StaffList", JsonConvert.SerializeObject(DsNhanVien));
                HttpContext.Session.SetString("LastStaffId", JsonConvert.SerializeObject(LastStaffId));
            }
            if(key != "") {
                List<NhanVien> dsTimKiem = new List<NhanVien>();
                foreach (NhanVien nv in DsNhanVien) {
                    if(nv.hoTen.ToLower().Contains(key.ToLower()) || nv.diaChi.ToLower().Contains(key.ToLower())) {
                        dsTimKiem.Add(nv);
                    }
                }
                return View(dsTimKiem);

            }
            return View(DsNhanVien);

        }
        [HttpPost]
        public IActionResult Create(NhanVien newItem = null)
        {
            
            byte[] json;
            if (HttpContext.Session.TryGetValue("StaffList", out json)) {
                DsNhanVien = JsonConvert.DeserializeObject<List<NhanVien>>(HttpContext.Session.GetString("StaffList"));
                LastStaffId = JsonConvert.DeserializeObject<float>(HttpContext.Session.GetString("LastStaffId"));
                newItem.hoTen = xuLyTen(newItem.hoTen);
                if (!IsDuplicatedStaff(newItem)) {
                    DsNhanVien.Add(newItem);
                }  
                LastStaffId += 1;
                HttpContext.Session.SetString("LastStaffId", JsonConvert.SerializeObject(LastStaffId));
                HttpContext.Session.SetString("StaffList", JsonConvert.SerializeObject(DsNhanVien));
                
            }
            return Redirect("/staff/index");
        }
        [HttpGet]
        public IActionResult Create()
        {
            LastStaffId = JsonConvert.DeserializeObject<float>(HttpContext.Session.GetString("LastStaffId"));
            if (this.LastStaffId % 10 == 0) {
                ViewBag.LastStaffId = "NV-" + (this.LastStaffId / 10000).ToString().Substring(2) + "0";
            }
            else {
                ViewBag.LastStaffId = "NV-" + (this.LastStaffId / 10000).ToString().Substring(2);
            }
            ViewBag.StaffListJson = HttpContext.Session.GetString("StaffList");
            return View();
        }
        [HttpPost]
        public IActionResult Edit(NhanVien newItem = null)
        {
            var list = SessionHelper.GetValue("tesst");
            byte[] json;
            if (HttpContext.Session.TryGetValue("StaffList", out json)) {
                DsNhanVien = JsonConvert.DeserializeObject<List<NhanVien>>(HttpContext.Session.GetString("StaffList"));         
            }
            newItem.hoTen = xuLyTen(newItem.hoTen);
            for (int i  = 0;i < DsNhanVien.Count;i++) {
                if (DsNhanVien[i].hoTen == newItem.hoTen && DateTime.Compare(DsNhanVien[i].ngaySinh,newItem.ngaySinh) == 0 && DsNhanVien[i].maNhanVien != newItem.maNhanVien) {
                    ViewBag.error = true;
                    return View();
                }
                
            }
            for (int i = 0; i < DsNhanVien.Count; i++) {
                if (DsNhanVien[i].maNhanVien == newItem.maNhanVien) {
                    DsNhanVien[i] = newItem;
                    HttpContext.Session.SetString("StaffList", JsonConvert.SerializeObject(DsNhanVien));
                    break;
                }
            }
            
            return Redirect("/staff/index");
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            byte[] json;
            if (HttpContext.Session.TryGetValue("StaffList", out json)) {
                DsNhanVien = JsonConvert.DeserializeObject<List<NhanVien>>(HttpContext.Session.GetString("StaffList"));
            }
            return View(GetByMNV(id));
        }
        public IActionResult Update()
        {
            return View();
        }
        public IActionResult Delete(string id)
        {
            byte[] json;
            if (HttpContext.Session.TryGetValue("StaffList", out json)) {
                DsNhanVien = JsonConvert.DeserializeObject<List<NhanVien>>(HttpContext.Session.GetString("StaffList"));
            }
            for (int i = 0; i < DsNhanVien.Count; i++) {
                if (DsNhanVien[i].maNhanVien == id) {
                    DsNhanVien.RemoveAt(i);
                    HttpContext.Session.SetString("StaffList", JsonConvert.SerializeObject(DsNhanVien));
                }
            }
            return Redirect("/staff/index");
        }
        public IActionResult Report()
        {
            return Content("dang xay dung");
        }
       public StaffController()
        {
            TaoDsNhanVien();
            LastStaffId = 6;
            SessionHelper.SetValue("StaffList", DsNhanVien);
        }
        public void TaoDsNhanVien()
        {
            DsNhanVien.Add(new NhanVien("NV-0001", "Đoàn Duy Quý", DateTime.Parse("28/6/2000"), "09715869331", "Hà Nội", "Nhan vien", 5));
            DsNhanVien.Add(new NhanVien("NV-0002", "Nguyễn Văn A", DateTime.Parse("3/7/1999"), "09715869331", "Hồ Chí Minh", "Nhan vien", 5));
            DsNhanVien.Add(new NhanVien("NV-0003", "Le Thi B", DateTime.Parse("8/12/1996"), "09715869331", "Hà Nội", "Nhan vien", 5));
            DsNhanVien.Add(new NhanVien("NV-0004", "Trần Văn C", DateTime.Parse("28/6/2000"), "09715869331", "Hải Phòng", "Nhan vien", 5));
            DsNhanVien.Add(new NhanVien("NV-0005", "Bùi Tiến Đạt", DateTime.Parse("24/9/2000"), "09715869331", "Đà Nẵng", "Nhan vien", 5));
        }
        public NhanVien GetByMNV(string MNV)
        {
            NhanVien result = null;
            DsNhanVien = JsonConvert.DeserializeObject<List<NhanVien>>(HttpContext.Session.GetString("StaffList"));
            foreach(NhanVien nv in DsNhanVien) {
                if (nv.maNhanVien == MNV) result = nv;
            }

            return result; 
        }
        public string xuLyTen(string name)
        {

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
            string result = "";
            for (int i = 0; i < arrName.Count; i++) {
                result += arrName[i].ToString();
            }
            return result;
        }

        public bool IsDuplicatedStaff(NhanVien pnv)
        {
            pnv.hoTen = xuLyTen(pnv.hoTen);
            bool daTonTai = false;
            byte[] json;
            if (HttpContext.Session.TryGetValue("StaffList", out json)) {
                DsNhanVien = JsonConvert.DeserializeObject<List<NhanVien>>(HttpContext.Session.GetString("StaffList"));
            }
            foreach (NhanVien nv in DsNhanVien) {
                if (nv.hoTen == pnv.hoTen) {
                    if (DateTime.Compare(nv.ngaySinh, pnv.ngaySinh) == 0) {
                        daTonTai = true;
                        break;

                    }
                    else { daTonTai = false; }
                }
                else { daTonTai = false; }
            }
            return daTonTai;
        }
    }
}
