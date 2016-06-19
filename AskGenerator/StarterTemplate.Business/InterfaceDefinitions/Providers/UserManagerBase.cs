using System;
using System.Threading.Tasks;
using AskGenerator.Business.Entities;
using Microsoft.AspNet.Identity;
namespace AskGenerator.Business.InterfaceDefinitions.Providers
{
    public abstract class UserManagerBase : UserManager<User>
    {
        public abstract User FindByLoginKey(string key);

        public abstract Task<User> FindByLoginKeyAsync(string key);

        public UserManagerBase(IUserStore<User> store)
            : base(store)
        {
        }
    }
}
