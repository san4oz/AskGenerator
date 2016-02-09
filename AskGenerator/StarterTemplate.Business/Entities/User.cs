using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Entities
{
    public class User : IdentityUser
    {
        public string GroupId { get; set; }

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
}
