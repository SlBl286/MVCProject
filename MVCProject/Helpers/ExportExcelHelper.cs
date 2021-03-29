
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
            using(var excelPackage = new ExcelPackage()){
                excelPackage.Workbook.Properties.Author = "Qý";
                excelPackage.Workbook.Properties.Title = "Staff List";
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                var excelWorkSheet = excelPackage.Workbook.Worksheets.Add("stafflist");

                excelWorkSheet.Cells["A1"].Value = "test";

                FileInfo fo = new FileInfo(@"wwwroot\data\test.xlsx");
                excelPackage.SaveAs(fo);

            }
            return true;
        }
    }
}
