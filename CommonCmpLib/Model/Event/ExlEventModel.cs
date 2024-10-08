using System.Collections.Generic;

namespace CommonCmpLib
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
