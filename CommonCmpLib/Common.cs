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

using CommonCmpLib.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CommonCmpLib
{

    public static class Common
    {
        #region Types
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

        public static void ListParameterToXml()
        {
            //StringBuilder sb;
            //// Create an example ParametersModel instance
            //var parameterModel = new ParameterModel
            //{
            //    ParameterID = "1",
            //    ParameterName = "Tem",
            //    Locator = "A1",
            //    Unit = "Cel",
            //    Type = "Sen",
            //    Array = "False",
            //    Function = "Mea",
            //    Arg = "None",
            //    Extension = new ParameterExtentionModel
            //    {
            //        DataSource = new ParameterDataSource
            //        {
            //            Sourcetype = "Memory",
            //            Memory = new ParameterMemory
            //            {
            //                MemoryName = "Mem1",
            //                Offset = "10",
            //                SourceType = "TypeA",
            //                SourceArray = "Array1"
            //            },
            //            Finds = null,
            //        }
            //    }
            //};
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

        public static JsonXmlConversionResult ConvertXmlToJson_Parameter(string x_strXmlPath, string x_strJsonPath)
        {
            XmlDocument xmlDoc;
            string strJson;
            JsonXmlConversionResult objResult = new JsonXmlConversionResult();

            try
            {
                // Load XML file
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

                objResult.Message = $"Conversion successful! JSON saved to: {x_strJsonPath}";
            }
            catch (Exception ex)
            {
                // If an error occurs
                objResult.IsSuccess = false;
                objResult.Message = $"Error during conversion: {ex.Message}";
            }
            return objResult;
        }

        public static JsonXmlConversionResult ConvertJsonToXml_Parameter(string x_strJsonPath, string x_strXmlPath)
        {
            XmlDocument xmlDoc;
            string strJsonData;
            JsonXmlConversionResult objResult = new JsonXmlConversionResult();
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
                objResult.Message = $"Conversion successful! XML saved to: {x_strXmlPath}";
            }
            catch (Exception ex)
            {
                // If an error occurs
                objResult.IsSuccess = false;
                objResult.Message = $"Error during conversion: {ex.Message}";
            }
            return objResult;
        }

    }
}
