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
        /// <param name="ignoreCache">Indicates whether settings should be taken ftom cache or not.</param>
        /// <returns>Website settings.</returns>
        WebsiteSettings Website(bool ignoreCache = false);

        /// <summary>
        /// Gets general system settings.
        /// </summary>
        /// <param name="ignoreCache">Indicates whether settings should be taken ftom cache or not.</param>
        /// <returns>General system settings.</returns>
        GeneralSettings General(bool ignoreCache = false);
    }
}
