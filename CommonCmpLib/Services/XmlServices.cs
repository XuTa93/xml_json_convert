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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;

namespace CommonCmpLib
{
    public static class XmlServices
    {
        #region Types
        private static readonly XmlWriterSettings m_objXmlSettings = new XmlWriterSettings
        {
            Indent = true,              // Format the XML with indentation
            OmitXmlDeclaration = true, // Include XML declaration
        };
        #endregion Types

        #region Methods
        /// <summary>
        /// Converts a list of parameters stored in dictionaries into an XML format and saves it to a file.
        /// </summary>
        /// <returns>A string indicating success or an error message if an exception occurs.</returns>
        public static string GenerateParameterToXml(List<Dictionary<string, string>> x_lstData, string x_strOutFolder)
        {
            string strFilePath;
            string strError;
            string strXml;

            try
            {
                strError = "Checking";
                strFilePath = Path.Combine(x_strOutFolder, "parameters.xml");

                // Generate XML string from data
                strXml = GenerateParameterXmlString(x_lstData);

                // Save the generated XML string to the specified file path and return any potential save errors
                return SaveXmlToFile(strXml, strFilePath);
            }
            catch (Exception objEx) // Catch all other exceptions
            {
                StackTrace objStackTrace = new StackTrace();
                string strMethodName = objStackTrace.GetFrame(0).GetMethod().Name;
                strError = Convert.HandleException(objEx, strMethodName); // Handle the exception and return the error message
            }

            return strError;
        }

        /// <summary>
        /// Generate XML string from data
        /// </summary>
        /// <returns></returns>
        private static string GenerateParameterXmlString(List<Dictionary<string, string>> x_lstData)
        {
            using (StringWriter strWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(strWriter, m_objXmlSettings))
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

                return strWriter.ToString();
            }
        }

        /// <summary>
        /// Creates an XML file representing trace requests based on the provided list of data dictionaries.
        /// </summary>
        /// /// <returns>A string indicating success or an error message if an exception occurs.</returns>
        public static string GenerateTraceToXml(List<Dictionary<string, string>> x_lstData, string x_strOutFolder)
        {
            string strError;
            string strFilePath;
            string strXml;
            List<ExlTraceRequestModel> lstTrace;

            try
            {
                strError = "Checking";
                strFilePath = Path.Combine(x_strOutFolder, "TraceRequest.xml");

                // Convert the list of data dictionaries to a list of ExlTraceRequestModel objects
                lstTrace = ConvertTraceModelToList(x_lstData);

                // Generate the XML content as a string from the list of trace request models
                strXml = GenerateTraceXmlString(lstTrace);

                // Save the generated XML string to the specified file path and return any potential save errors
                return SaveXmlToFile(strXml, strFilePath);
            }
            catch (Exception objEx) // Catch all other exceptions
            {
                StackTrace objStackTrace = new StackTrace();
                string strMethodName = objStackTrace.GetFrame(0).GetMethod().Name;
                strError = Convert.HandleException(objEx, strMethodName); // Handle the exception and return the error message
            }

            return strError;
        }

        /// <summary>
        /// Generate XML string from data
        /// </summary>
        /// <returns></returns>
        private static string GenerateTraceXmlString(List<ExlTraceRequestModel> x_lstTrace)
        {
            using (StringWriter strWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(strWriter, m_objXmlSettings))
                {
                    // Start writing the XML document
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("ns1", "tracerequests", "urn:ConfigFileSchema");

                    foreach (ExlTraceRequestModel objTrace in x_lstTrace)
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

                return strWriter.ToString();
            }
        }


        /// <summary>
        /// Creates an XML file representing event triggers based on the provided event requests.
        /// </summary>
        /// /// <returns>A string indicating success or an error message if an exception occurs.</returns>
        public static string GenerateEventTriggerToXml(List<Dictionary<string, string>> x_lstData, string x_strOutFolder)
        {
            string strError;
            string strEvtTgPath;
            string strXmlEvtTg;
            List<ExlEventModel> lstEvent;

            try
            {
                strError = "Checking";
                strEvtTgPath = Path.Combine(x_strOutFolder, "EventTrigger.xml");

                lstEvent = ConvertEventModelToList(x_lstData); // Convert data dictionaries to event models

                strXmlEvtTg = GenerateEventTriggersXmlString(lstEvent); // Generate XML string for EventTrigger
                strError = SaveXmlToFile(strXmlEvtTg, strEvtTgPath); // Save EventTrigger XML and capture errors

                return strError;
            }
            catch (Exception objEx) // Catch all other exceptions
            {
                StackTrace objStackTrace = new StackTrace();
                string strMethodName = objStackTrace.GetFrame(0).GetMethod().Name;
                strError = Convert.HandleException(objEx, strMethodName); // Handle the exception and return the error message
            }

            return strError;
        }
        
