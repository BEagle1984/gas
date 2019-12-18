using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Gas
{
    public class AssemblyScanner : IAssemblyScanner
    {
        private readonly Assembly[] _assemblies;
        private IEnumerable<Type> _allTypes; 

        /// <summary>
        /// Creates a new instance of the <see cref="AssemblyScanner"/>.
        /// </summary>
        /// <param name="assemblies">The assemblies to be scanned.</param>
        public AssemblyScanner(Assembly[] assemblies)
        {
            _assemblies = assemblies ?? throw new ArgumentNullException(nameof(assemblies));
        }

        #region Scan (assemblies selection / factory methods)

        /// <summary>
        /// Creates an <see cref="IAssemblyScanner"/> to scan the specified assemblies.
        /// </summary>
        /// <param name="assemblies">The assemblies to be scanned.</param>
        /// <returns>An instance of <see cref="IAssemblyScanner"/> to scan the specified assemblies.</returns>
        public static IAssemblyScanner Scan(params Assembly[] assemblies) =>
            new AssemblyScanner(assemblies);

        /// <summary>
        /// Creates an <see cref="IAssemblyScanner"/> to scan the specified assemblies.
        /// </summary>
        /// <param name="assemblies">The assemblies to be scanned.</param>
        /// <returns>An instance of <see cref="IAssemblyScanner"/> to scan the specified assemblies.</returns>
        public static IAssemblyScanner Scan(IEnumerable<Assembly> assemblies) => Scan(assemblies?.ToArray());

        /// <summary>
        /// Creates an <see cref="IAssemblyScanner"/> to scan the assemblies containing the specified types.
        /// </summary>
        /// <param name="types">The types to be used to infer the assemblies to be scanned.</param>
        /// <returns>An instance of <see cref="IAssemblyScanner"/> to scan the specified assemblies.</returns>
        public static IAssemblyScanner Scan(params Type[] types) => Scan(types?.Select(t => t.Assembly));

        /// <summary>
        /// Creates an <see cref="IAssemblyScanner"/> to scan the assemblies containing the specified types.
        /// </summary>
        /// <param name="types">The types to be used to infer the assemblies to be scanned.</param>
        /// <returns>An instance of <see cref="IAssemblyScanner"/> to scan the specified assemblies.</returns>
        public static IAssemblyScanner Scan(IEnumerable<Type> types) => Scan(types?.Select(t => t.Assembly));

        #endregion

        private IEnumerable<Type> AllTypes => _allTypes ??= _assemblies.SelectMany(a => a.GetTypes());

        #region ForEachType* (types selection methods)

        /// <inheritdoc cref="IAssemblyScanner"/>
        public ITypeScanner ForEachTypeMatching(Func<Type, bool> predicate) => 
            new TypeScanner(AllTypes.Where(predicate), this);

        /// <inheritdoc cref="IAssemblyScanner"/>
        public ITypeScanner ForEachTypeImplementing<T>() => 
            ForEachTypeMatching(t => typeof(T).IsAssignableFrom(t));

        /// <inheritdoc cref="IAssemblyScanner"/>
        public ITypeScanner ForEachTypeImplementing(params Type[] types) => 
            ForEachTypeMatching(t => types.Any(T => T.IsAssignableFrom(t)));

        /// <inheritdoc cref="IAssemblyScanner"/>
        public ITypeScanner ForEachTypeImplementing(IEnumerable<Type> types) => 
            ForEachTypeImplementing(types.ToArray());

        #endregion
        
        #region ForEachTypeConcreteClass (types selection methods)

        /// <inheritdoc cref="IAssemblyScanner"/>
        public ITypeScanner ForEachConcreteClassMatching(Func<Type, bool> predicate) =>
            ForEachTypeMatching(t => t.IsClass && !t.IsAbstract && predicate(t));

        /// <inheritdoc cref="IAssemblyScanner"/>
        public ITypeScanner ForEachConcreteClassImplementing<T>() =>
            ForEachConcreteClassMatching(t => typeof(T).IsAssignableFrom(t));

        /// <inheritdoc cref="IAssemblyScanner"/>
        public ITypeScanner ForEachConcreteClassImplementing(params Type[] types) => 
            ForEachConcreteClassMatching(t => types.Any(T => T.IsAssignableFrom(t)));

        /// <inheritdoc cref="IAssemblyScanner"/>
        public ITypeScanner ForEachConcreteClassImplementing(IEnumerable<Type> types) => 
            ForEachConcreteClassImplementing(types.ToArray());

        #endregion
    }
}