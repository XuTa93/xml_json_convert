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

        public static void ListParameterToXml()
        {
            StringBuilder sb;


            // Create an example ParametersModel instance
 
            //// Create a StringBuilder to hold the XML content
            //sb = new StringBuilder();
            //// Create XmlWriterSettings with OmitXmlDeclaration = true
            //XmlWriterSettings settings = new XmlWriterSettings
            //{
            //    Indent = true,              // Format the XML with indentation
            //    OmitXmlDeclaration = true,
            //};
            //using (XmlWriter writer = XmlWriter.Create(sb, settings))
            //{
            //    // Bắt đầu tài liệu XML
            //    writer.WriteStartDocument();
            //    writer.WriteStartElement("ns1", "parameters", "urn:ConfigFileSchema");

            //    // Tạo thẻ parameter với các thuộc tính
            //    writer.WriteStartElement("parameter");
            //    writer.WriteAttributeString("paramid", parameterModel.ParameterID);
            //    writer.WriteAttributeString("paramname", parameterModel.ParameterName);
            //    writer.WriteAttributeString("locator", parameterModel.Locator);
            //    writer.WriteAttributeString("unit", parameterModel.Unit);
            //    writer.WriteAttributeString("type", parameterModel.Type);
            //    writer.WriteAttributeString("array", parameterModel.Array);
            //    writer.WriteAttributeString("function", parameterModel.Function);
            //    writer.WriteAttributeString("arg", parameterModel.Arg);

            //    // Bắt đầu phần extension
            //    writer.WriteStartElement("extension");

            //    // Bắt đầu phần datasource với thuộc tính sourcetype
            //    writer.WriteStartElement("datasource");
            //    writer.WriteAttributeString("sourcetype", parameterModel.Extension.DataSource.Sourcetype);

            //    // Bắt đầu phần memory với các thuộc tính
            //    writer.WriteStartElement("memory");
            //    writer.WriteAttributeString("memname", parameterModel.Extension.DataSource.Memory.MemoryName);
            //    writer.WriteAttributeString("offset", parameterModel.Extension.DataSource.Memory.Offset);
            //    writer.WriteAttributeString("stype", parameterModel.Extension.DataSource.Memory.SourceType);
            //    writer.WriteAttributeString("sarray", parameterModel.Extension.DataSource.Memory.SourceArray);

            //    // Đóng thẻ memory
            //    writer.WriteEndElement();

            //    // Thẻ fins tự đóng
            //    writer.WriteElementString("fins", string.Empty);

            //    // Đóng thẻ datasource
            //    writer.WriteEndElement();

            //    // Đóng thẻ extension
            //    writer.WriteEndElement();

            //    // Đóng thẻ parameter
            //    writer.WriteEndElement();

            //    // Đóng thẻ parameters
            //    writer.WriteEndElement();

            //    // Kết thúc tài liệu XML
            //    writer.WriteEndDocument();

            //}
            //string xmlString = sb.ToString();
            //Console.WriteLine(xmlString);

            string filePath = "parameters.xml";
            //File.WriteAllText(filePath, xmlString);

            ConvertXmlToJson_Parameter(filePath, "parameters.json");
            ConvertJsonToXml_Parameter("parameters.json", filePath);

            //var json = XmlToJson(filePath);
        }

        public static void ListTraceToXml()
        {
            string xmlOutput;
            // Khởi tạo đối tượng TraceRequest với các giá trị mẫu
            TraceRequest traceRequest = new TraceRequest
            {
                TraceID = "001",
                TraceName = "Sample Trace",
                Description = "This is a sample trace request.",
                Triggers = new Triggers
                {
                    StartOn = "",
                    StopOn = "2024-10-04T12:00:00"
                },
                Parameters = new Parameters
                {
                    ParameterList = new List<Parameter>
                {
                    new Parameter { ParameterID = "param1" },
            
                }
                }
            };
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,              // Format the XML with indentation
                OmitXmlDeclaration = true,
            };

            // Serialize đối tượng thành XML bằng XmlWriter
            using (StringWriter stringWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
                {
                    // Bắt đầu viết tài liệu XML
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("ns1", "tracerequests", "urn:ConfigFileSchema");

                    // Bắt đầu phần tử tracerequest
                    xmlWriter.WriteStartElement("tracerequest");
                    xmlWriter.WriteAttributeString("traceid", traceRequest.TraceID);
                    xmlWriter.WriteAttributeString("tracename", traceRequest.TraceName);
                    xmlWriter.WriteAttributeString("description", traceRequest.Description);

                    // Viết phần tử triggers
                    xmlWriter.WriteStartElement("triggers");
                    xmlWriter.WriteElementString("starton", traceRequest.Triggers.StartOn);
                    xmlWriter.WriteElementString("stopon", traceRequest.Triggers.StopOn);
                    xmlWriter.WriteEndElement(); // Kết thúc triggers

                    // Viết phần tử parameters
                    xmlWriter.WriteStartElement("parameters");
                   
                    foreach (var parameter in traceRequest.Parameters.ParameterList)
                    {
                        xmlWriter.WriteStartElement("parameter"); // Mở phần tử parameter
                        xmlWriter.WriteAttributeString("paramid", parameter.ParameterID);
                        xmlWriter.WriteEndElement(); // Kết thúc parameter
                    }
                    
                    xmlWriter.WriteEndElement(); // Kết thúc parameters

                    // Kết thúc phần tử tracerequest
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                    // Kết thúc tài liệu XML
                    xmlWriter.WriteEndDocument();
                }
                xmlOutput = stringWriter.ToString();

            }
            // Chuyển đổi XML sang JSON
            string jsonOutput = ConvertXmlToJson(xmlOutput);
            //var valueSet = JsonConvert.DeserializeObject<TraceRequest>(traceRequest);
            var jsonOutpu = JsonConvert.SerializeObject(traceRequest, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine("JSON Output:");
            Console.WriteLine(jsonOutput);
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
