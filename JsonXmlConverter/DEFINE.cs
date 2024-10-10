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

using System.Collections.Generic;

namespace JsonXmlConveter
{
    /// <summary>
    /// Parameter, TraceRequest, EventTrigger, EventRequest, DataCollectionPlan
    /// </summary>
    public enum DataType
    {
        Parameter,
        TraceRequest,
        EventTrigger,
        EventRequest,
        DataCollectionPlan,
        Unknown
    }
    public enum ExcelSheetName
    {
        Parameter,
        TraceRequest,
        Event,
        DataCollectionPlan
    }
    public static class DEFINE
    {
        //Common
        public const string NS1_PROPERTY = "xmlns:ns1";
        public const string NS1_NAMESPACE = "urn:ConfigFileSchema";

        public const string NS1_PARAMETERS = "ns1:parameters";
        public const string NS1_TRACEREQUESTS = "ns1:tracerequests";
        public const string NS1_EVENTTRIGGERS = "ns1:eventtriggers";
        public const string NS1_EVENTREQUESTS = "ns1:eventrequests";
        public const string DCPTABLES = "DCPTables";

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
        public const string SHEET_NAME_TRIGGER = "EventTrigger";
        public const string SHEET_NAME_REQUEST = "EventRequest";
        public const int ROW_HEADER = 2;
        public const string COLUNM_KEY = "E";
        public static readonly string[] MANDATORY_FIELDS = new string[] { "No.", "EventID", "EventName", "AndOr", "ParameterID", "Equation", "Value" };
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
