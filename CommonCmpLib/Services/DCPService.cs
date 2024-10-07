using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CommonCmpLib
{
    public static class DCPService
    {
        #region Type
        private const string SHEET_NAME = "DataCollectionPlan";
        private const string A1 = "No.";
        private const string B1 = "MachineName";
        private const string C1 = "PlanID";
        private const string D1 = "PlanName";
        private const string E1 = "Description";
        private const string F1 = "StartEvent";
        private const string G1 = "EndEvent";
        private const string H1 = "TimeRequest";
        private const string I2 = "ParameterID";
        private const int HEADER_ROW = 2;
        private const int START_ROW = HEADER_ROW + 1;
        static readonly Dictionary<string, string> COLUMN_HEADERS = new Dictionary<string, string>
        {
            { "A1", A1  },
            { "B1", B1 },
            { "C1", C1 },
            { "D1", D1 },
            { "E1", E1 },
            { "F1", F1 },
            { "G1", G1 },
            { "H1", H1 },
            { "I2", I2 }
        };
        #endregion Type

        #region Methods
        /// <summary>
        /// Process data collection plan from an Excel file.
        /// </summary>
        public static ExcelProcessResult<ExlDCPModel> ReadFromExcel(string x_strFilePath)
        {
            // Initialize variables for processing
            ExcelProcessResult<ExlDCPModel> objPlanProcess;
            List<ExlDCPModel> lstPlanList;
            List<string> lstHeaderErr;
            List<string> lstCellErr;
            IXLWorksheet objWorksheet;
            bool bIsSuccess = true;

            // Initialize lists for storing errors and plan data
            lstHeaderErr = new List<string>();
            lstCellErr = new List<string>();
            lstPlanList = new List<ExlDCPModel>();

            try
            {
                // Open the Excel file stream
                using (FileStream objStream = new FileStream(x_strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    // Load the Excel workbook
                    using (XLWorkbook workbook = new XLWorkbook(objStream))
                    {
                        objWorksheet = workbook.Worksheet(SHEET_NAME);

                        // Validate headers
                        ValidateHeaders(objWorksheet, lstHeaderErr);
                        bIsSuccess = lstHeaderErr.Count == 0;

                        // Extract data from worksheet
                        lstPlanList = ExtractDataFromWorksheet(objWorksheet, ref bIsSuccess, lstCellErr);
                    }
                }

                // Fill in missing Mandatory fields by copying from the previous row
                lstCellErr = ValidateMandatoryFieldsAndFillCell(lstPlanList);

                // Prepare the result object with the processed data and any errors
                objPlanProcess = new ExcelProcessResult<ExlDCPModel>
                {
                    Models = lstPlanList,
                    TotalRow = lstPlanList.Count,
                    IsSuccess = bIsSuccess && (lstCellErr.Any() == false),
                    CellError = lstCellErr,
                    HeadersError = lstHeaderErr,
                    Message = bIsSuccess ? $"{SHEET_NAME} Processing completed successfully." : $"{SHEET_NAME} : There were errors during processing."
                };

                return objPlanProcess;
            }
            catch (IOException objEx)
            {
                return CreateErrorResult($"{SHEET_NAME} Error reading the Excel file: {objEx.Message}");
            }
            catch (Exception objEx)
            {
                return CreateErrorResult($"{SHEET_NAME} An error occurred: {objEx.Message}");
            }
        }

        /// <summary>
        /// Create Error Result
        /// </summary>
        /// <returns></returns>
        private static ExcelProcessResult<ExlDCPModel> CreateErrorResult(string x_strMessage)
        {
            return new ExcelProcessResult<ExlDCPModel>
            {
                Models = null,
                TotalRow = 0,
                IsSuccess = false,
                CellError = new List<string>(),
                HeadersError = new List<string>(),
                Message = x_strMessage
            };
        }

        /// <summary>
        /// Validate Headers
        /// </summary>
        private static void ValidateHeaders(IXLWorksheet x_objWorksheet, List<string> lstHeaderErr)
        {
            foreach (KeyValuePair<string, string> header in COLUMN_HEADERS)
            {
                string strHeaderName = x_objWorksheet.Cell(header.Key).GetValue<string>();
                if (strHeaderName != header.Value)
                {
                    lstHeaderErr.Add($"{header.Key} : {strHeaderName} != {header.Value}");
                }
            }
        }

        /// <summary>
        /// Extract data from worksheet 
        /// </summary>
        /// <returns></returns>
        private static List<ExlDCPModel> ExtractDataFromWorksheet(IXLWorksheet x_objWorksheet, ref bool x_bIsSuccess, List<string> x_lstCellErr)
        {
            List<ExlDCPModel> lstPlanList = new List<ExlDCPModel>();
            int nRow = START_ROW;

            while (!x_objWorksheet.Cell(nRow, 9).IsEmpty())
            {
                var objPlanData = new ExlDCPModel
                {
                    No = GetCellValue<string>(x_objWorksheet, nRow, 1),
                    MachineName = GetCellValue<string>(x_objWorksheet, nRow, 2),
                    PlanID = GetCellValue<string>(x_objWorksheet, nRow, 3),
                    PlanName = GetCellValue<string>(x_objWorksheet, nRow, 4),
                    Description = GetCellValue<string>(x_objWorksheet, nRow, 5),
                    StartEvent = GetCellValue<string>(x_objWorksheet, nRow, 6),
                    EndEvent = GetCellValue<string>(x_objWorksheet, nRow, 7),
                    TimeRequest = GetCellValue<string>(x_objWorksheet, nRow, 8),
                    ParameterID = GetCellValue<string>(x_objWorksheet, nRow, 9)
                };

                lstPlanList.Add(objPlanData);
                nRow++;
            }

            return lstPlanList;
        }

        /// <summary>
        /// Validate MandatoryFields And Fill Empty Cells
        /// </summary>
        /// <returns></returns>
        private static List<string> ValidateMandatoryFieldsAndFillCell(List<ExlDCPModel> x_lstPlanList)
        {
            List<string> lstCellErr = new List<string>();

            // Loop through each plan entry
            for (int i = 0; i < x_lstPlanList.Count; i++)
            {
                bool bCheckCellErr = false;
                string strRowErr = $"Row {i + START_ROW} has cells that have not been entered : ";

                // Check if the current row is the first or has a filled 'No' field
                if (i == 0 || !string.IsNullOrEmpty(x_lstPlanList[i].No))
                {
                    // Check mandatory fields for the current row
                    CheckMandatoryFields(x_lstPlanList[i], ref strRowErr, ref bCheckCellErr);
                }
                else
                {
                    // Copy data from the previous row if the 'No' field is empty
                    CopyFromPreviousRow(x_lstPlanList, i);
                }

                // If there are any errors in the row, add them to the list
                if (bCheckCellErr)
                {
                    lstCellErr.Add(strRowErr);
                }
            }

            return lstCellErr; // Return the list of errors found
        }

        /// <summary>
        /// Copy values from the previous row into the current row for missing fields.
        /// </summary>
        private static void CopyFromPreviousRow(List<ExlDCPModel> x_lstPlanList, int x_nIndex)
        {
            x_lstPlanList[x_nIndex].No = x_lstPlanList[x_nIndex - 1].No;
            x_lstPlanList[x_nIndex].MachineName = x_lstPlanList[x_nIndex - 1].MachineName;
            x_lstPlanList[x_nIndex].PlanID = x_lstPlanList[x_nIndex - 1].PlanID;
            x_lstPlanList[x_nIndex].PlanName = x_lstPlanList[x_nIndex - 1].PlanName;
            x_lstPlanList[x_nIndex].Description = x_lstPlanList[x_nIndex - 1].Description;
            x_lstPlanList[x_nIndex].StartEvent = x_lstPlanList[x_nIndex - 1].StartEvent;
            x_lstPlanList[x_nIndex].EndEvent = x_lstPlanList[x_nIndex - 1].EndEvent;
            x_lstPlanList[x_nIndex].TimeRequest = x_lstPlanList[x_nIndex - 1].TimeRequest;
            x_lstPlanList[x_nIndex].ParameterID = x_lstPlanList[x_nIndex - 1].ParameterID;
        }

        /// <summary>
        /// Check mandatory fields for the current row and log any errors.
        /// </summary>
        private static void CheckMandatoryFields(ExlDCPModel x_objPlan, ref string x_strRowErr, ref bool x_bCheckCellErr)
        {
            if (string.IsNullOrEmpty(x_objPlan.No))
            {
                x_strRowErr += $"{A1} ,";
                x_bCheckCellErr = true;
            }

            if (string.IsNullOrEmpty(x_objPlan.MachineName))
            {
                x_strRowErr += $"{B1} ,";
                x_bCheckCellErr = true;
            }

            if (string.IsNullOrEmpty(x_objPlan.PlanID))
            {
                x_strRowErr += $"{C1} ,";
                x_bCheckCellErr = true;
            }

            if (string.IsNullOrEmpty(x_objPlan.ParameterID))
            {
                x_strRowErr += $"{I2} ,";
                x_bCheckCellErr = true;
            }
        }

        /// <summary>
        /// Generic method to get the value from a cell.
        /// </summary>
        private static T GetCellValue<T>(IXLWorksheet x_objWorksheet, int x_nRow, int x_nColumn)
        {
            try
            {
                return x_objWorksheet.Cell(x_nRow, x_nColumn).GetValue<T>();
            }
            catch (Exception objEx)
            {
                string strWorksheetName = x_objWorksheet.Name;
                throw new ApplicationException($"Failed to get cell value at ({x_nRow}, {x_nColumn}) in Worksheet '{strWorksheetName}': {objEx.Message}", objEx);
            }
        }
        #endregion Methods
    }
}