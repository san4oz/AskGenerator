using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.InterfaceDefinitions.Providers
{
    public interface IBaseEntityProvider<T>
    {
        bool Create(T entity);

        bool Update(T entity);

        bool Delete(T entity);

        bool Delete(string id);

        T Get(string id);

        List<T> All();
    }
}
