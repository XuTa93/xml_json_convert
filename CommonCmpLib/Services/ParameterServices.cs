using ClosedXML.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace CommonCmpLib
{
    //public static class ParameterServices
    //{
    //    private const string A1 = "No.";
    //    private const string B1 = "ParameterID";
    //    private const string C1 = "ParameterName";
    //    private const string D1 = "Locator";
    //    private const string E1 = "Unit";
    //    private const string F1 = "Type";
    //    private const string G1 = "Array";
    //    private const string H1 = "Function";
    //    private const string I1 = "Arg";
    //    private const string J1 = "Sourcetype";
    //    private const string K1 = "MemoryName";
    //    private const string L1 = "Offset";
    //    private const string M1 = "SourceType";
    //    private const string N1 = "SourceArray";
    //    private const int HEADER_ROW = 1;
    //    private const int START_ROW = HEADER_ROW + 1;
    //    static readonly List<string> LIST_COLUMN = new List<string> {"", A1, B1, C1, D1, E1, F1, G1, H1, I1, J1, K1, L1, M1, N1 };

    //    /// <summary>
    //    /// Read Parameters Sheet From Excel File 
    //    /// </summary>
    //    /// <returns></returns>
    //    public static ExcelProcessResult<ExlParameterModel> ReadFromExcel(string x_strfilePath)
    //    {
    //        // Initialize variables for processing
    //        ExcelProcessResult<ExlParameterModel> objParProcess;
    //        List<ExlParameterModel> lstParameter;
    //        ExlParameterModel objParameter;
    //        List<string> lstHeaderErr;
    //        List<string> lstCellErr;
    //        IXLWorksheet objWorksheet;
    //        bool bIsSuccess = true;
    //        int nRow = START_ROW; // Assuming the first row is the header

    //        // Initialize lists for storing errors and parameters
    //        lstHeaderErr = new List<string>();
    //        lstCellErr = new List<string>();
    //        lstParameter = new List<ExlParameterModel>();

    //        // Open Excel file stream for reading
    //        using (FileStream objStream = new FileStream(x_strfilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
    //        {
    //            // Load Excel workbook
    //            using (XLWorkbook workbook = new XLWorkbook(objStream))
    //            {
    //                objWorksheet = workbook.Worksheet(ExcelSheetName.Parameter.ToString());

    //                // Check for header errors
    //                for (int i = 1; i < LIST_COLUMN.Count; i++)
    //                {
    //                    string strHeaderName = objWorksheet.Cell(HEADER_ROW, i).GetValue<string>();

    //                    // If header does not match, log an error and set IsSuccess to false
    //                    if (strHeaderName != LIST_COLUMN[i])
    //                    {
    //                        lstHeaderErr.Add($"{objWorksheet.Cell(HEADER_ROW, i).Address} : {strHeaderName} != {LIST_COLUMN[i]}");
    //                        bIsSuccess = false;
    //                    }
    //                }

    //                // Process each row until an empty cell is encountered in the first column
    //                while (!objWorksheet.Cell(nRow, 1).IsEmpty())
    //                {
    //                    bool bCheckCellErr = false;
    //                    // Check for rows errors
    //                    string strRowErr = "";
    //                    strRowErr += ($"Row {nRow} has cells that have not been entered : ");

    //                    objParameter = new ExlParameterModel();

    //                    // Read each column's value and validate
    //                    objParameter.No = objWorksheet.Cell(nRow, 1).GetValue<string>();
    //                    if (string.IsNullOrEmpty(objParameter.No) == true)
    //                    {
    //                        strRowErr += $"{A1} ,";
    //                        bCheckCellErr = true;
    //                    }
    //                    objParameter.ParameterID = objWorksheet.Cell(nRow, 2).GetValue<string>();
    //                    if (string.IsNullOrEmpty(objParameter.ParameterID) == true)
    //                    {
    //                        strRowErr += $"{B1} ,";
    //                        bCheckCellErr = true;
    //                    }

    //                    objParameter.ParameterName = objWorksheet.Cell(nRow, 3).GetValue<string>();
    //                    if (string.IsNullOrEmpty(objParameter.ParameterName) == true)
    //                    {
    //                        strRowErr += $"{C1} ,";
    //                        bCheckCellErr = true;
    //                    }

    //                    objParameter.Locator = objWorksheet.Cell(nRow, 4).GetValue<string>();
    //                    if (string.IsNullOrEmpty(objParameter.Locator) == true)
    //                    {
    //                        strRowErr += $"{D1} ,";
    //                        bCheckCellErr = true;
    //                    }

    //                    objParameter.Unit = objWorksheet.Cell(nRow, 5).GetValue<string>();
    //                    objParameter.Type = objWorksheet.Cell(nRow, 6).GetValue<string>();
    //                    if (string.IsNullOrEmpty(objParameter.Type) == true)
    //                    {
    //                        strRowErr += $"{F1} ,";
    //                        bCheckCellErr = true;
    //                    }

    //                    objParameter.Array = objWorksheet.Cell(nRow, 7).GetValue<string>();
    //                    if (string.IsNullOrEmpty(objParameter.Array) == true)
    //                    {
    //                        strRowErr += $"{G1} ,";
    //                        bCheckCellErr = true;
    //                    }

    //                    objParameter.Function = objWorksheet.Cell(nRow, 8).GetValue<string>();
    //                    objParameter.Arg = objWorksheet.Cell(nRow, 9).GetValue<string>();
    //                    objParameter.Sourcetype = objWorksheet.Cell(nRow, 10).GetValue<string>();
    //                    if (string.IsNullOrEmpty(objParameter.Sourcetype) == true)
    //                    {
    //                        strRowErr += $"{J1} ,";
    //                        bCheckCellErr = true;
    //                    }

    //                    objParameter.MemoryName = objWorksheet.Cell(nRow, 11).GetValue<string>();
    //                    if (string.IsNullOrEmpty(objParameter.MemoryName) == true)
    //                    {
    //                        strRowErr += $"{K1} ,";
    //                        bCheckCellErr = true;
    //                    }

    //                    objParameter.Offset = objWorksheet.Cell(nRow, 12).GetValue<string>();
    //                    if (string.IsNullOrEmpty(objParameter.Offset) == true)
    //                    {
    //                        strRowErr += $"{L1} ,";
    //                        bCheckCellErr = true;
    //                    }

    //                    objParameter.SourceType = objWorksheet.Cell(nRow, 13).GetValue<string>();
    //                    if (string.IsNullOrEmpty(objParameter.SourceType) == true)
    //                    {
    //                        strRowErr += $"{M1} ,";
    //                        bCheckCellErr = true;
    //                    }

    //                    objParameter.SourceArray = objWorksheet.Cell(nRow, 14).GetValue<string>();
    //                    if (string.IsNullOrEmpty(objParameter.SourceArray) == true)
    //                    {
    //                        strRowErr += $"{N1} ,";
    //                        bCheckCellErr = true;
    //                    }

    //                    // If there are any errors in the row, log them and set IsSuccess to false
    //                    if (bCheckCellErr == true)
    //                    {
    //                        lstCellErr.Add(strRowErr);
    //                        bIsSuccess = false;
    //                    }

    //                    // Add the processed parameter to the list
    //                    lstParameter.Add(objParameter);
    //                    nRow++;
    //                }

    //                // Prepare the result object with the processed data and any errors
    //                objParProcess = new ExcelProcessResult<ExlParameterModel>();
    //                objParProcess.Models = lstParameter;
    //                objParProcess.TotalRow = nRow - START_ROW;
    //                objParProcess.IsSuccess = bIsSuccess;
    //                objParProcess.CellError = lstCellErr;
    //                objParProcess.HeadersError = lstHeaderErr;
    //            }
    //        }
    //        return objParProcess;
    //    }


    //    public static bool ExportParameterToXml(List<ExlParameterModel> x_lstParaModel, string x_FolderPath)
    //    {

    //        return true;
    //    }
    //    public static bool CreatePrameterFile(string filePath)
    //    {
    //        // Đường dẫn để lưu file Excel
    //        filePath = "ParameterData.xlsx";

    //        // Tạo workbook mới
    //        using (var workbook = new XLWorkbook())
    //        {
    //            // Tạo worksheet mới
    //            var worksheet = workbook.Worksheets.Add("Parameters");

    //            // Thêm tiêu đề cho các cột
    //            worksheet.Cell(1, 1).Value = "No.";
    //            worksheet.Cell(1, 2).Value = "ParameterID";
    //            worksheet.Cell(1, 3).Value = "ParameterName";
    //            worksheet.Cell(1, 4).Value = "Locator";
    //            worksheet.Cell(1, 5).Value = "Unit";
    //            worksheet.Cell(1, 6).Value = "Type";
    //            worksheet.Cell(1, 7).Value = "Array";
    //            worksheet.Cell(1, 8).Value = "Function";
    //            worksheet.Cell(1, 9).Value = "Arg";
    //            worksheet.Cell(1, 10).Value = "Sourcetype";
    //            worksheet.Cell(1, 11).Value = "MemoryName";
    //            worksheet.Cell(1, 12).Value = "Offset";
    //            worksheet.Cell(1, 13).Value = "SourceType";
    //            worksheet.Cell(1, 14).Value = "SourceArray";

    //            // Tô màu cho hàng tiêu đề
    //            var headerRange = worksheet.Range("A1:N1"); // Phạm vi từ cột A đến cột N của hàng 1
    //            headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
    //            headerRange.Style.Font.Bold = true; // Làm đậm chữ trong header

    //            // Thêm dữ liệu vào các dòng tiếp theo
    //            string[,] data = new string[,]
    //            {
    //            { "1", "MD1033", "LLA_CassetStatus", "Master", "I2", "I2", "1", "", "", "CommonMemory", "MASTER", "0", "I2", "1" },
    //            { "2", "MD1083", "LLB_CassetStatus", "Master", "I2", "I2", "1", "", "", "CommonMemory", "MASTER", "100", "I2", "1" },
    //            { "3", "MD1148", "Tr_Press", "Master", "Pa", "F4", "1", "1001", "", "CommonMemory", "MASTER", "230", "I4", "1" }
    //            };

    //            int startRow = 2;
    //            // Duyệt qua các dòng dữ liệu và thêm vào worksheet
    //            for (int i = 0; i < data.GetLength(0); i++)
    //            {
    //                for (int j = 0; j < data.GetLength(1); j++)
    //                {
    //                    worksheet.Cell(startRow + i, j + 1).Value = data[i, j]; // Ghi giá trị từ mảng vào worksheet
    //                }
    //            }

    //            // Kiểm tra xem có dòng nào đã được sử dụng hay không
    //            if (worksheet.LastRowUsed() != null)
    //            {
    //                // Thiết lập phạm vi cho tất cả các dòng dữ liệu (bắt đầu từ dòng 2)

    //                var dataRange = worksheet.Range(startRow, 1, 20, 14); // Phạm vi từ dòng 2 đến dòng cuối cùng

    //                // Tạo định dạng có điều kiện để tô màu xen kẽ cho các dòng
    //                var conditionalFormatting = dataRange.AddConditionalFormat();
    //                conditionalFormatting
    //                    .WhenIsTrue("=MOD(ROW(), 2) = 0") // Công thức để chọn dòng chẵn
    //                    .Fill.SetBackgroundColor(XLColor.LightGray); // Tô màu xám nhạt cho các dòng chẵn
    //                                                                 // Kẻ khung cho toàn bộ phạm vi
    //                dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin; // Đường viền ngoài
    //                dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;  // Đường viền bên trong
    //            }
    //            // Thiết lập dữ liệu bắt buộc cho cột "No." từ dòng 2 đến dòng cuối cùng
    //            int dataLastRow = worksheet.LastRowUsed().RowNumber(); // Lấy số dòng cuối cùng có dữ liệu

    //            var mandatoryColumnRange = worksheet.Range(startRow, 1, dataLastRow, 1); // Chỉ định phạm vi cột "No."

    //            // Áp dụng Data Validation để bắt buộc phải nhập dữ liệu cho cột "No."
    //            var validation = mandatoryColumnRange.CreateDataValidation();
    //            validation.IgnoreBlanks = false; // Không cho phép để trống
    //            validation.ErrorMessage = "This field is mandatory. Please enter a value.";
    //            validation.ShowErrorMessage = true; // Hiển thị thông báo lỗi nếu không nhập dữ liệu
    //            validation.ShowInputMessage = true;
    //            validation.InputMessage = "Please enter a value for this mandatory field.";
    //            validation.WholeNumber.EqualOrGreaterThan(1); // Xác định giá trị phải là số nguyên và lớn hơn hoặc bằng 1

    //            worksheet.Columns().AdjustToContents(); // Điều chỉnh độ rộng cột cho phù hợp với nội dung

    //            // Lưu file Excel
    //            workbook.SaveAs(filePath);
    //        }
    //        Console.WriteLine("File Excel đã được tạo thành công tại " + filePath);
    //        return true;
    //    }
    //}
}
