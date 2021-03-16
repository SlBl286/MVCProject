using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Create(NhanVien newItem = null)
        {

            byte[] json;
            if (HttpContext.Session.TryGetValue("StaffList", out json)) {
                DsNhanVien = JsonConvert.DeserializeObject<List<NhanVien>>(HttpContext.Session.GetString("StaffList"));
                LastStaffId = JsonConvert.DeserializeObject<float>(HttpContext.Session.GetString("LastStaffId"));
                DsNhanVien.Add(newItem);
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
            
            return View();
        }
        [HttpPost]
        public IActionResult Edit(NhanVien newItem = null)
        {
            byte[] json;
            if (HttpContext.Session.TryGetValue("StaffList", out json)) {
                DsNhanVien = JsonConvert.DeserializeObject<List<NhanVien>>(HttpContext.Session.GetString("StaffList"));         
            }
            for (int i  = 0;i < DsNhanVien.Count;i++) {
                if (DsNhanVien[i].maNhanVien == newItem.maNhanVien) {
                    DsNhanVien[i] = newItem;
                    HttpContext.Session.SetString("StaffList", JsonConvert.SerializeObject(DsNhanVien));
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
        }
        public void TaoDsNhanVien()
        {
            DsNhanVien.Add(new NhanVien("NV-0001", "Đoàn Duy Quý", DateTime.Parse("28/6/2000"), "09715869331", "Ha Noi", "Nhan vien", 5));
            DsNhanVien.Add(new NhanVien("NV-0002", "Nguyen Van A", DateTime.Parse("3/7/1999"), "09715869331", "Ha Noi", "Nhan vien", 5));
            DsNhanVien.Add(new NhanVien("NV-0003", "Le Thi B", DateTime.Parse("8/12/1996"), "09715869331", "Ha Noi", "Nhan vien", 5));
            DsNhanVien.Add(new NhanVien("NV-0004", "Trần Văn C", DateTime.Parse("28/6/2000"), "09715869331", "Ha Noi", "Nhan vien", 5));
            DsNhanVien.Add(new NhanVien("NV-0005", "Bùi Tiến Đạt", DateTime.Parse("24/9/2000"), "09715869331", "Ha Noi", "Nhan vien", 5));
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
    }
}
