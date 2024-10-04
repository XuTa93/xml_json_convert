namespace CommonCmpLib.Model
{
    public class ExlTraceRequestModel
    {
        // Mandatory fields
        public string TraceID { get; set; } // Corresponds to 'traceid'
        public string TraceName { get; set; } // Corresponds to 'tracename'
        public string ParameterID { get; set; } // Corresponds to 'paramid'

        // Optional fields
        public string Description { get; set; } // Corresponds to 'description'
        public string StartOn { get; set; } // Corresponds to 'starton'
        public string StopOn { get; set; } // Corresponds to 'stopon'
    }


}
