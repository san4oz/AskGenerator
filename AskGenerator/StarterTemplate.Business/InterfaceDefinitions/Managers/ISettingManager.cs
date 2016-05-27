using AskGenerator.Business.Entities;
using AskGenerator.Business.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.InterfaceDefinitions.Managers
{
    public interface ISettingManager : IBaseEntityManager<Settings>
    {
        /// <summary>
        /// Gets website settings.
        /// </summary>
        /// <returns>Website settings.</returns>
        WebsiteSettings Website();

        /// <summary>
        /// Gets general system settings.
        /// </summary>
        /// <returns>General system settings.</returns>
        GeneralSettings General();
    }
}
