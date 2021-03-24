using Microsoft.AspNetCore.Http;
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
            LastStaffId = JsonConvert.DeserializeObject<float>(HttpContext.Session.GetString("LastStaffId"));
            if (this.LastStaffId % 10 == 0) {
                ViewBag.LastStaffId = "NV-" + (this.LastStaffId / 10000).ToString().Substring(2) + "0";
            }
            else {
                ViewBag.LastStaffId = "NV-" + (this.LastStaffId / 10000).ToString().Substring(2);
            }
            ViewBag.StaffListJson = HttpContext.Session.GetString("StaffList");
            ViewData["temp"] = null;
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
                newItem.hoTen = XuLyTen(newItem.hoTen);
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
            byte[] json;
            if (HttpContext.Session.TryGetValue("StaffList", out json)) {
                DsNhanVien = JsonConvert.DeserializeObject<List<NhanVien>>(HttpContext.Session.GetString("StaffList"));         
            }
            newItem.hoTen = XuLyTen(newItem.hoTen);
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
        [HttpPost]
        public bool Delete(string maNhanVien)
        {
            byte[] json;
            if (HttpContext.Session.TryGetValue("StaffList", out json)) {
                DsNhanVien = JsonConvert.DeserializeObject<List<NhanVien>>(HttpContext.Session.GetString("StaffList"));
            }
            for (int i = 0; i < DsNhanVien.Count; i++) {
                if (DsNhanVien[i].maNhanVien == maNhanVien) {
                    DsNhanVien.RemoveAt(i);
                    HttpContext.Session.SetString("StaffList", JsonConvert.SerializeObject(DsNhanVien));
                }
            }
            return true;
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
            DsNhanVien.Add(new NhanVien("NV-0001", "Đoàn Duy Quý", DateTime.Parse("28/6/2000"), "0971586931", "Hà Nội", "Nhan vien", 5));
            DsNhanVien.Add(new NhanVien("NV-0002", "Nguyễn Văn A", DateTime.Parse("3/7/1999"), "0971586931", "Hồ Chí Minh", "Nhan vien", 5));
            DsNhanVien.Add(new NhanVien("NV-0003", "Le Thi B", DateTime.Parse("8/12/1996"), "0971586931", "Hà Nội", "Nhan vien", 5));
            DsNhanVien.Add(new NhanVien("NV-0004", "Trần Văn C", DateTime.Parse("28/6/2000"), "0971586931", "Hải Phòng", "Nhan vien", 5));
            DsNhanVien.Add(new NhanVien("NV-0005", "Bùi Tiến Đạt", DateTime.Parse("24/9/2000"), "0971586931", "Đà Nẵng", "Nhan vien", 5));
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
            pnv.hoTen = XuLyTen(pnv.hoTen);
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
        public bool EditValidate(NhanVien pnv)
        {
            bool daTonTai = false;
            byte[] json;
            if (HttpContext.Session.TryGetValue("StaffList", out json)) {
                DsNhanVien = JsonConvert.DeserializeObject<List<NhanVien>>(HttpContext.Session.GetString("StaffList"));
            }
            pnv.hoTen = XuLyTen(pnv.hoTen);
            for (int i = 0; i < DsNhanVien.Count; i++) {
                if (DsNhanVien[i].hoTen == pnv.hoTen && DateTime.Compare(DsNhanVien[i].ngaySinh, pnv.ngaySinh) == 0 && DsNhanVien[i].maNhanVien != pnv.maNhanVien) {
                    daTonTai = true;
                    break;
                }
                
            }
            return daTonTai;
        }
    }
}
