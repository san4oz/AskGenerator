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
    /// <summary>
    /// A base class for entity whith ID field and XML-serializable dictionary.
    /// </summary>
    public abstract class Entity
    {
        public virtual string Id { get; set; }

        /// <summary>
        /// Indicates whether entity has empty fields.
        /// </summary>
        public bool HasEmptyFields { get { return Fields == null || Fields.Count == 0; } }

        protected EntityFields Fields { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid().ToString();
            Fields = new EntityFields();
        }

        /// <summary>
        /// Initializes fields from XML fields.
        /// </summary>
        public virtual void Initialize()
        {
        }

        /// <summary>
        /// Save fields to XML fields.
        /// </summary>
        public virtual void Apply()
        {
        }

        /// <summary>
        /// Adds current <see cref="Fields"/> to <paramref name="entity"/>'s <see cref="Fields"/>.
        /// </summary>
        /// <param name="entity">Entity copy fields to.</param>
        public virtual void CopyFieldsTo(Entity entity)
        {
            if (entity.Fields.Count == 0)
            {
                entity.Fields = new EntityFields(this.Fields);
            }
            else
            {
                foreach (var pair in this.Fields)
                    entity.Fields[pair.Key] = pair.Value;
            }
        }

        #region EF mapping
        [Column("Fields")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string FieldsXml
        {
            get
            {
                var manager = new XmlSerializerManager();
                return manager.Serialize(Fields);
            }
            set
            {
                var manager = new XmlSerializerManager();
                Fields = (EntityFields)manager.Deserialize(value, typeof(EntityFields)) ?? new EntityFields();
            }
        }
        #endregion
    }
}
