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
    public static class CommonCmpLib
    {
        #region Types
        public const string _PREFIX = "@";
        #endregion Types

        public static void ListParameterToXml()
        {  
            // Create an example ParametersModel instance
            var parameterModel = new ParameterModel
            {
                ParameterID = "1",
                ParameterName = "Tem",
                Locator = "A1",
                Unit = "Cel",
                Type = "Sen",
                Array = "False",
                Function = "Mea",
                Arg = "None",
                Extension = new ParameterExtentionModel
                {
                    DataSource = new ParameterDataSource
                    {
                        Sourcetype = "Memory",
                        Memory = new ParameterMemory
                        {
                            MemoryName = "Mem1",
                            Offset = "10",
                            SourceType = "TypeA",
                            SourceArray = "Array1"
                        },
                        Finds = null,
                    }
                }
            };
            StringBuilder sb = new StringBuilder();

            // Create a StringBuilder to hold the XML content
             sb = new StringBuilder();
            // Create XmlWriterSettings with OmitXmlDeclaration = true
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,              // Format the XML with indentation
                OmitXmlDeclaration = true,              
            };
            // Create an XmlWriter with the specified settings
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                // Write the start of the document and the custom root element with namespace
                writer.WriteStartDocument();
                writer.WriteStartElement("ns1", "parameters", "urn:ConfigFileSchema");

                // Create an XmlSerializerNamespaces to remove default namespaces
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
               
                for (int i = 0; i < 1; i++)
                {
                    namespaces.Add(string.Empty, string.Empty); // No namespaces for the inner XML
                    // Use XmlSerializer to serialize the parametersModel into the writer
                    XmlSerializer serializer = new XmlSerializer(typeof(ParameterModel));
                    serializer.Serialize(writer, parameterModel, namespaces);
                }
                // Close the root element
                writer.WriteEndElement(); // End parameters
                writer.WriteEndDocument();
            }
            string xmlString = sb.ToString();
            Console.WriteLine(xmlString);
            string filePath = "parameters.xml";
            File.WriteAllText(filePath, xmlString);

            var json = XmlToJson(filePath);

        }
        // Utility function to remove the XML declaration
        private static string RemoveXmlDeclaration(string xml)
        {
            int index = xml.IndexOf("?>");
            if (index > -1)
            {
                xml = xml.Substring(index + 2).Trim();
            }
            return xml;
        }
        public static string ConvertXmlToJson_Parameter(string x_strXmlPath, string x_strJsonPath)
        {

            string strJson;
            bool bFileIsExist = File.Exists(x_strXmlPath);
            if (bFileIsExist)
            {
                strJson = XmlToJson(x_strXmlPath);
                return strJson;
            }
            else
            {
                return XmlToJson($"{x_strXmlPath} \r\n File does not exist.");
            }
        }

        public static string ConvertJsonToXml_Parameter(string x_strJsonPath, string x_strXmlPath)
        {
            string strJson;
            string strXml;
            bool bFileIsExist = File.Exists(x_strJsonPath);
            if (bFileIsExist)
            {
                strJson = File.ReadAllText(x_strJsonPath);
                strXml = JsonToXml(strJson);
                return strXml;
            }
            else
            {
                return XmlToJson($"{x_strJsonPath} \r\n File does not exist.");
            }
        }

        static string XmlToJson(string x_strXmlPath)
        {
            XmlDocument xmlDoc;

            string strJson;
            xmlDoc = new XmlDocument();
            xmlDoc.Load(x_strXmlPath);


             strJson = JsonConvert.SerializeObject(xmlDoc, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Include // Include null values
            });
            string strXmlOutput;
            JObject jsonObject = JObject.Parse(strJson);

            return strJson;
        }

        static string JsonToXml(string x_strJsonPath)
        {
            XmlDocument xmlDoc;
            string strXmlOutput;
            JObject jsonObject = JObject.Parse(x_strJsonPath);

            // Create a new XmlDocument
            xmlDoc = new XmlDocument();

            // Convert only the inner elements without wrapping them in a root element
            foreach (JProperty property in jsonObject.Properties())
            {
                XmlElement element = ConvertJObjectToXmlElement((JObject)property.Value, property.Name, xmlDoc);
                xmlDoc.AppendChild(element);
            }
            // Convert back to XML string
            strXmlOutput = xmlDoc.OuterXml;

            return strXmlOutput;
        }

        //static JObject ConvertXmlNodeToJObject(XmlNode node)
        //{
        //    JObject jsonObj = new JObject();

        //    // Add attributes with a prefix
        //    if (node.Attributes != null)
        //    {
        //        foreach (XmlAttribute attr in node.Attributes)
        //        {
        //            jsonObj[_PREFIX + attr.Name] = attr.Value;
        //        }
        //    }

        //    // Add child elements
        //    foreach (XmlNode childNode in node.ChildNodes)
        //    {
        //        if (childNode is XmlElement)
        //        {
        //            jsonObj[childNode.Name] = ConvertXmlNodeToJObject(childNode);
        //        }
        //        else if (childNode is XmlText)
        //        {
        //            jsonObj["#text"] = childNode.Value;
        //        }
        //    }

        //    return jsonObj;
        //}

        static XmlElement ConvertJObjectToXmlElement(JObject jsonObject, string elementName, XmlDocument xmlDoc)
        {

            XmlElement element = xmlDoc.CreateElement(elementName);
            // Loop through each property in the JSON object
            foreach (var property in jsonObject.Properties())
            {
                if (property.Name.StartsWith(_PREFIX))  // Attribute
                {
                    string attrName = property.Name.Substring(1);  // Remove the prefix '@'
                    string attrValue = property.Value.ToString();
                    element.SetAttribute(attrName, attrValue);
                }
                else if (property.Name == "#text")  // Text content
                {
                    element.InnerText = property.Value.ToString();
                }
                else  // Child element
                {
                    XmlElement childElement = ConvertJObjectToXmlElement((JObject)property.Value, property.Name, xmlDoc);
                    element.AppendChild(childElement);
                }
            }

            return element;
        }
    }
}
