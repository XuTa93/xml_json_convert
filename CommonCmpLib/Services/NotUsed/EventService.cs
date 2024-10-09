using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;

namespace CommonCmpLib
{
    //public static class EventService
    //{
    //    private const string SHEET_NAME = "Event";
    //    private const string A1 = "No.";
    //    private const string B1 = "EventID";
    //    private const string C1 = "EventName";
    //    private const string D2 = "AndOr";
    //    private const string E2 = "ParameterID";
    //    private const string F2 = "Equation";
    //    private const string G2 = "Value";
    //    private const int HEADER_ROW = 2;
    //    private const int START_ROW = HEADER_ROW + 1;
    //    static readonly Dictionary<string, string> COLUMN_HEADERS = new Dictionary<string, string>
    //    {
    //        { "A1", A1  },
    //        { "B1", B1 },
    //        { "C1", C1 },
    //        { "D2", D2 },
    //        { "E2", E2 },
    //        { "F2", F2 },
    //        { "G2", G2 },
    //    };

    //    /// <summary>
    //    /// Process event data from an Excel file.
    //    /// </summary>
    //    public static ExcelProcessResult<ExlEventRequestModel> ReadFromExcel(string x_strFilePath)
    //    {
    //        // Initialize variables for processing
    //        ExcelProcessResult<ExlEventRequestModel> objEventProcess;
    //        List<ExlEventRequestModel> lstEventList;
    //        ExlEventRequestModel objEventData;
    //        List<string> lstHeaderErr;
    //        List<string> lstCellErr;
    //        IXLWorksheet objWorksheet;
    //        bool bIsSuccess = true;
    //        int nRow = START_ROW;

    //        // Initialize lists for storing errors and event data
    //        lstHeaderErr = new List<string>();
    //        lstCellErr = new List<string>();
    //        lstEventList = new List<ExlEventRequestModel>();

    //        try
    //        {
    //            // Open the Excel file stream
    //            using (FileStream objStream = new FileStream(x_strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
    //            {
    //                // Load the Excel workbook
    //                using (XLWorkbook workbook = new XLWorkbook(objStream))
    //                {
    //                    objWorksheet = workbook.Worksheet(SHEET_NAME);  // Change to Event sheet

    //                    // Check for header errors
    //                    foreach (KeyValuePair<string, string> header in COLUMN_HEADERS)
    //                    {
    //                        string strHeaderName = objWorksheet.Cell(header.Key).GetValue<string>();
    //                        if (strHeaderName != header.Value)
    //                        {
    //                            lstHeaderErr.Add($"{header.Key} : {strHeaderName} != {header.Value}");
    //                            bIsSuccess = false;
    //                        }
    //                    }

    //                    // Loop through rows until an empty cell is found in column 5 (ParameterID)
    //                    while (objWorksheet.Cell(nRow, 5).IsEmpty() == false)
    //                    {
    //                        // Create and populate a new event data model
    //                        objEventData = new ExlEventRequestModel();

    //                        objEventData.No = GetCellValue<string>(objWorksheet, nRow, 1);
    //                        objEventData.EventID = GetCellValue<string>(objWorksheet, nRow, 2);
    //                        objEventData.EventName = GetCellValue<string>(objWorksheet, nRow, 3);
    //                        objEventData.AndOr = GetCellValue<string>(objWorksheet, nRow, 4);
    //                        objEventData.ParameterID = GetCellValue<string>(objWorksheet, nRow, 5);
    //                        objEventData.Equation = GetCellValue<string>(objWorksheet, nRow, 6);
    //                        objEventData.Value = GetCellValue<string>(objWorksheet, nRow, 7);

    //                        // Add the event data to the list
    //                        lstEventList.Add(objEventData);
    //                        nRow++;
    //                    }
    //                }
    //            }

    //            // Fill in missing Mandatory fields by copying from the previous row
    //            for (int i = 0; i < lstEventList.Count; i++)
    //            {
    //                bool bCheckCellErr = false;
    //                string strRowErr = $"Row {i + START_ROW} has cells that have not been entered : ";

    //                // Check mandatory fields for the current row
    //                if (i == 0 || !string.IsNullOrEmpty(lstEventList[i].No))
    //                {
    //                    CheckMandatoryFields(lstEventList[i], ref strRowErr, ref bCheckCellErr);
    //                }
    //                else
    //                {
    //                    CopyFromPreviousRow(lstEventList, i);
    //                }

