using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Entities
{
    public class Badge
    {
        /// <summary>
        /// Gets or sets badge (question) ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets image path.
        /// </summary>
        public string Image { get; set; }

        public float Limit { get; set; }

        public string ToolTipFormat { get; set; }

        public string Description { get; set; }
    }
}
