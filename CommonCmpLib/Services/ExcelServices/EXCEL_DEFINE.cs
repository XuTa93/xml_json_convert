using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCmpLib
{
    public enum ExcelSheetName
    {
        Parameter,
        Trace,
        Event,
        DataCollectionPlan
    }
    public static class DEFINE
    {
        // Parameter
        public const string No = "No.";
        public const string ParameterID = "ParameterID";
        public const string ParameterName = "ParameterName";
        public const string Locator = "Locator";
        public const string Unit = "Unit";
        public const string Type = "Type";
        public const string Array = "Array";
        public const string Function = "Function";
        public const string Arg = "Arg";
        public const string Sourcetype = "Sourcetype";
        public const string MemoryName = "MemoryName";
        public const string Offset = "Offset";
        public const string SourceType = "SourceType";
        public const string SourceArray = "SourceArray";
        public const string Description = "Description";

        // Trace
        public const string TraceID = "TraceID";
        public const string TraceName = "TraceName";
        public const string StartOn = "StartOn";
        public const string StopOn = "StopOn";

        //Event
        public const string EventID = "EventID";
        public const string EventName = "EventName";
        public const string AndOr = "AndOr";
        public const string Equation = "Equation";
        public const string Value = "Value";

        //DataCollectionPlan
        public const string MachineName = "MachineName";
        public const string PlanID = "PlanID";
        public const string PlanName = "PlanName";
        public const string StartEvent = "StartEvent";
        public const string EndEvent = "EndEvent";
        public const string TimeRequest = "TimeRequest";
    }
    public static class PARAMETER
    {   
        public const string SHEET_NAME = "Parameter";
        public const int ROW_HEADER = 1;
        public const string COLUNM_KEY = "B";
        public static readonly string[] MANDATORY_FIELDS = new string[] 
        { 
            DEFINE.No, DEFINE.ParameterID, DEFINE.ParameterName, DEFINE.Locator, DEFINE.Type, DEFINE.Array, DEFINE.Sourcetype, DEFINE.MemoryName, DEFINE.Offset, DEFINE.SourceType, DEFINE.SourceArray 
        };
        public static readonly Dictionary<string, string> COLUNMS = new Dictionary<string, string>
        {
            { "A1", DEFINE.No },
            { "B1", DEFINE.ParameterID },
            { "C1", DEFINE.ParameterName },
            { "D1", DEFINE.Locator },
            { "E1", DEFINE.Unit },
            { "F1", DEFINE.Type },
            { "G1", DEFINE.Array },
            { "H1", DEFINE.Function },
            { "I1", DEFINE.Arg },
            { "J1", DEFINE.Sourcetype },
            { "K1", DEFINE.MemoryName },
            { "L1", DEFINE.Offset },
            { "M1", DEFINE.SourceType },
            { "N1", DEFINE.SourceArray }
        };
    }

    public static class TRACE
    {
        public const string SHEET_NAME = "Trace";
        public const int ROW_HEADER = 1;
        public const string COLUNM_KEY = "G";
        public static readonly string[] MANDATORY_FIELDS = new string[] 
        {
            DEFINE.No, DEFINE.TraceID, DEFINE.TraceName, DEFINE.ParameterID 
        };
        public static readonly Dictionary<string, string> COLUNMS = new Dictionary<string, string>
        {
            { "A1", DEFINE.No },
            { "B1", DEFINE.TraceID },
            { "C1", DEFINE.TraceName },
            { "D1", DEFINE.Description },
            { "E1", DEFINE.StartOn },
            { "F1", DEFINE.StopOn },
            { "G1", DEFINE.ParameterID }
        };
    }
    public static class EVENT
    {
        public const string SHEET_NAME = "Event";
        public const int ROW_HEADER = 2;
        public const string COLUNM_KEY = "E";
        public static readonly string[] MANDATORY_FIELDS = new string[] { "No.", "EventID", "EventName", "AndOr", "ParameterID", "Equation", "Value"};
        public static readonly Dictionary<string, string> COLUNMS = new Dictionary<string, string>
        {
            { "A1", DEFINE.No },
            { "B1", DEFINE.EventID },
            { "C1", DEFINE.EventName },
            { "D2", DEFINE.AndOr },
            { "E2", DEFINE.ParameterID },
            { "F2", DEFINE.Equation },
            { "G2", DEFINE.Value },
        };
    }
    public static class DCP
    {
        public const string SHEET_NAME = "DataCollectionPlan";
        public const int ROW_HEADER = 2;
        public const string COLUNM_KEY = "I";
        public static readonly string[] MANDATORY_FIELDS = new string[] { "No.", "MachineName", "PlanID", "PlanName", "ParameterID" };
        public static readonly Dictionary<string, string> COLUNMS = new Dictionary<string, string>
        {
            { "A1", DEFINE.No },
            { "B1", DEFINE.MachineName },
            { "C1", DEFINE.PlanID },
            { "D1", DEFINE.PlanName },
            { "E1", DEFINE.Description },
            { "F1", DEFINE.StartEvent },
            { "G1", DEFINE.EndEvent },
            { "H1", DEFINE.TimeRequest },
            { "I2", DEFINE.ParameterID },
        };
    }

}
