using ClosedXML.Excel;
using CommonCmpLib.Model;
using System;
using System.Collections.Generic;

namespace CommonCmpLib.CreateTemplate
{
    public static class ParameterServices
    {
        public static List<ParameterModel> ReadParametersFromExcel(string filePath)
        {
            var parameters = new List<ParameterModel>();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                int row = 2; // Assuming the first row is the header

                while (!worksheet.Cell(row, 1).IsEmpty())
                {
                    var parameter = new ParameterModel
                    {
                        No = worksheet.Cell(row, 1).GetValue<int>(),
                        ParameterID = worksheet.Cell(row, 2).GetValue<string>(),
                        ParameterName = worksheet.Cell(row, 3).GetValue<string>(),
                        Locator = worksheet.Cell(row, 4).GetValue<string>(),
                        Unit = worksheet.Cell(row, 5).GetValue<string>(),
                        Type = worksheet.Cell(row, 6).GetValue<string>(),
                        Array = worksheet.Cell(row, 7).GetValue<string>(),  // Now a string
                        Function = worksheet.Cell(row, 8).GetValue<string>(),
                        Arg = worksheet.Cell(row, 9).GetValue<string>(),
                        SourceType = worksheet.Cell(row, 10).GetValue<string>(),
                        MemoryName = worksheet.Cell(row, 11).GetValue<string>(),
                        Offset = worksheet.Cell(row, 12).GetValue<string>(),
                        SourceArray = worksheet.Cell(row, 13).GetValue<string>()  // Now a string
                    };

                    parameters.Add(parameter);
                    row++;
                }
            }

            return parameters;
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
