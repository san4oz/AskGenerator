using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        static Regex xmlReplacer = new Regex(@"\<\?xml.+\?\>(\r\n)?", RegexOptions.Compiled);

        public string Serialize<T>(T obj)
        {
            var xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = true;
            xws.Encoding = Encoding.UTF8;
            xws.NamespaceHandling = NamespaceHandling.OmitDuplicates;
            var ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);
            using (var stringWriter = new StringWriter())
            using (var xmlWriter = XmlTextWriter.Create(stringWriter, xws))
            {
                GetSerializer(obj.GetType()).Serialize(xmlWriter, obj, ns);
                xmlWriter.Flush();
                stringWriter.Flush();

                return xmlReplacer.Replace(stringWriter.ToString(), string.Empty);
            }
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
