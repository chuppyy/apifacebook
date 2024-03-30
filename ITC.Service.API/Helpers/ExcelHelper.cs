using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ITC.Service.API.Helpers;

/// <summary>
///     Class hỗ trợ xuất file excel
/// </summary>
public class ExcelHelper
{
    /// <summary>
    ///     Kiểm tra email có hợp lệ hay không
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                  RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examines the domain part of the email and normalizes it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                var domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
        catch (ArgumentException)
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(email,
                                 @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                                 RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    /// <summary>
    ///     Xuất file excel
    /// </summary>
    public byte[] ExportExcel(string title, List<string[]> header, IEnumerable<object> values, string unitName = "")
    {
        try
        {
            using (var excel = new ExcelPackage())
            {
                var workSheet    = excel.Workbook.Worksheets.Add(title);
                var headerRange1 = "A1:" + char.ConvertFromUtf32(header[0].Length + 64) + "1";

                //Định dạng hàng đơn vị
                workSheet.Row(1).Height                    = 30;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                workSheet.Row(1).Style.VerticalAlignment   = ExcelVerticalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold           = true;
                workSheet.Row(1).Style.Font.Size           = 12;
                workSheet.Row(1).Style.Font.Name           = "Times New Roman";
                workSheet.Row(1).Style.Font.Color
                         .SetColor(ColorTranslator.FromHtml(GlobalTemplateKey.TitleForegroundColor));

                workSheet.Cells[1, 1].Value                  = $"Đơn vị: {unitName}";
                workSheet.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[1, 1].Style.Fill.BackgroundColor
                         .SetColor(ColorTranslator.FromHtml(GlobalTemplateKey.TitleBackgroundColor));
                workSheet.Cells[headerRange1].Merge = true;

                // Determine the header range (e.g. A1:D1)
                var headerRange = "A2:" + char.ConvertFromUtf32(header[0].Length + 64) + "2";
                //Định dạng hàng tiêu dề
                workSheet.Row(2).Height                    = 30;
                workSheet.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(2).Style.VerticalAlignment   = ExcelVerticalAlignment.Center;
                workSheet.Row(2).Style.Font.Bold           = true;
                workSheet.Row(2).Style.Font.Size           = GlobalTemplateKey.FontSizeTitle;
                workSheet.Row(2).Style.Font.Name           = "Times New Roman";
                workSheet.Row(2).Style.Font.Color
                         .SetColor(ColorTranslator.FromHtml(GlobalTemplateKey.TitleForegroundColor));

                workSheet.Cells[2, 1].Value                  = title;
                workSheet.Cells[2, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[2, 1].Style.Fill.BackgroundColor
                         .SetColor(ColorTranslator.FromHtml(GlobalTemplateKey.TitleBackgroundColor));

                workSheet.Cells[headerRange].Merge                     = true;
                workSheet.Cells[headerRange].Style.Border.Left.Style   = ExcelBorderStyle.Thin;
                workSheet.Cells[headerRange].Style.Border.Right.Style  = ExcelBorderStyle.Thin;
                workSheet.Cells[headerRange].Style.Border.Top.Style    = ExcelBorderStyle.Thin;
                workSheet.Cells[headerRange].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                headerRange = "A3:" + char.ConvertFromUtf32(header[0].Length + 64) + "3";
                // Popular header row data
                workSheet.Cells[headerRange].LoadFromArrays(header);
                workSheet.Cells[headerRange].Style.Font.Size           = GlobalTemplateKey.FontSizeRecord;
                workSheet.Cells[headerRange].Style.Font.Name           = "Times New Roman";
                workSheet.Cells[headerRange].Style.Font.Bold           = true;
                workSheet.Cells[headerRange].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[headerRange].Style.VerticalAlignment   = ExcelVerticalAlignment.Center;
                workSheet.Cells[headerRange].Style.Fill.PatternType    = ExcelFillStyle.Solid;
                workSheet.Cells[headerRange].Style.Fill.BackgroundColor
                         .SetColor(ColorTranslator.FromHtml(GlobalTemplateKey.RecordBackgroundColor));
                workSheet.Cells[headerRange].Style.Border.Left.Style   = ExcelBorderStyle.Thin;
                workSheet.Cells[headerRange].Style.Border.Right.Style  = ExcelBorderStyle.Thin;
                workSheet.Cells[headerRange].Style.Border.Top.Style    = ExcelBorderStyle.Thin;
                workSheet.Cells[headerRange].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Row(3).Style.Font.Color
                         .SetColor(ColorTranslator.FromHtml(GlobalTemplateKey.RecordForegroundColor));
                workSheet.Row(3).Height = 40;
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
                        workSheet.Cells[countRow, i + 1].Value = arrayObject[i];
                    workSheet.Cells[headerRange2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[headerRange2].Style.VerticalAlignment   = ExcelVerticalAlignment.Center;
                    workSheet.Cells[headerRange2].Style.Font.Size           = GlobalTemplateKey.FontSizeRecord;
                    workSheet.Cells[headerRange2].Style.Font.Name           = "Times New Roman";
                    workSheet.Cells[headerRange2].Style.Border.Left.Style   = ExcelBorderStyle.Thin;
                    workSheet.Cells[headerRange2].Style.Border.Right.Style  = ExcelBorderStyle.Thin;
                    workSheet.Cells[headerRange2].Style.Border.Top.Style    = ExcelBorderStyle.Thin;
                    workSheet.Cells[headerRange2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    countRow++;
                }


                for (var i = 1; i <= header.FirstOrDefault()!.Length; i++)
                {
                    var column = workSheet.Column(i);
                    column.AutoFit();
                    column.Width += 5;
                }

                var bin = excel.GetAsByteArray();
                return bin;
            }
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    ///     Cài đặt định dạng các trường tiêu đề excel
    /// </summary>
    public void SetRecordExcel(ExcelWorksheet workSheet, int row, IEnumerable<string> languages, int start, int end)
    {
        //Định dạng hàng
        workSheet.Row(row).Height                    = 40;
        workSheet.Row(row).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        workSheet.Row(row).Style.VerticalAlignment   = ExcelVerticalAlignment.Center;
        workSheet.Row(row).Style.Font.Bold           = true;
        workSheet.Row(row).Style.Font.Size           = GlobalTemplateKey.FontSizeRecord;
        workSheet.Row(row).Style.Font.Name           = "Times New Roman";
        workSheet.Row(row).Style.Font.Color
                 .SetColor(ColorTranslator.FromHtml(GlobalTemplateKey.RecordForegroundColor));

        for (var i = start; i <= end; i++)
        {
            var cell = workSheet.Cells[row, i];
            if (languages.Count() > i)
                cell.Value = languages.ElementAt(i);
            cell.Style.Font.Bold        = true;
            cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cell.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(GlobalTemplateKey
                                                                                  .RecordBackgroundColor));
            cell.Style.Border.Left.Style   = ExcelBorderStyle.Thin;
            cell.Style.Border.Right.Style  = ExcelBorderStyle.Thin;
            cell.Style.Border.Top.Style    = ExcelBorderStyle.Thin;
            cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        }
    }

    /// <summary>
    ///     Cài đặt tiêu đề của trang excel
    /// </summary>
    public void SetTitleExcel(ExcelWorksheet workSheet, int row, string title, string mergeCell)
    {
        //Định dạng hàng tiêu dề
        workSheet.Row(row).Height                    = 30;
        workSheet.Row(row).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        workSheet.Row(row).Style.VerticalAlignment   = ExcelVerticalAlignment.Center;
        workSheet.Row(row).Style.Font.Bold           = true;
        workSheet.Row(row).Style.Font.Size           = GlobalTemplateKey.FontSizeTitle;
        workSheet.Row(row).Style.Font.Name           = "Times New Roman";
        workSheet.Row(row).Style.Font.Color
                 .SetColor(ColorTranslator.FromHtml(GlobalTemplateKey.TitleForegroundColor));

        workSheet.Cells[row, 1].Value                  = title;
        workSheet.Cells[row, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        workSheet.Cells[row, 1].Style.Fill.BackgroundColor
                 .SetColor(ColorTranslator.FromHtml(GlobalTemplateKey.TitleBackgroundColor));

        workSheet.Cells[mergeCell].Merge = true;
    }
}

/// <summary>
///     Cài đặt một số key màu cho template
/// </summary>
public static class GlobalTemplateKey
{
#region Static Fields and Constants

    /// <summary>
    ///     Màu nền tiêu đề
    /// </summary>
    public const string TitleBackgroundColor = "#C6E0B4";

    /// <summary>
    ///     Màu chữ tiêu đề
    /// </summary>
    public const string TitleForegroundColor = "#000000";

    /// <summary>
    ///     Kích cỡ font chữ tiêu đề
    /// </summary>
    public const int FontSizeTitle = 14;

    /// <summary>
    ///     Màu nền các trường
    /// </summary>
    public const string RecordBackgroundColor = "#C6E0B4";

    /// <summary>
    ///     Màu chữ các trường
    /// </summary>
    public const string RecordForegroundColor = "#000000";

    /// <summary>
    ///     Kích cỡ font chữ các trường
    /// </summary>
    public const int FontSizeRecord = 12;

#endregion
}