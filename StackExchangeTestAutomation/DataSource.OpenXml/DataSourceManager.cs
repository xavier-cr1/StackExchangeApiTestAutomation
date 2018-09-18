namespace DataSource.OpenXml
{
    using DataSource.OpenXml.Contracts;
    using System.Linq;
    using API.Models;
    using System.Collections.Generic;
    using TestData.OpenXml;
    using System.Data;
    using System.IO;

    public class DataSourceManager : IDataSourceContracts
    {
        private const string _xlsPath = @"\..\StackExchangeTestAutomation\DataSource.OpenXml\DataIn\DataInExcel.xls";

        public Answers GetAnswerById(int answerId)
        {
            return GetAnswersListFromXls().SingleOrDefault(x => x.AnswerId.Equals(answerId));
        }

        private ICollection<Answers> GetAnswersListFromXls()
        {
            return OpenXmlConnection.ExcelMappingToAnserws(GetExcelDatableBySheetName("Answers"));
        }

        private DataTable GetExcelDatableBySheetName(string sheetName)
        {
            return OpenXmlConnection.ReadExcelSheet(Directory.GetCurrentDirectory() + _xlsPath, sheetName);
        }
    }
}
