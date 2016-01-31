using AskGenerator.App_Start.AutoMapper;
using AskGenerator.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AskGenerator.ViewModels
{
    public class StudentViewModel : BaseViewModel, IMapFrom<Student>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public GroupViewModel Group { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
    }
}
