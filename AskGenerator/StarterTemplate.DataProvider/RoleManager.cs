using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace AskGenerator.DataProvider
{
    public class RoleManager : RoleManagerBase
    {
        public RoleManager(RoleStore<Role> store)
                : base(store) 
        {}

        public static RoleManagerBase Create(IdentityFactoryOptions<RoleManagerBase> options,
                                        IOwinContext context)
        {
            return new RoleManager(new RoleStore<Role>(context.Get<AppContext>()));
        }
    }

}