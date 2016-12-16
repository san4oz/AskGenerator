using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AskGenerator.DataProvider
{
    public class UserManager : UserManagerBase
    {
        public UserManager(IUserStore<User> store)
            : base(store)
        {
            var provider = new DpapiDataProtectionProvider("Evaluator");

            this.UserTokenProvider = new DataProtectorTokenProvider<User>(provider.Create("DefaultTokens"));
        }

        public static UserManagerBase Create(IdentityFactoryOptions<UserManagerBase> options,
                                                IOwinContext context)
        {
            var db = context.Get<AppContext>();
            var manager = new UserManager(new UserStore<User>(db));
            return manager;
        }

        public async override Task<ClaimsIdentity> CreateIdentityAsync(User user, string authenticationType)
        {
            var result = await base.CreateIdentityAsync(user, authenticationType);

            result.AddClaim(new Claim(ClaimTypes.Email, user.Email ?? string.Empty));
            if(user.StudentId.IsEmpty())
                result.AddClaim(new Claim("FacultyId", user.GroupId ?? string.Empty));
            else
                result.AddClaim(new Claim("GroupId", user.GroupId ?? string.Empty));
            result.AddClaim(new Claim("UId", user.Id));

            return result;
        }

        public override Task<IdentityResult> CreateAsync(User user, string password)
        {
            if (string.IsNullOrEmpty(user.Id))
                user.Id = Guid.NewGuid().ToString();

            return base.CreateAsync(user, password);
        }

        public override User FindByLoginKey(string key)
        {
            if (key.IsEmpty())
                return null;
            return this.Users.SingleOrDefault(u => u.LoginKey == key);
        }

        public override Task<User> FindByLoginKeyAsync(string key)
        {
            if (key.IsEmpty())
                return null;
            return Task.Factory.StartNew(() => FindByLoginKey(key));
        }
    }
}