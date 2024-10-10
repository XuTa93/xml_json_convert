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
    public class ExlDCPModel
    {
        public string No { get; set; }

        public string MachineName { get; set; }

        public string PlanID { get; set; }

        public string PlanName { get; set; }

        public string Description { get; set; }

        public string StartEvent { get; set; }

        public string EndEvent { get; set; }

        public string TimeRequest { get; set; }

        public List<string> ParametersID { get; set; }
    }

}
