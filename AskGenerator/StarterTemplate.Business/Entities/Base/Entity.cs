using AskGenerator.Business.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
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
}
