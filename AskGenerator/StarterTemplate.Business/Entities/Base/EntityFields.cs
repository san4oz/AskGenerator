using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace AskGenerator.Business.Entities.Base
{
    [XmlRoot("Fields")]
    public class EntityFields : XmlSerializableDictionary<object>
    {
        public TValue GetOrCreate<TValue>(string key) where TValue : new()
        {
            return GetOrCreate(key, () => new TValue());
        }

        public TValue GetOrCreate<TValue>(string key, Func<TValue> createValue)
        {
            if (key == null)
                return default(TValue);
            object t;
            this.TryGetValue(key, out t);
            if (t == null)
            {
                var value = createValue();
                this[key] = value;
                return value;
            }
            return (TValue)t;
        }

        public TValue GetOrDefault<TValue>(string key)
        {
            var value = default(TValue);
            if (key == null)
                return value;

            object t;
            this.TryGetValue(key, out t);
            if (t != null)
                value = (TValue)t;

            return value;
        }
    }

    [XmlRoot("Fields")]
    [Serializable]
    public class XmlSerializableDictionary<TValue>
        : Dictionary<string, TValue>, IXmlSerializable
    {
        private XmlSerializerManager sm = new XmlSerializerManager();

        #region IXmlSerializable members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (!reader.IsEmptyElement)
            {
                if (!reader.Read())
                    return;
                while (!reader.EOF && reader.NodeType != XmlNodeType.EndElement)
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        reader.MoveToFirstAttribute();
                        string name = reader.Value;
                        reader.MoveToNextAttribute();
                        string type = reader.Value;
                        TValue value = default(TValue);

                        if (reader.MoveToElement())
                            reader.Read();
                        if (type != "Null")
                        {
                            var clrType = Type.GetType(type);
                            if (clrType == null)
                                throw new XmlException(string.Format("Cannot deserialize an entity field of type '{0}'"
                                    + "because the type information is not found.", type));

                            value = (TValue)sm.Deserialize(reader.ReadOuterXml(), clrType);
                            reader.Read();
                        }
                        this.Add(name, value);
                    }
                }
            }
            reader.Read();
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach (var pair in this)
            {
                writer.WriteStartElement("field");
                SerializeField(writer, pair);
                writer.WriteEndElement();
            }
        }

        static Regex xmlReplacer = new Regex(@"\<\?xml.+\?\>(\r\n)?", RegexOptions.Compiled);

        private void SerializeField(XmlWriter writer, KeyValuePair<string, TValue> pair)
        {

            writer.WriteAttributeString("name", pair.Key);

            writer.WriteAttributeString("type", GetAssemblyName(pair.Value));

            if (pair.Value != null)
            {
                string xml = sm.Serialize(pair.Value);
                xml = xmlReplacer.Replace(xml, string.Empty);
                writer.WriteRaw(xml);
            }

        }

        private string GetAssemblyName(TValue value)
        {
            var assemblyName = "Null";

            if (value != null)
            {
                Type type = value.GetType();
                assemblyName = GetTypeNameWithAssembly(type);

                if (type.IsGenericType)
                {
                    var processedTypes = new System.Collections.Generic.HashSet<Type>();
                    foreach (Type argumentType in type.GenericTypeArguments)
                    {
                        if (!processedTypes.Contains(argumentType))
                        {
                            processedTypes.Add(argumentType);
                            assemblyName = assemblyName.Replace(argumentType.ToString(), "[" + GetTypeNameWithAssembly(argumentType) + "]");
                        }
                    }
                }
            }
            return assemblyName;
        }

        private string GetTypeNameWithAssembly(Type type)
        {
            return type.ToString() + ", " + type.Assembly.GetName().Name;
        }
        #endregion
    }
}
