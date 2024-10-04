using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCmpLib
{
    public class JsonXmlConversionResult
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
