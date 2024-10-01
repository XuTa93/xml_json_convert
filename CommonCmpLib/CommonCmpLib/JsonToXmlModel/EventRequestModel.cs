
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CommonCmpLib.JsonToXmlModel
{
	[XmlRoot(ElementName = "parameter")]
	public class Parameter
	{

		[XmlAttribute(AttributeName = "paramid")]
	
		public string Paramid { get; set; }
	}

	[XmlRoot(ElementName = "eventrequest")]
	public class EventRequest
	{

		[XmlElement(ElementName = "parameter")]
		public Parameter Parameter { get; set; }

		[XmlAttribute(AttributeName = "eventname")]
		public string Eventname { get; set; }

		
		[XmlAttribute(AttributeName = "eventid")]
		public string Eventid { get; set; }
	}
    public class PersonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var person = (EventRequest)value;

            writer.WriteStartObject();
            writer.WritePropertyName("@name");
            writer.WriteValue(person.Eventid);
            writer.WritePropertyName("@age");
            writer.WriteValue(person.Eventname);
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var person = new EventRequest();

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndObject)
                    break;

                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string propertyName = (string)reader.Value;
                    if (propertyName == "@name")
                    {
                        reader.Read();
                        person.Eventid = reader.Value.ToString();
                    }
                    else if (propertyName == "@age")
                    {
                        reader.Read();
                        person.Eventname = reader.Value.ToString();
                    }
                }
            }

            return person;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(EventRequest);
        }
    }


}
