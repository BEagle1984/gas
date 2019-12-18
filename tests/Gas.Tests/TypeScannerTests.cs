using System;
using System.Collections.Generic;
using FluentAssertions;
using Gas.Tests.Types;
using Xunit;

namespace Gas.Tests
{
    public class TypeScannerTests
    {
        [Fact]
        public void ForEachImplementedInterface_SomeTypes_ExpectedPairsAreProcessed()
        {
            var types = new List<(Type classType, Type interfaceType)>();

            AssemblyScanner.Scan(typeof(IService))
                .ForEachConcreteClassImplementing<IServiceA>()
                .ForEachImplementedInterface()
                .Do((classType, interfaceType) => types.Add((classType, interfaceType)));

            types.Should().BeEquivalentTo(new List<(Type classType, Type interfaceType)>
            {
                (typeof(ServiceA), typeof(IServiceA)),
                (typeof(ServiceA), typeof(IService)),
                (typeof(SpecialServiceA), typeof(ISpecialServiceA)),
                (typeof(SpecialServiceA), typeof(IServiceA)),
                (typeof(SpecialServiceA), typeof(IService))
            });
        }
        
        [Fact]
        public void Do_SomeTypes_ExpectedTypesAreProcessed()
        {
            var types = new List<Type>();

            AssemblyScanner.Scan(typeof(IService))
                .ForEachConcreteClassImplementing<IServiceA>()
                .Do(t => types.Add(t));

            types.Should().BeEquivalentTo(
                typeof(ServiceA),
                typeof(SpecialServiceA));
        }
    }
}