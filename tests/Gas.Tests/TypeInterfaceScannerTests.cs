using System;
using System.Collections.Generic;
using FluentAssertions;
using Gas.Tests.Types;
using Xunit;

namespace Gas.Tests
{
    public class TypeInterfaceScannerTests
    {
        [Fact]
        public void Do_SomePairs_ExpectedPairsAreProcessed()
        {
            var types = new List<(Type classType, Type interfaceType)>();

            AssemblyScanner.Scan(typeof(IService))
                .ForEachConcreteClassImplementing<ISpecialServiceA>()
                .ForEachImplementedInterface()
                .Do((classType, interfaceType) => types.Add((classType, interfaceType)));

            types.Should().BeEquivalentTo(new List<(Type classType, Type interfaceType)>
            {
                (typeof(SpecialServiceA), typeof(ISpecialServiceA)),
                (typeof(SpecialServiceA), typeof(IServiceA)),
                (typeof(SpecialServiceA), typeof(IService))
            });
        }
        
        [Fact]
        public void DoAll_SomePairs_ExpectedPairsAreProcessed()
        {
            var types = new List<(Type classType, Type interfaceType)>();

            AssemblyScanner.Scan(typeof(IService))
                .ForEachConcreteClassImplementing<ISpecialServiceA>()
                .ForEachImplementedInterface()
                .DoForAll((classType, interfaceType) => types.Add((classType, interfaceType)));

            types.Should().BeEquivalentTo(new List<(Type classType, Type interfaceType)>
            {
                (typeof(SpecialServiceA), typeof(ISpecialServiceA)),
                (typeof(SpecialServiceA), typeof(IServiceA)),
                (typeof(SpecialServiceA), typeof(IService)),
                (typeof(SpecialServiceA), typeof(IDisposable))
            });
        }
    }
}