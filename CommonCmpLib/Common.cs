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
using System.Xml.Linq;

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
        private const string NS1_PROPERTY = "xmlns:ns1";
        private const string NS1_NAMESPACE = "urn:ConfigFileSchema";
        private const string NS1_PARAMETERS = "ns1:parameters";
        private const string NS1_TRACEREQUESTS = "ns1:tracerequests";
        private const string NS1_EVENTTRIGGERS = "ns1:eventtriggers";
        private const string NS1_EVENTREQUESTS = "ns1:eventrequests";
        private const string DCPTABLES = "DCPTables";
        #endregion Types

        public static void ParseXmlToJson<T>(string x_strXmlPath)
        {

            FileType fileType;
            fileType = ValidateFile(x_strXmlPath);
            switch (fileType)
            {
                case FileType.Parameter:
                    break;
                case FileType.TraceRequest:
                    ParseTraceXmlToList(x_strXmlPath);
                    break;
                case FileType.EventTrigger:
                    ParseEventXmlToList(x_strXmlPath, "eventtrigger");
                    break;
                case FileType.EventRequest:
                    ParseEventXmlToList(x_strXmlPath, "eventrequest");
                    break;
                case FileType.DataCollectionPlan:
                    break;
                case FileType.Unknown:
                    ParseDCPXmlToModel(x_strXmlPath);
                    break;
                default:
                    break;
            }
        }
        public static string DictionaryDataXml(ExcelProcessResult x_objResult,string x_strOutFolder)
        {
            if (string.IsNullOrEmpty(x_strOutFolder))
            {
                return "";
            }
            if (x_objResult.IsSuccess == false)
            {
                return "Read Excel Error";
            }

            switch (x_objResult.SheetName)
            {
                case PARAMETER.SHEET_NAME:
                    //Create Parameter Xml
                    CreateParameterToXml(x_objResult.Models, x_strOutFolder);
                    break;
                case TRACE.SHEET_NAME:
                    //Create TraceRequest Xml
                    CreateTraceToXml(x_objResult.Models, x_strOutFolder);
                    break;

                case EVENT.SHEET_NAME:
                    //Create EventTrigger and EventRequst Xml
                    CreateEventsXml(x_objResult.Models, x_strOutFolder);
                    break;

                case DCP.SHEET_NAME:
                    //Create DataCollectionPlan Xml
                    CreateDCPXml(x_objResult.Models, x_strOutFolder);
                    break;

                default:
                    break;
            }
            return "";  
        }


        /// <summary>
        /// Converts a list of parameters stored in dictionaries into an XML format and saves it to a file.
        /// </summary>
        public static void CreateParameterToXml(List<Dictionary<string, string>> x_lstData, string x_strOutFolder)
        {
            string strFilePath = Path.Combine(x_strOutFolder,"parameters.xml");

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
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Creates an XML file representing trace requests based on the provided list of data dictionaries.
        /// </summary>
        public static void CreateTraceToXml(List<Dictionary<string, string>> x_lstData, string x_strOutFolder)
        {
            string strFilePath;
            List<ExlTraceRequestModel> lstTrace;

            strFilePath = Path.Combine(x_strOutFolder, "traces.xml");
            lstTrace = ConvertTraceModelToList(x_lstData);
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
                File.WriteAllText(strFilePath, xmlString);
            }

            // Test

            var C = ParseTraceXmlToList(strFilePath);

            WriteTraceRequestToJson_UsingJsonWriter(C, "trace.json");

            var read = File.ReadAllText("trace.json");

            ConvertJsonToXml_Parameter("trace.json", "trace_test.xml");
        }

        /// <summary>
        /// Creates an XML file representing event triggers based on the provided event requests.
        /// </summary>
        public static void CreateEventsXml(List<Dictionary<string, string>> x_lstData, string x_strOutFolder)
        {
            string strEvtTrPath;
            string strEvtRq;
            List<ExlEventModel> lstEvent;
          
            strEvtTrPath = Path.Combine(x_strOutFolder, "EventTrigger.xml");
            strEvtRq = Path.Combine(x_strOutFolder, "EventRequest.xml");
            lstEvent = ConvertEventModelToList(x_lstData);

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
                File.WriteAllText(strEvtTrPath, xmlString);
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
                File.WriteAllText(strEvtRq, xmlString);
            }
        }

        /// <summary>
        /// Creates an XML file representing DataCollectionPlan requests based on the provided list of data dictionaries.
        /// </summary>
        public static void CreateDCPXml(List<Dictionary<string, string>> x_lstData, string x_strOutFolder)
        {
            string strFilePath;
            List<ExlDCPModel> lstData;

            lstData = ConvertToDCPModelList(x_lstData);

            // Create XmlWriterSettings with indentation and omit XML declaration
            XmlWriterSettings objXmlSettings = new XmlWriterSettings
            {
                Indent = true,              // Format the XML with indentation
                OmitXmlDeclaration = true,  // Omit XML declaration
            };

            foreach (var objDCP in lstData)
            {
                // Define the file path for the XML file
                strFilePath = Path.Combine(x_strOutFolder, $"{objDCP.PlanName}.xml");

                using (StringWriter strWriter = new StringWriter())
                {
                    strWriter.WriteLine("<?xml version=\"1.0\"?>"); // Manually write XML declaration
                    using (XmlWriter xmlWriter = XmlWriter.Create(strWriter, objXmlSettings))
                    {
                        // Start writing XML document
                        xmlWriter.WriteStartDocument();

                        // Add xsi and xsd namespace attributes
                        xmlWriter.WriteStartElement("DCPTables", "urn:ConfigFileSchema");
                        xmlWriter.WriteAttributeString("xmlns", "xsi", "", "http://www.w3.org/2001/XMLSchema-instance");
                        xmlWriter.WriteAttributeString("xmlns", "xsd", "", "http://www.w3.org/2001/XMLSchema");

                        // Create DCPTable element
                        xmlWriter.WriteStartElement("DCPTable","");

                        // Create Consumer element
                        xmlWriter.WriteStartElement("Consumer");
                        xmlWriter.WriteEndElement(); // End Consumer

                        // Write Macname element with value from objDCP.MachineName
                        xmlWriter.WriteElementString("Macname", objDCP.MachineName);

                        // Create DataCollectionPlan element with attributes
                        xmlWriter.WriteStartElement("DataCollectionPlan");
                        xmlWriter.WriteAttributeString("id", objDCP.PlanID);
                        xmlWriter.WriteAttributeString("name", objDCP.PlanName);
                        xmlWriter.WriteAttributeString("Description", objDCP.Description);
                        xmlWriter.WriteAttributeString("monitorIntervalInSeconds", ""); // Empty value for monitorIntervalInSeconds

                        // StartEvent element
                        xmlWriter.WriteStartElement("StartEvent");
                        xmlWriter.WriteStartElement("EventRequests");
                        xmlWriter.WriteStartElement("EventRequest");
                        xmlWriter.WriteAttributeString("eventId", objDCP.StartEvent); // Set eventId for StartEvent
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
                        xmlWriter.WriteAttributeString("eventId", objDCP.EndEvent); // Set eventId for EndEvent
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

                        // Iterate through ParametersID and create ParameterRequest elements
                        foreach (string strPara in objDCP.ParametersID)
                        {
                            xmlWriter.WriteStartElement("ParameterRequest"); // Start ParameterRequest element
                            xmlWriter.WriteAttributeString("parameterName", strPara); // Set parameterName attribute
                            xmlWriter.WriteEndElement(); // End ParameterRequest
                        }

                        xmlWriter.WriteEndElement(); // End ParameterRequests
                        xmlWriter.WriteEndElement(); // End Contents

                        xmlWriter.WriteEndElement(); // End DataCollectionPlan
                        xmlWriter.WriteEndElement(); // End DCPTable
                        xmlWriter.WriteEndElement(); // End DCPTables

                        // End XML document
                        xmlWriter.WriteEndDocument();
                    }

                    // Save the XML content to the specified file path
                    File.WriteAllText(strFilePath, strWriter.ToString());
                }
            }
        }


        /// <summary>
        /// Parses the Parameter from an XML file and converts them into a list of ExlEventModel objects.
        /// </summary>
        /// <returns>List of ExlParameterModel objects parsed from the XML file.</returns>
        public static List<ExlParameterModel> ParseParameterXmlToList(string strXmlPath, string x_strName)
        {
            // Load the XML document from the provided path
            XDocument objXmlDoc = XDocument.Load(strXmlPath);
            var lstPara = objXmlDoc.Descendants()
                .Where(obj => obj.Name.LocalName.ToLower() == x_strName)
                .Select(objEvent => new ExlParameterModel
                {

                }).ToList();

            return lstPara;
        }

        /// <summary>
        /// Parses the TraceRequest from an XML file and converts them into a list of ExlEventModel objects.
        /// </summary>
        /// <returns>List of ExlTraceRequestModel objects parsed from the XML file.</returns>
        public static List<ExlTraceRequestModel> ParseTraceXmlToList(string strXmlPath)
        {
            // Load the XML document from the provided path
            XDocument objXmlDoc = XDocument.Load(strXmlPath);

            // Parse the XML and create a list of ExlTraceRequestModel objects
            var lstTrace = objXmlDoc.Descendants()
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

            return lstTrace;
        }

        /// <summary>
        /// Parses the event requests from an XML file and converts them into a list of ExlEventModel objects.
        /// </summary>
        /// <returns>List of ExlEventModel objects parsed from the XML file.</returns>
        public static List<ExlEventModel> ParseEventXmlToList(string strXmlPath, string x_strName)
        {
            // Load the XML document from the provided path
            XDocument objXmlDoc = XDocument.Load(strXmlPath);

            // Query to select event elements based on the specified name
            List<ExlEventModel> lstEvents = objXmlDoc.Descendants()
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
            return lstEvents;
        }

        /// <summary>
        /// Parses the DataCollectionPlan requests from an XML file and converts them into a list of ExlEventModel objects.
        /// </summary>
        /// <returns>List of ExlDCPModel objects parsed from the XML file.</returns>

        public static ExlDCPModel ParseDCPXmlToModel(string strXmlPath)
        {
            string strNamespace = "";
            // Tạo một đối tượng ExlDCPModel để lưu thông tin
            ExlDCPModel dcpModel = new ExlDCPModel();

            // Load XML document using XDocument
            XDocument objXmlDoc = XDocument.Load(strXmlPath);

            // Tìm phần tử DCPTable
            var dcpTableElement = objXmlDoc.Root.Element("" + "DCPTable");

            if (dcpTableElement != null)
            {
                // Lấy giá trị của MachineName từ phần tử Macname
                dcpModel.MachineName = dcpTableElement.Element(strNamespace + "Macname")?.Value;

                // Lấy phần tử DataCollectionPlan
                var dataCollectionPlanElement = dcpTableElement.Element(strNamespace + "DataCollectionPlan");

                if (dataCollectionPlanElement != null)
                {
                    // Lấy các thuộc tính từ phần tử DataCollectionPlan
                    dcpModel.PlanID = dataCollectionPlanElement.Attribute("id")?.Value;
                    dcpModel.PlanName = dataCollectionPlanElement.Attribute("name")?.Value;
                    dcpModel.Description = dataCollectionPlanElement.Attribute("Description")?.Value;

                    // Lấy StartEvent -> EventRequest -> eventId
                    dcpModel.StartEvent = dataCollectionPlanElement
                        .Element(strNamespace + "StartEvent")?
                        .Element(strNamespace + "EventRequests")?
                        .Element(strNamespace + "EventRequest")?
                        .Attribute("eventId")?.Value;

                    // Lấy EndEvent -> EventRequest -> eventId
                    dcpModel.EndEvent = dataCollectionPlanElement
                        .Element(strNamespace + "EndEvent")?
                        .Element(strNamespace + "EventRequests")?
                        .Element(strNamespace + "EventRequest")?
                        .Attribute("eventId")?.Value;

                    // Lấy danh sách ParametersID từ Contents -> ParameterRequests
                    dcpModel.ParametersID = dataCollectionPlanElement
                        .Element(strNamespace + "Contents")?
                        .Element(strNamespace + "ParameterRequests")?
                        .Elements(strNamespace + "ParameterRequest")
                        .Select(param => param.Attribute("parameterName")?.Value)
                        .Where(param => param != null)
                        .ToList();
                }
            }

            return dcpModel;
        }


        private static List<ExlTraceRequestModel> ConvertTraceModelToList(List<Dictionary<string, string>> x_lstData)
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
            return lstTrace;
        }

        private static List<ExlEventModel> ConvertEventModelToList(List<Dictionary<string, string>> x_lstData)
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


        public static void WriteTraceRequestToJson_UsingJsonWriter(List<ExlTraceRequestModel> x_objTraces, string x_strJsonPath)
        {
            // Create a JsonTextWriter to write to the specified JSON file
            using (StreamWriter streamWriter = new StreamWriter(x_strJsonPath))
            using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
            {
                jsonWriter.Formatting = Newtonsoft.Json.Formatting.Indented; // Set formatting for readability

                // Start writing the JSON structure
                jsonWriter.WriteStartObject(); // Start root object

                jsonWriter.WritePropertyName(NS1_TRACEREQUESTS);
                jsonWriter.WriteStartObject(); // Start the tracerequests object

                jsonWriter.WritePropertyName("@" + NS1_PROPERTY);
                jsonWriter.WriteValue(NS1_NAMESPACE); // Add the namespace

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
        }

        public static void WriteEventRequestsToJson_UsingJsonWriter(List<ExlEventModel> x_lstEventRequest, string x_strJsonPath)
        {
            // Tạo StreamWriter để ghi dữ liệu ra file
            using (StreamWriter strWriter = new StreamWriter(x_strJsonPath))
            {
                // Tạo JsonWriter để ghi dữ liệu dưới định dạng JSON
                using (JsonWriter jsonWriter = new JsonTextWriter(strWriter))
                {
                    jsonWriter.Formatting = Newtonsoft.Json.Formatting.Indented; // Định dạng JSON dễ đọc

                    // Bắt đầu viết JSON
                    jsonWriter.WriteStartObject(); // Mở dấu ngoặc của object chính

                    jsonWriter.WritePropertyName(NS1_EVENTREQUESTS);
                    jsonWriter.WriteStartObject();
                    jsonWriter.WritePropertyName('@' + NS1_PROPERTY);
                    jsonWriter.WriteValue(NS1_NAMESPACE);


                    jsonWriter.WritePropertyName("eventrequest"); // Tên của property chính (có @)
                    jsonWriter.WriteStartArray(); // Mở dấu ngoặc của mảng eventrequests

                    // Duyệt qua danh sách các sự kiện
                    foreach (ExlEventModel objEventRequest in x_lstEventRequest)
                    {
                        jsonWriter.WriteStartObject(); // Mở dấu ngoặc cho mỗi eventrequest

                        // Viết thuộc tính eventid và eventname với ký tự @
                        jsonWriter.WritePropertyName("@eventid");
                        jsonWriter.WriteValue(objEventRequest.EventID);

                        jsonWriter.WritePropertyName("@eventname");
                        jsonWriter.WriteValue(objEventRequest.EventName);

                        // Viết danh sách parameters (mảng)
                        jsonWriter.WritePropertyName("parameters");
                        jsonWriter.WriteStartObject(); // Mở object parameters

                        jsonWriter.WritePropertyName("parameter"); // Tên của mảng parameter
                        jsonWriter.WriteStartArray(); // Bắt đầu mảng parameter

                        foreach (string strParameterID in objEventRequest.ParametersID)
                        {
                            jsonWriter.WriteStartObject(); // Mở dấu ngoặc cho mỗi parameter
                            jsonWriter.WritePropertyName("@paramid");
                            jsonWriter.WriteValue(strParameterID);
                            jsonWriter.WriteEndObject(); // Đóng dấu ngoặc cho parameter
                        }

                        jsonWriter.WriteEndArray(); // Đóng mảng parameter
                        jsonWriter.WriteEndObject(); // Đóng dấu ngoặc của mảng parameters
                        jsonWriter.WriteEndObject(); // 
                    }

                    jsonWriter.WriteEndArray(); // 
                    jsonWriter.WriteEndObject(); //

                    jsonWriter.WriteEndObject(); // 
                }
            }
        }

        public static void WriteEvenTriggerToJson_UsingJsonWriter(List<ExlEventModel> lstEventModels, string x_strJsonPath)
        {
            using (StreamWriter file = File.CreateText(x_strJsonPath))
            {
                using (JsonWriter writer = new JsonTextWriter(file))
                {
                    // Set formatting options for pretty printing
                    writer.Formatting = Newtonsoft.Json.Formatting.Indented;

                    // Start the main JSON object
                    writer.WriteStartObject();
                    writer.WritePropertyName("ns1:eventtriggers"); // Property name for eventtriggers
                    writer.WriteStartObject(); // Start the eventtriggers object
                    writer.WritePropertyName("@xmlns:ns1"); // Namespace property
                    writer.WriteValue("urn:ConfigFileSchema"); // Namespace value

                    // Write the eventtrigger array
                    writer.WritePropertyName("eventtrigger"); // Property name for eventtrigger
                    writer.WriteStartArray(); // Start of the array for eventtrigger objects

                    // Iterate through each event model
                    foreach (var eventModel in lstEventModels)
                    {
                        writer.WriteStartObject(); // Start of the eventtrigger object
                        writer.WritePropertyName($"@eventid"); // Property name with prefix '@'
                        writer.WriteValue(eventModel.EventID); // Write event ID
                        writer.WritePropertyName($"@eventname"); // Property name with prefix '@'
                        writer.WriteValue(eventModel.EventName); // Write event name

                        // Start the extension object
                        writer.WritePropertyName("extension");
                        writer.WriteStartObject(); // Start of the extension object

                        // Start the detectconditions object
                        writer.WritePropertyName("detectconditions");
                        writer.WriteStartObject(); // Start of the detectconditions object
                        writer.WritePropertyName("@andor"); // Property name with prefix '@'
                        writer.WriteValue(eventModel.AndOr); // Write AndOr value

                        // Start the parameter array
                        writer.WritePropertyName("parameter");
                        writer.WriteStartArray(); // Start of the parameter array

                        // Add all parameters based on event model
                        foreach (var paramId in eventModel.ParametersID)
                        {
                            writer.WriteStartObject(); // Start of the parameter object
                            writer.WritePropertyName("@paramid"); // Property name with prefix '@'
                            writer.WriteValue(paramId); // Write the parameter ID
                            writer.WritePropertyName("@equation"); // Property name with prefix '@'
                            writer.WriteValue(eventModel.Equation); // Write equation value
                            writer.WritePropertyName("@value"); // Property name with prefix '@'
                            writer.WriteValue(eventModel.Value); // Write value
                            writer.WriteEndObject(); // End of the parameter object
                        }

                        // End the parameter array
                        writer.WriteEndArray();
                        writer.WriteEndObject(); // End of the detectconditions object
                        writer.WriteEndObject(); // End of the extension object
                        writer.WriteEndObject(); // End of the eventtrigger object
                    }

                    // End the eventtrigger array
                    writer.WriteEndArray();
                    writer.WriteEndObject(); // End the eventtriggers object
                    writer.WriteEndObject(); // End the main JSON object
                }
            }
        }

        public static void WriteDCPToJson_UsingJsonWriter(ExlDCPModel x_objDCP, string x_strJsonPath)
        {
            using (StreamWriter objStreamWriter = new StreamWriter(x_strJsonPath))
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
                jsonWriter.WritePropertyName("DCPTables");
                jsonWriter.WriteStartObject();
                jsonWriter.WritePropertyName("@xmlns:xsi");
                jsonWriter.WriteValue("http://www.w3.org/2001/XMLSchema-instance");
                jsonWriter.WritePropertyName("@xmlns:xsd");
                jsonWriter.WriteValue("http://www.w3.org/2001/XMLSchema");
                jsonWriter.WritePropertyName("@xmlns");
                jsonWriter.WriteValue("urn:ConfigFileSchema");

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

    }
}
