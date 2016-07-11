using AskGenerator.Business.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AskGenerator.Mvc.Components.Attributes
{
    /// <summary>
    /// Represents an attribute that is used to restrict access by callers to an action method by roles.
    /// </summary>
    public class RolesAuthorizeAttribute : AuthorizeAttribute
    {
        public RolesAuthorizeAttribute() : base()
        {
            Order = 0; 
        }

        /// <summary>
        /// Initilizes new instance with restricting role.
        /// </summary>
        /// <param name="role">Role to allow access for.</param>
        public RolesAuthorizeAttribute(string role)
            : this()
        {
            Roles = role;
        }

        /// <summary>
        /// Initilizes new instance with several restricting roles.
        /// </summary>
        /// <param name="roles">Roles to allow access for.</param>
        public RolesAuthorizeAttribute(params string[] roles)
            : this()
        {
            Roles = roles.Join(",");
        }
    }
}
