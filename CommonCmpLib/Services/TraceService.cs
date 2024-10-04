using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CommonCmpLib
{
    public static class TraceService
    {
        public static void TraceServiceMain(string strFilePath)
        {
            List<ExlTraceRequestModel> traceList = new List<ExlTraceRequestModel>();
            // Mở tệp Excel với FileStream chỉ đọc
            using (FileStream fs = new FileStream(strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var workbook = new XLWorkbook(fs))
                {
                    var worksheet = workbook.Worksheet(2);  // Lấy sheet đầu tiên
                    int nRow = 2;  // Bắt đầu đọc từ hàng thứ 2, vì hàng đầu tiên là tiêu đề

                    while (!worksheet.Cell(nRow, 7).IsEmpty())  // Đọc cột No (cột 1)
                    {
                        var traceData = new ExlTraceRequestModel
                        {
                            No = worksheet.Cell(nRow, 1).GetValue<string>(),  // Đọc giá trị từ cột No
                            TraceID = worksheet.Cell(nRow, 2).GetValue<string>(),
                            TraceName = worksheet.Cell(nRow, 3).GetValue<string>(),
                            Description = worksheet.Cell(nRow, 4).GetValue<string>(),
                            StartOn = worksheet.Cell(nRow, 5).GetValue<string>(),
                            StopOn = worksheet.Cell(nRow, 6).GetValue<string>(),
                            ParameterID = worksheet.Cell(nRow, 7).GetValue<string>(),
                        };

                        traceList.Add(traceData);
                        nRow++;
                    }
                }
            }

            for (int i = 1; i < traceList.Count; i++)
            {
                if (string.IsNullOrEmpty(traceList[i].No))
                {
                    traceList[i].No = traceList[i - 1].No;
                    traceList[i].TraceID = traceList[i - 1].TraceID;
                    traceList[i].TraceName = traceList[i - 1].TraceName;
                    traceList[i].Description = traceList[i - 1].Description;
                    //traceList[i].StartOn = traceList[i - 1].StartOn;
                    //traceList[i].StopOn = traceList[i - 1].StopOn;
                }
            }
            var groupedTraces = traceList
                .GroupBy(t => t.TraceID)
                .Select(group => group.ToList()) // Chỉ lấy danh sách các đối tượng trong nhóm
                .ToList();
            foreach (var item in groupedTraces)
            {
                
            }

            foreach (var trace in traceList)
            {
                Console.WriteLine($"No: {trace.No}, TraceID: {trace.TraceID}, TraceName: {trace.TraceName}, Description: {trace.Description}, Parameter ID {trace.ParameterID}");
            }
        }
    }
}
