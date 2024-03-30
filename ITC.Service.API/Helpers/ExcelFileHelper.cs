#region

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;

#endregion

namespace ITC.Service.API.Helpers;

/// <summary>
/// </summary>
public class ExcelFileHelper
{
#region Methods

    /// <summary>
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="header"></param>
    /// <param name="values"></param>
    /// <param name="unit"></param>
    /// <param name="title"></param>
    /// <param name="isSchool"></param>
    /// <returns></returns>
    public static bool ExportExcel(string filePath, List<string[]> header, IEnumerable<object> values, string unit,
                                   string title,    bool           isSchool = false)
    {
        try
        {
            using (var excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("Sheet 1");
                // Determine the header range (e.g. A1:D1)
                var headerRange = "A3:" + char.ConvertFromUtf32(header[0].Length + 64) + "3";

                // Target a worksheet
                var worksheet = excel.Workbook.Worksheets["Sheet 1"];

                // Popular header row data
                worksheet.Cells[headerRange].LoadFromArrays(header);
                worksheet.Cells[headerRange].Style.Font.Size           = 12;
                worksheet.Cells[headerRange].Style.Font.Name           = "Times New Roman";
                worksheet.Cells[headerRange].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[headerRange].Style.VerticalAlignment   = ExcelVerticalAlignment.Center;
                worksheet.Cells[headerRange].Style.Fill.PatternType    = ExcelFillStyle.Solid;
                worksheet.Cells[headerRange].Style.Fill.BackgroundColor
                         .SetColor(ColorTranslator.FromHtml("#C6E0B4"));
                worksheet.Row(3).Height = 37.75;
                var countRow   = 4;
                var countOrder = 97;
                foreach (var item in values)
                {
                    var arrayObject = item.GetType()
                                          .GetProperties()
                                          .Select(p =>
                                          {
                                              var value = p.GetValue(item, null);
                                              return value == null ? null : value.ToString();
                                          })
                                          .ToArray();

                    var headerRange2 =
                        $"A{countRow}:{((char)(countOrder + arrayObject.Length - 1)).ToString().ToUpper()}{countRow}";
                    for (var i = 0; i < arrayObject.Length; i++)
                        worksheet.Cells[countRow, i + 1].Value = arrayObject[i];
                    worksheet.Cells[headerRange2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[headerRange2].Style.VerticalAlignment   = ExcelVerticalAlignment.Center;
                    worksheet.Cells[headerRange2].Style.Font.Size           = 12;
                    worksheet.Cells[headerRange2].Style.Font.Name           = "Times New Roman";
                    countRow++;
                }


                //Tiêu đề
                var headerRangeUnit = "A1:" + char.ConvertFromUtf32(header[0].Length + 64) + "1";
                worksheet.Cells[headerRangeUnit].Merge                     = true;
                worksheet.Cells[headerRangeUnit].Style.Font.Size           = 14;
                worksheet.Cells[headerRangeUnit].Style.Font.Name           = "Times New Roman";
                worksheet.Cells[headerRangeUnit].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                worksheet.Cells[headerRangeUnit].Style.VerticalAlignment   = ExcelVerticalAlignment.Center;
                worksheet.Cells[headerRangeUnit].Value                     = $"Đơn vị: {unit}";

                var headerRangeTitle = "A2:" + char.ConvertFromUtf32(header[0].Length + 64) + "2";
                worksheet.Cells[headerRangeTitle].Merge                     = true;
                worksheet.Cells[headerRangeTitle].Style.Font.Size           = 17;
                worksheet.Cells[headerRangeTitle].Style.Font.Name           = "Times New Roman";
                worksheet.Cells[headerRangeTitle].Style.Font.Bold           = true;
                worksheet.Cells[headerRangeTitle].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[headerRangeTitle].Style.VerticalAlignment   = ExcelVerticalAlignment.Center;
                worksheet.Cells[headerRangeTitle].Value                     = title;
                worksheet.Row(2).Height                                     = 50;

                for (var i = 1; i <= header.FirstOrDefault()!.Length; i++) worksheet.Column(i).AutoFit();
                if (isSchool)
                    worksheet.Column(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                var excelFile = new FileInfo(filePath);
                excel.SaveAs(excelFile);
                return true;
            }
        }
        catch
        {
            return false;
        }
    }

#endregion
}