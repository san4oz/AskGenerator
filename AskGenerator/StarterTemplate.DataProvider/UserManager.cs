using AskGenerator.Business.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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

        public override Task<IdentityResult> CreateAsync(User user, string password)
        {
            if (string.IsNullOrEmpty(user.Id))
                user.Id = Guid.NewGuid().ToString();

            return base.CreateAsync(user, password);
        }

        public virtual User FindByLoginKey(string key)
        {
            if (key.IsEmpty())
                return null;
            return this.Users.SingleOrDefault(u => u.LoginKey == key);
        }

        public virtual Task<User> FindByLoginKeyAsync(string key)
        {
            if (key.IsEmpty())
                return null;
            return Task.Factory.StartNew(() => FindByLoginKey(key));
        }
    }
}