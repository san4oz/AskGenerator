using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AskGenerator.Business.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Security.Claims;

namespace AskGenerator.DataProvider
{
    public class UserManager : UserManager<User>
    {
        public UserManager(IUserStore<User> store)
            : base(store)
        {
        }

        public static UserManager Create(IdentityFactoryOptions<UserManager> options,
                                                IOwinContext context)
        {
            var db = context.Get<AppContext>();
            var manager = new UserManager(new UserStore<User>(db));
            return manager;
        }

        public async override Task<ClaimsIdentity> CreateIdentityAsync(User user, string authenticationType)
        {
            var result = await base.CreateIdentityAsync(user, authenticationType);

            result.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            result.AddClaim(new Claim("GroupId", user.GroupId));

            return result;
        }
    }
}