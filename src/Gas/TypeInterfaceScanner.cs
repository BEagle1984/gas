using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Gas
{
    internal class TypeInterfaceScanner : ITypeInterfaceScanner
    {
        private readonly IEnumerable<Type> _types;
        private readonly IAssemblyScanner _assemblyScanner;

        public TypeInterfaceScanner(IEnumerable<Type> types, IAssemblyScanner assemblyScanner)
        {
            _types = types ?? throw new ArgumentNullException(nameof(types));
            _assemblyScanner = assemblyScanner ?? throw new ArgumentNullException(nameof(assemblyScanner));
        }

        /// <see cref="ITypeInterfaceScanner"/>
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public IAssemblyScanner Do(Action<Type, Type> action) =>
            Do(t => !t.FullName.StartsWith("System."), action);

        /// <see cref="ITypeInterfaceScanner"/>
        public IAssemblyScanner DoForAll(Action<Type, Type> action) =>
            Do(_ => true, action);

        private IAssemblyScanner Do(Func<Type, bool> interfaceFilter, Action<Type, Type> action)
        {
            _types.ForEach(t => 
                t.GetInterfaces()
                    .Where(interfaceFilter)
                    .ForEach(i => action(t, i)));
            
            return _assemblyScanner;
        }
    }
}