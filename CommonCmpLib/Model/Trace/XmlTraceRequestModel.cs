using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CommonCmpLib
{
    [XmlRoot("tracerequest")]
    public class TraceRequest
    {
        [XmlAttribute("traceid")]
        public string TraceID { get; set; }

        [XmlAttribute("tracename")]
        public string TraceName { get; set; }

        [XmlAttribute("description")]
        public string Description { get; set; }

        [XmlElement("triggers")]
        public Triggers Triggers { get; set; }

        [XmlElement("parameters")]
        public Parameters Parameters { get; set; }
    }

    public class Triggers
    {
        [XmlElement("starton")]
        public string StartOn { get; set; }

        [XmlElement("stopon")]
        public string StopOn { get; set; }
    }

    public class Parameters
    {
        [XmlElement("parameter")]
        public List<Parameter> ParameterList { get; set; } = new List<Parameter>();
    } 

    public class Parameter
    {
        [XmlAttribute("paramid")]
        public string ParameterID { get; set; }
    }
}
