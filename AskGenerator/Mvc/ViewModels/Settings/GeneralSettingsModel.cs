using AskGenerator.App_Start.AutoMapper;
using AskGenerator.Business.Entities;
using AskGenerator.Business.Entities.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Mvc.ViewModels.Settings
{
    using R = Resources.Resource;
    public class GeneralSettingsModel : AskGenerator.ViewModels.BaseViewModel, IHaveCustomMappings
    {
        [UIHint("Iterations")]
        public List<GeneralSettings.Iteration> Iterations { get; set; }

        [Display(Name = "BannedDomains", ResourceType = typeof(R))]
        [DataType(DataType.MultilineText)]
        public string BannedDomains { get; set; }
        
        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            var conf = (AutoMapper.IMapperConfiguration)configuration;
            conf.CreateMap<GeneralSettings, GeneralSettingsModel>()
                    .AfterMap((s, model) => model.BannedDomains = s.BannedDomains.Join(";"))
                .ReverseMap()
                    .AfterMap((model, s) => s.BannedDomains = model.BannedDomains.Split(';'));
        }
    }
}
