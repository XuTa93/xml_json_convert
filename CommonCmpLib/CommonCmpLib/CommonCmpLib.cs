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

using CommonCmpLib.JsonToXmlModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


namespace CommonCmpLib
{
    public static class CommonCmpLib
    {
        public static string ConvertXmlToJson_Parameter(string x_strXmlPath, string x_strJsonPath)
        {
            ReadXml(x_strXmlPath);
            return "";
        }

        public static string ConvertJsonToXml_Parameter(string x_strXmlPath, string x_strJsonPath)
        {

            return "";
        }

        static void ReadXml(string x_strXmlPath)
        {
            XmlDocument xmlDoc;
            XmlNode xmlNodeDoc;
            xmlDoc = new XmlDocument();

            xmlDoc.Load(x_strXmlPath);

            xmlNodeDoc = xmlDoc.DocumentElement;
            //XmlNode eventTriggerNode = xmlDoc.SelectSingleNode("//eventrequest");

            XmlSerializer serializer = new XmlSerializer(typeof(EventRequest));
            using (StringReader reader = new StringReader(xmlNodeDoc.InnerXml))
            {
                var test = (EventRequest)serializer.Deserialize(reader);
                // Convert the DataCollectionPlan instance to JSON
                // Create a JavaScriptSerializer instance

                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new PersonConverter());
                settings.Formatting = Newtonsoft.Json.Formatting.Indented; // Optional for pretty printing

                string jsonString = JsonConvert.SerializeObject(test, settings);
                Console.WriteLine(jsonString);
            }
            
        }
    }
}
