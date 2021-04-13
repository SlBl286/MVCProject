using System.IO;
using System;
using MVCProject.Models;
using System.Collections.Generic;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;

namespace MVCProject.Helpers
{
    public static class ExcelHelper
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
                    excelWorkSheet.Cells["I1"].Value = "Phòng Ban";
                    excelWorkSheet.Cells["A2"].LoadFromCollection(ds);
                    excelWorkSheet.Cells["C2:C"+ (ds.Count+1).ToString()].Style.Numberformat.Format = "dd-MM-yyyy";
                    
                    excelWorkSheet.Cells["A1:I"+ (ds.Count+1).ToString()].Style.Font.Size = 15;
                    excelWorkSheet.Cells["A1:I1"].Style.Font.Bold = true;
                    excelWorkSheet.Cells["A1:I1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    excelWorkSheet.Cells["A1:I1"].Style.Fill.BackgroundColor.SetColor(Color.Green);
                    excelWorkSheet.Cells["A1:I1"].Style.Border.Top.Style = ExcelBorderStyle.Thick;
                    excelWorkSheet.Cells["A1:I1"].Style.Border.Right.Style = ExcelBorderStyle.Thick;
                    excelWorkSheet.Cells["A1:I1"].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                    excelWorkSheet.Cells["A1:I1"].Style.Border.Left.Style = ExcelBorderStyle.Thick;
                    //make the borders of cells A18 - J18 double and with a purple color
                    excelWorkSheet.Cells["A2:I"+ (ds.Count+1).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Double;
                    excelWorkSheet.Cells["A2:I"+ (ds.Count+1).ToString()].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                    excelWorkSheet.Cells["A2:I"+ (ds.Count+1).ToString()].Style.Border.Top.Color.SetColor(Color.Purple);
                    excelWorkSheet.Cells["A2:I"+ (ds.Count+1).ToString()].Style.Border.Bottom.Color.SetColor(Color.Purple);
                    excelWorkSheet.Cells["A2:I"+ (ds.Count+1).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Double;
                    excelWorkSheet.Cells["A2:I"+ (ds.Count+1).ToString()].Style.Border.Left.Style = ExcelBorderStyle.Double;
                    excelWorkSheet.Cells["A2:I"+ (ds.Count+1).ToString()].Style.Border.Right.Color.SetColor(Color.Purple);
                    excelWorkSheet.Cells["A2:I"+ (ds.Count+1).ToString()].Style.Border.Left.Color.SetColor(Color.Purple);
                    double minimumSize = 15;
                    excelWorkSheet.Cells[excelWorkSheet.Dimension.Address].AutoFitColumns(minimumSize);
         
                    double maximumSize = 100;
                    excelWorkSheet.Cells[excelWorkSheet.Dimension.Address].AutoFitColumns(minimumSize, maximumSize);
                    double rowHeight = 30;
                    for (int row = 1; row <= ds.Count+1; row++)
                    {
                        excelWorkSheet.Row(row).Height = rowHeight;
                    }
                    
                    for (int col = 1; col <= excelWorkSheet.Dimension.End.Column; col++)
                    {
                        excelWorkSheet.Column(col).Width = excelWorkSheet.Column(col).Width + 1;
                    }
                    excelWorkSheet.Cells["A1:I"+ (ds.Count+1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    excelWorkSheet.Cells["A1:I"+ (ds.Count+1).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    excelWorkSheet.Cells["B2:B"+(ds.Count+1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    excelWorkSheet.Cells["E2:E"+(ds.Count+1).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    excelWorkSheet.Cells["A1:I"+ (ds.Count+1).ToString()].Style.WrapText = true;
                   excelWorkSheet.Column(8).Hidden = true;
                    excelPackage.SaveAs(file);

                }       
            return true;
        }
        public static List<NhanVien> Import(MemoryStream stream)
        {
            var list = new List<NhanVien>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream)) {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                worksheet.Column(8).Hidden = false;
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++) {
                    list.Add(new NhanVien {
                        MaNhanVien = worksheet.Cells[row, 1].Value.ToString().Trim(),
                        HoTen = worksheet.Cells[row, 2].Value.ToString().Trim(),
                        NgaySinh = DateTime.FromOADate(float.Parse(worksheet.Cells[row, 3].Value.ToString().Trim())),
                        SoDienThoai = worksheet.Cells[row, 4].Value.ToString().Trim(),
                        DiaChi = worksheet.Cells[row, 5].Value.ToString().Trim(),
                        ChucVu = worksheet.Cells[row, 6].Value.ToString().Trim(),
                        SoNamCongTac = int.Parse(worksheet.Cells[row, 7].Value.ToString().Trim()),
                        PhongBan_Id = int.Parse(worksheet.Cells[row, 8].Value.ToString().Trim()),
                        PhongBan = worksheet.Cells[row, 9].Value.ToString().Trim(),

                    });
                }
            }
            return list;
        }
     }
}