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

namespace JsonXmlConveter
{
    public class ExlTraceRequestModel
    {
        // Mandatory fields
        public string No { get; set; }
        public string TraceID { get; set; }
        public string TraceName { get; set; }
        public List<string> ParametersID { get; set; }

        // Optional fields
        public string Description { get; set; }
        public string StartOn { get; set; }
        public string StopOn { get; set; }
    }

}
