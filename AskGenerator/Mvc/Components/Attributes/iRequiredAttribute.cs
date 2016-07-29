using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Mvc.Components.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class iRequiredAttribute : RequiredAttribute
    {
        public iRequiredAttribute()
        {
            base.ErrorMessageResourceName = "Required";
            base.ErrorMessageResourceType = typeof(Resources.Resource);
        }
    }
}
