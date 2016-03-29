using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace AskGenerator.Business
{
    public class XmlSerializerManager
    {
        private Type serializerType = null;
        private XmlSerializer serializer = null;

        public XmlSerializer GetSerializer(Type type)
        {
            if (serializerType == type && serializer != null)
                return serializer;
            serializerType = type;
            serializer = new XmlSerializer(type);
            return serializer;
        }

        public string Serialize<T>(T obj)
        {
            var stringWriter = new System.IO.StringWriter();
            GetSerializer(obj.GetType()).Serialize(stringWriter, obj);
            return stringWriter.ToString();
        }

        #region Deserialize
        public object Deserialize(XmlReader reader, Type type)
        {
            return GetSerializer(type).Deserialize(reader);
        }

        public object Deserialize(TextReader reader, Type type)
        {
            return GetSerializer(type).Deserialize(reader);
        }

        public object Deserialize(string xml, Type type)
        {
            if (xml.IsNullOrWhiteSpace())
                return null;
            using (var sr = new StringReader(xml))
                return Deserialize(sr, type);
        }
        #endregion
    }
}
