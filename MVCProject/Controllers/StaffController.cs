using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Controllers
{
    public class StaffController : Controller
    {
        public List<NhanVien> DsNhanVien = new List<NhanVien> {
            new NhanVien("NV-0001","Đoàn Duy Quý",DateTime.Parse("28/6/2000"),"09715869331","Ha Noi","Nhan vien",5),
            new NhanVien("NV-0002","Nguyen Van A",DateTime.Parse("3/7/1999"),"09715869331","Ha Noi","Nhan vien",5),
            new NhanVien("NV-0003","Le Thi B",DateTime.Parse("8/12/1996"),"09715869331","Ha Noi","Nhan vien",5),
            new NhanVien("NV-0004","Trần Văn C",DateTime.Parse("28/6/2000"),"09715869331","Ha Noi","Nhan vien",5),
            new NhanVien("NV-0005","Bùi Tiến Đạt",DateTime.Parse("24/9/2000"),"09715869331","Ha Noi","Nhan vien",5),
        };
        public IActionResult Index()
        {
            HttpContext.Session.SetString("dsNhanVien", JsonConvert.SerializeObject(DsNhanVien));

            return View(DsNhanVien);
        }
        [HttpPost]
        public IActionResult Create(NhanVien newItem = null)
        {
            HttpContext.Session.SetString("NhanVienMoi", JsonConvert.SerializeObject(newItem));
            return View();
        }
        [HttpGet]
        public IActionResult Create(int Id = 0)
        {
            return View();
        }
        public IActionResult Edit()
        {
            return Content("dang xay dung");
        }
        public IActionResult Update()
        {
            return Content("dang xay dung");
        }
        public IActionResult Delete()
        {
            return Content("dang xay dung");
        }
        public IActionResult Report()
        {
            return Content("dang xay dung");
        }
    }
}
