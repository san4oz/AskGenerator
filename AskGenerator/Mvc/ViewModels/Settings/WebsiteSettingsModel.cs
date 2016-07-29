using AskGenerator.App_Start.AutoMapper;
using AskGenerator.Business.Entities.Settings;
using System.ComponentModel.DataAnnotations;

namespace AskGenerator.Mvc.ViewModels.Settings
{
    using Components.Attributes;
    using System.Web.Mvc;
    using R = Resources.Resource;
    public class WebsiteSettingsModel : AskGenerator.ViewModels.BaseViewModel, IMapFrom<WebsiteSettings>
    {
        [Display(Name = "IsVotingEnabled", ResourceType = typeof(R))]
        public bool IsVotingEnabled { get; set; }

        [Display(Name = "RegisterOpened", ResourceType = typeof(R))]
        public bool RegisterOpened { get; set; }

        [Display(Name = "VotingDisabledText", ResourceType = typeof(R))]
        [iRequired]
        [AllowHtml]
        public string VotingDisabledText { get; set; }

        [Display(Name = "TimeBanner", ResourceType = typeof(R))]
        [UIHint("TimeBannerSettings")]
        public WebsiteSettings.TimeBannerSettings TimeBanner { get; set; }
    }
}
