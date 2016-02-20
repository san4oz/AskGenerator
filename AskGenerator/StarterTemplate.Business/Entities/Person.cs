using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AskGenerator.Business.Entities
{
    public class Person : Entity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets path to the person image.
        /// </summary>
        public string Image { get; set; }

        public bool IsMale { get; set; }
    }
}
