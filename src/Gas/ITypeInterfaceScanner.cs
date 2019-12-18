using System;
using System.Collections;

namespace Gas
{
    public interface ITypeInterfaceScanner
    {
        /// <summary>
        /// Executes the specified action for each <see cref="Type"/> and interface pair, excluding the 
        /// interfaces from the <c>System</c> namespace (e.g. <see cref="IDisposable"/>, <see cref="IEnumerable"/>, ...)
        /// </summary>
        /// <param name="action">The action to be performed. The two <see cref="Type"/> parameters are respectively
        /// the type itself and the implemented interface.</param>
        /// <returns>The instance of <see cref="IAssemblyScanner"/> that indirectly created this <see cref="ITypeInterfaceScanner"/>.</returns>
        IAssemblyScanner Do(Action<Type, Type> action);

        /// <summary>
        /// Executes the specified action for each <see cref="Type"/> and interface pair, no interface is excluded.
        /// </summary>
        /// <param name="action">The action to be performed. The two <see cref="Type"/> parameters are respectively
        /// the type itself and the implemented interface.</param>
        /// <returns>The instance of <see cref="IAssemblyScanner"/> that indirectly created this <see cref="ITypeInterfaceScanner"/>.</returns>
        IAssemblyScanner DoForAll(Action<Type, Type> action);
    }
}