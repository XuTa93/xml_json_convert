using JsonXmlConveter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JsonXmlConverter
{
    public static class XmlServices
    {
        /// <summary>
        /// Parses the XML file and creates a list of ExlTraceRequestModel objects.
        /// </summary>
        /// <returns>A string indicating an error message if an error occurs; otherwise, returns string.Empty.</returns>
        public static string ParseTraceXmlToList(string strXmlPath, out List<ExlTraceRequestModel> x_lstTrace)
        {
            x_lstTrace = new List<ExlTraceRequestModel>();
            try
            {
                // Load the XML document from the provided path
                XDocument objXmlDoc = XDocument.Load(strXmlPath);

                // Parse the XML and create a list of ExlTraceRequestModel objects
                x_lstTrace = objXmlDoc.Descendants()
                    .Where(obj => obj.Name.LocalName.ToLower() == "tracerequest")
                    .Select(objTrace => new ExlTraceRequestModel
                    {
                        No = objTrace.Attribute("no")?.Value, // Assuming 'No' is an attribute
                        TraceID = objTrace.Attribute("traceid")?.Value,
                        TraceName = objTrace.Attribute("tracename")?.Value,
                        Description = objTrace.Attribute("description")?.Value,
                        StartOn = objTrace.Descendants()
                            .FirstOrDefault(trigger => trigger.Name.LocalName.ToLower() == "starton")?.Value,
                        StopOn = objTrace.Descendants()
                            .FirstOrDefault(trigger => trigger.Name.LocalName.ToLower() == "stopon")?.Value,
                        ParametersID = objTrace.Descendants()
                            .Where(p => p.Name.LocalName.ToLower() == "parameter")
                            .Select(parameter => parameter.Attribute("paramid")?.Value)
                            .ToList()
                    }).ToList();

                return string.Empty; // Return string.Empty if parsing is successful
            }
            catch (Exception objEx) // Catch all exceptions
            {
                StackTrace objStackTrace = new StackTrace();
                string strMethodName = objStackTrace.GetFrame(0).GetMethod().Name;
                return JsonXmlConveter.Converter.HandleException(objEx, strMethodName); // Handle the exception and return the error message
            }
        }
        public static ConvertResult XmlToJson_TraceRequest(string x_strXmlPath, string x_strJsonPath, DataType x_dataType)
        {
            ConvertResult objResult;
            List<ExlTraceRequestModel> lstTrace;
            string strJsonResult;
            string srtParseResult;
            objResult = new ConvertResult();
            srtParseResult = ParseTraceXmlToList(x_strXmlPath, out lstTrace);

            // Validate the input file to check if it is of type Parameter
            if (x_dataType != DataType.TraceRequest)
            {
                objResult.IsSuccess = false;
                objResult.Message = $"{x_dataType} type of the provided file.";
                return objResult;
            }

            if (string.IsNullOrEmpty(srtParseResult) == true)
            {
                strJsonResult = JsonServices.WriteTraceRequestToJson(lstTrace, x_strJsonPath);
                if (string.IsNullOrEmpty(srtParseResult) == true)
                {
                    objResult.IsSuccess = true;
                }
                else
                {
                    objResult.IsSuccess = false;
                    objResult.Message = strJsonResult;
                }
            }
            else
            {
                objResult.IsSuccess = false;
                objResult.Message = srtParseResult;
            }
            return objResult;
        }

        /// <summary>
        /// Parses the XML file and creates a list of ExlEventModel objects based on the specified name.
        /// </summary>
        /// <returns>A string indicating an error message if an error occurs; otherwise, returns string.Empty.</returns>
        public static string ParseEventXmlToList(string strXmlPath, string x_strName, out List<ExlEventModel> lstEvents)
        {
            lstEvents = new List<ExlEventModel>(); // Initialize the output list

            try
            {
                // Load the XML document from the provided path
                XDocument objXmlDoc = XDocument.Load(strXmlPath);

                // Query to select event elements based on the specified name
                lstEvents = objXmlDoc.Descendants()
                    .Where(obj => obj.Name.LocalName.ToLower() == x_strName)
                    .Select(objEvent => new ExlEventModel
                    {
                        EventID = objEvent.Attribute("eventid")?.Value,
                        EventName = objEvent.Attribute("eventname")?.Value,
                        AndOr = objEvent.Descendants("detectconditions").FirstOrDefault()?.Attribute("andor")?.Value,
                        Equation = objEvent.Descendants("parameter").FirstOrDefault()?.Attribute("equation")?.Value,
                        Value = objEvent.Descendants("parameter").FirstOrDefault()?.Attribute("value")?.Value,
                        ParametersID = objEvent.Descendants()
                            .Where(p => p.Name.LocalName.ToLower() == "parameter")
                            .Select(parameter => parameter.Attribute("paramid")?.Value)
                            .ToList()
                    }).ToList();

                return string.Empty; // Return string.Empty if parsing is successful
            }
            catch (Exception objEx) // Catch all exceptions
            {
                StackTrace objStackTrace = new StackTrace();
                string strMethodName = objStackTrace.GetFrame(0).GetMethod().Name;
                return Converter.HandleException(objEx, strMethodName); // Handle the exception and return the error message
            }
        }

        public static ConvertResult XmlToJson_EventTrigger(string x_strXmlPath, string x_strJsonPath, DataType x_dataType)
        {
            ConvertResult objResult;
            List<ExlEventModel> lstEvt;
            string strJsonResult;
            string srtParseResult;

            objResult = new ConvertResult();
            srtParseResult = ParseEventXmlToList(x_strXmlPath, "EventTrigger.xml", out lstEvt);

            // Validate the input file to check if it is of type Parameter
            if (x_dataType != DataType.EventTrigger)
            {
                objResult.IsSuccess = false;
                objResult.Message = $"{x_dataType} type of the provided file.";
                return objResult;
            }

            if (string.IsNullOrEmpty(srtParseResult) == true)
            {
                strJsonResult = JsonServices.WriteEvenTriggerToJson(lstEvt, x_strJsonPath);
                if (string.IsNullOrEmpty(srtParseResult) == true)
                {
                    objResult.IsSuccess = true;
                }
                else
                {
                    objResult.IsSuccess = false;
                    objResult.Message = strJsonResult;
                }
            }
            else
            {
                objResult.IsSuccess = false;
                objResult.Message = srtParseResult;
            }
            return objResult;
        }

        public static ConvertResult XmlToJson_EventRequest(string x_strXmlPath, string x_strJsonPath, DataType x_dataType)
        {
            ConvertResult objResult;
            List<ExlEventModel> lstEvt;
            string strJsonResult;
            string srtParseResult;

            objResult = new ConvertResult();
            srtParseResult = ParseEventXmlToList(x_strXmlPath, "EventRequest.xml", out lstEvt);

            // Validate the input file to check if it is of type Parameter
            if (x_dataType != DataType.EventRequest)
            {
                objResult.IsSuccess = false;
                objResult.Message = $"{x_dataType} type of the provided file.";
                return objResult;
            }

            if (string.IsNullOrEmpty(srtParseResult) == true)
            {
                strJsonResult = JsonServices.WriteEventRequestToJson(lstEvt, x_strJsonPath);
                if (string.IsNullOrEmpty(srtParseResult) == true)
                {
                    objResult.IsSuccess = true;
                }
                else
                {
                    objResult.IsSuccess = false;
                    objResult.Message = strJsonResult;
                }
            }
            else
            {
                objResult.IsSuccess = false;
                objResult.Message = srtParseResult;
            }
            return objResult;
        }

        /// <summary>
        /// Parses the XML file and creates an ExlDCPModel object based on the contents of the file.
        /// </summary>
        /// <returns>An ExlDCPModel object populated with data from the XML file, or null if an error occurs.</returns>
        public static string ParseDCPXmlToModel(string strXmlPath, out ExlDCPModel dcpModel)
        {
            dcpModel = new ExlDCPModel(); // Initialize the output model
            string strNamespace = "";

            try
            {
                // Load the XML document using XDocument
                XDocument objXmlDoc = XDocument.Load(strXmlPath);

                // Find the DCPTable element in the XML document
                var dcpTableElement = objXmlDoc.Root.Element(strNamespace + "DCPTable");

                if (dcpTableElement != null)
                {
                    // Get the MachineName value from the Macname element
                    dcpModel.MachineName = dcpTableElement.Element(strNamespace + "Macname")?.Value;

                    // Find the DataCollectionPlan element
                    var dataCollectionPlanElement = dcpTableElement.Element(strNamespace + "DataCollectionPlan");

                    if (dataCollectionPlanElement != null)
                    {
                        // Get attributes from the DataCollectionPlan element
                        dcpModel.PlanID = dataCollectionPlanElement.Attribute("id")?.Value;
                        dcpModel.PlanName = dataCollectionPlanElement.Attribute("name")?.Value;
                        dcpModel.Description = dataCollectionPlanElement.Attribute("Description")?.Value;

                        // Retrieve the StartEvent -> EventRequest -> eventId value
                        dcpModel.StartEvent = dataCollectionPlanElement
                            .Element(strNamespace + "StartEvent")?
                            .Element(strNamespace + "EventRequests")?
                            .Element(strNamespace + "EventRequest")?
                            .Attribute("eventId")?.Value;

                        // Retrieve the EndEvent -> EventRequest -> eventId value
                        dcpModel.EndEvent = dataCollectionPlanElement
                            .Element(strNamespace + "EndEvent")?
                            .Element(strNamespace + "EventRequests")?
                            .Element(strNamespace + "EventRequest")?
                            .Attribute("eventId")?.Value;

                        // Retrieve the list of ParametersID from Contents -> ParameterRequests
                        dcpModel.ParametersID = dataCollectionPlanElement
                            .Element(strNamespace + "Contents")?
                            .Element(strNamespace + "ParameterRequests")?
                            .Elements(strNamespace + "ParameterRequest")
                            .Select(param => param.Attribute("parameterName")?.Value)
                            .Where(param => param != null)
                            .ToList();
                    }
                }

                return string.Empty; // Return string.Empty if parsing is successful
            }
            catch (Exception objEx) // Catch all exceptions
            {
                StackTrace objStackTrace = new StackTrace();
                string strMethodName = objStackTrace.GetFrame(0).GetMethod().Name;
                return Converter.HandleException(objEx, strMethodName); // Handle the exception and return the error message
            }
        }
    }
}
