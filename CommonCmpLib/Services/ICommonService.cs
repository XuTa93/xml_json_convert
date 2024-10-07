using ClosedXML.Excel;
using System.Collections.Generic;

namespace CommonCmpLib
{
    public interface IExcelDataService<T>
    {
        Dictionary<string, string> Colunms { get; set; }
        // Methods that need to be implemented using the generic type T
        ExcelProcessResult<T> ReadFromExcel(string x_strFilePath);
        ExcelProcessResult<T> CreateErrorResult(string x_strMessage);
        void ValidateHeaders(IXLWorksheet x_objWorksheet, List<string> x_lstHeaderErr);
        List<Dictionary<string, T>> ExtractDataFromWorksheet(IXLWorksheet x_objWorksheet);
        List<string> ValidateMandatoryFieldsAndFillCell(List<Dictionary<string, T>> x_lstPlanList);
        void CopyFromPreviousRow(List<Dictionary<string, T>> x_lstPlanList, int x_nIndex);
        void CheckMandatoryFields(Dictionary<string, T> x_objModel, ref string strRowErr, ref bool bCheckCellErr);
        U GetCellValue<U>(IXLWorksheet x_objWorksheet, int nRow, int nCol);
    }
    public class IExcelDataService : IExcelDataService<T>
    {

    }
}
