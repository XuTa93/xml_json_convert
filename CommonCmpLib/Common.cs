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
using System.Text;
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

        public static string  DictionaryDataXml(ExcelProcessResult x_objResult)
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
                    CreateTraceToXml(x_objResult.Models);
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
            StringBuilder strBuilder = new StringBuilder();

            // Create XmlWriterSettings with OmitXmlDeclaration = true
            XmlWriterSettings objXmlSettings = new XmlWriterSettings
            {
                Indent = true,              // Format the XML with indentation
                OmitXmlDeclaration = true,  // Omit the XML declaration
            };

            try
            {
                using (XmlWriter writer = XmlWriter.Create(strBuilder, objXmlSettings))
                {
                    // Start the XML document
                    writer.WriteStartDocument();
                    writer.WriteStartElement("ns1", "parameters", "urn:ConfigFileSchema");

                    // Iterate over the list of parameter dictionaries
                    foreach (Dictionary<string, string> objPara in x_lstData)
                    {
                        // Create parameter element with attributes, checking if the key exists
                        writer.WriteStartElement("parameter");
                        writer.WriteAttributeString("paramid", objPara.ContainsKey(DEFINE.ParameterID) ? objPara[DEFINE.ParameterID] : string.Empty);
                        writer.WriteAttributeString("paramname", objPara.ContainsKey(DEFINE.ParameterName) ? objPara[DEFINE.ParameterName] : string.Empty);
                        writer.WriteAttributeString("locator", objPara.ContainsKey(DEFINE.Locator) ? objPara[DEFINE.Locator] : string.Empty);
                        writer.WriteAttributeString("unit", objPara.ContainsKey(DEFINE.Unit) ? objPara[DEFINE.Unit] : string.Empty);
                        writer.WriteAttributeString("type", objPara.ContainsKey(DEFINE.Type) ? objPara[DEFINE.Type] : string.Empty);
                        writer.WriteAttributeString("array", objPara.ContainsKey(DEFINE.Array) ? objPara[DEFINE.Array] : string.Empty);
                        writer.WriteAttributeString("function", objPara.ContainsKey(DEFINE.Function) ? objPara[DEFINE.Function] : string.Empty);
                        writer.WriteAttributeString("arg", objPara.ContainsKey(DEFINE.Arg) ? objPara[DEFINE.Arg] : string.Empty);

                        // Start the extension element
                        writer.WriteStartElement("extension");

                        // Start the datasource element with sourcetype attribute
                        writer.WriteStartElement("datasource");
                        writer.WriteAttributeString("sourcetype", objPara.ContainsKey(DEFINE.Sourcetype) ? objPara[DEFINE.Sourcetype] : string.Empty);

                        // Start the memory element with attributes
                        writer.WriteStartElement("memory");
                        writer.WriteAttributeString("memname", objPara.ContainsKey(DEFINE.MemoryName) ? objPara[DEFINE.MemoryName] : string.Empty);
                        writer.WriteAttributeString("offset", objPara.ContainsKey(DEFINE.Offset) ? objPara[DEFINE.Offset] : string.Empty);
                        writer.WriteAttributeString("stype", objPara.ContainsKey(DEFINE.SourceType) ? objPara[DEFINE.SourceType] : string.Empty);
                        writer.WriteAttributeString("sarray", objPara.ContainsKey(DEFINE.Array) ? objPara[DEFINE.Array] : string.Empty);

                        // Close the memory element
                        writer.WriteEndElement();

                        // Write the fins element (self-closing)
                        writer.WriteElementString("fins", string.Empty);

                        // Close the datasource element
                        writer.WriteEndElement();

                        // Close the extension element
                        writer.WriteEndElement();

                        // Close the parameter element
                        writer.WriteEndElement();
                    }

                    // Close the parameters element
                    writer.WriteEndElement();

                    // End the XML document
                    writer.WriteEndDocument();
                }

                // Convert the StringBuilder content to a string
                string xmlString = strBuilder.ToString();

                // Define the file path for the XML file and save it
                string filePath = "parameters.xml";
                File.WriteAllText(filePath, xmlString);

                // Convert XML to JSON and back (assuming methods exist for this)
                ConvertXmlToJson_Parameter(filePath, "parameters.json");
                ConvertJsonToXml_Parameter("parameters.json", filePath);
            }
            catch (Exception ex)
            {
                // Catch any exceptions and output the error message
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static void CreateTraceToXml(List<Dictionary<string, string>> x_lstData)
        {
            // Chuyển đổi dữ liệu Dictionary thành danh sách Trace với nhóm ParameterID
            var lstTrace = x_lstData
                .GroupBy(obj => new
                {
                    TraceID = obj.ContainsKey(DEFINE.TraceID) ? obj[DEFINE.TraceID] : null,
                    TraceName = obj.ContainsKey(DEFINE.TraceName) ? obj[DEFINE.TraceName] : null,
                    Description = obj[DEFINE.Description],
                    StartOn = obj[DEFINE.StartOn],
                    StopOn = obj[DEFINE.StopOn]
                })
                .Select(objTrace => new ExlTraceRequestModel
                {
                    TraceID = objTrace.Key.TraceID,
                    TraceName = objTrace.Key.TraceName,
                    Description = objTrace.Key.Description,
                    StartOn = objTrace.Key.StartOn,
                    StopOn = objTrace.Key.StopOn,
                    ParametersID = objTrace.Select(lstPar => lstPar[DEFINE.ParameterID]).ToList() // Nhóm tất cả ParameterID lại
        }).ToList();

            StringBuilder strBuilder = new StringBuilder();

            // Create XmlWriterSettings with OmitXmlDeclaration = true
            XmlWriterSettings objXmlSettings = new XmlWriterSettings
            {
                Indent = true,              // Format the XML with indentation
                OmitXmlDeclaration = false,
                Encoding = Encoding.UTF8,
            };

            // Create the XML file with UTF-8 encoding
            using (XmlWriter xmlWriter = XmlWriter.Create(strBuilder, objXmlSettings))
            {
                // Bắt đầu viết tài liệu XML
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("ns1", "tracerequests", "urn:ConfigFileSchema");

                foreach (ExlTraceRequestModel objTrace in lstTrace)
                {
                    // Bắt đầu phần tử tracerequest
                    xmlWriter.WriteStartElement("tracerequest");

                    xmlWriter.WriteAttributeString("traceid", objTrace.TraceID);
                    xmlWriter.WriteAttributeString("tracename", objTrace.TraceName);
                    xmlWriter.WriteAttributeString("description", objTrace.Description);

                    // Viết phần tử triggers
                    xmlWriter.WriteStartElement("triggers");
                    xmlWriter.WriteElementString("starton", objTrace.StartOn);
                    xmlWriter.WriteElementString("stopon", objTrace.StopOn);
                    xmlWriter.WriteEndElement(); // Kết thúc triggers

                    // Viết phần tử parameters
                    xmlWriter.WriteStartElement("parameters");

                    //for (int i = 0; i < objTrace.ParametersID.Count; i++)
                    //{
                    //    xmlWriter.WriteStartElement("parameter"); // Mở phần tử parameter
                    //    xmlWriter.WriteAttributeString("paramid", objTrace.ParametersID[i]);
                    //    xmlWriter.WriteEndElement(); // Kết thúc parameter
                    //}
                    foreach (string strPara in objTrace.ParametersID)
                    {
                        xmlWriter.WriteStartElement("parameter"); // Mở phần tử parameter
                        xmlWriter.WriteAttributeString("paramid", strPara);
                        xmlWriter.WriteEndElement(); // Kết thúc parameter
                    }

                    xmlWriter.WriteEndElement(); // Kết thúc parameters

                    // Kết thúc phần tử tracerequest
                    xmlWriter.WriteEndElement();
                }

                // Kết thúc phần tử tracerequests
                xmlWriter.WriteEndElement();

                // Kết thúc tài liệu XML
                xmlWriter.WriteEndDocument();
            }

            // Convert the StringBuilder content to a string
            string xmlString = strBuilder.ToString();

            // Define the file path for the XML file and save it
            string filePath = "parameters.xml";
            File.WriteAllText(filePath, xmlString);

            // Convert XML to JSON and back (assuming methods exist for this)
            ConvertXmlToJson_Parameter(filePath, "traces.json");
            ConvertJsonToXml_Parameter("traces.json", filePath);
        }

        public static void CreateDCPXml(List<Dictionary<string, string>> x_lstData)
        {
            string xmlString;
            List<ExlDCPModel> lstData = ConvertToDCPModelList(x_lstData);
            // StringBuilder to hold the XML content
            StringBuilder strBuilder;

            // Create XmlWriterSettings with indentation and omit XML declaration
            XmlWriterSettings objXmlSettings = new XmlWriterSettings
            {
                Indent = true,              // Format the XML with indentation
                OmitXmlDeclaration = true, // Include XML declaration
            };

            foreach (var objDCP in lstData)
            {
                // Define the file path for the XML file
                string str_filePath = $"{objDCP.PlanName}.xml";
                strBuilder = new StringBuilder();
                using (XmlWriter xmlWriter = XmlWriter.Create(strBuilder, objXmlSettings))
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
                    xmlWriter.WriteAttributeString("id", objDCP.PlanID); // Empty id attribute
                    xmlWriter.WriteAttributeString("name", objDCP.PlanName); // Empty name attribute
                    xmlWriter.WriteAttributeString("Description", objDCP.Description); // Empty Description attribute
                    xmlWriter.WriteAttributeString("monitorIntervalInSeconds", ""); // Empty monitorIntervalInSeconds attribute

                    // StartEvent element
                    xmlWriter.WriteStartElement("StartEvent");
                    xmlWriter.WriteStartElement("EventRequests");
                    xmlWriter.WriteStartElement("EventRequest");
                    xmlWriter.WriteAttributeString("eventId", objDCP.StartEvent); // Empty eventId attribute
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
                    xmlWriter.WriteAttributeString("eventId", objDCP.EndEvent); // Empty eventId attribute
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
                    //xmlWriter.WriteStartElement("ParameterRequest");

                    foreach (string strPara in objDCP.ParametersID)
                    {
                        xmlWriter.WriteStartElement("ParameterRequest"); // Mở phần tử parameter
                        xmlWriter.WriteAttributeString("parameterName", strPara);
                        xmlWriter.WriteEndElement(); // Kết thúc parameter
                    }
                    //xmlWriter.WriteAttributeString("parameterName", objDCP.ParametersID); // Empty parameterName attribute
                    
                    //xmlWriter.WriteEndElement(); // End ParameterRequest
                    xmlWriter.WriteEndElement(); // End ParameterRequests
                    xmlWriter.WriteEndElement(); // End Contents

                    xmlWriter.WriteEndElement(); // End DataCollectionPlan
                    xmlWriter.WriteEndElement(); // End DCPTable
                    xmlWriter.WriteEndElement(); // End DCPTables

                    // End XML document
                    xmlWriter.WriteEndDocument();
                    
                }
                xmlString = strBuilder.ToString();
                // Convert the StringBuilder content to a string
                using (FileStream fs = new FileStream(str_filePath, FileMode.Create, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                {
                    sw.WriteLine("<?xml version=\"1.0\"?>"); // Manually write XML declaration
                    sw.Write(xmlString);
                }
                // Đọc lại tệp XML sau khi lưu
                string xmlContent = File.ReadAllText(str_filePath);
                // Convert XML to JSON and back if needed
                ConvertXmlToJson_Parameter(str_filePath, Path.ChangeExtension(str_filePath, ".json"));
                ConvertJsonToXml_Parameter(Path.ChangeExtension(str_filePath, ".json"), str_filePath);
            }

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

        // Tạo một JsonConverter tùy chỉnh để thêm prefix '@' cho các thuộc tính
        public class CustomJsonConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                if (value == null)
                {
                    writer.WriteNull();
                    return;
                }
                var obj = value.GetType();
                writer.WriteStartObject();

                foreach (var property in obj.GetProperties())
                {
                    writer.WritePropertyName($"@{property.Name}"); // Thêm dấu '@' vào tên thuộc tính
                    var propertyValue = property.GetValue(value);

                    // Kiểm tra nếu thuộc tính là một danh sách và serialize nó
                    if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        // Nếu là danh sách, serialize từng phần tử
                        serializer.Serialize(writer, propertyValue);
                    }
                    else
                    {
                        serializer.Serialize(writer, propertyValue);
                    }
                }

                writer.WriteEndObject();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException("Deserialization not implemented."); // Chỉ thực hiện serialization trong ví dụ này
            }

            public override bool CanConvert(Type objectType)
            {
                return true; // Cho phép tất cả các loại
            }
        }
        // Hàm chuyển đổi XML sang JSON
        public static string ConvertXmlToJson(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            string jsonText = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);
            return jsonText;
        }

        /// <summary>
        /// Validates the file at the specified path and determines its type based on the content.
        /// </summary>
        /// <param name="x_strFilePath">The path of the file to validate.</param>
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
