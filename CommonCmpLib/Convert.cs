//*****************************************************************************
// 		        ：
// 内容		    ：ExcelToXmlList: Xml <=> Json Convert
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

namespace ExcelToXmlList
{

    public static class Convert
    {
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

                    ObjConvertResult = XmlServices.GenerateParameterToXml(x_objExlResult.Models, x_strOutFolder, $"{x_objExlResult.SheetName}.xml");
                    break;

                case TRACE.SHEET_NAME:
                    //Create TraceRequest Xml
                    ObjConvertResult = XmlServices.GenerateTraceToXml(x_objExlResult.Models, x_strOutFolder, $"{x_objExlResult.SheetName}.xml");
                    break;

                case EVENT.SHEET_NAME:
                    //Create EventTrigger and EventRequst Xml
                    ObjConvertResult = XmlServices.GenerateEventTriggerToXml(x_objExlResult.Models, x_strOutFolder, $"{EVENT.SHEET_NAME_TRIGGER}.xml");
                    break;

                case DCP.SHEET_NAME:
                    //Create DataCollectionPlan Xml
                    ObjConvertResult = XmlServices.GenerateDCPXml(x_objExlResult.Models, x_strOutFolder);
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