        /// <summary>
        /// Creates an XML file representing event triggers based on the provided event requests.
        /// </summary>
        /// /// <returns>A string indicating success or an error message if an exception occurs.</returns>
        public static string GenerateEventRequestToXml(List<Dictionary<string, string>> x_lstData, string x_strOutFolder)
        {
            string strError;
            string strEvtRqPath;
            string strXmlEvtRq;
            List<ExlEventModel> lstEvent;

            try
            {
                strError = "Checking";
                strEvtRqPath = Path.Combine(x_strOutFolder, "EventRequest.xml");

                lstEvent = ConvertEventModelToList(x_lstData); // Convert data dictionaries to event models

                strXmlEvtRq = GenerateEventRequestsXmlString(lstEvent); // Generate XML string for EventRequest
                strError = SaveXmlToFile(strXmlEvtRq, strEvtRqPath); // Save EventRequest XML and capture errors

                return strError;
            }
            catch (Exception objEx) // Catch all other exceptions
            {
                StackTrace objStackTrace = new StackTrace();
                string strMethodName = objStackTrace.GetFrame(0).GetMethod().Name;
                strError = Convert.HandleException(objEx, strMethodName); // Handle the exception and return the error message
            }

            return strError;
        }

        /// <summary>
        /// Generate XML string from data
        /// </summary>
        /// <returns></returns>
        private static string GenerateEventTriggersXmlString(List<ExlEventModel> x_lstEvent)
        {
            using (StringWriter strWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(strWriter, m_objXmlSettings))
                {
                    // Start writing the XML document
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("ns1", "eventtriggers", "urn:ConfigFileSchema");

                    foreach (ExlEventModel objEventTrigger in x_lstEvent)
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
                return strWriter.ToString();
            }
        }

        /// <summary>
        /// Generate XML string from data
        /// </summary>
        /// <returns></returns>
        private static string GenerateEventRequestsXmlString(List<ExlEventModel> x_lstEvent)
        {
            using (StringWriter strWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(strWriter, m_objXmlSettings))
                {
                    // Start writing the XML document
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("ns1", "eventrequests", "urn:ConfigFileSchema");

                    foreach (ExlEventModel objEventRequest in x_lstEvent)
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
                return strWriter.ToString();
            }
        }

        /// <summary>
        /// Creates and saves XML files for each DCP plan based on the provided list of dictionaries.
        /// </summary>
        /// /// <returns>A string indicating success or an error message if an exception occurs.</returns>
        public static string GenerateDCPXml(List<Dictionary<string, string>> x_lstData, string x_strOutFolder)
        {
            string strError;
            string strFilePath;
            string strFolderPath;
            string strSaveError;
            string strXml;
            List<ExlDCPModel> lstData;

            try
            {
                strError = "Checking"; // Initial check status

                // Convert the input data to a list of DCP models
                lstData = ConvertToDCPModelList(x_lstData);

                // Iterate through each DCP model and generate corresponding XML files
                foreach (var objDCP in lstData)
                {
                    // Define the file path for the XML file based on the plan name
                    strFolderPath = Path.Combine( x_strOutFolder, "DCP");
                    
                    // Create the directory if it doesn't exist
                    if (Directory.Exists(strFolderPath) == false)
                    {
                        Directory.CreateDirectory(strFolderPath);
                    }
                    strFilePath = Path.Combine(strFolderPath, $"{objDCP.PlanName}.xml");

                    // Generate the XML string from the DCP model
                    strXml = GenerateDCPXmlString(objDCP);

                    // Save the XML string to the file
                    strSaveError = SaveXmlToFile(strXml, strFilePath);

                    // Check for errors during file saving
                    if (string.IsNullOrEmpty(strSaveError))
                    {
                        if (strError != "Checking")
                        {
                            // Append error messages for each plan if any issues occur
                            strError += $"{objDCP.PlanName} : {strSaveError}.";
                        }
                    }
                }
                // No Error
                if (strError == "Checking")
                {
                    strError = string.Empty;
                }
            }
            catch (Exception objEx) // Handle any exceptions that occur
            {
                StackTrace objStackTrace = new StackTrace();
                string strMethodName = objStackTrace.GetFrame(0).GetMethod().Name;
                strError = Convert.HandleException(objEx, strMethodName); // Handle the exception and return the error message
            }

            return strError;
        }

