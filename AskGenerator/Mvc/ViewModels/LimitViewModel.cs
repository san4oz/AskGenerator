using AskGenerator.App_Start.AutoMapper;
using AskGenerator.Business.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AskGenerator.ViewModels
{
    using R = Resources.Resource;

    public class LimitViewModel : IMapFrom<Question.Limit>
    {
        /// <summary>
        /// Gets or sets badge (question) ID.
        /// </summary>
        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [Display(Name = "Limit_IsEnabled", ResourceType = typeof(R))]
        public bool IsEnabled { get; set; }

        [Display(Name = "Photo", ResourceType = typeof(R))]
        public HttpPostedFileBase ImageFile { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Image { get; set; }

        [MaxLength(50)]
        [Display(Name = "Tooltip", ResourceType = typeof(R))]
        public string ToolTip { get; set; }

        [Display(Name = "Description", ResourceType = typeof(R))]
        public string Description { get; set; }
    }
}
