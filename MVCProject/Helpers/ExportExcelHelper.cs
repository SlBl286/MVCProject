using System.IO;
using System;
using MVCProject.Models;
using System.Collections.Generic;
using OfficeOpenXml;

namespace MVCProject.Helpers
{
    public static class ExportExcelHelper
    {
        public static bool Export(List<NhanVien> ds){
            FileInfo file = new FileInfo(@"wwwroot\data\Danh_Sach_Nhan_Vien.xlsx");
                file.Delete();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using(var excelPackage = new ExcelPackage(file)){
                    
                    excelPackage.Workbook.Properties.Author = "Qý";
                    excelPackage.Workbook.Properties.Title = "Staff List";
                    excelPackage.Workbook.Properties.Created = DateTime.Now;

                    var excelWorkSheet = excelPackage.Workbook.Worksheets.Add("staffList");
      
                   
                    excelWorkSheet.Cells["A1"].Value = "Mã Nhân Viên";
                    excelWorkSheet.Cells["B1"].Value = "Họ Tên";
                    excelWorkSheet.Cells["C1"].Value = "Ngày Sinh";
                    excelWorkSheet.Cells["D1"].Value = "Số Điện Thoại";
                    excelWorkSheet.Cells["E1"].Value = "Địa Chỉ";
                    excelWorkSheet.Cells["F1"].Value = "Chức Vụ";
                    excelWorkSheet.Cells["G1"].Value = "Số Năm Công Tác";
                    excelWorkSheet.Cells["H1"].Value = "Phòng Ban";
                    excelWorkSheet.Cells["A2"].LoadFromCollection(ds);
                    excelWorkSheet.Cells["C2:C"+ (ds.Count+1).ToString()].Style.Numberformat.Format = "dd-MM-yyyy";
                    double minimumSize = 10;
                    excelWorkSheet.Cells[excelWorkSheet.Dimension.Address].AutoFitColumns(minimumSize);
         
                    double maximumSize = 50;
                    excelWorkSheet.Cells[excelWorkSheet.Dimension.Address].AutoFitColumns(minimumSize, maximumSize);
                
                    for (int col = 1; col <= excelWorkSheet.Dimension.End.Column; col++)
                    {
                        excelWorkSheet.Column(col).Width = excelWorkSheet.Column(col).Width + 1;
                    }
                    excelPackage.SaveAs(file);

                }       
            return true;
        }
    }
}
