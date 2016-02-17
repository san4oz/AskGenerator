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
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "FirstNameRequired")]
        [Display(Name = "FirstName", ResourceType = typeof(Resources.Resource))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "LastNameRequired")]
        [Display(Name = "LastName", ResourceType = typeof(Resources.Resource))]

        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets path to the person image.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
        ErrorMessageResourceName = "ImageRequired")]
        [Display(Name = "Image", ResourceType = typeof(Resources.Resource))]

        public string Image { get; set; }
    }
}
