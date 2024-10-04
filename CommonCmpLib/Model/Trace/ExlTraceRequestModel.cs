using System.Collections.Generic;

namespace CommonCmpLib
{
    public class ExlTraceRequestModel
    {
        // Mandatory fields
        public string No { get; set; }
        public string TraceID { get; set; }
        public string TraceName { get; set; }
        public string ParameterID { get; set; }

        // Optional fields
        public string Description { get; set; }
        public string StartOn { get; set; }
        public string StopOn { get; set; }

    }

}
