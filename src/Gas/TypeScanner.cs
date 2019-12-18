using System;
using System.Collections.Generic;

namespace Gas
{
    internal class TypeScanner : ITypeScanner
    {
        private readonly IEnumerable<Type> _types;
        private readonly IAssemblyScanner _assemblyScanner;

        public TypeScanner(IEnumerable<Type> types, IAssemblyScanner assemblyScanner)
        {
            _types = types ?? throw new ArgumentNullException(nameof(types));
            _assemblyScanner = assemblyScanner ?? throw new ArgumentNullException(nameof(assemblyScanner));
        }

        /// <inheritdoc cref="ITypeScanner"/>
        public IAssemblyScanner Do(Action<Type> action)
        {
            _types.ForEach(action);
            return _assemblyScanner;
        }

        /// <inheritdoc cref="ITypeScanner"/>
        public ITypeInterfaceScanner ForEachImplementedInterface() =>
            new TypeInterfaceScanner(_types, _assemblyScanner);
    }
}