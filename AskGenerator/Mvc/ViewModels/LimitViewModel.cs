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
    public class LimitViewModel : IMapFrom<Question.Limit>
    {
        /// <summary>
        /// Gets or sets badge (question) ID.
        /// </summary>
        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [Range(0, 10.0)]
        public float AvgLimit { get; set; }

        [Display(Name = "Photo", ResourceType = typeof(Resources.Resource))]
        public HttpPostedFileBase ImageFile { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Image { get; set; }

        [MaxLength(50)]
        [Display(Name = "Tooltip", ResourceType = typeof(Resources.Resource))]
        public string ToolTip { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.Resource))]
        public string Description { get; set; }
    }
}