        /// <summary>
        /// Generate DCP DataCollectionPlan string from data
        /// </summary>
        /// <returns>Error Satus</returns>
        private static string GenerateDCPXmlString(ExlDCPModel x_objDCP)
        {
            using (StringWriter strWriter = new StringWriter())
            {
                strWriter.WriteLine("<?xml version=\"1.0\"?>"); // Manually write XML declaration
                using (XmlWriter xmlWriter = XmlWriter.Create(strWriter, m_objXmlSettings))
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
                    xmlWriter.WriteElementString("Macname", x_objDCP.MachineName);

                    // Create DataCollectionPlan element with attributes
                    xmlWriter.WriteStartElement("DataCollectionPlan");
                    xmlWriter.WriteAttributeString("id", x_objDCP.PlanID);
                    xmlWriter.WriteAttributeString("name", x_objDCP.PlanName);
                    xmlWriter.WriteAttributeString("Description", x_objDCP.Description);
                    xmlWriter.WriteAttributeString("monitorIntervalInSeconds", ""); // Empty value for monitorIntervalInSeconds

                    // StartEvent element
                    xmlWriter.WriteStartElement("StartEvent");
                    xmlWriter.WriteStartElement("EventRequests");
                    xmlWriter.WriteStartElement("EventRequest");
                    xmlWriter.WriteAttributeString("eventId", x_objDCP.StartEvent); // Set eventId for StartEvent
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
                    xmlWriter.WriteAttributeString("eventId", x_objDCP.EndEvent); // Set eventId for EndEvent
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
                    foreach (string strPara in x_objDCP.ParametersID)
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
                return strWriter.ToString();
            }

        }

        /// <summary>
        /// Converts a list of dictionaries representing trace data into a list of ExlTraceRequestModel objects,
        /// </summary>
        /// <returns>A list of ExlTraceRequestModel objects grouped by trace attributes.</returns>
        private static List<ExlTraceRequestModel> ConvertTraceModelToList(List<Dictionary<string, string>> x_lstData)
        {
            // Convert Dictionary data to a list of Trace grouped by ParameterID
            var lstTrace = x_lstData
                .GroupBy(obj => new
                {
                    // Group by TraceID, TraceName, Description, StartOn, StopOn
                    TraceID = obj.ContainsKey(DEFINE.TraceID) ? obj[DEFINE.TraceID] : string.Empty,
                    TraceName = obj.ContainsKey(DEFINE.TraceName) ? obj[DEFINE.TraceName] : string.Empty,
                    Description = obj.ContainsKey(DEFINE.Description) ? obj[DEFINE.Description] : string.Empty,
                    StartOn = obj.ContainsKey(DEFINE.StartOn) ? obj[DEFINE.StartOn] : string.Empty,
                    StopOn = obj.ContainsKey(DEFINE.StopOn) ? obj[DEFINE.StopOn] : string.Empty
                })
                .Select(objTrace => new ExlTraceRequestModel
                {
                    // Create a new ExlTraceRequestModel object for each group
                    TraceID = objTrace.Key.TraceID,
                    TraceName = objTrace.Key.TraceName,
                    Description = objTrace.Key.Description,
                    StartOn = objTrace.Key.StartOn,
                    StopOn = objTrace.Key.StopOn,
                    ParametersID = objTrace
                        .Where(par => par.ContainsKey(DEFINE.ParameterID)) // Ensure to only take ParameterID if it exists
                        .Select(lstPar => lstPar[DEFINE.ParameterID]) // Select the ParameterID value
                        .ToList() // Group all ParameterIDs together into a list
                }).ToList(); // Convert the IEnumerable to a List<ExlTraceRequestModel>

            return lstTrace; // Return the final list of ExlTraceRequestModel objects
        }

