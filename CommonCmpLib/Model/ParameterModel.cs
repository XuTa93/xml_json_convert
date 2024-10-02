using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCmpLib.Model
{
    public class ParameterModel
    {
        public int No { get; set; } // This is still an integer
        public string ParameterID { get; set; }
        public string ParameterName { get; set; }
        public string Locator { get; set; }
        public string Unit { get; set; }
        public string Type { get; set; }
        public string Array { get; set; } // Changed to string
        public string Function { get; set; }
        public string Arg { get; set; }
        public string SourceType { get; set; }
        public string MemoryName { get; set; }
        public string Offset { get; set; }
        public string SourceArray { get; set; }
    }
}
