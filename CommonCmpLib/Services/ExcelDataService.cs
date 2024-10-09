//*****************************************************************************
// 		    ：
// 内容		：Collect and transform data Excel to Xml & Xml <=> Json
// 		    ：
// 作成者	：TangLx
// 作成日	：2024/10/04
// 		    ：
// 修正履歴	：
// 		    ：
// 		    ：
//*****************************************************************************

using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommonCmpLib
{
    public class ExcelDataService
    {
        #region Fields
        private Dictionary<string, string> m_objColunms;
        private int m_nRowHeader;
        private string m_nColunmKey;
        private string m_strSheetName;
        private ExcelProcessResult m_objProcessResult;
        private string[] m_arrMandatoryFields;

        #endregion Fields

        #region Properties
        public Dictionary<string, string> Colunms
        {
            get
            {
                return m_objColunms;
            }
        }
        public int RowStart 
        {
            get 
            { 
                return m_nRowHeader + 1; 
            }
        }
        public int RowHeader
        {
            get
            {
                return m_nRowHeader;
            }
        }
        public string ColunmKey
        {
            get
            {
                return m_nColunmKey;
            }
        }
        public string SheetName
        {
            get
            {
                return m_strSheetName;
            }
        }
        public string[] MandatoryFields
        {
            get
            {
                return m_arrMandatoryFields;
            }
        }
        public ExcelProcessResult ProcessResult
        {
            get
            {
                return m_objProcessResult;
            }
        }
        #endregion Properties

        #region Contructors
        /// <summary>
        /// Constructor to enforce factory method
        /// </summary>
        public ExcelDataService(string x_strSheetName, int x_nRowHeader, string x_nColunmKey, string[] x_lstMandatoryFields, Dictionary<string, string> x_objColunm)
        {
            m_objColunms = x_objColunm;
            m_nRowHeader = x_nRowHeader;
            m_strSheetName = x_strSheetName;
            m_nColunmKey = x_nColunmKey;
            m_arrMandatoryFields = x_lstMandatoryFields;
        }

        // Factory method
        public static ExcelDataService Create(ExcelSheetName x_strSheetName)
        {
            switch (x_strSheetName)
            {
                case ExcelSheetName.DataCollectionPlan:
                    return new ExcelDataService(DCP.SHEET_NAME, DCP.ROW_HEADER, DCP.COLUNM_KEY, DCP.MANDATORY_FIELDS, DCP.COLUNMS);

                case ExcelSheetName.Event:
                    return new ExcelDataService(EVENT.SHEET_NAME, EVENT.ROW_HEADER, EVENT.COLUNM_KEY, EVENT.MANDATORY_FIELDS, EVENT.COLUNMS);

                case ExcelSheetName.Parameter:
                    return new ExcelDataService(PARAMETER.SHEET_NAME, PARAMETER.ROW_HEADER, PARAMETER.COLUNM_KEY, PARAMETER.MANDATORY_FIELDS, PARAMETER.COLUNMS);

                case ExcelSheetName.Trace:
                    return new ExcelDataService(TRACE.SHEET_NAME, TRACE.ROW_HEADER, TRACE.COLUNM_KEY, TRACE.MANDATORY_FIELDS, TRACE.COLUNMS);
                default:
                    break;
            }
            throw new ArgumentException("Invalid sheet name");
        }

        #endregion Contructors

        #region Methods
        /// <summary>
        /// Get All Sheet Name in file
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Process data collection plan from an Excel file.
        /// </summary>
        public ExcelProcessResult Read(string x_strFilePath)
        {
            // Initialize variables for processing
            List<Dictionary<string, string>> lstData;
            List<string> lstHeaderErr;
            List<string> lstCellErr;
            IXLWorksheet objWorksheet;
            bool bIsSuccess;
            StringBuilder strMessage;

            // Initialize lists for storing errors and plan data
            lstHeaderErr = new List<string>();
            lstCellErr = new List<string>();
            lstData = new List<Dictionary<string, string>>();
            m_objProcessResult = new ExcelProcessResult();

            try
            {
                // Check if the file exists
                if (!File.Exists(x_strFilePath))
                {
                    m_objProcessResult.IsSuccess = false;
                    m_objProcessResult.Message = $"- {DateTime.Now} : {SheetName} File not found: {x_strFilePath} {Environment.NewLine}";
                    return m_objProcessResult;
                }

                // Get Sheet Name
                m_objProcessResult.SheetName = SheetName;

                // Open the Excel file stream
                using (FileStream objStream = new FileStream(x_strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    // Load the Excel workbook
                    using (XLWorkbook workbook = new XLWorkbook(objStream))
                    {
                        objWorksheet = workbook.Worksheet(SheetName);

                        // Validate headers
                        ValidateHeaders(objWorksheet, lstHeaderErr);

                        // Extract data from worksheet
                        ExtractDataFromWorksheet(objWorksheet, lstData);
                    }
                }

                // Fill in missing mandatory fields by copying from the previous row
                ValidateMandatoryFieldsAndFillCell(lstData, lstCellErr);

                // Check overall success status
                bIsSuccess = (lstHeaderErr.Count == 0) && (lstCellErr.Count == 0);

                // Prepare the result object with the processed data and any errors
                m_objProcessResult.IsSuccess = bIsSuccess;
                m_objProcessResult.TotalRow = lstData.Count;
                m_objProcessResult.HeadersError = lstHeaderErr;
                m_objProcessResult.CellError = lstCellErr;
                m_objProcessResult.Models = lstData;
                
                // Prepare the message
                strMessage = new StringBuilder();
                strMessage.Append("- " + DateTime.Now.ToString() + " : ");
                strMessage.Append(SheetName);
                if (bIsSuccess == true)
                {
                    strMessage.AppendLine(" Processing completed.");
                }
                else
                {
                    strMessage.Append(" : There were ");
                    strMessage.Append($"{lstHeaderErr.Count} Headers error, ");
                    strMessage.AppendLine($"{lstCellErr.Count} Rows error ");
                }


                if (lstHeaderErr.Any())
                {
                    strMessage.AppendLine($"  - {SheetName} Header Error:");
                    foreach (string headerErr in lstHeaderErr)
                    {
                        strMessage.AppendLine($"     + {headerErr}");
                    }
                }

                if (lstCellErr.Any())
                {
                    strMessage.AppendLine($"  - {SheetName} Cell Error:");
                    foreach (string cellErr in lstCellErr)
                    {
                        strMessage.AppendLine($"     + {cellErr}");
                    }
                }
                m_objProcessResult.Message = strMessage.ToString().TrimEnd() + Environment.NewLine; // Remove trailing whitespace

                return m_objProcessResult;
            }
            catch (IOException objEx)
            {
                m_objProcessResult.IsSuccess = false;
                m_objProcessResult.Message = $"- {DateTime.Now} : {SheetName} Sheet Error reading the Excel file: {objEx.Message} {Environment.NewLine}";
                return m_objProcessResult;
            }
            catch (Exception objEx)
            {
                m_objProcessResult.IsSuccess = false;
                m_objProcessResult.Message = $"- {DateTime.Now} :{m_strSheetName} Sheet An error occurred: {objEx.Message} {Environment.NewLine}";
                return m_objProcessResult;
            }
        }

        /// <summary>
        /// Validate Headers in the specified worksheet against predefined column headers.
        /// </summary>
        public void ValidateHeaders(IXLWorksheet x_objWorksheet, List<string> x_lstHeaderErr)
        {

            // Iterate through each defined header in COLUMN_HEADERS
            foreach (KeyValuePair<string, string> header in Colunms)
            {
                // Retrieve the header name from the worksheet
                string strHeaderName = x_objWorksheet.Cell(header.Key).GetValue<string>();

                // Compare the retrieved header name with the expected value
                if (strHeaderName != header.Value)
                {
                    // If they do not match, log the error
                    x_lstHeaderErr.Add($"{header.Key} : Current {strHeaderName}  is not Equal {header.Value}");
                }
            }
        }

        /// <summary>
        /// Extract data from the specified worksheet.
        /// </summary>
        public void ExtractDataFromWorksheet(IXLWorksheet x_objWorksheet, List<Dictionary<string, string>> x_lstData)
        {
            // Determine the starting row, which is the first row after the header
            int nRow = RowHeader + 1;

            // Loop through the rows until an empty cell is found in the 9th column
            while (x_objWorksheet.Cell(nRow, GetColunm(ColunmKey)).IsEmpty() == false)
            {
                // Create a new dictionary to store values of the current row
                Dictionary<string, string> objRowData = new Dictionary<string, string>();

                // Retrieve and store the value of each cell in the current row, using the header as the key
                foreach (KeyValuePair<string, string> objHeader in Colunms)
                {
                    // Convert value Equation in Event  
                    if ( (SheetName == EVENT.SHEET_NAME) && (objHeader.Value == DEFINE.Equation) )
                    {
                        string strEquation = GetCellValue<string>(x_objWorksheet, nRow, objHeader.Key);
                        if (strEquation == "!=")
                        {
                            objRowData[objHeader.Value] = "1";
                        }
                        else if (strEquation == "=")
                        {
                            objRowData[objHeader.Value] = "0";
                        }
                    }
                    else
                    {
                        objRowData[objHeader.Value] = GetCellValue<string>(x_objWorksheet, nRow, objHeader.Key);
                    }                
                }

                // Add the row data to the list
                x_lstData.Add(objRowData);

                // Move to the next row
                nRow++;
            }
        }

        /// <summary>
        /// Validates mandatory fields in the provided data list and fills in any missing values from the previous row.
        /// </summary>
        public void ValidateMandatoryFieldsAndFillCell(List<Dictionary<string, string>> x_lstData, List<string> x_lstCellErr)
        {
            string strCellErr;
            // Loop through each plan entry
            for (int i = 0; i < x_lstData.Count; i++)
            {
                // Check if the current row is the first or has a filled 'No' field
                if ((i == 0) || (string.IsNullOrEmpty(x_lstData[i]["No."]) == false))
                {
                    // Check mandatory fields for the current row
                    strCellErr = CheckMandatoryField(x_lstData[i], i);
                    if (string.IsNullOrEmpty(strCellErr) == false)
                    {
                        x_lstCellErr.Add(strCellErr);
                    }
                }
                else
                {
                    // Copy data from the previous row if the 'No' field is empty
                    CopyFromPreviousRow(x_lstData, i);
                }
            }
        }

        /// <summary>
        /// Checks if mandatory fields in the provided row data are present and not empty.
        /// </summary>
        /// <returns>A string containing an error message if any mandatory field is missing; otherwise, an empty string.</returns>
        public string CheckMandatoryField(Dictionary<string, string> x_objRowData, int x_nRow)
        {
            // Initialize a list to store any errors found during validation
            string srtlstCellErr = string.Empty;

            // Loop through the mandatory fields provided in x_arrMandatoryFields
            foreach (string strField in MandatoryFields)
            {
                // Check if the mandatory field exists in the dictionary and if it's empty or null
                if ((x_objRowData.ContainsKey(strField) == false) || (string.IsNullOrEmpty(x_objRowData[strField])))
                {
                    // Add an error message to lstCellErr indicating the missing field for the current row
                    srtlstCellErr = ($"Row {x_nRow + RowStart} : Missing mandatory field {strField}");
                }
            }

            // Return the list of cell errors for this row
            return srtlstCellErr;
        }

        /// <summary>
        /// If a field is empty or null in the current row, it is filled with the value from the previous row.
        /// </summary>
        public void CopyFromPreviousRow(List<Dictionary<string, string>> x_lstData, int x_nIndex)
        {
            // Retrieve the previous row's data for comparison
            Dictionary<string, string> objPreviousRow = x_lstData[x_nIndex - 1];

            // Retrieve the current row's data
            Dictionary<string, string> objCurrentRow = x_lstData[x_nIndex];

            // Iterate over each field in the current row
            foreach (KeyValuePair<string, string> objField in objPreviousRow)
            {
                if (objField.Key != "ParameterID" || (string.IsNullOrEmpty(objCurrentRow[objField.Key]) == false))
                {
                    objCurrentRow[objField.Key] = objPreviousRow[objField.Key];
                }
            }

            // After filling, replace the current row in the plan list with the updated row
            x_lstData[x_nIndex] = objCurrentRow;
        }

        /// <summary>
        /// Get Colunm by Cell
        /// </summary>
        public string GetColunm(string x_strInput)
        {
            return new string(x_strInput.Where(c => char.IsDigit(c) == false).ToArray());
        }

        /// <summary>
        /// Generic method to get the value from a cell.
        /// </summary>
        public T GetCellValue<T>(IXLWorksheet x_objWorksheet, int x_nRow, string x_strColumn)
        {
            try
            {
                return x_objWorksheet.Cell(x_nRow, GetColunm(x_strColumn)).GetValue<T>();
            }
            catch (Exception objEx)
            {
                string strWorksheetName = x_objWorksheet.Name;
                throw new ApplicationException($"Failed to get cell value at ({x_nRow}, {x_strColumn}) in Worksheet '{strWorksheetName}': {objEx.Message}", objEx);
            }
        }
        #endregion Methods
    }
}
