using System;
using System.Collections.Generic;

namespace Gas
{
    public interface IAssemblyScanner
    {
        /// <summary>
        /// Filters the types in the scanned assemblies according to the provided predicate and
        /// returns an <see cref="ITypeScanner"/> that can be used to perform an action for each <see cref="Type"/>.
        /// </summary>
        /// <param name="predicate">The filter to be applied.</param>
        /// <returns>An instance of <see cref="ITypeScanner"/> that can be used to perform an action on
        /// each <see cref="Type"/>.</returns>
        ITypeScanner ForEachTypeMatching(Func<Type, bool> predicate);

        /// <summary>
        /// Finds the types in the scanned assemblies that implement the specified type <typeparamref name="T"/>
        /// and returns an <see cref="ITypeScanner"/> that can be used to perform an action for each <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="T">The base class or interface to be used to filter the types in the scanned assemblies.</typeparam>
        /// <returns>An instance of <see cref="ITypeScanner"/> that can be used to perform an action on
        /// each <see cref="Type"/>.</returns>
        ITypeScanner ForEachTypeImplementing<T>();

        /// <summary>
        /// Finds the types in the scanned assemblies that implement any of specified types
        /// and returns an <see cref="ITypeScanner"/> that can be used to perform an action for each <see cref="Type"/>.
        /// </summary>
        /// <param name="types">The base classes or interfaces to be used to filter the types in the scanned assemblies.</param>
        /// <returns>An instance of <see cref="ITypeScanner"/> that can be used to perform an action on
        /// each <see cref="Type"/>.</returns>
        ITypeScanner ForEachTypeImplementing(params Type[] types);

        /// <summary>
        /// Finds the types in the scanned assemblies that implement any of specified types
        /// and returns an <see cref="ITypeScanner"/> that can be used to perform an action for each <see cref="Type"/>.
        /// </summary>
        /// <param name="types">The base classes or interfaces to be used to filter the types in the scanned assemblies.</param>
        /// <returns>An instance of <see cref="ITypeScanner"/> that can be used to perform an action on
        /// each <see cref="Type"/>.</returns>
        ITypeScanner ForEachTypeImplementing(IEnumerable<Type> types);

        /// <summary>
        /// Filters the non-abstract classes in the scanned assemblies according to the provided predicate and
        /// returns an <see cref="ITypeScanner"/> that can be used to perform an action for each <see cref="Type"/>.
        /// </summary>
        /// <param name="predicate">The filter to be applied.</param>
        /// <returns>An instance of <see cref="ITypeScanner"/> that can be used to perform an action on
        /// each <see cref="Type"/>.</returns>
        ITypeScanner ForEachConcreteClassMatching(Func<Type, bool> predicate);

        /// <summary>
        /// Finds the non-abstract classes in the scanned assemblies that implement the specified type
        /// <typeparamref name="T"/> and returns an <see cref="ITypeScanner"/> that can be used to perform an
        /// action for each <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="T">The base class or interface to be used to filter the types in the scanned assemblies.</typeparam>
        /// <returns>An instance of <see cref="ITypeScanner"/> that can be used to perform an action on
        /// each <see cref="Type"/>.</returns>
        ITypeScanner ForEachConcreteClassImplementing<T>();

        /// <summary>
        /// Finds the non-abstract classes in the scanned assemblies that implement any of specified types
        /// and returns an <see cref="ITypeScanner"/> that can be used to perform an action for each <see cref="Type"/>.
        /// </summary>
        /// <param name="types">The base classes or interfaces to be used to filter the types in the scanned assemblies.</param>
        /// <returns>An instance of <see cref="ITypeScanner"/> that can be used to perform an action on
        /// each <see cref="Type"/>.</returns>
        ITypeScanner ForEachConcreteClassImplementing(params Type[] types);

        /// <summary>
        /// Finds the non-abstract classes in the scanned assemblies that implement any of specified types
        /// and returns an <see cref="ITypeScanner"/> that can be used to perform an action for each <see cref="Type"/>.
        /// </summary>
        /// <param name="types">The base classes or interfaces to be used to filter the types in the scanned assemblies.</param>
        /// <returns>An instance of <see cref="ITypeScanner"/> that can be used to perform an action on
        /// each <see cref="Type"/>.</returns>
        ITypeScanner ForEachConcreteClassImplementing(IEnumerable<Type> types);
    }
}