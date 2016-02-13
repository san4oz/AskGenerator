﻿using AskGenerator.Business.Entities;
using AskGenerator.Business.InterfaceDefinitions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.DataProvider.Providers
{
    public class VoteProvider : BaseProvider<Vote>, IVoteProvider
    {
        public List<Vote> List(string userId)
        {
            return GetSet(set => set.AsQueryable().Where(v => v.AccountId.Equals(userId)).ToList());
        }
    }
}
