using AskGenerator.Business.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace AskGenerator.Business.InterfaceDefinitions.Providers
{
    public class RoleManagerBase : RoleManager<Role>
    {
        public RoleManagerBase(RoleStore<Role> store)
                : base(store) 
        {}
    }

}