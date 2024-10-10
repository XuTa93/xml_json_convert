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

using System.Collections.Generic;

namespace ExcelToXmlList
{
    public class ExlEventModel
    {
        public string No { get; set; }
        public string EventID { get; set; }
        public string EventName { get; set; }
        public string AndOr { get; set; }    
        public string Equation { get; set; }
        public string Value { get; set; }

        public List<string> ParametersID { get; set; }
    }
}
