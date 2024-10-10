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

    public static class Convert
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

        #endregion Types

        public static ConvertResult GenerateXmlFile(ExcelProcessResult x_objExlResult,string x_strOutFolder)
        {
            ConvertResult ObjConvertResult;
            string strXmlResult;
            ObjConvertResult = new ConvertResult();
            strXmlResult = string.Empty;
            if (string.IsNullOrEmpty(x_strOutFolder))
            {
                ObjConvertResult.IsSuccess = false;
                ObjConvertResult.Message = $"{x_strOutFolder} Is Empty";
                return ObjConvertResult;
            }
            if (x_objExlResult.IsSuccess == false)
            {
                ObjConvertResult.IsSuccess = false;
                ObjConvertResult.Message = $"{x_objExlResult.Message}";
            }

            switch (x_objExlResult.SheetName)
            {
                case PARAMETER.SHEET_NAME:
                    //Create Parameter Xml
                    strXmlResult = XmlServices.GenerateParameterToXml(x_objExlResult.Models, x_strOutFolder);
                    break;

                case TRACE.SHEET_NAME:
                    //Create TraceRequest Xml
                    XmlServices.GenerateTraceToXml(x_objExlResult.Models, x_strOutFolder);
                    break;

                case EVENT.SHEET_NAME:
                    //Create EventTrigger and EventRequst Xml
                    XmlServices.GenerateEventTriggerToXml(x_objExlResult.Models, x_strOutFolder);
                    break;

                case DCP.SHEET_NAME:
                    //Create DataCollectionPlan Xml
                    XmlServices.GenerateDCPXml(x_objExlResult.Models, x_strOutFolder);
                    break;

                default:
                    ObjConvertResult.IsSuccess = false;
                    ObjConvertResult.Message = $"The {x_objExlResult.SheetName} sheet data is not valid";
                    break;
            }

            if (string.IsNullOrEmpty(strXmlResult))
            {
                strXmlResult = $"{x_objExlResult.SheetName} : successfully converted to xml";
                ObjConvertResult.Message = strXmlResult;
            }
            else
            {

            }

            return ObjConvertResult;
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
            if (strFileContent.Contains(DEFINE.NS1_PARAMETERS))
            {
                return FileType.Parameter;
            }
            else if (strFileContent.Contains(DEFINE.NS1_TRACEREQUESTS))
            {
                return FileType.TraceRequest;
            }
            else if (strFileContent.Contains(DEFINE.NS1_EVENTTRIGGERS))
            {
                return FileType.EventTrigger;
            }
            else if (strFileContent.Contains(DEFINE.NS1_EVENTREQUESTS))
            {
                return FileType.EventRequest;
            }
            else if (strFileContent.Contains(DEFINE.DCPTABLES))
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
        public static ConvertResult ConvertXmlToJson_Parameter(string x_strXmlPath, string x_strJsonPath)
        {
            // Declare variables at the beginning
            FileType fileType;
            XmlDocument xmlDoc;
            string strJson;
            ConvertResult objResult = new ConvertResult();

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
        public static ConvertResult ConvertJsonToXml_Parameter(string x_strJsonPath, string x_strXmlPath)
        {
            // Declare variables at the beginning
            FileType fileType;
            XmlDocument xmlDoc;
            string strJsonData;
            ConvertResult objResult = new ConvertResult();

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
