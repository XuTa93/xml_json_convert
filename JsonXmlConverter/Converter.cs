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

using JsonXmlConverter;
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
    public static class Converter
    {

        /// <summary>
        /// Converts an XML file to JSON format if the file .
        /// </summary>
        /// <returns>Returns a JsonXmlConversionResult indicating the success status and message of the conversion.</returns>
        public static ConvertResult ConvertXmlToJson_Parameter(string x_strXmlPath, string x_strJsonPath)
        {
            // Declare variables at the beginning
            ConvertResult objResult;
            DataType datatType;
            XmlDocument xmlDoc;
            string strJson;
            string strFileName;
            objResult = new ConvertResult();
            strFileName = Path.GetFileName(x_strXmlPath);

            // Validate the input file to check if it is of type Parameter
            datatType = ValidateType(x_strXmlPath);
            if (datatType != DataType.Parameter)
            {
                objResult.IsSuccess = false;
                objResult.Message = $"{datatType} type of the provided file.";
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
                objResult.Message = $"{strFileName} [ {datatType} Type ]  Converted to JSON : {x_strJsonPath}";
            }
            catch (Exception xmlEx)
            {
                StackTrace objStackTrace = new StackTrace();
                string strMethodName = objStackTrace.GetFrame(0).GetMethod().Name;
                // If an error occurs
                objResult.IsSuccess = false;
                objResult.Message = $"{strFileName} [ {datatType} Type ] XML Error during conversion: {HandleException(xmlEx, strMethodName)}";
            }
            return objResult;
        }

        private static ConvertResult XmlToJson_Common(string x_strXmlPath, string x_strJsonPath, DataType x_dataType, Func<string, string, DataType, ConvertResult> x_CallBack)
        {
            ConvertResult objResult;
            string strFileName;

            strFileName = Path.GetFileName(x_strXmlPath);
            objResult = new ConvertResult();

            try
            {
                objResult = x_CallBack(x_strXmlPath, x_strJsonPath, x_dataType);

                if (objResult.IsSuccess == true)
                {
                    objResult.Message = $"{strFileName} [ {x_dataType} Type ]  Converted to JSON : {x_strJsonPath}";
                }
                else
                {
                    objResult.Message = $"{strFileName} [ {x_dataType} Type ] : {objResult.Message}";
                }

            }
            catch (Exception xmlEx)
            {
                StackTrace objStackTrace = new StackTrace();
                string strMethodName = objStackTrace.GetFrame(0).GetMethod().Name;
                objResult.IsSuccess = false;
                objResult.Message = $"{strFileName} [ {x_dataType} Type ] XML Error during conversion: {HandleException(xmlEx, strMethodName)}";
            }
            return objResult;
        }
        public static ConvertResult ConvertXmlToJson_TraceRequest(string x_strXmlPath, string x_strJsonPath)
        {
            DataType dataType = ValidateType(x_strXmlPath);
            return XmlToJson_Common(x_strXmlPath, x_strJsonPath, dataType, XmlServices.XmlToJson_TraceRequest);
        }

        public static ConvertResult ConvertXmlToJson_EventTrigger(string x_strXmlPath, string x_strJsonPath)
        {
            DataType dataType;

            // Validate the input file to check if it is of type Parameter
            dataType = ValidateType(x_strXmlPath);
            return XmlToJson_Common(x_strXmlPath, x_strJsonPath, dataType, XmlServices.XmlToJson_EventTrigger);
        }

        public static ConvertResult ConvertXmlToJson_EventRequest(string x_strXmlPath, string x_strJsonPath)
        {
            DataType dataType;

            // Validate the input file to check if it is of type Parameter
            dataType = ValidateType(x_strXmlPath);
            return XmlToJson_Common(x_strXmlPath, x_strJsonPath, dataType, XmlServices.XmlToJson_EventRequest);
        }

        /// <summary>
        /// Converts a JSON file to XML format if the file is of type Parameter.
        /// </summary>
        /// <returns>Returns a JsonXmlConversionResult indicating the success status and message of the conversion.</returns>
        public static ConvertResult ConvertJsonToXml(string x_strJsonPath, string x_strXmlPath)
        {
            // Declare variables at the beginning
            DataType converType;
            XmlDocument xmlDoc;
            string strJsonData;
            ConvertResult objResult = new ConvertResult();

            // Validate the input file to check if it is of type Parameter
            converType = ValidateType(x_strJsonPath);
            if (converType == DataType.Unknown)
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
        public static DataType ValidateType(string x_strFilePath)
        {
            // Declare variable for file content at the beginning
            string strFileContent;

            // Return Unknown if the file does not exist
            if (File.Exists(x_strFilePath) == false)
            {
                return DataType.Unknown;
            }

            // Load the file content
            strFileContent = File.ReadAllText(x_strFilePath);

            // Determine the file type based on the content
            if (strFileContent.Contains(DEFINE.NS1_PARAMETERS))
            {
                return DataType.Parameter;
            }
            else if (strFileContent.Contains(DEFINE.NS1_TRACEREQUESTS))
            {
                return DataType.TraceRequest;
            }
            else if (strFileContent.Contains(DEFINE.NS1_EVENTTRIGGERS))
            {
                return DataType.EventTrigger;
            }
            else if (strFileContent.Contains(DEFINE.NS1_EVENTREQUESTS))
            {
                return DataType.EventRequest;
            }
            else if (strFileContent.Contains(DEFINE.DCPTABLES))
            {
                return DataType.DataCollectionPlan;
            }

            // If none of the above matches, return Unknown
            return DataType.Unknown;
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
    }
}
