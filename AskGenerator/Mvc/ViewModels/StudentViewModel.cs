using AskGenerator.App_Start.AutoMapper;
using AskGenerator.Business.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AskGenerator.ViewModels
{
    public class StudentViewModel : BaseViewModel, IMapFrom<Student>
    {
        [Display(Name="Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Группа")]
        public GroupViewModel Group { get; set; }

        public string Image { get; set; }

        [Display(Name = "Фото")]
        public HttpPostedFileBase ImageFile { get; set; }

        [Display(Name="Студент")]
        public string Name 
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }   
        }
    }
}
