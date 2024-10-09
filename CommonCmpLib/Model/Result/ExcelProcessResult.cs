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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCmpLib
{
    public class ExcelProcessResult
    {
        public bool IsSuccess { get; internal set; }
        public string SheetName { get; internal set; }
        public int TotalRow { get; internal set; }
        public List<string> HeadersError { get; internal set; }
        public List<string> CellError { get; internal set; }
        public List< Dictionary<string, string> > Models { get; set; }
        public string Message { get; internal set; } = string.Empty;
    }
}
