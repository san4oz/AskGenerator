using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Managers
{
    public class SubscribersManager : BaseEntityManager<Subscriber, IBaseEntityProvider<Subscriber>>
    {
        public SubscribersManager(IBaseEntityProvider<Subscriber> provider) : base(provider) { }

        protected override string Name { get { return "Subscriber"; } }

        public override bool Create(Subscriber entity)
        {
            if(All().FirstOrDefault(s => s.Email == entity.Email) == null)
                return base.Create(entity);

            return false;
        }
    }
}