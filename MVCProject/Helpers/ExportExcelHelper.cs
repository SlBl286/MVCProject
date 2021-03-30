
using System.IO;
using System;
using MVCProject.Models;
using System.Collections.Generic;
using OfficeOpenXml;

namespace MVCProject.Helpers
{
    public static class ExportExcelHelper
    {
        public static bool Export(List<NhanVien> ds = null){
            FileInfo file = new FileInfo(@"wwwroot\data\test.xlsx");
            if(file.Exists){
                using(var excelPackage = new ExcelPackage(file)){
                    excelPackage.Workbook.Properties.Author = "Qý";
                    excelPackage.Workbook.Properties.Title = "Staff List";
                    excelPackage.Workbook.Properties.Created = DateTime.Now;

                    var excelWorkSheet = excelPackage.Workbook.Worksheets["staffList"];

                    excelWorkSheet.Cells["A1"].LoadFromCollection(ds);


                    excelPackage.SaveAs(file);

                }
            }
            else{
                using(var excelPackage = new ExcelPackage()){
                    excelPackage.Workbook.Properties.Author = "Qý";
                    excelPackage.Workbook.Properties.Title = "Staff List";
                    excelPackage.Workbook.Properties.Created = DateTime.Now;

                    var excelWorkSheet = excelPackage.Workbook.Worksheets.Add("stafflist");

                    excelWorkSheet.Cells["A1"].LoadFromCollection(ds);
                    excelPackage.SaveAs(file);

                }
            }
            
            return true;
        }
    }
}
