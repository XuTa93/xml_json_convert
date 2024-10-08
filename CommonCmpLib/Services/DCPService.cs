using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommonCmpLib
{
    //public static class DCPService
    //{
    //    #region Type
    //    private const string SHEET_NAME = "DataCollectionPlan";
    //    private const string A1 = "No.";
    //    private const string B1 = "MachineName";
    //    private const string C1 = "PlanID";
    //    private const string D1 = "PlanName";
    //    private const string E1 = "Description";
    //    private const string F1 = "StartEvent";
    //    private const string G1 = "EndEvent";
    //    private const string H1 = "TimeRequest";
    //    private const string I2 = "ParameterID";
    //    private const int HEADER_ROW = 2;
    //    private const int START_ROW = HEADER_ROW + 1;
    //    static readonly Dictionary<string, string> COLUMN_HEADERS = new Dictionary<string, string>
    //    {
    //        { "A1", A1  },
    //        { "B1", B1 },
    //        { "C1", C1 },
    //        { "D1", D1 },
    //        { "E1", E1 },
    //        { "F1", F1 },
    //        { "G1", G1 },
    //        { "H1", H1 },
    //        { "I2", I2 }
    //    };
    //    #endregion Type

    //    #region Methods
    //    /// <summary>
    //    /// Process data collection plan from an Excel file.
    //    /// </summary>
    //    public static ExcelProcessResult<ExlDCPModel> ReadFromExcel(string x_strFilePath)
    //    {
    //        // Initialize variables for processing
    //        ExcelProcessResult<ExlDCPModel> objPlanProcess;
    //        List<ExlDCPModel> lstPlanList;
    //        List<string> lstHeaderErr;
    //        List<string> lstCellErr;
    //        IXLWorksheet objWorksheet;
    //        bool bIsSuccess;
    //        StringBuilder strMessage;

    //        // Initialize lists for storing errors and plan data
    //        bIsSuccess = true;
    //        lstHeaderErr = new List<string>();
    //        lstCellErr = new List<string>();
    //        lstPlanList = new List<ExlDCPModel>();

    //        try
    //        {
    //            // Open the Excel file stream
    //            using (FileStream objStream = new FileStream(x_strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
    //            {
    //                // Load the Excel workbook
    //                using (XLWorkbook workbook = new XLWorkbook(objStream))
    //                {
    //                    objWorksheet = workbook.Worksheet(SHEET_NAME);

    //                    // Validate headers
    //                    ValidateHeaders(objWorksheet, lstHeaderErr);
    //                    bIsSuccess = lstHeaderErr.Count == 0;

    //                    // Extract data from worksheet
    //                    lstPlanList = ExtractDataFromWorksheet(objWorksheet);
    //                }
    //            }

    //            // Fill in missing Mandatory fields by copying from the previous row
    //            lstCellErr = ValidateMandatoryFieldsAndFillCell(lstPlanList);

    //            // Prepare the result object with the processed data and any errors
    //            bIsSuccess = bIsSuccess && (lstCellErr.Any() == false);

    //            // Prepare the message
    //            strMessage = new StringBuilder();
    //            strMessage.AppendLine(bIsSuccess ? $"{SHEET_NAME} Processing completed successfully." : $"{SHEET_NAME} : There were errors during processing.");

    //            if (lstHeaderErr.Any())
    //            {
    //                strMessage.AppendLine($"\t- {SHEET_NAME} Header Error:");
    //                foreach (string headerErr in lstHeaderErr)
    //                {
    //                    strMessage.AppendLine($"\t\t+ {headerErr}");
    //                }
    //            }

    //            if (lstCellErr.Any())
    //            {
    //                strMessage.AppendLine($"\t- {SHEET_NAME} Cell Error:");
    //                foreach (string cellErr in lstCellErr)
    //                {
    //                    strMessage.AppendLine($"\t\t+ {cellErr}");
    //                }
    //            }

    //            objPlanProcess = new ExcelProcessResult<ExlDCPModel>
    //            {
    //                Models = lstPlanList,
    //                TotalRow = lstPlanList.Count,
    //                IsSuccess = bIsSuccess,
    //                CellError = lstCellErr,
    //                HeadersError = lstHeaderErr,
    //                Message = strMessage.ToString().TrimEnd() // Loại bỏ ký tự trắng ở cuối
    //            };

    //            return objPlanProcess;
    //        }
    //        catch (IOException objEx)
    //        {
    //            return CreateErrorResult($"{SHEET_NAME} Sheet Error reading the Excel file: {objEx.Message}");
    //        }
    //        catch (Exception objEx)
    //        {
    //            return CreateErrorResult($"{SHEET_NAME} Sheet An error occurred: {objEx.Message}");
    //        }
    //    }

    //    /// <summary>
    //    /// Create Error Result
    //    /// </summary>
    //    /// <returns></returns>
    //    private static ExcelProcessResult<ExlDCPModel> CreateErrorResult(string x_strMessage)
    //    {
    //        return new ExcelProcessResult<ExlDCPModel>
    //        {
    //            Models = null,
    //            TotalRow = 0,
    //            IsSuccess = false,
    //            CellError = new List<string>(),
    //            HeadersError = new List<string>(),
    //            Message = x_strMessage
    //        };
    //    }

    //    /// <summary>
    //    /// Validate Headers in the specified worksheet against predefined column headers.
    //    /// </summary>
    //    /// <param name="x_objWorksheet">The worksheet to validate.</param>
    //    /// <param name="lstHeaderErr">The list to store header errors.</param>
    //    private static void ValidateHeaders(IXLWorksheet x_objWorksheet, List<string> lstHeaderErr)
    //    {
    //        // Iterate through each defined header in COLUMN_HEADERS
    //        foreach (KeyValuePair<string, string> header in COLUMN_HEADERS)
    //        {
    //            // Retrieve the header name from the worksheet
    //            string strHeaderName = x_objWorksheet.Cell(header.Key).GetValue<string>();

    //            // Compare the retrieved header name with the expected value
    //            if (strHeaderName != header.Value)
    //            {
    //                // If they do not match, log the error
    //                lstHeaderErr.Add($"{header.Key} : {strHeaderName} != {header.Value}");
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// Extract data from the specified worksheet.
    //    /// </summary>
    //    /// <param name="x_objWorksheet">The worksheet to extract data from.</param>
    //    /// <param name="x_bIsSuccess">Flag indicating success of the operation (passed by reference).</param>
    //    /// <param name="x_lstCellErr">List to store any cell errors encountered.</param>
    //    /// <returns>List of extracted ExlDCPModel objects.</returns>
    //    private static List<ExlDCPModel> ExtractDataFromWorksheet(IXLWorksheet x_objWorksheet)
    //    {
    //        List<ExlDCPModel> lstPlanList = new List<ExlDCPModel>();
    //        int nRow = START_ROW;

    //        // Loop through the rows until an empty cell is found in the 9th column
    //        while (!x_objWorksheet.Cell(nRow, 9).IsEmpty())
    //        {
    //            // Create a new instance of ExlDCPModel and populate it with cell values
    //            ExlDCPModel objPlanData = new ExlDCPModel
    //            {
    //                No = GetCellValue<string>(x_objWorksheet, nRow, 1),
    //                MachineName = GetCellValue<string>(x_objWorksheet, nRow, 2),
    //                PlanID = GetCellValue<string>(x_objWorksheet, nRow, 3),
    //                PlanName = GetCellValue<string>(x_objWorksheet, nRow, 4),
    //                Description = GetCellValue<string>(x_objWorksheet, nRow, 5),
    //                StartEvent = GetCellValue<string>(x_objWorksheet, nRow, 6),
    //                EndEvent = GetCellValue<string>(x_objWorksheet, nRow, 7),
    //                TimeRequest = GetCellValue<string>(x_objWorksheet, nRow, 8),
    //                ParameterID = GetCellValue<string>(x_objWorksheet, nRow, 9)
    //            };

    //            // Optionally validate objPlanData here and add errors to x_lstCellErr if needed
    //            lstPlanList.Add(objPlanData);
    //            nRow++;
    //        }

    //        return lstPlanList;
    //    }

    //    /// <summary>
    //    /// Validate mandatory fields and fill in empty cells if necessary.
    //    /// </summary>
    //    /// <param name="x_lstPlanList">List of plans to validate.</param>
    //    /// <returns>List of error messages for any mandatory fields that were not filled.</returns>
    //    private static List<string> ValidateMandatoryFieldsAndFillCell(List<ExlDCPModel> x_lstPlanList)
    //    {
    //        List<string> lstCellErr = new List<string>();

    //        // Loop through each plan entry
    //        for (int i = 0; i < x_lstPlanList.Count; i++)
    //        {
    //            bool bHasCellError = false; // Flag to indicate if there are any cell errors
    //            string strRowErr = $"Row {i + START_ROW} has cells that have not been entered: ";

    //            // Check if the current row is the first or has a filled 'No' field
    //            if (i == 0 || !string.IsNullOrEmpty(x_lstPlanList[i].No))
    //            {
    //                // Check mandatory fields for the current row
    //                CheckMandatoryFields(x_lstPlanList[i], ref strRowErr, ref bHasCellError);
    //            }
    //            else
    //            {
    //                // Copy data from the previous row if the 'No' field is empty
    //                CopyFromPreviousRow(x_lstPlanList, i);
    //            }

    //            // If there are any errors in the row, add them to the list
    //            if (bHasCellError)
    //            {
    //                // Ensure strRowErr is not just the initial message
    //                if (strRowErr != $"Row {i + START_ROW} has cells that have not been entered: ")
    //                {
    //                    lstCellErr.Add(strRowErr);
    //                }
    //            }
    //        }

    //        return lstCellErr; // Return the list of errors found
    //    }

    //    /// <summary>
    //    /// Copy values from the previous row into the current row for missing fields.
    //    /// </summary>
    //    /// <param name="x_lstPlanList">List of plans containing the data.</param>
    //    /// <param name="x_nIndex">Index of the current row to fill in.</param>
    //    private static void CopyFromPreviousRow(List<ExlDCPModel> x_lstPlanList, int x_nIndex)
    //    {
    //        ExlDCPModel previousRow = x_lstPlanList[x_nIndex - 1];
    //        // Copy values from the previous row
    //        x_lstPlanList[x_nIndex].No = previousRow.No;
    //        x_lstPlanList[x_nIndex].MachineName = previousRow.MachineName;
    //        x_lstPlanList[x_nIndex].PlanID = previousRow.PlanID;
    //        x_lstPlanList[x_nIndex].PlanName = previousRow.PlanName;
    //        x_lstPlanList[x_nIndex].Description = previousRow.Description;
    //        x_lstPlanList[x_nIndex].StartEvent = previousRow.StartEvent;
    //        x_lstPlanList[x_nIndex].EndEvent = previousRow.EndEvent;
    //        x_lstPlanList[x_nIndex].TimeRequest = previousRow.TimeRequest;
    //    }

    //    /// <summary>
    //    /// Check mandatory fields for the current row and log any errors.
    //    /// </summary>
    //    private static void CheckMandatoryFields(ExlDCPModel x_objPlan, ref string x_strRowErr, ref bool x_bCheckCellErr)
    //    {
    //        if (string.IsNullOrEmpty(x_objPlan.No))
    //        {
    //            x_strRowErr += $"{A1} ,";
    //            x_bCheckCellErr = true;
    //        }

    //        if (string.IsNullOrEmpty(x_objPlan.MachineName))
    //        {
    //            x_strRowErr += $"{B1} ,";
    //            x_bCheckCellErr = true;
    //        }

    //        if (string.IsNullOrEmpty(x_objPlan.PlanID))
    //        {
    //            x_strRowErr += $"{C1} ,";
    //            x_bCheckCellErr = true;
    //        }

    //        if (string.IsNullOrEmpty(x_objPlan.PlanName))
    //        {
    //            x_strRowErr += $"{D1} ,";
    //            x_bCheckCellErr = true;
    //        }

    //        if (string.IsNullOrEmpty(x_objPlan.ParameterID))
    //        {
    //            x_strRowErr += $"{I2} ,";
    //            x_bCheckCellErr = true;
    //        }
    //        x_strRowErr = x_strRowErr.TrimEnd(',', ' ');
    //    }

    //    /// <summary>
    //    /// Generic method to get the value from a cell.
    //    /// </summary>
    //    private static T GetCellValue<T>(IXLWorksheet x_objWorksheet, int x_nRow, int x_nColumn)
    //    {
    //        try
    //        {
    //            return x_objWorksheet.Cell(x_nRow, x_nColumn).GetValue<T>();
    //        }
    //        catch (Exception objEx)
    //        {
    //            string strWorksheetName = x_objWorksheet.Name;
    //            throw new ApplicationException($"Failed to get cell value at ({x_nRow}, {x_nColumn}) in Worksheet '{strWorksheetName}': {objEx.Message}", objEx);
    //        }
    //    }
    //    #endregion Methods
    //}
}