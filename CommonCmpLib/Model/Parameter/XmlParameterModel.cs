using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CommonCmpLib.Model
{
    [XmlRoot(ElementName = "parameters")]
    public class ParametersModel
    {
        [XmlElement(ElementName = "parameter")]
        public List<ParameterModel> Parameters { get; set; }
    }

    [XmlRoot(ElementName = "parameter")]
    public class ParameterModel
    {
        [XmlAttribute(AttributeName = "paramid")]
        public string ParameterID { get; set; }

        [XmlAttribute(AttributeName = "paramname")]
        public string ParameterName { get; set; }

        [XmlAttribute(AttributeName = "locator")]
        public string Locator { get; set; }

        [XmlAttribute(AttributeName = "unit")]
        public string Unit { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [XmlAttribute(AttributeName = "array")]
        public string Array { get; set; }

        [XmlAttribute(AttributeName = "function")]
        public string Function { get; set; }

        [XmlAttribute(AttributeName = "arg")]
        public string Arg { get; set; }

        [XmlElement(ElementName = "extension")]
        public ParameterExtentionModel Extension { get; set; }
    }

   
    public class ParameterExtentionModel
    {
        [XmlElement(ElementName = "datasource")]
        public ParameterDataSource DataSource { get; set; }
    }

    [XmlRoot(ElementName = "datasource")]
    public class ParameterDataSource
    {
        [XmlAttribute(AttributeName = "sourcetype")]
        public string Sourcetype { get; set; }

        [XmlElement(ElementName = "memory")]
        public ParameterMemory Memory { get; set; }

        [XmlElement(ElementName = "fins", IsNullable = true)]
        public object Finds { get; set; }
    }


    public class ParameterMemory
    {
        [XmlAttribute(AttributeName = "memname")]
        public string MemoryName { get; set; }

        [XmlAttribute(AttributeName = "offset")]
        public string Offset { get; set; }

        [XmlAttribute(AttributeName = "stype")]
        public string SourceType { get; set; }

        [XmlAttribute(AttributeName = "sarray")]
        public string SourceArray { get; set; }
    }

    public class ParameterFinds
    {

    }

}
