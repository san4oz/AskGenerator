﻿using AskGenerator.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.InterfaceDefinitions.Providers
{
    public interface IGroupProvider : IBaseEntityProvider<Group>
    {
        List<Group> GetById(IEnumerable<string> ids);

        List<Group> AllWithoutIncl();
    }
}