    //                // If there are any errors in the row, log them and set IsSuccess to false
    //                if (bCheckCellErr)
    //                {
    //                    lstCellErr.Add(strRowErr);
    //                    bIsSuccess = false;
    //                }
    //            }

    //            // Prepare the result object with the processed data and any errors
    //            objEventProcess = new ExcelProcessResult<ExlEventRequestModel>
    //            {
    //                Models = lstEventList,
    //                TotalRow = nRow - START_ROW,
    //                IsSuccess = bIsSuccess,
    //                CellError = lstCellErr,
    //                HeadersError = lstHeaderErr,
    //                Message = bIsSuccess ? $"{SHEET_NAME} Processing completed successfully." : $"{SHEET_NAME} There were errors during processing."
    //            };
    //            return objEventProcess;
    //        }
    //        catch (IOException objEx)
    //        {
    //            objEventProcess = new ExcelProcessResult<ExlEventRequestModel>
    //            {
    //                Models = null,
    //                TotalRow = 0,
    //                IsSuccess = false,
    //                CellError = new List<string>(),
    //                HeadersError = new List<string>(),
    //                Message = $"Error reading the Excel file: {objEx.Message}"
    //            };
    //            return objEventProcess; // Return the process result with the error message
    //        }
    //        catch (Exception objEx)
    //        {
    //            objEventProcess = new ExcelProcessResult<ExlEventRequestModel>
    //            {
    //                Models = null,
    //                TotalRow = 0,
    //                IsSuccess = false,
    //                CellError = new List<string>(),
    //                HeadersError = new List<string>(),
    //                Message = $"An error occurred: {objEx.Message}"
    //            };
    //            return objEventProcess; // Return the process result with the error message
    //        }
    //    }


    //    /// <summary>
    //    /// Copy values from the previous row into the current row for missing fields.
    //    /// </summary>
    //    private static void CopyFromPreviousRow(List<ExlEventRequestModel> x_lstEventList, int x_nIndex)
    //    {
    //        x_lstEventList[x_nIndex].No = x_lstEventList[x_nIndex - 1].No;
    //        x_lstEventList[x_nIndex].EventID = x_lstEventList[x_nIndex - 1].EventID;
    //        x_lstEventList[x_nIndex].EventName = x_lstEventList[x_nIndex - 1].EventName;
    //        x_lstEventList[x_nIndex].AndOr = x_lstEventList[x_nIndex - 1].AndOr;
    //        x_lstEventList[x_nIndex].ParameterID = x_lstEventList[x_nIndex - 1].ParameterID;
    //        x_lstEventList[x_nIndex].Equation = x_lstEventList[x_nIndex - 1].Equation;
    //        x_lstEventList[x_nIndex].Value = x_lstEventList[x_nIndex - 1].Value;
    //    }

    //    /// <summary>
    //    /// Check mandatory fields for the current row and log any errors.
    //    /// </summary>
    //    private static void CheckMandatoryFields(ExlEventRequestModel x_lstEvent, ref string x_strRowErr, ref bool x_bCheckCellErr)
    //    {
    //        if (string.IsNullOrEmpty(x_lstEvent.No))
    //        {
    //            x_strRowErr += $"{A1} ,";
    //            x_bCheckCellErr = true;
    //        }

    //        if (string.IsNullOrEmpty(x_lstEvent.EventID))
    //        {
    //            x_strRowErr += $"{B1} ,";
    //            x_bCheckCellErr = true;
    //        }

    //        if (string.IsNullOrEmpty(x_lstEvent.EventName))
    //        {
    //            x_strRowErr += $"{C1} ,";
    //            x_bCheckCellErr = true;
    //        }

    //        if (string.IsNullOrEmpty(x_lstEvent.AndOr))
    //        {
    //            x_strRowErr += $"{D2} ,";
    //            x_bCheckCellErr = true;
    //        }

    //        if (string.IsNullOrEmpty(x_lstEvent.ParameterID))
    //        {
    //            x_strRowErr += $"{E2} ,";
    //            x_bCheckCellErr = true;
    //        }

    //        if (string.IsNullOrEmpty(x_lstEvent.Equation))
    //        {
    //            x_strRowErr += $"{F2} ,";
    //            x_bCheckCellErr = true;
    //        }

    //        if (string.IsNullOrEmpty(x_lstEvent.Value))
    //        {
    //            x_strRowErr += $"{G2} ,";
    //            x_bCheckCellErr = true;
    //        }
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
    //}
}
