﻿using AskGenerator.App_Start.AutoMapper;
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
    public class TeacherViewModel : BaseViewModel, IHaveCustomMappings
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
        public string FullName { get { return LastName + ' ' + FirstName; } }

        public float AverageMark { get; set; }

        public IList<TeacherBadge> Badges { get; set; }

        public string GetShortName()
        {
            if (FirstName.IsEmpty())
                return LastName;
            if(FirstName.EndsWith("."))
                return FullName;

            var names = FirstName.Trim(' ').Split(' ');
            var result = LastName + ' ' + names[0].First() + '.';
            if (names.Length > 1)
                result += names[names.Length - 1].First() + ".";

            return result;
        }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            var conf = (AutoMapper.IMapperConfiguration)configuration;
            var defaultMark = new Mark() { Answer = -0.001f };
            conf.CreateMap<Teacher, TeacherViewModel>()
            .ReverseMap();
        }
    }

    public class TeacherBadge
    {
        public string Id { get; set; }

        public float Mark { get; set; }
    }
}
