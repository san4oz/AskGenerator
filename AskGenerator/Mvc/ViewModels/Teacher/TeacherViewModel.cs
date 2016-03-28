using AskGenerator.App_Start.AutoMapper;
using AskGenerator.Business.Entities;
using AskGenerator.Mvc.ViewModels;
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
    public class TeacherViewModel : BaseViewModel, IPerson, IMapFrom<Teacher>
    {
        public TeacherViewModel()
            : base()
        {
            IsMale = true;
            Badges = new List<TeacherBadge>();
        }

        [HiddenInput(DisplayValue = false)]
        public override string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Group> Groups { get; set; }

        public List<string> SelectedGroups { get; set; }

        /// <summary>
        /// Gets or sets value indicating sex of teacher.
        /// </summary>
        [Required]
        [Display(Name = "Is male")]
        public bool IsMale { get; set; }

        [Display(Name = "Фото")]
        public HttpPostedFileBase ImageFile { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Image { get; set; }

        public string TeamId { get; set; }

        [ScaffoldColumn(false)]
        public TeacherBadge AverageMark { get; set; }

        [ScaffoldColumn(false)]
        public int VotesCount { get; set; }

        [ScaffoldColumn(false)]
        public IList<TeacherBadge> Badges { get; set; }
    }
}
