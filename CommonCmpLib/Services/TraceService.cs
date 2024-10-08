using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CommonCmpLib
{

    //public static class TraceService
    //{
    //    private const string A1 = "No.";
    //    private const string B1 = "TraceID";
    //    private const string C1 = "TraceName";
    //    private const string D1 = "Description";
    //    private const string E1 = "StartOn";
    //    private const string F1 = "StopOn";
    //    private const string G1 = "ParameterID";
    //    private const int HEADER_ROW = 1;
    //    private const int START_ROW = HEADER_ROW + 1;
    //    static readonly List<string> LIST_COLUMN = new List<string> { "", A1, B1, C1, D1, E1, F1, G1 };

    //    /// <summary>
    //    /// Process trace data from an Excel file.
    //    /// </summary>
    //    public static ExcelProcessResult<ExlTraceRequestModel> ReadFromExcel(string x_strFilePath)
    //    {
    //        // Initialize variables for processing
    //        ExcelProcessResult<ExlTraceRequestModel> objTraceProcess;
    //        List<ExlTraceRequestModel> lstTraceList;
    //        ExlTraceRequestModel objTraceData;
    //        List<string> lstHeaderErr;
    //        List<string> lstCellErr;
    //        IXLWorksheet objWorksheet;
    //        bool bIsSuccess = true;
    //        int nRow = START_ROW;

    //        // Initialize lists for storing errors and trace data
    //        lstHeaderErr = new List<string>();
    //        lstCellErr = new List<string>();
    //        lstTraceList = new List<ExlTraceRequestModel>();

    //        try
    //        {
    //            // Open the Excel file stream
    //            using (FileStream objStream = new FileStream(x_strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
    //            {
    //                // Load the Excel workbook
    //                using (XLWorkbook workbook = new XLWorkbook(objStream))
    //                {
    //                    objWorksheet = workbook.Worksheet(ExcelSheetName.Trace.ToString());  // Change to Trace sheet

    //                    // Check for header errors
    //                    for (int i = 1; i < LIST_COLUMN.Count; i++)
    //                    {
    //                        string strHeaderName = objWorksheet.Cell(HEADER_ROW, i).GetValue<string>();
    //                        if (strHeaderName != LIST_COLUMN[i])
    //                        {
    //                            lstHeaderErr.Add($"{objWorksheet.Cell(HEADER_ROW, i).Address} : {strHeaderName} != {LIST_COLUMN[i]}");
    //                            bIsSuccess = false;
    //                        }
    //                    }

    //                    // Loop through rows until an empty cell is found in column 7 (ParameterID)
    //                    while (objWorksheet.Cell(nRow, 7).IsEmpty() == false)
    //                    {
    //                        // Create and populate a new trace data model
    //                        objTraceData = new ExlTraceRequestModel();

    //                        objTraceData.No = GetCellValue<string>(objWorksheet, nRow, 1);
    //                        objTraceData.TraceID = GetCellValue<string>(objWorksheet, nRow, 2);
    //                        objTraceData.TraceName = GetCellValue<string>(objWorksheet, nRow, 3);
    //                        objTraceData.Description = GetCellValue<string>(objWorksheet, nRow, 4);
    //                        objTraceData.StartOn = GetCellValue<string>(objWorksheet, nRow, 5);
    //                        objTraceData.StopOn = GetCellValue<string>(objWorksheet, nRow, 6);
    //                        objTraceData.ParameterID = GetCellValue<string>(objWorksheet, nRow, 7);

    //                        // Add the trace data to the list
    //                        lstTraceList.Add(objTraceData);
    //                        nRow++;
    //                    }
    //                }
    //            }

    //            // Fill in missing Mandatory fields by copying from the previous row
    //            for (int i = 0; i < lstTraceList.Count; i++)
    //            {
    //                bool bCheckCellErr = false;
    //                string strRowErr = $"Row {i + START_ROW} has cells that have not been entered : ";

    //                // Check mandatory fields for the current row
    //                if (i == 0 || !string.IsNullOrEmpty(lstTraceList[i].No))
    //                {
    //                    CheckMandatoryFields(lstTraceList, i, ref strRowErr, ref bCheckCellErr);
    //                }
    //                else
    //                {
    //                    CopyFromPreviousRow(lstTraceList, i);
    //                }

    //                // If there are any errors in the row, log them and set IsSuccess to false
    //                if (bCheckCellErr)
    //                {
    //                    lstCellErr.Add(strRowErr);
    //                    bIsSuccess = false;
    //                }
    //            }

    //            // Prepare the result object with the processed data and any errors
    //            objTraceProcess = new ExcelProcessResult<ExlTraceRequestModel>();
    //            objTraceProcess.Models = lstTraceList;
    //            objTraceProcess.TotalRow = nRow - START_ROW;
    //            objTraceProcess.IsSuccess = bIsSuccess;
    //            objTraceProcess.CellError = lstCellErr;
    //            objTraceProcess.HeadersError = lstHeaderErr;
    //            return objTraceProcess;
    //        }
    //        catch (IOException objEx)
    //        {
    //            throw new ApplicationException($"Error reading the Excel file: {objEx.Message}", objEx);
    //        }
    //        catch (Exception objEx)
    //        {
    //            throw new ApplicationException($"An error occurred: {objEx.Message}", objEx);
    //        }
    //    }

    //    /// <summary>
    //    /// Copy values from the previous row into the current row for missing fields.
    //    /// </summary>
    //    private static void CopyFromPreviousRow(List<ExlTraceRequestModel> x_lstTraceList, int x_nIndex)
    //    {
    //        x_lstTraceList[x_nIndex].No = x_lstTraceList[x_nIndex - 1].No;
    //        x_lstTraceList[x_nIndex].TraceID = x_lstTraceList[x_nIndex - 1].TraceID;
    //        x_lstTraceList[x_nIndex].TraceName = x_lstTraceList[x_nIndex - 1].TraceName;
    //        x_lstTraceList[x_nIndex].Description = x_lstTraceList[x_nIndex - 1].Description;
    //        x_lstTraceList[x_nIndex].StartOn = x_lstTraceList[x_nIndex - 1].StartOn;
    //        x_lstTraceList[x_nIndex].StopOn = x_lstTraceList[x_nIndex - 1].StopOn;
    //        x_lstTraceList[x_nIndex].ParameterID = x_lstTraceList[x_nIndex - 1].ParameterID;
    //    }

    //    /// <summary>
    //    /// Check mandatory fields for the current row and log any errors.
    //    /// </summary>
    //    private static void CheckMandatoryFields(List<ExlTraceRequestModel> x_lstTraceList, int x_nIndex, ref string x_strRowErr, ref bool x_bCheckCellErr)
    //    {
    //        if (string.IsNullOrEmpty(x_lstTraceList[x_nIndex].No))
    //        {
    //            x_strRowErr += $"{A1} ,";
    //            x_bCheckCellErr = true;
    //        }

    //        if (string.IsNullOrEmpty(x_lstTraceList[x_nIndex].TraceID))
    //        {
    //            x_strRowErr += $"{B1} ,";
    //            x_bCheckCellErr = true;
    //        }

    //        if (string.IsNullOrEmpty(x_lstTraceList[x_nIndex].TraceName))
    //        {
    //            x_strRowErr += $"{C1} ,";
    //            x_bCheckCellErr = true;
    //        }

    //        if (string.IsNullOrEmpty(x_lstTraceList[x_nIndex].ParameterID))
    //        {
    //            x_strRowErr += $"{G1} ,";
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
