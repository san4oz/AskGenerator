using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.ViewModels
{
    public class BaseViewModel
    {
        public virtual string Id { get; set; }

        public BaseViewModel()
        {
            //Id = Guid.NewGuid().ToString();
        }
    }
}
