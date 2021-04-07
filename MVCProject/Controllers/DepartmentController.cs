using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCProject.Helpers;
namespace MVCProject.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly int itemPerPage = 8;

        public DepartmentController()
        {

        }
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.itemPerPage = itemPerPage;
            return View();
        }
        
        [HttpPost]
        public int Create(PhongBan newItem){
            newItem.TenPhongBan = GHelper.XuLyTen(newItem.TenPhongBan);
            DBHelper.CreatePB(newItem);
            if( ((int)DBHelper.GetDP().Count % itemPerPage == 0) && ((int)DBHelper.GetDP().Count / itemPerPage > 0)){
                return (int)DBHelper.GetDP().Count / itemPerPage -1;
            }
            return (int)DBHelper.GetDP().Count / itemPerPage;
        }
        [HttpGet]
        public IActionResult Create(){
            return View();
        }
        public bool IsDuplicated(PhongBan ppb)
        {
            ppb.TenPhongBan = GHelper.XuLyTen(ppb.TenPhongBan);
            bool daTonTai = false;
            List<PhongBan> DsPhongBan = new List<PhongBan>();
            foreach (PhongBan pb in DsPhongBan) {
                if (pb.TenPhongBan == ppb.TenPhongBan) {
                    daTonTai = true;
                }
                else { daTonTai = false; }
            }
            return daTonTai;
        }
         [HttpPost]
        public int Edit(PhongBan newItem = null, int pageIndex = 0)
        {
            newItem.TenPhongBan = GHelper.XuLyTen(newItem.TenPhongBan);
            DBHelper.UpdateDP(newItem);
            return pageIndex;
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(DBHelper.GetbyMDP(id));
        }
        public IActionResult GetPage(int pageIndex){
            
            ViewBag.itemPerPage = itemPerPage;
            ViewBag.pageIndex = pageIndex;
            return View(DBHelper.GetDP());
        }
         public IActionResult PageNav(string currentPage = "p-1"){
                if((int)DBHelper.GetDP().Count % itemPerPage == 0 ){
                    ViewBag.currentPage = "p-" + ((int)DBHelper.GetDP().Count / itemPerPage).ToString();
                }
                else{
                    ViewBag.currentPage = currentPage;
                }
                ViewBag.pageNumber = (int)DBHelper.GetDP().Count / itemPerPage;
            
            return View();
        }
        [HttpPost]
        public int Delete(int id, int pageIndex = 0)
        {
            DBHelper.DeleteDP(id);
            return pageIndex;
        }
    }
}
