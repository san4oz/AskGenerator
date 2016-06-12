using AskGenerator.Business.Entities;
using AskGenerator.Business.Entities.Settings;
using AskGenerator.Business.InterfaceDefinitions.Managers;
using AskGenerator.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Managers
{
    public class SettingManager : BaseEntityManager<Settings, ISettingProvider>, ISettingManager
    {
        public SettingManager(ISettingProvider provider)
            : base(provider)
        { }

        protected override string Name { get { return "Settings"; } }

        /// <summary>
        /// Gets (and creates if not found) settings group.
        /// </summary>
        /// <typeparam name="TSetting">Type of setting group.</typeparam>
        /// <param name="id">The ID of settings group. If <c>null</c> default group ID will be used.</param>
        /// <returns>Settings group.</returns>
        protected virtual TSetting GetOrCreate<TSetting>(string id = null) where TSetting : Settings, new()
        {
            TSetting res = new TSetting();
            if (!id.IsEmpty())
                res.Id = id;

            var settings = FromCache(res.Id, () => Get(res.Id));

            if (settings == null)
                Create(res);
            else
                settings.CopyFieldsTo(res);

            res.Initialize();
            return (TSetting)res;
        }

        public override bool Create(Settings entity)
        {
            var settings = new Settings() { Id = entity.Id };
            entity.CopyFieldsTo(settings);
            return base.Create(settings);
        }

        public override bool Update(Settings entity)
        {
            RemoveFromCache(entity.Id);
            entity.Apply();
            var settings = new Settings() { Id = entity.Id };
            entity.CopyFieldsTo(settings);
            return base.Update(settings);
        }

        public WebsiteSettings Website()
        {
            return GetOrCreate<WebsiteSettings>();
        }

        /// <summary>
        /// Gets general system settings.
        /// </summary>
        /// <returns>General system settings.</returns>
        public GeneralSettings General()
        {
            return GetOrCreate<GeneralSettings>();
        }
    }
}
