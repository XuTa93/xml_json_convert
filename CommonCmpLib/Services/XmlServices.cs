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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace CommonCmpLib
{
    public static class XmlServices
    {
        /// <summary>
        /// Converts a list of parameters stored in dictionaries into an XML format and saves it to a file.
        /// </summary>
        public static string CreateParameterToXml(List<Dictionary<string, string>> x_lstData, string x_strOutFolder)
        {
            string strFilePath = Path.Combine(x_strOutFolder, "parameters.xml");
            string strError = "Checking";

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
                        xmlWriter.WriteStartElement("ns1", "parameters", DEFINE.NS1_NAMESPACE);

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
                    strError = "";
                }
            }
            catch (Exception objEx)
            {
                strError = objEx.Message;
            }
            return strError;
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
                        xmlWriter.WriteStartElement("DCPTable", "");

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


        // chưa đổi comment
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
    }
}
