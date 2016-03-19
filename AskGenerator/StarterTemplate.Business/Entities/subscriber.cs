using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Entities
{
    public class Subscriber : Entity
    {
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessageResourceName = "RegularAddress",
                                                                               ErrorMessageResourceType = typeof(Resources.Resource))]
        [MaxLength(128)]
        [Index(IsUnique=true)]
        public string Email { get; set; }
    }
}
