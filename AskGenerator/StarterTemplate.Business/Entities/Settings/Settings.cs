using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.Business.Entities.Settings
{
    public class Settings : Entity
    {
        /// <summary>
        /// Gets setting value from <see cref="Fields"/>.
        /// </summary>
        /// <typeparam name="TValue">Type of value.</typeparam>
        /// <param name="key">Setting key.</param>
        /// <returns>Value.</returns>
        public TValue Get<TValue>(string key)
        {
            return Fields.GetOrDefault<TValue>(key);
        }

        /// <summary>
        /// Saves setting to <see cref="Fields"/>.
        /// </summary>
        /// <typeparam name="TValue">Type o value</typeparam>
        /// <param name="key">Setting key.</param>
        /// <param name="value">Setting value.</param>
        protected void Set(string key, object value)
        {
            Fields[key] = value;
        }
    }
}
