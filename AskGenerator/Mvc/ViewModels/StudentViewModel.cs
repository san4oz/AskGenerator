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
        [Display(Name = "FirstName", ResourceType = typeof(Resources.Resource))]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Resources.Resource))]
        public string LastName { get; set; }

        [Display(Name = "Group", ResourceType = typeof(Resources.Resource))]
        public GroupViewModel Group { get; set; }

        public string Image { get; set; }

        [Display(Name = "Photo", ResourceType = typeof(Resources.Resource))]
        public HttpPostedFileBase ImageFile { get; set; }

        [Display(Name = "Student", ResourceType = typeof(Resources.Resource))]
        public string Name 
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }   
        }
    }
}
