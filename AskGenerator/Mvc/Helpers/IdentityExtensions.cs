using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class IdentityExtensions
    {
        public static string GetGroupId(this IIdentity identity)
        {
            return identity.GetValue("GroupId");
        }

        public static string GetId(this IIdentity identity)
        {
            return identity.GetValue("UId");
        }

        public static string GetEmail(this IIdentity identity)
        {
            return identity.GetValue(ClaimTypes.Email);
        }

        private static string GetValue(this IIdentity identity, string name)
        {
            if (identity == null)
                throw new ArgumentNullException("identity", "Identity could not be null.");

            var ci = identity as ClaimsIdentity;
            if (ci != null)
            {
                var id = ci.FindFirst(name);
                if (id != null)
                    return id.Value;
            }
            return null;
        }
    }
}
