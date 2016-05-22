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


        protected virtual TSetting GetOrCreate<TSetting>(string id) where TSetting : Settings, new()
        {

            var settings = FromCache(id, () => Get(id));
            TSetting res = new TSetting() { Id = id };
            if (settings == null)
                Create(res);
            else
                settings.CopyFieldsTo(res);
            
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

            var settings = new Settings() { Id = entity.Id };
            entity.CopyFieldsTo(settings);
            return base.Update(settings);
        }

        public WebsiteSettings Website()
        {

            return GetOrCreate<WebsiteSettings>("WebsiteSettings");
        }
    }
}
