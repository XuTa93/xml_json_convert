using ClosedXML.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace CommonCmpLib
{
    public static class ParameterServices
    {
        private const string A1 = "No.";
        private const string B1 = "ParameterID";
        private const string C1 = "ParameterName";
        private const string D1 = "Locator";
        private const string E1 = "Unit";
        private const string F1 = "Type";
        private const string G1 = "Array";
        private const string H1 = "Function";
        private const string I1 = "Arg";
        private const string J1 = "Sourcetype";
        private const string K1 = "MemoryName";
        private const string L1 = "Offset";
        private const string M1 = "SourceType";
        private const string N1 = "SourceArray";
        private const int HEADER_ROW = 1;
        private const int START_ROW = HEADER_ROW + 1;
        static readonly List<string> LIST_COLUMN = new List<string> {"", A1, B1, C1, D1, E1, F1, G1, H1, I1, J1, K1, L1, M1, N1 };

        public static ExcelProcessResult<ExlParameterModel> ReadParametersFromExcel(string x_strfilePath)
        {
            ExcelProcessResult<ExlParameterModel> objParProcess;
            List<ExlParameterModel> lstParameter;
            ExlParameterModel objParameter;
            List<string> lstHeaderErr;
            List<string> lstCellErr;
            IXLWorksheet objWorksheet;
            bool bIsSuccess = true;
            int uRow = START_ROW; // Assuming the first row is the header
            lstHeaderErr = new List<string>();
            lstCellErr = new List<string>();
            lstParameter = new List<ExlParameterModel>();
            using (FileStream objStream = new FileStream(x_strfilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (XLWorkbook workbook = new XLWorkbook(objStream))
                {
                    objWorksheet = workbook.Worksheet(ExcelSheetName.Parameter.ToString());

                    // Check Header Error
                    for (int i = 1; i < LIST_COLUMN.Count; i++)
                    {
                        string strHeaderName = objWorksheet.Cell(HEADER_ROW, i).GetValue<string>();
                        if (strHeaderName != LIST_COLUMN[i])
                        {
                            lstHeaderErr.Add($"{objWorksheet.Cell(HEADER_ROW, i).Address} : {strHeaderName} != {LIST_COLUMN[i]}");
                            bIsSuccess = false;
                        };
                    }

                    while (!objWorksheet.Cell(uRow, 1).IsEmpty())
                    {
                        List<string> lstRowErr = new List<string>();
                        lstRowErr.Add($"Row {uRow} :");
                        objParameter = new ExlParameterModel();
                        objParameter.No = objWorksheet.Cell(uRow, 1).GetValue<string>();
                        objParameter.ParameterID = objWorksheet.Cell(uRow, 2).GetValue<string>();
                        if (string.IsNullOrEmpty(objParameter.ParameterID))
                        {
                            lstRowErr.Add("ParameterID");
                        }
                        objParameter.ParameterName = objWorksheet.Cell(uRow, 3).GetValue<string>();
                        if (string.IsNullOrEmpty(objParameter.ParameterName))
                        {
                            lstRowErr.Add ("ParameterName");
                        }
                        objParameter.Locator = objWorksheet.Cell(uRow, 4).GetValue<string>();
                        if (string.IsNullOrEmpty(objParameter.Locator))
                        {
                            lstRowErr.Add("Locator");
                        }
                        objParameter.Unit = objWorksheet.Cell(uRow, 5).GetValue<string>();
                        objParameter.Type = objWorksheet.Cell(uRow, 6).GetValue<string>();
                        if (string.IsNullOrEmpty(objParameter.Type))
                        {
                            lstRowErr.Add("Type");
                        }
                        objParameter.Array = objWorksheet.Cell(uRow, 7).GetValue<string>();
                        if (string.IsNullOrEmpty(objParameter.Array))
                        {
                            lstRowErr.Add("Array");
                        }
                        objParameter.Function = objWorksheet.Cell(uRow, 8).GetValue<string>();
                        objParameter.Arg = objWorksheet.Cell(uRow, 9).GetValue<string>();
                        objParameter.Sourcetype = objWorksheet.Cell(uRow, 10).GetValue<string>();
                        if (string.IsNullOrEmpty(objParameter.Sourcetype))
                        {
                            lstRowErr.Add("Sourcetype");
                        }              
                        objParameter.MemoryName = objWorksheet.Cell(uRow, 11).GetValue<string>();
                        if (string.IsNullOrEmpty(objParameter.MemoryName))
                        {
                            lstRowErr.Add("MemoryName");
                        }
                        objParameter.Offset = objWorksheet.Cell(uRow, 12).GetValue<string>();
                        if (string.IsNullOrEmpty(objParameter.Offset))
                        {
                            lstRowErr.Add("Offset");
                        }
                        objParameter.SourceType = objWorksheet.Cell(uRow, 13).GetValue<string>();
                        if (string.IsNullOrEmpty(objParameter.SourceType))
                        {
                            lstRowErr.Add("SourceType");
                        }
                        objParameter.SourceArray = objWorksheet.Cell(uRow, 14).GetValue<string>();
                        if (string.IsNullOrEmpty(objParameter.SourceArray))
                        {
                            lstRowErr.Add("SourceArray");
                        }

                        if (lstRowErr.Count > 1)
                        {
                            lstCellErr.Add(JsonConvert.SerializeObject(lstRowErr));
                            bIsSuccess = false;
                        }

                        lstParameter.Add(objParameter);
                        uRow++;
                    }

                    objParProcess = new ExcelProcessResult<ExlParameterModel>();
                    objParProcess.Models = lstParameter;
                    objParProcess.TotalRow = uRow - START_ROW;
                    objParProcess.IsSuccess = bIsSuccess;
                    objParProcess.CellError = lstCellErr;
                    objParProcess.HeadersError = lstHeaderErr;
                }
            }
            return objParProcess;
        }

        public static bool ExportParameterToXml(List<ExlParameterModel> x_lstParaModel, string x_FolderPath)
        {

            return true;
        }
        public static bool CreatePrameterFile(string filePath)
        {
            // Đường dẫn để lưu file Excel
            filePath = "ParameterData.xlsx";

            // Tạo workbook mới
            using (var workbook = new XLWorkbook())
            {
                // Tạo worksheet mới
                var worksheet = workbook.Worksheets.Add("Parameters");

                // Thêm tiêu đề cho các cột
                worksheet.Cell(1, 1).Value = "No.";
                worksheet.Cell(1, 2).Value = "ParameterID";
                worksheet.Cell(1, 3).Value = "ParameterName";
                worksheet.Cell(1, 4).Value = "Locator";
                worksheet.Cell(1, 5).Value = "Unit";
                worksheet.Cell(1, 6).Value = "Type";
                worksheet.Cell(1, 7).Value = "Array";
                worksheet.Cell(1, 8).Value = "Function";
                worksheet.Cell(1, 9).Value = "Arg";
                worksheet.Cell(1, 10).Value = "Sourcetype";
                worksheet.Cell(1, 11).Value = "MemoryName";
                worksheet.Cell(1, 12).Value = "Offset";
                worksheet.Cell(1, 13).Value = "SourceType";
                worksheet.Cell(1, 14).Value = "SourceArray";

                // Tô màu cho hàng tiêu đề
                var headerRange = worksheet.Range("A1:N1"); // Phạm vi từ cột A đến cột N của hàng 1
                headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
                headerRange.Style.Font.Bold = true; // Làm đậm chữ trong header

                // Thêm dữ liệu vào các dòng tiếp theo
                string[,] data = new string[,]
                {
                { "1", "MD1033", "LLA_CassetStatus", "Master", "I2", "I2", "1", "", "", "CommonMemory", "MASTER", "0", "I2", "1" },
                { "2", "MD1083", "LLB_CassetStatus", "Master", "I2", "I2", "1", "", "", "CommonMemory", "MASTER", "100", "I2", "1" },
                { "3", "MD1148", "Tr_Press", "Master", "Pa", "F4", "1", "1001", "", "CommonMemory", "MASTER", "230", "I4", "1" }
                };

                int startRow = 2;
                // Duyệt qua các dòng dữ liệu và thêm vào worksheet
                for (int i = 0; i < data.GetLength(0); i++)
                {
                    for (int j = 0; j < data.GetLength(1); j++)
                    {
                        worksheet.Cell(startRow + i, j + 1).Value = data[i, j]; // Ghi giá trị từ mảng vào worksheet
                    }
                }

                // Kiểm tra xem có dòng nào đã được sử dụng hay không
                if (worksheet.LastRowUsed() != null)
                {
                    // Thiết lập phạm vi cho tất cả các dòng dữ liệu (bắt đầu từ dòng 2)

                    var dataRange = worksheet.Range(startRow, 1, 20, 14); // Phạm vi từ dòng 2 đến dòng cuối cùng

                    // Tạo định dạng có điều kiện để tô màu xen kẽ cho các dòng
                    var conditionalFormatting = dataRange.AddConditionalFormat();
                    conditionalFormatting
                        .WhenIsTrue("=MOD(ROW(), 2) = 0") // Công thức để chọn dòng chẵn
                        .Fill.SetBackgroundColor(XLColor.LightGray); // Tô màu xám nhạt cho các dòng chẵn
                                                                     // Kẻ khung cho toàn bộ phạm vi
                    dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin; // Đường viền ngoài
                    dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;  // Đường viền bên trong
                }
                // Thiết lập dữ liệu bắt buộc cho cột "No." từ dòng 2 đến dòng cuối cùng
                int dataLastRow = worksheet.LastRowUsed().RowNumber(); // Lấy số dòng cuối cùng có dữ liệu

                var mandatoryColumnRange = worksheet.Range(startRow, 1, dataLastRow, 1); // Chỉ định phạm vi cột "No."

                // Áp dụng Data Validation để bắt buộc phải nhập dữ liệu cho cột "No."
                var validation = mandatoryColumnRange.CreateDataValidation();
                validation.IgnoreBlanks = false; // Không cho phép để trống
                validation.ErrorMessage = "This field is mandatory. Please enter a value.";
                validation.ShowErrorMessage = true; // Hiển thị thông báo lỗi nếu không nhập dữ liệu
                validation.ShowInputMessage = true;
                validation.InputMessage = "Please enter a value for this mandatory field.";
                validation.WholeNumber.EqualOrGreaterThan(1); // Xác định giá trị phải là số nguyên và lớn hơn hoặc bằng 1

                worksheet.Columns().AdjustToContents(); // Điều chỉnh độ rộng cột cho phù hợp với nội dung

                // Lưu file Excel
                workbook.SaveAs(filePath);
            }
            Console.WriteLine("File Excel đã được tạo thành công tại " + filePath);
            return true;
        }
    }
}
