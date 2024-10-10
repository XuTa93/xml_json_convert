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

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace JsonXmlConveter
{
    public static class Convert
    {
        /// <summary>
        /// Write TraceRequest Models To Json Using Json Writer
        /// </summary>
        public static string WriteTraceRequestToJson(List<ExlTraceRequestModel> x_objTraces, string x_strJsonPath)
        {
            try
            {
                // Create a JsonTextWriter to write to the specified JSON file
                using (StreamWriter streamWriter = new StreamWriter(x_strJsonPath))
                using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
                {
                    jsonWriter.Formatting = Newtonsoft.Json.Formatting.Indented; // Set formatting for readability

                    // Start writing the JSON structure
                    jsonWriter.WriteStartObject(); // Start root object

                    jsonWriter.WritePropertyName(DEFINE.NS1_TRACEREQUESTS);
                    jsonWriter.WriteStartObject(); // Start the tracerequests object

                    jsonWriter.WritePropertyName("@" + DEFINE.NS1_PROPERTY);
                    jsonWriter.WriteValue(DEFINE.NS1_NAMESPACE); // Add the namespace

                    jsonWriter.WritePropertyName("tracerequest");
                    jsonWriter.WriteStartArray(); // Start the array of tracerequests

                    foreach (var request in x_objTraces)
                    {
                        jsonWriter.WriteStartObject(); // Start each tracerequest object

                        // Write mandatory fields with "@" prefix
                        jsonWriter.WritePropertyName("@traceid");
                        jsonWriter.WriteValue(request.TraceID ?? string.Empty);

                        jsonWriter.WritePropertyName("@tracename");
                        jsonWriter.WriteValue(request.TraceName ?? string.Empty);

                        jsonWriter.WritePropertyName("@description");
                        jsonWriter.WriteValue(request.Description ?? string.Empty);

                        // Write triggers
                        jsonWriter.WritePropertyName("triggers");
                        jsonWriter.WriteStartObject(); // Start triggers object

                        jsonWriter.WritePropertyName("starton");
                        jsonWriter.WriteValue(request.StartOn ?? (object)null); // Allow null

                        jsonWriter.WritePropertyName("stopon");
                        jsonWriter.WriteValue(request.StopOn ?? (object)null); // Allow null

                        jsonWriter.WriteEndObject(); // End triggers object

                        // Write parameters
                        jsonWriter.WritePropertyName("parameters");
                        jsonWriter.WriteStartObject(); // Start parameters object

                        jsonWriter.WritePropertyName("parameter");
                        jsonWriter.WriteStartArray(); // Start the array of parameters

                        foreach (var paramId in request.ParametersID)
                        {
                            jsonWriter.WriteStartObject(); // Start each parameter object
                            jsonWriter.WritePropertyName("@paramid");
                            jsonWriter.WriteValue(paramId ?? string.Empty); // Use empty string if null
                            jsonWriter.WriteEndObject(); // End parameter object
                        }

                        jsonWriter.WriteEndArray(); // End the array of parameters
                        jsonWriter.WriteEndObject(); // End parameters object

                        jsonWriter.WriteEndObject(); // End each tracerequest object
                    }

                    jsonWriter.WriteEndArray(); // End the array of tracerequests
                    jsonWriter.WriteEndObject(); // End the tracerequests object
                    jsonWriter.WriteEndObject(); // End root object
                }

                return string.Empty; // Return empty string if successful
            }
            catch (Exception objEx) // Catch all exceptions
            {
                StackTrace objStackTrace = new StackTrace();
                string strMethodName = objStackTrace.GetFrame(0).GetMethod().Name;
                return HandleException(objEx, strMethodName); // Handle the exception and return the error message
            }

        }

        /// <summary>
        /// Write EventRequest Models To Json Using Json Writer
        /// </summary>
        public static string WriteEventRequestToJson(List<ExlEventModel> x_lstEventRequest, string x_strJsonPath)
        {
            try
            {
                // Create StreamWriter to write data to file
                using (StreamWriter strWriter = new StreamWriter(x_strJsonPath))
                {
                    // Create JsonWriter to write data in JSON format
                    using (JsonWriter jsonWriter = new JsonTextWriter(strWriter))
                    {
                        jsonWriter.Formatting = Newtonsoft.Json.Formatting.Indented; // Format JSON for readability

                        // Start writing JSON
                        jsonWriter.WriteStartObject(); // Open the main object

                        jsonWriter.WritePropertyName(DEFINE.NS1_TRACEREQUESTS);
                        jsonWriter.WriteStartObject(); // Start object

                        jsonWriter.WritePropertyName("@" + DEFINE.NS1_PROPERTY);
                        jsonWriter.WriteValue(DEFINE.NS1_NAMESPACE); // Add the namespace

                        jsonWriter.WritePropertyName("eventrequest"); // Name of the main property (with @)
                        jsonWriter.WriteStartArray(); // Open array of eventrequests

                        // Iterate through the list of events
                        foreach (ExlEventModel objEventRequest in x_lstEventRequest)
                        {
                            jsonWriter.WriteStartObject(); // Open object for each eventrequest

                            // Write the eventid and eventname attributes with @ symbol
                            jsonWriter.WritePropertyName("@eventid");
                            jsonWriter.WriteValue(objEventRequest.EventID);

                            jsonWriter.WritePropertyName("@eventname");
                            jsonWriter.WriteValue(objEventRequest.EventName);

                            // Write the parameters list (array)
                            jsonWriter.WritePropertyName("parameters");
                            jsonWriter.WriteStartObject(); // Open parameters object

                            jsonWriter.WritePropertyName("parameter"); // Name of the parameter array
                            jsonWriter.WriteStartArray(); // Start array of parameters

                            foreach (string strParameterID in objEventRequest.ParametersID)
                            {
                                jsonWriter.WriteStartObject(); // Open object for each parameter
                                jsonWriter.WritePropertyName("@paramid");
                                jsonWriter.WriteValue(strParameterID);
                                jsonWriter.WriteEndObject(); // Close the object for parameter
                            }

                            jsonWriter.WriteEndArray(); // Close parameter array
                            jsonWriter.WriteEndObject(); // Close parameters object
                            jsonWriter.WriteEndObject(); // Close eventrequest object
                        }

                        jsonWriter.WriteEndArray(); // Close eventrequests array
                        jsonWriter.WriteEndObject(); // Close main object

                        jsonWriter.WriteEndObject(); // Close root object
                    }
                }

                return string.Empty; // Return empty string if successful
            }
            catch (Exception objEx) // Catch all exceptions
            {
                StackTrace objStackTrace = new StackTrace();
                string strMethodName = objStackTrace.GetFrame(0).GetMethod().Name;
                return HandleException(objEx, strMethodName); // Handle the exception and return the error message
            }
        }

        /// <summary>
        /// Write EvenTrigger Models To Json Using Json Writer
        /// </summary>
        public static string WriteEvenTriggerToJson(List<ExlEventModel> lstEventModels, string x_strJsonPath)
        {
            try
            {
                using (StreamWriter file = File.CreateText(x_strJsonPath))
                {
                    using (JsonWriter jsonWriter = new JsonTextWriter(file))
                    {
                        // Set formatting options for pretty printing
                        jsonWriter.Formatting = Newtonsoft.Json.Formatting.Indented;

                        // Start the main JSON object
                        jsonWriter.WriteStartObject();

                        jsonWriter.WritePropertyName(DEFINE.NS1_TRACEREQUESTS);
                        jsonWriter.WriteStartObject(); // Start  object

                        jsonWriter.WritePropertyName("@" + DEFINE.NS1_PROPERTY);
                        jsonWriter.WriteValue(DEFINE.NS1_NAMESPACE); // Add the namespace

                        // Write the eventtrigger array
                        jsonWriter.WritePropertyName("eventtrigger"); // Property name for eventtrigger
                        jsonWriter.WriteStartArray(); // Start of the array for eventtrigger objects

                        // Iterate through each event model
                        foreach (var eventModel in lstEventModels)
                        {
                            jsonWriter.WriteStartObject(); // Start of the eventtrigger object
                            jsonWriter.WritePropertyName($"@eventid"); // Property name with prefix '@'
                            jsonWriter.WriteValue(eventModel.EventID); // Write event ID
                            jsonWriter.WritePropertyName($"@eventname"); // Property name with prefix '@'
                            jsonWriter.WriteValue(eventModel.EventName); // Write event name

                            // Start the extension object
                            jsonWriter.WritePropertyName("extension");
                            jsonWriter.WriteStartObject(); // Start of the extension object

                            // Start the detectconditions object
                            jsonWriter.WritePropertyName("detectconditions");
                            jsonWriter.WriteStartObject(); // Start of the detectconditions object
                            jsonWriter.WritePropertyName("@andor"); // Property name with prefix '@'
                            jsonWriter.WriteValue(eventModel.AndOr); // Write AndOr value

                            // Start the parameter array
                            jsonWriter.WritePropertyName("parameter");
                            jsonWriter.WriteStartArray(); // Start of the parameter array

                            // Add all parameters based on event model
                            foreach (var paramId in eventModel.ParametersID)
                            {
                                jsonWriter.WriteStartObject(); // Start of the parameter object
                                jsonWriter.WritePropertyName("@paramid"); // Property name with prefix '@'
                                jsonWriter.WriteValue(paramId); // Write the parameter ID
                                jsonWriter.WritePropertyName("@equation"); // Property name with prefix '@'
                                jsonWriter.WriteValue(eventModel.Equation); // Write equation value
                                jsonWriter.WritePropertyName("@value"); // Property name with prefix '@'
                                jsonWriter.WriteValue(eventModel.Value); // Write value
                                jsonWriter.WriteEndObject(); // End of the parameter object
                            }

                            // End the parameter array
                            jsonWriter.WriteEndArray();
                            jsonWriter.WriteEndObject(); // End of the detectconditions object
                            jsonWriter.WriteEndObject(); // End of the extension object
                            jsonWriter.WriteEndObject(); // End of the eventtrigger object
                        }

                        // End the eventtrigger array
                        jsonWriter.WriteEndArray();
                        jsonWriter.WriteEndObject(); // End the eventtriggers object
                        jsonWriter.WriteEndObject(); // End the main JSON object
                    }
                }

                return string.Empty; // Return empty string if successful
            }
            catch (Exception objEx) // Catch all exceptions
            {
                StackTrace objStackTrace = new StackTrace();
                string strMethodName = objStackTrace.GetFrame(0).GetMethod().Name;
                return HandleException(objEx, strMethodName); // Handle the exception and return the error message
            }
        }

        /// <summary>
        /// Write DCP Models To Json Using Json Writer
        /// </summary>
        public static string WriteDCPToJson(ExlDCPModel x_objDCP, string x_strJsonPath)
        {
            try
            {
                using (StreamWriter objStreamWriter = new StreamWriter(x_strJsonPath))
                {
                    using (JsonWriter jsonWriter = new JsonTextWriter(objStreamWriter))
                    {
                        jsonWriter.Formatting = Newtonsoft.Json.Formatting.Indented;

                        jsonWriter.WriteStartObject(); // Start Root object

                        // XML version declaration
                        jsonWriter.WritePropertyName("?xml");
                        jsonWriter.WriteStartObject();
                        jsonWriter.WritePropertyName("@version");
                        jsonWriter.WriteValue("1.0");
                        jsonWriter.WriteEndObject();

                        // DCPTables
                        jsonWriter.WritePropertyName(DEFINE.DCPTABLES);
                        jsonWriter.WriteStartObject();
                        jsonWriter.WritePropertyName("@xmlns:xsi");
                        jsonWriter.WriteValue("http://www.w3.org/2001/XMLSchema-instance");
                        jsonWriter.WritePropertyName("@xmlns:xsd");
                        jsonWriter.WriteValue("http://www.w3.org/2001/XMLSchema");
                        jsonWriter.WritePropertyName("@xmlns");
                        jsonWriter.WriteValue(DEFINE.NS1_NAMESPACE);

                        // DCPTable
                        jsonWriter.WritePropertyName("DCPTable");
                        jsonWriter.WriteStartObject();
                        jsonWriter.WritePropertyName("@xmlns");
                        jsonWriter.WriteValue("");

                        // Consumer
                        jsonWriter.WritePropertyName("Consumer");
                        jsonWriter.WriteNull();

                        // Macname
                        jsonWriter.WritePropertyName("Macname");
                        jsonWriter.WriteValue(x_objDCP.MachineName);

                        // DataCollectionPlan
                        jsonWriter.WritePropertyName("DataCollectionPlan");
                        jsonWriter.WriteStartObject();
                        jsonWriter.WritePropertyName("@id");
                        jsonWriter.WriteValue(x_objDCP.PlanID);
                        jsonWriter.WritePropertyName("@name");
                        jsonWriter.WriteValue(x_objDCP.PlanName);
                        jsonWriter.WritePropertyName("@Description");
                        jsonWriter.WriteValue(x_objDCP.Description);
                        jsonWriter.WritePropertyName("@monitorIntervalInSeconds");
                        jsonWriter.WriteValue("");

                        // StartEvent
                        jsonWriter.WritePropertyName("StartEvent");
                        jsonWriter.WriteStartObject();
                        jsonWriter.WritePropertyName("EventRequests");
                        jsonWriter.WriteStartObject();
                        jsonWriter.WritePropertyName("EventRequest");
                        jsonWriter.WriteStartObject();
                        jsonWriter.WritePropertyName("@eventId");
                        jsonWriter.WriteValue(x_objDCP.StartEvent);
                        jsonWriter.WriteEndObject(); // End EventRequest
                        jsonWriter.WritePropertyName("Extension");
                        jsonWriter.WriteNull();
                        jsonWriter.WriteEndObject(); // End EventRequests
                        jsonWriter.WritePropertyName("ParameterRequests");
                        jsonWriter.WriteNull();
                        jsonWriter.WriteEndObject(); // End StartEvent

                        // EndEvent
                        jsonWriter.WritePropertyName("EndEvent");
                        jsonWriter.WriteStartObject();
                        jsonWriter.WritePropertyName("EventRequests");
                        jsonWriter.WriteStartObject();
                        jsonWriter.WritePropertyName("EventRequest");
                        jsonWriter.WriteStartObject();
                        jsonWriter.WritePropertyName("@eventId");
                        jsonWriter.WriteValue(x_objDCP.EndEvent);
                        jsonWriter.WriteEndObject(); // End EventRequest
                        jsonWriter.WritePropertyName("Extension");
                        jsonWriter.WriteNull();
                        jsonWriter.WriteEndObject(); // End EventRequests
                        jsonWriter.WritePropertyName("ParameterRequests");
                        jsonWriter.WriteNull();
                        jsonWriter.WritePropertyName("TimeRequests");
                        jsonWriter.WriteStartObject();
                        jsonWriter.WritePropertyName("TimeRequest");
                        jsonWriter.WriteNull();
                        jsonWriter.WriteEndObject(); // End TimeRequests
                        jsonWriter.WriteEndObject(); // End EndEvent

                        // Contents
                        jsonWriter.WritePropertyName("Contents");
                        jsonWriter.WriteStartObject();
                        jsonWriter.WritePropertyName("EventRequests");
                        jsonWriter.WriteNull();
                        jsonWriter.WritePropertyName("TraceRequests");
                        jsonWriter.WriteNull();
                        jsonWriter.WritePropertyName("ParameterRequests");
                        jsonWriter.WriteStartObject();
                        jsonWriter.WritePropertyName("ParameterRequest");
                        jsonWriter.WriteStartArray();
                        foreach (string strParameter in x_objDCP.ParametersID)
                        {
                            jsonWriter.WriteStartObject();
                            jsonWriter.WritePropertyName("@parameterName");
                            jsonWriter.WriteValue(strParameter);
                            jsonWriter.WriteEndObject();
                        }
                        jsonWriter.WriteEndArray();
                        jsonWriter.WriteEndObject(); // End ParameterRequests
                        jsonWriter.WriteEndObject(); // End Contents

                        jsonWriter.WriteEndObject(); // End DCPTable
                        jsonWriter.WriteEndObject(); // End DCPTables

                        jsonWriter.WriteEndObject(); // End Root object
                    }
                }    
                   
                return string.Empty; // Return empty string if successful
            }
            catch (Exception objEx) // Catch all exceptions
            {
                StackTrace objStackTrace = new StackTrace();
                string strMethodName = objStackTrace.GetFrame(0).GetMethod().Name;
                return HandleException(objEx, strMethodName); // Handle the exception and return the error message
            }

        }

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
                return HandleException(objEx, strMethodName); // Handle the exception and return the error message
            }
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
                return HandleException(objEx, strMethodName); // Handle the exception and return the error message
            }
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
                return HandleException(objEx, strMethodName); // Handle the exception and return the error message
            }
        }

        /// <summary>
        /// Handles exceptions and returns a corresponding error message.
        /// </summary>
        internal static string HandleException(Exception objEx, string x_strHandleName)
        {
            string strLogErr = x_strHandleName + ": ";
            switch (objEx)
            {
                case IOException ioEx:
                    strLogErr += "IO error occurred: " + ioEx.Message;
                    break;
                case UnauthorizedAccessException unauthEx:
                    strLogErr += "Access error: " + unauthEx.Message;
                    break;
                case XmlException xmlEx:
                    strLogErr += "XML error: " + xmlEx.Message;
                    break;
                case ArgumentNullException argEx:
                    strLogErr += "Null argument error: " + argEx.Message;
                    break;
                default:
                    strLogErr += "An unexpected error occurred: " + objEx.Message;
                    break;
            }
            return strLogErr;
        }

        /// <summary>
        /// Converts an XML file to JSON format if the file is of type Parameter.
        /// </summary>
        /// <param name="x_strXmlPath">The path of the XML file to convert.</param>
        /// <param name="x_strJsonPath">The path where the resulting JSON file will be saved.</param>
        /// <returns>Returns a JsonXmlConversionResult indicating the success status and message of the conversion.</returns>
        public static ConvertResult ConvertXmlToJson_Parameter(string x_strXmlPath, string x_strJsonPath)
        {
            // Declare variables at the beginning
            ConvertType fileType;
            XmlDocument xmlDoc;
            string strJson;
            ConvertResult objResult = new ConvertResult();

            // Validate the input file to check if it is of type Parameter
            fileType = ValidateFile(x_strXmlPath);
            if (fileType == ConvertType.Unknown)
            {
                objResult.IsSuccess = false;
                objResult.Message = $"Unknown type of the provided file.";
                return objResult;
            }

            try
            {
                xmlDoc = new XmlDocument();
                xmlDoc.Load(x_strXmlPath);

                // Store the original XML content as a string
                objResult.XmlData = xmlDoc.OuterXml;

                // Convert XML to JSON
                strJson = JsonConvert.SerializeObject(xmlDoc, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include // Include null values
                });

                // Store JSON in the result object
                objResult.JsonData = strJson;

                // Save the JSON data to the specified file path
                File.WriteAllText(x_strJsonPath, strJson);

                // If the conversion and saving are successful
                objResult.IsSuccess = true;
                objResult.Message = $"{fileType} XML File Conversion successful! JSON saved to: {x_strJsonPath}";
            }
            catch (XmlException)
            {
                objResult.IsSuccess = false;
                objResult.Message = "The provided file is not a valid XML format.";
                return objResult;
            }
            catch (Exception xmlEx)
            {
                // If an error occurs
                objResult.IsSuccess = false;
                objResult.Message = $"{fileType} XML Error during conversion: {xmlEx.Message}";
            }
            return objResult;
        }

        /// <summary>
        /// Converts a JSON file to XML format if the file is of type Parameter.
        /// </summary>
        /// <param name="x_strJsonPath">The path of the JSON file to convert.</param>
        /// <param name="x_strXmlPath">The path where the resulting XML file will be saved.</param>
        /// <returns>Returns a JsonXmlConversionResult indicating the success status and message of the conversion.</returns>
        public static ConvertResult ConvertJsonToXml_Parameter(string x_strJsonPath, string x_strXmlPath)
        {
            // Declare variables at the beginning
            ConvertType converType;
            XmlDocument xmlDoc;
            string strJsonData;
            ConvertResult objResult = new ConvertResult();

            // Validate the input file to check if it is of type Parameter
            converType = ValidateFile(x_strJsonPath);
            if (converType == ConvertType.Unknown)
            {
                objResult.IsSuccess = false;
                objResult.Message = $"Unknown type of the provided file.";
                return objResult;
            }
            try
            {
                // Read JSON file content
                strJsonData = File.ReadAllText(x_strJsonPath);

                // Convert JSON to XmlDocument
                xmlDoc = JsonConvert.DeserializeXmlNode(strJsonData);

                // Store the original JSON content as a string (optional)
                objResult.JsonData = strJsonData;

                // Save the XmlDocument to the specified file path
                xmlDoc.Save(x_strXmlPath);

                // If the conversion and saving are successful
                objResult.IsSuccess = true;
                objResult.XmlData = xmlDoc.OuterXml; // Store the converted XML
                objResult.Message = $"{converType} JSON File Conversion successful! XML saved to: {x_strXmlPath}";
            }
            catch (JsonException)
            {
                objResult.IsSuccess = false;
                objResult.Message = "The provided file is not a valid Json format.";
                return objResult;
            }
            catch (Exception objEx)
            {
                // If an error occurs
                objResult.IsSuccess = false;
                objResult.Message = $"{converType} JSON Error during conversion: {objEx.Message}";
            }

            return objResult;
        }

        /// <summary>
        /// Validates the file at the specified path and determines its type based on the content.
        /// </summary>
        /// <returns>Returns the FileType indicating the type of the file based on its content.</returns>
        public static ConvertType ValidateFile(string x_strFilePath)
        {
            // Declare variable for file content at the beginning
            string strFileContent;

            // Return Unknown if the file does not exist
            if (File.Exists(x_strFilePath) == false)
            {
                return ConvertType.Unknown;
            }

            // Load the file content
            strFileContent = File.ReadAllText(x_strFilePath);

            // Determine the file type based on the content
            if (strFileContent.Contains(DEFINE.NS1_PARAMETERS))
            {
                return ConvertType.Parameter;
            }
            else if (strFileContent.Contains(DEFINE.NS1_TRACEREQUESTS))
            {
                return ConvertType.TraceRequest;
            }
            else if (strFileContent.Contains(DEFINE.NS1_EVENTTRIGGERS))
            {
                return ConvertType.EventTrigger;
            }
            else if (strFileContent.Contains(DEFINE.NS1_EVENTREQUESTS))
            {
                return ConvertType.EventRequest;
            }
            else if (strFileContent.Contains(DEFINE.DCPTABLES))
            {
                return ConvertType.DataCollectionPlan;
            }

            // If none of the above matches, return Unknown
            return ConvertType.Unknown;
        }
    }
}
