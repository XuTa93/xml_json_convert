
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CommonCmpLib.JsonToXmlModel
{

    [System.Serializable]
    [XmlRoot(ElementName = "eventrequest")]
	public class EventRequest
    {
        [XmlAttribute(AttributeName = "eventid")]
        public string Eventid;

        [XmlAttribute(AttributeName = "eventname")]
        public string Eventname { get; set; }

        [XmlElement(ElementName = "parameter")]
		public Parameter Parameter { get; set; }
	}

    [XmlRoot(ElementName = "parameter")]
    public class Parameter
    {
        [XmlAttribute(AttributeName = "paramid")]
        public string Paramid { get; set; }
    }



}
