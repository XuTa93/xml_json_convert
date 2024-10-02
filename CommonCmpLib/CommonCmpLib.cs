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
using Newtonsoft.Json.Linq;
using System.IO;
using System.Xml;


namespace CommonCmpLib
{
    public static class CommonCmpLib
    {
        #region Types
        public  const  string _PREFIX = "@";
        #endregion Types
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
            JObject objJson;
            string strJson;
            xmlDoc = new XmlDocument();
            xmlDoc.Load(x_strXmlPath);
            objJson = ConvertXmlNodeToJObject(xmlDoc);

            strJson = JsonConvert.SerializeObject(objJson, Newtonsoft.Json.Formatting.Indented);


            string strXmlOutput;
            JObject jsonObject = JObject.Parse(strJson);

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


        static JObject ConvertXmlNodeToJObject(XmlNode node)
        {
            JObject jsonObj = new JObject();

            // Add attributes with a prefix
            if (node.Attributes != null)
            {
                foreach (XmlAttribute attr in node.Attributes)
                {
                    jsonObj[_PREFIX + attr.Name] = attr.Value;
                }
            }

            // Add child elements
            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode is XmlElement)
                {                 
                    jsonObj[childNode.Name] = ConvertXmlNodeToJObject(childNode);
                    //if (childNode.ChildNodes.Count == 0 )
                    //{
                    //    jsonObj[childNode.Name] = null;
                    //}
                }
                else if (childNode is XmlText)
                {
                    jsonObj["#text"] = childNode.Value;
                }
            }

            return jsonObj;
        }
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
