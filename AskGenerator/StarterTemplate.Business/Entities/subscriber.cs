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
        [MaxLength(128)]
        [Index(IsUnique=true)]
        public string Email { get; set; }
    }
}
