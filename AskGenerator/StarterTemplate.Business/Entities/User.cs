using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Entities
{
    public class User : IdentityUser
    {
        [Column(Order = 1)]
        [ForeignKey("Group")]
        public string GroupId { get; set; }

        [ForeignKey("Student")]
        public string StudentId { get; set; }

        public Group Group { get; set; }
        public Student Student { get; set; }

        public override string UserName
        {
            get
            {
                var name = base.UserName;
                if (string.IsNullOrEmpty(name))
                    base.UserName = name = Email.Split('@').FirstOrDefault();
                return name;
            }
            set
            {
                base.UserName = value;
            }
        }
    }

    public class Role : IdentityRole
    {
        public const string Admin = "admin";
        public const string User = "user";
        public const string SuperAdmin = "SA";
    }
}
