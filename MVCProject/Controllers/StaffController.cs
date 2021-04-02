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
        private readonly int itemPerPage = 8;
        [HttpGet]
        public IActionResult Index(int phongban_id=0)
        {
            ViewBag.PhongBanId = phongban_id;
            ViewBag.dsPhongBan = new List<PhongBan>(DBHelper.GetDP());
            ViewBag.pageNumberIndex = (int)DBHelper.Get().Count/ itemPerPage;
            return View(DBHelper.Get());
        }
        [HttpPost]
        public IActionResult Index( string key = "")
        {
            
            return View(DBHelper.Get(key));

        }
        [HttpPost]
        public IActionResult Search(string key = "")
        {
            
            List<NhanVien> dsTim = DBHelper.Get(key);
            ViewBag.dsPhongBan = new List<PhongBan>(DBHelper.GetDP());
            ViewBag.itemPerPage = itemPerPage;
            ViewBag.pageNumber = (int)dsTim.Count/ itemPerPage;
            ViewBag.currentPage = "p-1";
            return _Table((int)dsTim.Count/ itemPerPage,"p-1",dsTim);

        }
        public IActionResult DepartmentStaffList(int PhongBanId){
            List<NhanVien> dsTim = DBHelper.GetStaffByDP(PhongBanId);
            ViewBag.dsPhongBan = new List<PhongBan>(DBHelper.GetDP());
            ViewBag.itemPerPage = itemPerPage;
            ViewBag.pageNumber = (int)dsTim.Count/ itemPerPage;
            ViewBag.currentPage = "p-1";
            return _Table((int)dsTim.Count/ itemPerPage,"p-1",dsTim);
        }
        [HttpPost]
        public int Create(NhanVien newItem = null)
        {
            newItem.HoTen = GHelper.XuLyTen(newItem.HoTen);
            newItem.DiaChi = GHelper.XuLyTen(newItem.DiaChi);
            DBHelper.Create(newItem);
            if( ((int)DBHelper.Get().Count % itemPerPage == 0) && ((int)DBHelper.Get().Count / itemPerPage > 0)){
                return (int)DBHelper.Get().Count / itemPerPage -1;
            }
            return (int)DBHelper.Get().Count / itemPerPage;
        }
        [HttpGet]
        public IActionResult Create()
        {
            double LastStaffId = DBHelper.GetTheLastID();
            if (LastStaffId % 10 == 0) {
                ViewBag.LastStaffId = "NV-" + (LastStaffId / 10000).ToString().Substring(2) + "0";
            }
            else {
                ViewBag.LastStaffId = "NV-" + (LastStaffId / 10000).ToString().Substring(2);
            }
            ViewBag.dsPhongBan = new List<PhongBan>(DBHelper.GetDP());
            return View();
        }
        [HttpPost]
        public int Edit(NhanVien newItem = null, int pageIndex = 0)
        {
            newItem.HoTen = GHelper.XuLyTen(newItem.HoTen);
            newItem.DiaChi = GHelper.XuLyTen(newItem.DiaChi);
            DBHelper.Update(newItem);
            return pageIndex;
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            ViewBag.dsPhongBan = new List<PhongBan>(DBHelper.GetDP());
            return View(DBHelper.GetByMNV(id));
        }
        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public int Delete(string MaNhanVien, int pageIndex = 0)
        {
            DBHelper.Delete(MaNhanVien);
            return pageIndex;
        }
        public IActionResult Report()
        {
            return Content("dang xay dung");
        }
       public StaffController()
        {}
        public IActionResult _Table(int pageNumber,string currentPage = "p-1",List<NhanVien> dsNhanVien = null){
            if(dsNhanVien.Count == 0) dsNhanVien = DBHelper.Get();
            ViewBag.dsPhongBan = new List<PhongBan>(DBHelper.GetDP());
            ViewBag.itemPerPage = itemPerPage;
            ViewBag.pageNumber = pageNumber;
            ViewBag.currentPage = currentPage;
            ViewBag.pageIndex = Convert.ToInt32(ViewBag.currentPage.Substring(2))-1;
            return PartialView("_Table",dsNhanVien);
        }
        [HttpPost]
        public bool ExcelExport(){
            ExportExcelHelper.Export(DBHelper.Get());
            return true;
        }
        public bool IsDuplicatedStaff(NhanVien pnv)
        {
            pnv.HoTen = GHelper.XuLyTen(pnv.HoTen);
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
            pnv.HoTen = GHelper.XuLyTen(pnv.HoTen);
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
