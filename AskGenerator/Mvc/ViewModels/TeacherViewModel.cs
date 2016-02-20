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
    public class TeacherViewModel : BaseViewModel, IHaveCustomMappings
    {
        public TeacherViewModel():base()
        {
            IsMale = true;
        }

        [HiddenInput(DisplayValue=false)]
        public override string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Group> Groups { get; set; }

        public List<string> SelectedGroups { get; set; }

        /// <summary>
        /// Gets or sets value indicating sex of teacher.
        /// </summary>
        [Required]
        [Display(Name="Is male")]
        public bool IsMale { get; set; }

        [Display(Name = "Фото")]
        public HttpPostedFileBase ImageFile { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Image { get; set; }

        public string TeamId { get; set; }

        [ScaffoldColumn(false)]
        public string FullName { get { return FirstName + ' ' + LastName; } }

        public float AverageMark { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            var conf = (AutoMapper.IMapperConfiguration)configuration;
            conf.CreateMap<Teacher, TeacherViewModel>().AfterMap((m, v) => v.AverageMark = m.Marks.Any() ? m.Marks[0].Answer : 0f)
            .ReverseMap();
        }
    }
}
