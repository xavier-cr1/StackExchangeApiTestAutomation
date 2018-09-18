namespace TestData.OpenXml
{
    using DocumentFormat.OpenXml.Packaging;
    using DocumentFormat.OpenXml.Spreadsheet;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using API.Models;

    public class OpenXmlConnection
    {
        internal static readonly List<string> Headers = new List<string>();

        internal static DataTable ReadExcelSheet(string fname, string sheetName)
        {
            var dt = new DataTable();
            using (var doc = SpreadsheetDocument.Open(fname, false))
            {
                //Read the first Sheets 
                var sheet = doc.WorkbookPart.Workbook.Descendants<Sheet>().Single(x => x.Name == sheetName);
                var worksheetPart = doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart;
                if (worksheetPart == null) return dt;
                var worksheet = worksheetPart.Worksheet;
                var rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();

                foreach (var row in rows)
                {
                    //Read the first row as header
                    if (row.RowIndex.Value == 1)
                    {
                        foreach (var cell in row.Descendants<Cell>())
                        {
                            var colunmName = GetCellValue(doc, cell);
                            Headers.Add(colunmName);
                            dt.Columns.Add(colunmName);
                        }
                    }
                    else
                    {
                        dt.Rows.Add();
                        var i = 0;
                        foreach (var cell in row.Descendants<Cell>())
                        {
                            if (i < dt.Columns.Count)
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = GetCellValue(doc, cell);
                                i++;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// Gets the cell value.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="cell">The cell.</param>
        /// <returns></returns>
        private static string GetCellValue(SpreadsheetDocument doc, CellType cell)
        {
            string value;
            if (cell.DataType == null)
            {
                if (cell.CellValue != null)
                {
                    if (cell.CellReference.InnerText.Equals("B2"))
                    {
                        value = DateTime.FromOADate(int.Parse(cell.CellValue.InnerText)).ToShortDateString();
                    }
                    else
                    {
                        value = cell.CellValue.InnerText;
                    }
                }
                else
                {
                    value = string.Empty;
                }
            }
            else
            {
                value = cell.CellValue.InnerText;
            }
            if (cell.DataType == null || cell.DataType.Value != CellValues.SharedString) return value;

            return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
        }

        /// <summary>
        /// Gets the value of the property, if x means that should be empty (this is a hack for OpenXml, if blank cells are left returns unexpected results)
        /// </summary>
        /// <param name="rw">Data row.</param>
        /// <param name="property">Property of the page object model</param>
        /// <returns></returns>

        internal static bool ParseBoolCell(DataRow rw, string property)
        {
            return !(rw[property] is DBNull) && Convert.ToBoolean(rw[property].ToString() == "1" ? "true" : "false");
        }

        internal static int ParseIntCell(DataRow rw, string property)
        {
            return rw[property] is DBNull || rw[property].Equals("x") ? 0 : Convert.ToInt32(rw[property]);
        }

        internal static List<Answers> ExcelMappingToAnserws(DataTable dt)
        {
            var answerList = new List<Answers>();
            foreach (var rw in dt.AsEnumerable())
            {
                var answer = new Answers()
                {
                    IsAccepted = ParseBoolCell(rw, "IsAccepted"),
                    Score = ParseIntCell(rw, "Score"),
                    LastActivityDate = ParseIntCell(rw, "LastActivityDate"),
                    CreationDate = ParseIntCell(rw, "CreationDate"),
                    AnswerId = ParseIntCell(rw, "AnswerId"),
                    QuestionId = ParseIntCell(rw, "QuestionId"),
                };
                if (!answer.AnswerId.Equals(string.Empty)) answerList.Add(answer);
            }
            return answerList;
        }
    }
}
