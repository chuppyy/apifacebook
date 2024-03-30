using System;
using System.Linq;
using System.Text.RegularExpressions;
using Aspose.Words;
using Aspose.Words.Tables;

namespace ITC.Application.Helpers;

/// <summary>
///     Class hỗ trợ xuất file doc
/// </summary>
public class DocHelper
{
    public const            long   SizeLimit = 2147483648;
    private static readonly Random random    = new();

    private static readonly Regex regexIsRoman = new("^M{0,3}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})$");

    /// <summary>
    ///     Lấy ngẫu nhiên một chuỗi với độ dài truyền vào
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string RandomString(int length = 13)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length)
                                    .Select(s => s[random.Next(s.Length)]).ToArray());
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

    public static Cell EditCell(Document              doc, Row row, string content, int index, bool isBold = false,
                                bool                  isItalic              = false,
                                ParagraphAlignment    paragraphAlignment    = ParagraphAlignment.Center,
                                CellVerticalAlignment cellVerticalAlignment = CellVerticalAlignment.Center)
    {
        Cell cellObject = null;
        cellObject = row.Cells[index];
        cellObject.RemoveAllChildren();
        cellObject.EnsureMinimum();
        if (!string.IsNullOrEmpty(content))
            cellObject.FirstParagraph.Runs.Add(new Run(doc, content));
        cellObject.FirstParagraph.ParagraphFormat.Alignment = paragraphAlignment;
        foreach (Run run in cellObject.GetChildNodes(NodeType.Run, true))
        {
            run.Font.Bold   = isBold;
            run.Font.Italic = isItalic;
        }

        cellObject.CellFormat.VerticalAlignment = cellVerticalAlignment;
        return cellObject;
    }

    /// <summary>
    ///     Check string là số la mã hay không
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool IsRoman(string s)
    {
        return regexIsRoman.IsMatch(s);
    }
}