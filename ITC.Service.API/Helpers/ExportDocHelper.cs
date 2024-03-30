#region

using System;
using System.Linq;
using Aspose.Words;
using Aspose.Words.Tables;

#endregion

namespace ITC.Service.API.Helpers;

/// <summary>
/// </summary>
public static class ExportDocHelper
{
#region Methods

    /// <summary>
    /// </summary>
    /// <param name="doc"></param>
    public static void CheckCellsMerged(Document doc)
    {
        // Retrieve the first table in the document
        var table = (Table)doc.GetChild(NodeType.Table, 0, true);

        foreach (var row in table.Rows.OfType<Row>())
            foreach (var cell in row.Cells.OfType<Cell>())
                Console.WriteLine(PrintCellMergeType(cell));
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

    /// <summary>
    /// </summary>
    /// <param name="cell"></param>
    /// <returns></returns>
    public static string PrintCellMergeType(Cell cell)
    {
        var isHorizontallyMerged = cell.CellFormat.HorizontalMerge != CellMerge.None;
        var isVerticallyMerged   = cell.CellFormat.VerticalMerge   != CellMerge.None;
        var cellLocation =
            $"R{cell.ParentRow.ParentTable.IndexOf(cell.ParentRow) + 1}, C{cell.ParentRow.IndexOf(cell) + 1}";

        if (isHorizontallyMerged && isVerticallyMerged)
            return $"The cell at {cellLocation} is both horizontally and vertically merged";
        if (isHorizontallyMerged)
            return $"The cell at {cellLocation} is horizontally merged.";

        return isVerticallyMerged
                   ? $"The cell at {cellLocation} is vertically merged"
                   : $"The cell at {cellLocation} is not merged";
    }

#endregion
}