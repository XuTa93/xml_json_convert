//*****************************************************************************
// 		        ：
// 内容		    ：CommonCmpLib: Xml <=> Json Convert
// 		        ：
// 作成者		：TangLx
// 作成日		：2024/10/01
// 		        ：
// 修正履歴	    ：
// 		        ：
// 		        ：
//*****************************************************************************

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace CommonCmpLib
{

    public static class Common
    {
        #region Types
        /// <summary>
        /// Parameter, TraceRequest, EventTrigger, EventRequest, DataCollectionPlan
        /// </summary>
        public enum FileType
        {
            Parameter,
            TraceRequest,
            EventTrigger,
            EventRequest,
            DataCollectionPlan,
            Unknown
        }
        private const string NS1_PARAMETERS = "ns1:parameters";
        private const string NS1_TRACEREQUESTS = "ns1:tracerequests";
        private const string NS1_EVENTTRIGGERS = "ns1:eventtriggers";
        private const string NS1_EVENTREQUESTS = "ns1:eventrequests";
        private const string DCPTABLES = "DCPTables";
        #endregion Types

        public static string DictionaryDataXml(ExcelProcessResult x_objResult)
        {
            if (x_objResult.IsSuccess == false)
            {
                return "";
            }

            switch (x_objResult.SheetName)
            {
                case PARAMETER.SHEET_NAME:
                    CreateParameterToXml(x_objResult.Models);
                    break;
                case TRACE.SHEET_NAME:
                    CreateTraceToXml(x_objResult.Models);
                    break;

                case EVENT.SHEET_NAME:
                    CreateEventTriggersXml(x_objResult.Models);
                    break;

                case DCP.SHEET_NAME:
                    CreateDCPXml(x_objResult.Models);
                    break;

                default:
                    break;
            }
            return "";
        }

        /// <summary>
        /// Converts a list of parameters stored in dictionaries into an XML format and saves it to a file.
        /// </summary>
        public static void CreateParameterToXml(List<Dictionary<string, string>> x_lstData)
        {
            string strFilePath = "parameters.xml";
            // Create XmlWriterSettings with OmitXmlDeclaration = true
            XmlWriterSettings objXmlSettings = new XmlWriterSettings
            {
                Indent = true,              // Format the XML with indentation
                OmitXmlDeclaration = true,  // Omit the XML declaration
            };

            try
            {
                using (StringWriter strWriter = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(strWriter, objXmlSettings))
                    {
                        // Start the XML document
                        xmlWriter.WriteStartDocument();
                        xmlWriter.WriteStartElement("ns1", "parameters", "urn:ConfigFileSchema");

                        // Iterate over the list of parameter dictionaries
                        foreach (Dictionary<string, string> objPara in x_lstData)
                        {
                            // Create parameter element with attributes, checking if the key exists
                            xmlWriter.WriteStartElement("parameter");
                            xmlWriter.WriteAttributeString("paramid", objPara.ContainsKey(DEFINE.ParameterID) ? objPara[DEFINE.ParameterID] : string.Empty);
                            xmlWriter.WriteAttributeString("paramname", objPara.ContainsKey(DEFINE.ParameterName) ? objPara[DEFINE.ParameterName] : string.Empty);
                            xmlWriter.WriteAttributeString("locator", objPara.ContainsKey(DEFINE.Locator) ? objPara[DEFINE.Locator] : string.Empty);
                            xmlWriter.WriteAttributeString("unit", objPara.ContainsKey(DEFINE.Unit) ? objPara[DEFINE.Unit] : string.Empty);
                            xmlWriter.WriteAttributeString("type", objPara.ContainsKey(DEFINE.Type) ? objPara[DEFINE.Type] : string.Empty);
                            xmlWriter.WriteAttributeString("array", objPara.ContainsKey(DEFINE.Array) ? objPara[DEFINE.Array] : string.Empty);
                            xmlWriter.WriteAttributeString("function", objPara.ContainsKey(DEFINE.Function) ? objPara[DEFINE.Function] : string.Empty);
                            xmlWriter.WriteAttributeString("arg", objPara.ContainsKey(DEFINE.Arg) ? objPara[DEFINE.Arg] : string.Empty);

                            // Start the extension element
                            xmlWriter.WriteStartElement("extension");

                            // Start the datasource element with sourcetype attribute
                            xmlWriter.WriteStartElement("datasource");
                            xmlWriter.WriteAttributeString("sourcetype", objPara.ContainsKey(DEFINE.Sourcetype) ? objPara[DEFINE.Sourcetype] : string.Empty);

                            // Start the memory element with attributes
                            xmlWriter.WriteStartElement("memory");
                            xmlWriter.WriteAttributeString("memname", objPara.ContainsKey(DEFINE.MemoryName) ? objPara[DEFINE.MemoryName] : string.Empty);
                            xmlWriter.WriteAttributeString("offset", objPara.ContainsKey(DEFINE.Offset) ? objPara[DEFINE.Offset] : string.Empty);
                            xmlWriter.WriteAttributeString("stype", objPara.ContainsKey(DEFINE.SourceType) ? objPara[DEFINE.SourceType] : string.Empty);
                            xmlWriter.WriteAttributeString("sarray", objPara.ContainsKey(DEFINE.Array) ? objPara[DEFINE.Array] : string.Empty);

                            // Close the memory element
                            xmlWriter.WriteEndElement();

                            // Write the fins element (self-closing)
                            xmlWriter.WriteElementString("fins", string.Empty);

                            // Close the datasource element
                            xmlWriter.WriteEndElement();

                            // Close the extension element
                            xmlWriter.WriteEndElement();

                            // Close the parameter element
                            xmlWriter.WriteEndElement();
                        }

                        // Close the parameters element
                        xmlWriter.WriteEndElement();

                        // End the XML document
                        xmlWriter.WriteEndDocument();
                    }

                    // Convert the StringBuilder content to a string
                    string xmlString = strWriter.ToString();
                    // Define the file path for the XML file and save it

                    File.WriteAllText(strFilePath, xmlString);
                }

                // Convert XML to JSON and back (assuming methods exist for this)
                ConvertXmlToJson_Parameter(strFilePath, "parameters.json");
                ConvertJsonToXml_Parameter("parameters.json", strFilePath);
            }
            catch (Exception ex)
            {
                // Catch any exceptions and output the error message
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates an XML file representing event triggers based on the provided event requests.
        /// </summary>
        public static void CreateEventTriggersXml(List<Dictionary<string, string>> x_lstData)
        {
            List<ExlEventModel> lstEvent = ConvertToEventModelList(x_lstData);

            string strEventtriggers = "EventTrigger.xml";
            string strEventRequest = "EventRequest.xml";
            // Create XmlWriterSettings
            XmlWriterSettings objXmlSettings = new XmlWriterSettings
            {
                Indent = true,              // Format the XML with indentation
                OmitXmlDeclaration = true, // Include XML declaration
            };

            using (StringWriter strWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(strWriter, objXmlSettings))
                {
                    // Start writing the XML document
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("ns1", "eventtriggers", "urn:ConfigFileSchema");

                    foreach (ExlEventModel objEventTrigger in lstEvent)
                    {
                        // Create eventtrigger element
                        xmlWriter.WriteStartElement("eventtrigger");
                        xmlWriter.WriteAttributeString("eventid", objEventTrigger.EventID);
                        xmlWriter.WriteAttributeString("eventname", objEventTrigger.EventName);

                        // Create extension element
                        xmlWriter.WriteStartElement("extension");

                        // Create detectconditions element
                        xmlWriter.WriteStartElement("detectconditions");
                        xmlWriter.WriteAttributeString("andor", objEventTrigger.AndOr);

                        // Create parameter element
                        foreach (string strParameterID in objEventTrigger.ParametersID)
                        {
                            xmlWriter.WriteStartElement("parameter");
                            xmlWriter.WriteAttributeString("paramid", strParameterID);
                            xmlWriter.WriteAttributeString("equation", objEventTrigger.Equation);
                            xmlWriter.WriteAttributeString("value", objEventTrigger.Value);
                            xmlWriter.WriteEndElement(); // End parameter
                        }

                        xmlWriter.WriteEndElement(); // End detectconditions
                        xmlWriter.WriteEndElement(); // End extension

                        xmlWriter.WriteEndElement(); // End eventtrigger
                    }

                    // End the eventtriggers element
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();
                }

                // Save the XML content to a file
                string xmlString = strWriter.ToString();
                File.WriteAllText(strEventtriggers, xmlString);
            }

            using (StringWriter strWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(strWriter, objXmlSettings))
                {
                    // Start writing the XML document
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("ns1", "eventrequests", "urn:ConfigFileSchema");

                    foreach (ExlEventModel objEventRequest in lstEvent)
                    {
                        // Create eventrequest element
                        xmlWriter.WriteStartElement("eventrequest");
                        xmlWriter.WriteAttributeString("eventid", objEventRequest.EventID);
                        xmlWriter.WriteAttributeString("eventname", objEventRequest.EventName);

                        // Create parameter elements
                        foreach (string strParameterID in objEventRequest.ParametersID)
                        {
                            xmlWriter.WriteStartElement("parameter");
                            xmlWriter.WriteAttributeString("paramid", strParameterID);
                            xmlWriter.WriteEndElement(); // End parameter
                        }

                        xmlWriter.WriteEndElement(); // End eventrequest
                    }

                    // End the eventrequests element
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();
                }

                // Save the XML content to a file
                string xmlString = strWriter.ToString();
                File.WriteAllText(strEventRequest, xmlString);
            }

            // Convert XML to JSON and back (assuming methods exist for this)
            ConvertXmlToJson_Parameter(strEventtriggers, "eventtriggers.json");
            ConvertJsonToXml_Parameter("eventtriggers.json", strEventtriggers);
            ConvertXmlToJson_Parameter(strEventRequest, "eventrequest.json");
            ConvertJsonToXml_Parameter("eventrequest.json", strEventRequest);
        }


        /// <summary>
        /// Creates an XML file representing trace requests based on the provided list of data dictionaries.
        /// </summary>
        public static void CreateTraceToXml(List<Dictionary<string, string>> x_lstData)
        {
            // Convert Dictionary data to a list of Trace grouped by ParameterID
            var lstTrace = x_lstData
                .GroupBy(obj => new
                {
                    TraceID = obj.ContainsKey(DEFINE.TraceID) ? obj[DEFINE.TraceID] : string.Empty,
                    TraceName = obj.ContainsKey(DEFINE.TraceName) ? obj[DEFINE.TraceName] : string.Empty,
                    Description = obj.ContainsKey(DEFINE.Description) ? obj[DEFINE.Description] : string.Empty,
                    StartOn = obj.ContainsKey(DEFINE.StartOn) ? obj[DEFINE.StartOn] : string.Empty,
                    StopOn = obj.ContainsKey(DEFINE.StopOn) ? obj[DEFINE.StopOn] : string.Empty
                })
                .Select(objTrace => new ExlTraceRequestModel
                {
                    TraceID = objTrace.Key.TraceID,
                    TraceName = objTrace.Key.TraceName,
                    Description = objTrace.Key.Description,
                    StartOn = objTrace.Key.StartOn,
                    StopOn = objTrace.Key.StopOn,
                    ParametersID = objTrace
                        .Where(par => par.ContainsKey(DEFINE.ParameterID)) // Ensure to only take ParameterID if it exists
                        .Select(lstPar => lstPar[DEFINE.ParameterID])
                        .ToList() // Group all ParameterIDs together
                }).ToList();

            string filePath = "traces.xml";

            // Create XmlWriterSettings with OmitXmlDeclaration = true
            XmlWriterSettings objXmlSettings = new XmlWriterSettings
            {
                Indent = true,              // Format the XML with indentation
                OmitXmlDeclaration = true,  // Omit XML declaration
            };

            using (StringWriter strWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(strWriter, objXmlSettings))
                {
                    // Start writing the XML document
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("ns1", "tracerequests", "urn:ConfigFileSchema");

                    foreach (ExlTraceRequestModel objTrace in lstTrace)
                    {
                        // Start the tracerequest element
                        xmlWriter.WriteStartElement("tracerequest");

                        xmlWriter.WriteAttributeString("traceid", objTrace.TraceID);
                        xmlWriter.WriteAttributeString("tracename", objTrace.TraceName);
                        xmlWriter.WriteAttributeString("description", objTrace.Description);

                        // Write the triggers element
                        xmlWriter.WriteStartElement("triggers");
                        xmlWriter.WriteElementString("starton", objTrace.StartOn);
                        xmlWriter.WriteElementString("stopon", objTrace.StopOn);
                        xmlWriter.WriteEndElement(); // End triggers

                        // Write the parameters element
                        xmlWriter.WriteStartElement("parameters");
                        foreach (string strPara in objTrace.ParametersID)
                        {
                            xmlWriter.WriteStartElement("parameter"); // Start the parameter element
                            xmlWriter.WriteAttributeString("paramid", strPara);
                            xmlWriter.WriteEndElement(); // End parameter
                        }
                        xmlWriter.WriteEndElement(); // End parameters

                        // End the tracerequest element
                        xmlWriter.WriteEndElement();
                    }

                    // End the tracerequests element
                    xmlWriter.WriteEndElement();

                    // End the XML document
                    xmlWriter.WriteEndDocument();
                }

                // Save the XML content to a file
                string xmlString = strWriter.ToString();
                File.WriteAllText(filePath, xmlString);
            }

            // Convert XML to JSON and back (assuming methods exist for this)
            ConvertXmlToJson_Parameter(filePath, "traces.json");
            ConvertJsonToXml_Parameter("traces.json", filePath);
        }

        /// <summary>
        /// Creates an XML file representing DataCollectionPlan requests based on the provided list of data dictionaries.
        /// </summary>
        public static void CreateDCPXml(List<Dictionary<string, string>> x_lstData)
        {
            List<ExlDCPModel> lstData = ConvertToDCPModelList(x_lstData);

            // Create XmlWriterSettings with indentation and omit XML declaration
            XmlWriterSettings objXmlSettings = new XmlWriterSettings
            {
                Indent = true,              // Format the XML with indentation
                OmitXmlDeclaration = true,  // Omit XML declaration
            };

            foreach (var objDCP in lstData)
            {
                // Define the file path for the XML file
                string str_filePath = $"{objDCP.PlanName}.xml";

                using (StringWriter strWriter = new StringWriter())
                {
                    strWriter.WriteLine("<?xml version=\"1.0\"?>"); // Manually write XML declaration
                    using (XmlWriter xmlWriter = XmlWriter.Create(strWriter, objXmlSettings))
                    {
                        // Start writing XML document
                        xmlWriter.WriteStartDocument();
                        xmlWriter.WriteStartElement("DCPTables", "urn:ConfigFileSchema");

                        // Create DCPTable element
                        xmlWriter.WriteStartElement("DCPTable");

                        xmlWriter.WriteStartElement("Consumer");
                        xmlWriter.WriteEndElement(); // End Consumer

                        xmlWriter.WriteElementString("Macname", objDCP.MachineName); // Macname element with empty value

                        // Create DataCollectionPlan element with attributes
                        xmlWriter.WriteStartElement("DataCollectionPlan");
                        xmlWriter.WriteAttributeString("id", objDCP.PlanID);
                        xmlWriter.WriteAttributeString("name", objDCP.PlanName);
                        xmlWriter.WriteAttributeString("Description", objDCP.Description);
                        xmlWriter.WriteAttributeString("monitorIntervalInSeconds", "");

                        // StartEvent element
                        xmlWriter.WriteStartElement("StartEvent");
                        xmlWriter.WriteStartElement("EventRequests");
                        xmlWriter.WriteStartElement("EventRequest");
                        xmlWriter.WriteAttributeString("eventId", objDCP.StartEvent);
                        xmlWriter.WriteEndElement(); // End EventRequest
                        xmlWriter.WriteStartElement("Extension");
                        xmlWriter.WriteEndElement(); // End Extension
                        xmlWriter.WriteEndElement(); // End EventRequests
                        xmlWriter.WriteStartElement("ParameterRequests");
                        xmlWriter.WriteEndElement(); // End ParameterRequests
                        xmlWriter.WriteEndElement(); // End StartEvent

                        // EndEvent element
                        xmlWriter.WriteStartElement("EndEvent");
                        xmlWriter.WriteStartElement("EventRequests");
                        xmlWriter.WriteStartElement("EventRequest");
                        xmlWriter.WriteAttributeString("eventId", objDCP.EndEvent);
                        xmlWriter.WriteEndElement(); // End EventRequest
                        xmlWriter.WriteStartElement("Extension");
                        xmlWriter.WriteEndElement(); // End Extension
                        xmlWriter.WriteEndElement(); // End EventRequests

                        xmlWriter.WriteStartElement("ParameterRequests");
                        xmlWriter.WriteEndElement(); // End ParameterRequests

                        xmlWriter.WriteStartElement("TimeRequests");
                        xmlWriter.WriteStartElement("TimeRequest");
                        xmlWriter.WriteEndElement(); // End TimeRequest
                        xmlWriter.WriteEndElement(); // End TimeRequests

                        xmlWriter.WriteEndElement(); // End EndEvent

                        // Contents element
                        xmlWriter.WriteStartElement("Contents");
                        xmlWriter.WriteStartElement("EventRequests");
                        xmlWriter.WriteEndElement(); // End EventRequests
                        xmlWriter.WriteStartElement("TraceRequests");
                        xmlWriter.WriteEndElement(); // End TraceRequests
                        xmlWriter.WriteStartElement("ParameterRequests");

                        foreach (string strPara in objDCP.ParametersID)
                        {
                            xmlWriter.WriteStartElement("ParameterRequest"); // Mở phần tử parameter
                            xmlWriter.WriteAttributeString("parameterName", strPara);
                            xmlWriter.WriteEndElement(); // Kết thúc parameter
                        }

                        xmlWriter.WriteEndElement(); // End ParameterRequests
                        xmlWriter.WriteEndElement(); // End Contents

                        xmlWriter.WriteEndElement(); // End DataCollectionPlan
                        xmlWriter.WriteEndElement(); // End DCPTable
                        xmlWriter.WriteEndElement(); // End DCPTables

                        // End XML document
                        xmlWriter.WriteEndDocument();
                    }

                    // Lưu nội dung XML vào tệp
                    File.WriteAllText(str_filePath, strWriter.ToString());

                    // Đọc lại tệp XML sau khi lưu
                    string xmlContent = File.ReadAllText(str_filePath);
                    // Convert XML to JSON and back if needed
                    ConvertXmlToJson_Parameter(str_filePath, Path.ChangeExtension(str_filePath, ".json"));
                    ConvertJsonToXml_Parameter(Path.ChangeExtension(str_filePath, ".json"), str_filePath);
                }
            }
        }

        private static List<ExlEventModel> ConvertToEventModelList(List<Dictionary<string, string>> x_lstData)
        {
            // Convert dictionary data to a grouped list of ExlEventModel
            var lstEvent = x_lstData
                .GroupBy(data => new
                {
                    EventID = data.ContainsKey(DEFINE.EventID) ? data[DEFINE.EventID] : null,
                    EventName = data.ContainsKey(DEFINE.EventName) ? data[DEFINE.EventName] : null,
                    AndOr = data.ContainsKey(DEFINE.AndOr) ? data[DEFINE.AndOr] : null,
                    Equation = data.ContainsKey(DEFINE.Equation) ? data[DEFINE.Equation] : null,
                    Value = data.ContainsKey(DEFINE.Value) ? data[DEFINE.Value] : null,
                })
                .Select(objEvt => new ExlEventModel
                {
                    EventID = objEvt.Key.EventID,
                    EventName = objEvt.Key.EventName,
                    AndOr = objEvt.Key.AndOr,
                    Equation = objEvt.Key.Equation,
                    Value = objEvt.Key.Value,
                    ParametersID = objEvt
                        .Where(par => par.ContainsKey(DEFINE.ParameterID)) // Ensure to only take ParameterID if it exists
                        .Select(lstPar => lstPar[DEFINE.ParameterID])
                        .ToList() // Group all ParameterIDs together
                }).ToList();

            return lstEvent;
        }

        private static List<ExlDCPModel> ConvertToDCPModelList(List<Dictionary<string, string>> x_lstData)
        {
            // Group the data by PlanID and create the list of ExlDCPModel
            var lstDCP = x_lstData
                .GroupBy(obj => new
                {
                    No = obj.ContainsKey(DEFINE.No) ? obj[DEFINE.No] : null,
                    MachineName = obj.ContainsKey(DEFINE.MachineName) ? obj[DEFINE.MachineName] : null,
                    PlanID = obj.ContainsKey(DEFINE.PlanID) ? obj[DEFINE.PlanID] : null,
                    PlanName = obj.ContainsKey(DEFINE.PlanName) ? obj[DEFINE.PlanName] : null,
                    Description = obj.ContainsKey(DEFINE.Description) ? obj[DEFINE.Description] : null,
                    StartEvent = obj.ContainsKey(DEFINE.StartEvent) ? obj[DEFINE.StartEvent] : null,
                    EndEvent = obj.ContainsKey(DEFINE.EndEvent) ? obj[DEFINE.EndEvent] : null,
                    TimeRequest = obj.ContainsKey(DEFINE.TimeRequest) ? obj[DEFINE.TimeRequest] : null
                })
                .Select(objGroup => new ExlDCPModel
                {
                    No = objGroup.Key.No,
                    MachineName = objGroup.Key.MachineName,
                    PlanID = objGroup.Key.PlanID,
                    PlanName = objGroup.Key.PlanName,
                    Description = objGroup.Key.Description,
                    StartEvent = objGroup.Key.StartEvent,
                    EndEvent = objGroup.Key.EndEvent,
                    TimeRequest = objGroup.Key.TimeRequest,
                    ParametersID = objGroup.Select(g => g[DEFINE.ParameterID]).ToList() // Group all ParameterID
                })
                .ToList();

            return lstDCP;
        }

        /// <summary>
        /// Converts an XML file to JSON format if the file is of type Parameter.
        /// </summary>
        /// <param name="x_strXmlPath">The path of the XML file to convert.</param>
        /// <param name="x_strJsonPath">The path where the resulting JSON file will be saved.</param>
        /// <returns>Returns a JsonXmlConversionResult indicating the success status and message of the conversion.</returns>
        public static JsonXmlConvertResult ConvertXmlToJson_Parameter(string x_strXmlPath, string x_strJsonPath)
        {
            // Declare variables at the beginning
            FileType fileType;
            XmlDocument xmlDoc;
            string strJson;
            JsonXmlConvertResult objResult = new JsonXmlConvertResult();

            // Validate the input file to check if it is of type Parameter
            fileType = ValidateFile(x_strXmlPath);
            if (fileType == FileType.Unknown)
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
        public static JsonXmlConvertResult ConvertJsonToXml_Parameter(string x_strJsonPath, string x_strXmlPath)
        {
            // Declare variables at the beginning
            FileType fileType;
            XmlDocument xmlDoc;
            string strJsonData;
            JsonXmlConvertResult objResult = new JsonXmlConvertResult();

            // Validate the input file to check if it is of type Parameter
            fileType = ValidateFile(x_strJsonPath);
            if (fileType == FileType.Unknown)
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
                objResult.Message = $"{fileType} JSON File Conversion successful! XML saved to: {x_strXmlPath}";
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
                objResult.Message = $"{fileType} JSON Error during conversion: {objEx.Message}";
            }

            return objResult;
        }

        /// <summary>
        /// Validates the file at the specified path and determines its type based on the content.
        /// </summary>
        /// <returns>Returns the FileType indicating the type of the file based on its content.</returns>
        public static FileType ValidateFile(string x_strFilePath)
        {
            // Declare variable for file content at the beginning
            string strFileContent;

            // Return Unknown if the file does not exist
            if (File.Exists(x_strFilePath) == false)
            {
                return FileType.Unknown;
            }

            // Load the file content
            strFileContent = File.ReadAllText(x_strFilePath);

            // Determine the file type based on the content
            if (strFileContent.Contains(NS1_PARAMETERS))
            {
                return FileType.Parameter;
            }
            else if (strFileContent.Contains(NS1_TRACEREQUESTS))
            {
                return FileType.TraceRequest;
            }
            else if (strFileContent.Contains(NS1_EVENTTRIGGERS))
            {
                return FileType.EventTrigger;
            }
            else if (strFileContent.Contains(NS1_EVENTREQUESTS))
            {
                return FileType.EventRequest;
            }
            else if (strFileContent.Contains(DCPTABLES))
            {
                return FileType.DataCollectionPlan;
            }

            // If none of the above matches, return Unknown
            return FileType.Unknown;
        }

    }
}
