using System;
using System.Linq;
using Aspose.Words;
using Aspose.Words.Tables;

namespace ITC.Service.API.Helpers;

/// <summary>
///     Class hỗ trợ xuất file doc
/// </summary>
public class DocHelper
{
    /// <summary>
    /// </summary>
    public const long SizeLimit = 2147483648;

    private static readonly Random Random = new();

    /// <summary>
    ///     Lấy ngẫu nhiên một chuỗi với độ dài truyền vào
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string RandomString(int length = 13)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length)
                                    .Select(s => s[Random.Next(s.Length)]).ToArray());
    }

    /// <summary>
    ///     Thêm text vào bookmark văn bản
    /// </summary>
    /// <param name="doc">Đối tượng doc</param>
    /// <param name="bookmarkKey">Key bookmark</param>
    /// <param name="value">Giá trị muốn gán vào</param>
    public static void AddBookMark(Document doc, string bookmarkKey, string value)
    {
        var bookmark                        = doc.Range.Bookmarks[bookmarkKey];
        if (bookmark != null) bookmark.Text = !string.IsNullOrEmpty(value) ? value : "";
    }

    /// <summary>
    /// </summary>
    /// <param name="doc"></param>
    /// <param name="row"></param>
    /// <param name="content"></param>
    /// <param name="index"></param>
    /// <param name="isBold"></param>
    /// <param name="isItalic"></param>
    /// <param name="paragraphAlignment"></param>
    /// <param name="cellVerticalAlignment"></param>
    /// <returns></returns>
    public static Cell EditCell(Document              doc, Row row, string content, int index, bool isBold = false,
                                bool                  isItalic              = false,
                                ParagraphAlignment    paragraphAlignment    = ParagraphAlignment.Center,
                                CellVerticalAlignment cellVerticalAlignment = CellVerticalAlignment.Center)
    {
        var cellObject = row.Cells[index];
        cellObject.RemoveAllChildren();
        cellObject.EnsureMinimum();
        if (!string.IsNullOrEmpty(content))
            cellObject.FirstParagraph.Runs.Add(new Run(doc, content));
        cellObject.FirstParagraph.ParagraphFormat.Alignment = paragraphAlignment;
        foreach (var node in cellObject.GetChildNodes(NodeType.Run, true))
        {
            var run = (Run)node;
            run.Font.Bold   = isBold;
            run.Font.Italic = isItalic;
        }

        cellObject.CellFormat.VerticalAlignment = cellVerticalAlignment;
        return cellObject;
    }
}