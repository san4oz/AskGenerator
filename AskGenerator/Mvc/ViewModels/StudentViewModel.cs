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
    using R = Resources.Resource;
    public class StudentViewModel : BaseViewModel, IMapFrom<Student>
    {
        [Display(Name = "FirstName", ResourceType = typeof(R))]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(R))]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Group", ResourceType = typeof(R))]
        [UIHint("GroupSelector")]
        public string GroupId { get; set; }

        [Display(Name = "Group", ResourceType = typeof(R))]
        public GroupViewModel Group { get; set; }

        public string Image { get; set; }

        [Display(Name = "Photo", ResourceType = typeof(R))]
        public HttpPostedFileBase ImageFile { get; set; }

        /// <summary>
        /// Indicates whether student has registred account.
        /// </summary>
        public bool HasUserAccount { get; set; }

        [MaxLength(128)]
        public string AccountId { get; set; }

        [Display(Name = "Student", ResourceType = typeof(R))]
        public string Name 
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }   
        }
    }
}
