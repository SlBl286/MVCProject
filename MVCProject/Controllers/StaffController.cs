using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCProject.Helpers;
using MVCProject.Models;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MVCProject.Controllers
{
    
    public class StaffController : Controller
    {
        public enum Option
        {
            CREATE,
            EDIT
        }
        private readonly int itemPerPage = 8;
        [HttpGet]
        public IActionResult Index(int phongban_id=0)
        {
            ViewBag.PhongBanId = (int)phongban_id;
            ViewBag.dsPhongBan = new List<PhongBan>(DBHelper.GetDP());
            ViewBag.pageNumberIndex = (int)DBHelper.Get().Count/ itemPerPage;
            HttpContext.Session.SetString("currentStaffList",JsonConvert.SerializeObject(DBHelper.Get()));
            List<string> dsChucVu = DBHelper.GetChucVu();
            var selectListItems = dsChucVu.Select(x => new SelectListItem(){ Value = x, Text = x }).ToList();
            return View(selectListItems);
        }
        [HttpPost]
        public IActionResult Index( string key = "")
        {
            
            return View(DBHelper.Get(key));

        }
        [HttpPost]
        public IActionResult Search(string key = "")
        {
            if(key == null || key == "") HttpContext.Session.Remove("keySearch");
            HttpContext.Session.SetString("keySearch",JsonConvert.SerializeObject(key));
            List<NhanVien> dsTim = null;
            byte[] json;
            if (HttpContext.Session.TryGetValue("phongban_id", out json)) {
                    dsTim = DBHelper.Get(key,JsonConvert.DeserializeObject<int>(HttpContext.Session.GetString("phongban_id")));
            }
           else{
               dsTim = DBHelper.Get(key);
           }
            
            ViewBag.dsPhongBan = new List<PhongBan>(DBHelper.GetDP());
            ViewBag.itemPerPage = itemPerPage;
            if(dsTim == null){
                ViewBag.pageNumber = (int)DBHelper.Get().Count/ itemPerPage;
            }
            else{
                ViewBag.pageNumber = (int)dsTim.Count/ itemPerPage;
            }
            
            ViewBag.currentPage = "p-1";
            HttpContext.Session.SetString("currentStaffList",JsonConvert.SerializeObject(dsTim));
            return _Table((int)dsTim.Count/ itemPerPage,"p-1",dsTim);

        }
         public IActionResult AdvandSearch(string key = "",string chucVu = "",int min = 0,int max=0)
        {
            List<NhanVien> dsNhanVien = null;
            byte[] json;
            if (HttpContext.Session.TryGetValue("keySearch", out json)) {
                    key = JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("keySearch"));
            }
            int phongBanId = 0;
                
            if (HttpContext.Session.TryGetValue("phongban_id", out json)) {
                    phongBanId = JsonConvert.DeserializeObject<int>(HttpContext.Session.GetString("phongban_id"));
            }
                dsNhanVien = DBHelper.AdvandSearch(key,phongBanId,chucVu,min,max);
                
            ViewBag.dsPhongBan = new List<PhongBan>(DBHelper.GetDP());
            ViewBag.itemPerPage = itemPerPage;
            if(dsNhanVien == null){
                ViewBag.pageNumber = (int)DBHelper.Get().Count/ itemPerPage;
            }
            else{
                ViewBag.pageNumber = (int)dsNhanVien.Count/ itemPerPage;
            }
            
            ViewBag.currentPage = "p-1";
            HttpContext.Session.SetString("currentStaffList",JsonConvert.SerializeObject(dsNhanVien));
            return _Table((int)dsNhanVien.Count/ itemPerPage,"p-1",dsNhanVien);

        }
        public IActionResult DepartmentStaffList(int PhongBanId = 0){
            HttpContext.Session.SetString("phongban_id",JsonConvert.SerializeObject(PhongBanId));
            List<NhanVien> dsTim = null;
            byte[] json;
            if (HttpContext.Session.TryGetValue("keySearch", out json)) {
                    dsTim = DBHelper.Get(JsonConvert.DeserializeObject<string>(HttpContext.Session.GetString("keySearch")),PhongBanId);
            }
            else{
                dsTim = DBHelper.GetStaffByDP(PhongBanId);
            }
            
            ViewBag.dsPhongBan = new List<PhongBan>(DBHelper.GetDP());
            ViewBag.itemPerPage = itemPerPage;
                ViewBag.pageNumber = (int)dsTim.Count/ itemPerPage;
            ViewBag.currentPage = "p-1";
            HttpContext.Session.SetString("currentStaffList",JsonConvert.SerializeObject(dsTim));
            return _Table((int)dsTim.Count/ itemPerPage,"p-1",dsTim);
        }
        [HttpPost]
        public int Create(NhanVien newItem = null)
        { 
            newItem.HoTen = GHelper.XuLyTen(newItem.HoTen);
            newItem.DiaChi = GHelper.XuLyTen(newItem.DiaChi);
            newItem.ChucVu = GHelper.XuLyTen(newItem.ChucVu);
            DBHelper.Create(newItem);
            if( ((int)DBHelper.Get().Count % itemPerPage == 0) && ((int)DBHelper.Get().Count / itemPerPage > 0)){
                return (int)DBHelper.Get().Count / itemPerPage -1;
            }
            return (int)DBHelper.Get().Count / itemPerPage;
        }
        [HttpGet]
        public IActionResult Create()
        {
            
            ViewBag.LastStaffId = GHelper.LastStaffID();
            ViewBag.dsPhongBan = new List<PhongBan>(DBHelper.GetDP());
            return View();
        }
        [HttpPost]
        public int Edit(NhanVien newItem = null, int pageIndex = 0)
        {
            newItem.HoTen = GHelper.XuLyTen(newItem.HoTen);
            newItem.DiaChi = GHelper.XuLyTen(newItem.DiaChi);
            newItem.ChucVu = GHelper.XuLyTen(newItem.ChucVu);
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
            return Content($"dang xay dung");
        }
       public StaffController()
        {}
        public IActionResult _Table(int pageNumber,string currentPage = "p-1",List<NhanVien> dsNhanVien = null){

           byte[] json;
           if (dsNhanVien.Count == 0 && HttpContext.Session.TryGetValue("currentStaffList", out json)) {
                dsNhanVien = JsonConvert.DeserializeObject<List<NhanVien>>(HttpContext.Session.GetString("currentStaffList"));
           }
            ViewBag.dsPhongBan = new List<PhongBan>(DBHelper.GetDP());
            ViewBag.itemPerPage = itemPerPage;
            ViewBag.pageNumber = pageNumber;
            ViewBag.currentPage = currentPage;
            ViewBag.pageIndex = Convert.ToInt32(ViewBag.currentPage.Substring(2))-1;
            return PartialView("_Table",dsNhanVien);
        }
        public FileResult DownloadFile(){
            List<NhanVien> dsNhanVien = JsonConvert.DeserializeObject<List<NhanVien>>(HttpContext.Session.GetString("currentStaffList"));

            ExcelHelper.Export(dsNhanVien);
            string filePath = @"wwwroot/data/Danh_Sach_Nhan_Vien.xlsx";
            string fileName = "Danh_Sach_Nhan_Vien.xlsx";

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/force-download", fileName); 
        }

        [HttpPost]
        public async Task<IActionResult> Import(IFormFile excelfile){
            var list = new List<NhanVien>();
            int numOfUpdatedStaff=0,numOfCreatedStaff=0;
            using (var stream = new MemoryStream()) {
                await excelfile.CopyToAsync(stream);
                list = ExcelHelper.Import(stream);
                foreach (var nv in list){
                    if(DBHelper.GetPBID(nv.PhongBan) != -1 )
                        nv.PhongBan_Id = DBHelper.GetPBID(nv.PhongBan);
                    else{
                        DBHelper.CreatePB(new PhongBan{ TenPhongBan = nv.PhongBan});
                        nv.PhongBan_Id = DBHelper.GetPBID(nv.PhongBan);
                    }
                    if (!IsDuplicatedStaff(nv)) {
                        numOfCreatedStaff++;
                        nv.MaNhanVien = GHelper.LastStaffID();
                        Create(nv);
                    }
                    else if (IsDuplicatedStaff(nv,Option.EDIT)){
                        numOfUpdatedStaff++;
                        Edit(nv);
                    } 
                    
                }
            }
            ViewBag.numOfCreatedStaff = numOfCreatedStaff;
            ViewBag.numOfUpdatedStaff = numOfUpdatedStaff;
            return View();
        }
        public bool IsDuplicatedStaff(NhanVien pnv,Option option = Option.CREATE)
        {
            pnv.HoTen = GHelper.XuLyTen(pnv.HoTen);
            bool daTonTai = false;
            List<NhanVien> DsNhanVien = DBHelper.Get();
            foreach (NhanVien nv in DsNhanVien) {
                if(option == Option.CREATE){
                    if (nv.HoTen == pnv.HoTen && DateTime.Compare(nv.NgaySinh, pnv.NgaySinh) == 0 ) {
                            daTonTai = true;
                            break;
                    }
                    else { daTonTai = false; }
                }
                else if(option == Option.EDIT){
                    if (nv.HoTen == pnv.HoTen && DateTime.Compare(nv.NgaySinh, pnv.NgaySinh) == 0 && nv.MaNhanVien == pnv.MaNhanVien) {
                            daTonTai = true;
                            break;
                    }
                    else { daTonTai = false; }
                }
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
