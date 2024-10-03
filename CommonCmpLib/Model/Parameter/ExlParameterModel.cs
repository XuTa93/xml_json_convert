using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCmpLib
{
    public class ExlParameterModel
    {
        public string No { get; set; } 
        public string ParameterID { get; set; }
        public string ParameterName { get; set; }
        public string Locator { get; set; }
        public string Unit { get; set; }
        public string Type { get; set; }
        public string Array { get; set; } 
        public string Function { get; set; }
        public string Arg { get; set; }
        public string Sourcetype { get; set; }
        public string MemoryName { get; set; }
        public string Offset { get; set; }
        public string SourceType { get; set; }
        public string SourceArray { get; set; }
    }

    public class ExlParameterProcess
    {
        public bool IsSuccess { get;internal set; }
        public int TotalRow { get; internal set; }
        public List<string> HeadersError { get; internal set; }
        public List<string> CellError { get; internal set; }
        public List<ExlParameterModel> ParameterModels { get; set; }
    }
}
