using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Entities
{
    public interface IPerson
    {
        string FirstName { get; set; }

        string LastName { get; set; }
    }
}

namespace System
{
    public static class PersonExtentions
    {
        public static string FullName(this AskGenerator.Business.Entities.IPerson person)
        {
            return person.LastName + ' ' + person.FirstName;
        }

        public static string GetShortName(this AskGenerator.Business.Entities.IPerson person)
        {
            if (person.FirstName.IsEmpty())
                return person.LastName;
            if(person.FirstName.EndsWith("."))
                return person.FullName();

            var names = person.FirstName.Trim(' ').Split(' ');
            var result = person.LastName + ' ' + names[0].First() + '.';
            if (names.Length > 1)
                result += names[names.Length - 1].First() + ".";

            return result;
        }
    }
}
