using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Mvc.Components
{
    public class Resolver
    {
        public const string MaleShadow = "/Content/Images/Male.png";
        public const string FemaleShadow = "/Content/Images/Female.png";

        public string Image(string path, bool isMale)
        {
            if (string.IsNullOrWhiteSpace(path))
                return isMale ? MaleShadow : FemaleShadow;

            return path;
        }
    }
}
