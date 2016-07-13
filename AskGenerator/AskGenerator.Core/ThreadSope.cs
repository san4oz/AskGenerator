using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AskGenerator.Core
{
    /// <summary>
    /// A class which sets the indicator in the current thread when created and removes it when disposed.
    /// </summary>
    public sealed class ThreadScope : IDisposable
    {
        private string name;

        /// <summary>
        /// Determines whether the scope with the specified name has been created and not yet disposed.
        /// </summary>
        /// <param name="name">The name of the scope.</param>
        /// <returns>
        ///   <c>true</c> if the scope with the specified name has been created and not yet disposed; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInScope(string name)
        {
            return (bool)(Thread.GetData(Thread.GetNamedDataSlot(name)) ?? false);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadScope"/> class.
        /// </summary>
        /// <param name="name">The name of the scope.</param>
        public ThreadScope(string name)
        {
            if (name.IsEmpty())
                throw new ArgumentNullException("name", "Scope name could not be empty.");

            this.name = name;
            Thread.SetData(Thread.GetNamedDataSlot(name), true);
        }

        /// <summary>
        /// Removes an indicator from the current thread data.
        /// </summary>
        public void Dispose()
        {
            Thread.SetData(Thread.GetNamedDataSlot(name), false);
        }
    }
}
