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

namespace CommonCmpLib
{
    public class ConvertResult
    {
        /// <summary>
        /// Indicates whether the conversion was successful or not.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Contains a message regarding the conversion, e.g., error or success messages.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Stores the JSON data generated from the conversion, if the process was successful.
        /// </summary>
        public string JsonData { get; set; }

        /// <summary>
        /// Stores the XML data loaded from the file, if the process involves XML.
        /// </summary>
        public string XmlData { get; set; }

        /// <summary>
        /// Indicates the format of the file being processed.
        /// </summary>
        public string FileFormat { get; set; }
    }
}
