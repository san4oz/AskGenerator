using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AskGenerator.Business.Entities
{
    public abstract class Entity
    {
        public string Id { get; set; }

        protected EntityFields Fields { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid().ToString();
            Fields = new EntityFields();
        }

        #region EF mapping
        [Column("Fields")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string FieldsXml
        {
            get
            {
                var serializer = new XmlSerializer(typeof(EntityFields));
                using (var stringWriter = new StringWriter())
                {
                    serializer.Serialize(stringWriter, Fields);
                    stringWriter.Flush();
                    return stringWriter.ToString();
                }
            }
            set
            {
                if (value.IsEmpty())
                {
                    Fields = new EntityFields();
                }
                else
                {
                    var serializer = new XmlSerializer(typeof(EntityFields));
                    using (var stringReader = new StringReader(value))
                    {
                        Fields = (EntityFields)serializer.Deserialize(stringReader);
                    }
                }
            }
        }
        #endregion
    }

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
    }

    [XmlRoot("Fields")]
    public class XmlSerializableDictionary<TValue>
        : Dictionary<string, TValue>, IXmlSerializable
    {
        #region IXmlSerializable members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();
            if (wasEmpty)
                return;
            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");
                reader.ReadStartElement("key");
                var key = reader.ReadContentAsString();
                reader.ReadEndElement();
                reader.ReadStartElement("value");
                TValue value = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();
                this.Add(key, value);
                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
            foreach (var key in this.Keys)
            {
                writer.WriteStartElement("item");
                writer.WriteStartElement("key");
                writer.WriteString(key);
                writer.WriteEndElement();
                writer.WriteStartElement("value");
                TValue value = this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }
        #endregion
    }
}
