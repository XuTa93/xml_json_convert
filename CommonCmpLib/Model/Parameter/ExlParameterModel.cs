﻿using System.Collections.Generic;

namespace CommonCmpLib
{
    public class ExlParameterModel
    {
        // Mandatory fields
        public string No { get; set; }
        public string ParameterID { get; set; }
        public string ParameterName { get; set; }
        public string Locator { get; set; }
        public string Type { get; set; }
        public string Array { get; set; }
        public string Sourcetype { get; set; }
        public string MemoryName { get; set; }
        public string Offset { get; set; }
        public string SourceType { get; set; }
        public string SourceArray { get; set; }

        // Optional fields
        public string Unit { get; set; }
        public string Function { get; set; }
        public string Arg { get; set; }
    }
}
