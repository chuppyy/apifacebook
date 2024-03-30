#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Xceed.Document.NET;
using Xceed.Words.NET;

#endregion

namespace ITC.Service.API.Extensions;

/// <summary>
/// </summary>
public static class HelperExtensions
{
#region Methods

    /// <summary>
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="header"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static bool ExportExcel(string filePath, List<string[]> header, IEnumerable<object> values)
    {
        try
        {
            using (var excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("Sheet 1");
                // Determine the header range (e.g. A1:D1)
                var headerRange = "A1:" + char.ConvertFromUtf32(header[0].Length + 64) + "1";

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
                worksheet.Row(1).Height = 37.75;
                var countRow   = 2;
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


                // for (var i = 1; i <= header.FirstOrDefault().Length; i++) worksheet.Column(i).AutoFit();
                for (var i = 1; i <= header.FirstOrDefault()!.Length; i++) worksheet.Column(i).AutoFit();

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

    /// <summary>
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetContentType(string path)
    {
        var types = GetMimeTypes();
        var ext   = Path.GetExtension(path).ToLowerInvariant();
        return types[ext];
    }

    /// <summary>
    /// </summary>
    /// <param name="enumValue"></param>
    /// <returns></returns>
    public static string GetDisplayName(this Enum enumValue)
    {
        // return enumValue.GetType()
        //                 .GetMember(enumValue.ToString())
        //                 .First()
        //                 .GetCustomAttribute<DisplayAttribute>()
        //                 .GetName();
        return enumValue.GetType()
                        .GetMember(enumValue.ToString())
                        .First()
                        .GetCustomAttribute<DisplayAttribute>()
                        ?.GetName();
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public static Dictionary<string, string> GetMimeTypes()
    {
        return new Dictionary<string, string>
        {
            { ".txt", "text/plain" },
            { ".pdf", "application/pdf" },
            { ".doc", "application/vnd.ms-word" },
            { ".docx", "application/vnd.ms-word" },
            { ".xls", "application/vnd.ms-excel" },
            { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
            { ".xlsm", "application/vnd.ms-excel.sheet.macroEnabled.12" },
            { ".png", "image/png" },
            { ".jpg", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".gif", "image/gif" },
            { ".csv", "text/csv" },
            { ".zip", "application/zip" }
        };
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public static HttpClient InitClient()
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://localhost:44325/");
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        return httpClient;
    }

    /// <summary>
    /// </summary>
    /// <param name="docX"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Paragraph SetTextBookmart(DocX docX, string key, string value)
    {
        if (!string.IsNullOrEmpty(key))
            if (docX.Bookmarks[key] != null)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    docX.Bookmarks[key].SetText(value);
                    return docX.Bookmarks[key].Paragraph;
                }

                docX.Bookmarks[key].SetText("");
                return docX.Bookmarks[key].Paragraph;
            }

        return null;
    }

#endregion
}