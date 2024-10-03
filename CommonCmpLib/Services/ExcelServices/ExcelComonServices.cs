using ClosedXML.Excel;
using System.Collections.Generic;
using System.IO;

namespace CommonCmpLib
{
    public enum ExcelSheetName
    {
        Parameter,
        Trace,
        Event,
        DataCollectionPlan
    }
    public class ExcelComonServices
    {
        public const string PARAMETER_SHEETNAME = "Parameter";
        public const string TRACE_SHEETNAME = "Trace";
        public const string EVENT_SHEETNAME = "Event";
        public const string DCP_SHEETNAME = "DataCollectionPlan";
        public static List<string> GetSheetNames(string x_strfilePath)
        {
            List<string> lstSheetNames;
            bool bIsExits;

            // Check if the file exists
            bIsExits = File.Exists(x_strfilePath);
            if (bIsExits == false)
            {
                return null;
            }

            lstSheetNames = new List<string>();
            using (FileStream objStream = new FileStream(x_strfilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (XLWorkbook workbook = new XLWorkbook(objStream))
                {
                    // Loop through all the sheets in the workbook and add their names to the list
                    foreach (IXLWorksheet worksheet in workbook.Worksheets)
                    {
                        lstSheetNames.Add(worksheet.Name);
                    }
                }
            }
            return lstSheetNames;
        }
    }
}
