using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCmpLib
{
    public class ExcelProcessResult<T>
    {
        public bool IsSuccess { get; internal set; }
        public int TotalRow { get; internal set; }
        public List<string> HeadersError { get; internal set; }
        public List<string> CellError { get; internal set; }
        public List<T> Models { get; set; }

        // Constructor
        public ExcelProcessResult()
        {
            HeadersError = new List<string>();
            CellError = new List<string>();
            Models = new List<T>();
        }
    }
}