        /// <summary>
        /// Converts a list of dictionaries representing event data into a list of ExlEventModel objects,
        /// </summary>
        /// <returns>A list of ExlEventModel objects grouped by event attributes.</returns>
        private static List<ExlEventModel> ConvertEventModelToList(List<Dictionary<string, string>> x_lstData)
        {
            // Convert dictionary data to a grouped list of ExlEventModel
            var lstEvent = x_lstData
                .GroupBy(data => new
                {
                    // Group by EventID, EventName, AndOr, Equation, and Value
                    EventID = data.ContainsKey(DEFINE.EventID) ? data[DEFINE.EventID] : null,
                    EventName = data.ContainsKey(DEFINE.EventName) ? data[DEFINE.EventName] : null,
                    AndOr = data.ContainsKey(DEFINE.AndOr) ? data[DEFINE.AndOr] : null,
                    Equation = data.ContainsKey(DEFINE.Equation) ? data[DEFINE.Equation] : null,
                    Value = data.ContainsKey(DEFINE.Value) ? data[DEFINE.Value] : null,
                })
                .Select(objEvt => new ExlEventModel
                {
                    // Create a new ExlEventModel object for each group
                    EventID = objEvt.Key.EventID,
                    EventName = objEvt.Key.EventName,
                    AndOr = objEvt.Key.AndOr,
                    Equation = objEvt.Key.Equation,
                    Value = objEvt.Key.Value,
                    ParametersID = objEvt
                        .Where(par => par.ContainsKey(DEFINE.ParameterID)) // Ensure to only take ParameterID if it exists
                        .Select(lstPar => lstPar[DEFINE.ParameterID]) // Select the ParameterID value
                        .ToList() // Group all ParameterIDs together into a list
                }).ToList(); // Convert the IEnumerable to a List<ExlEventModel>

            return lstEvent; // Return the final list of ExlEventModel objects
        }

        /// <summary>
        /// Converts a list of dictionaries containing DCP data into a list of ExlDCPModel objects,
        /// </summary>
        /// <returns>A list of ExlDCPModel objects grouped by PlanID.</returns>
        private static List<ExlDCPModel> ConvertToDCPModelList(List<Dictionary<string, string>> x_lstData)
        {
            // Group the data by PlanID and create the list of ExlDCPModel
            var lstDCP = x_lstData
                .GroupBy(obj => new
                {
                    // Group by No, MachineName, PlanID, PlanName, Description, StartEvent, EndEvent, and TimeRequest
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
                    // Create a new ExlDCPModel object for each group
                    No = objGroup.Key.No,
                    MachineName = objGroup.Key.MachineName,
                    PlanID = objGroup.Key.PlanID,
                    PlanName = objGroup.Key.PlanName,
                    Description = objGroup.Key.Description,
                    StartEvent = objGroup.Key.StartEvent,
                    EndEvent = objGroup.Key.EndEvent,
                    TimeRequest = objGroup.Key.TimeRequest,
                    ParametersID = objGroup.Select(g => g[DEFINE.ParameterID]).ToList() // Group all ParameterIDs
                })
                .ToList(); // Convert the IEnumerable to a List<ExlDCPModel>

            return lstDCP; // Return the final list of ExlDCPModel objects
        }

        /// <summary>
        /// Save the XML string to a file.
        /// </summary>
        /// <returns>Error message if saving fails, otherwise an empty string.</returns>
        private static string SaveXmlToFile(string x_strXml, string x_strFilePath)
        {
            // Validate XML string
            if (string.IsNullOrEmpty(x_strXml))
            {
                return "XML value null";
            }

            try
            {
                File.WriteAllText(x_strFilePath, x_strXml);
                return ""; // Return empty string if successful
            }
            catch (Exception objEx)
            {
                StackTrace objStackTrace = new StackTrace();
                string strMethodName = objStackTrace.GetFrame(0).GetMethod().Name;
                return Convert.HandleException(objEx, strMethodName);
            }
        }

        #endregion Methods
    }
}
