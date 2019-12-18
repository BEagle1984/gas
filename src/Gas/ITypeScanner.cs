using System;

namespace Gas
{
    public interface ITypeScanner
    {
        /// <summary>
        /// Executes the specified action for each <see cref="Type"/>. 
        /// </summary>
        /// <param name="action">The action to be performed.</param>
        /// <returns>The instance of <see cref="IAssemblyScanner"/> that created this <see cref="ITypeScanner"/>.</returns>
        IAssemblyScanner Do(Action<Type> action);

        /// <summary>
        /// Retrieves all interfaces implemented by each type and returns an <see cref="ITypeInterfaceScanner"/> that
        /// can be used to perform an action for each <see cref="Type"/> and interface pair.
        /// </summary>
        /// <returns>An instance of <see cref="ITypeInterfaceScanner"/> that can be used to perform an action
        /// for each <see cref="Type"/> and interface pair.</returns>
        ITypeInterfaceScanner ForEachImplementedInterface();
    }
}