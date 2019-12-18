using System;
using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using Gas.Tests.OtherTestTypes;
using Gas.Tests.Types;
using Xunit;

namespace Gas.Tests
{
    public class AssemblyScannerTests
    {
        #region Scan

        [Fact]
        public void Scan_SingleAssembly_TypesAreLoaded()
        {
            var types = new List<Type>();
            
            AssemblyScanner.Scan(Assembly.GetExecutingAssembly())
                .ForEachTypeImplementing<IService>()
                .Do(t => types.Add(t));

            types.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void Scan_MultipleAssemblies_TypesAreLoadedFromAllAssemblies()
        {
            var types = new List<Type>();
            
            AssemblyScanner.Scan(
                    Assembly.GetExecutingAssembly(), 
                    typeof(IService).Assembly)
                .ForEachTypeImplementing<IService>()
                .Do(t => types.Add(t));

            types.Should().Contain(typeof(ServiceA));
            types.Should().Contain(typeof(ServiceD));
        }
        
        [Fact]
        public void Scan_AssembliesEnumerable_TypesAreLoadedFromAllAssemblies()
        {
            var types = new List<Type>();

            AssemblyScanner.Scan(
                    new List<Assembly>
                    {
                        Assembly.GetExecutingAssembly(),
                        typeof(IService).Assembly
                    })
                .ForEachTypeImplementing<IService>()
                .Do(t => types.Add(t));

            types.Should().Contain(typeof(ServiceA));
            types.Should().Contain(typeof(ServiceD));
        }
        
        [Fact]
        public void Scan_AssembliesFromMultipleTypes_TypesAreLoadedFromAllAssemblies()
        {
            var types = new List<Type>();
            
            AssemblyScanner.Scan(
                    typeof(IService), 
                    typeof(ServiceD))
                .ForEachTypeImplementing<IService>()
                .Do(t => types.Add(t));

            types.Should().Contain(typeof(ServiceA));
            types.Should().Contain(typeof(ServiceD));
        }
        
        [Fact]
        public void Scan_AssembliesFromTypesEnumerable_TypesAreLoadedFromAllAssemblies()
        {
            var types = new List<Type>();

            AssemblyScanner.Scan(
                    new List<Type>
                    {
                        typeof(IService),
                        typeof(ServiceD)
                    })
                .ForEachTypeImplementing<IService>()
                .Do(t => types.Add(t));

            types.Should().Contain(typeof(ServiceA));
            types.Should().Contain(typeof(ServiceD));
        }

        #endregion

        #region ForEachConcreteClass

        [Fact]
        public void ForEachConcreteClassImplementing_InterfaceViaTypeArgument_ExpectedTypesAreProcessed()
        {
            var types = new List<Type>();
            
            AssemblyScanner.Scan(typeof(IService))
                .ForEachConcreteClassImplementing<IService>()
                .Do(t => types.Add(t));

            types.Should().BeEquivalentTo(
                typeof(ServiceA), 
                typeof(SpecialServiceA), 
                typeof(ServiceB), 
                typeof(ServiceC));
        }
        
        [Fact]
        public void ForEachConcreteClassImplementing_MultipleInterfaces_ExpectedTypesAreProcessed()
        {
            var types = new List<Type>();
            
            AssemblyScanner.Scan(typeof(IService))
                .ForEachConcreteClassImplementing(typeof(IServiceA), typeof(IServiceB))
                .Do(t => types.Add(t));

            types.Should().BeEquivalentTo(
                typeof(ServiceA), 
                typeof(SpecialServiceA), 
                typeof(ServiceB));
        }
                
        [Fact]
        public void ForEachConcreteClassImplementing_InterfacesEnumerable_ExpectedTypesAreProcessed()
        {
            var types = new List<Type>();

            AssemblyScanner.Scan(typeof(IService))
                .ForEachConcreteClassImplementing(new List<Type>
                {
                    typeof(IServiceA),
                    typeof(IServiceB)
                })
                .Do(t => types.Add(t));

            types.Should().BeEquivalentTo(
                typeof(ServiceA), 
                typeof(SpecialServiceA), 
                typeof(ServiceB));
        }
        [Fact]
        public void ForEachConcreteClassImplementing_BaseClassViaTypeArgument_ExpectedTypesAreProcessed()
        {
            var types = new List<Type>();
            
            AssemblyScanner.Scan(typeof(IService))
                .ForEachConcreteClassImplementing<BaseServiceA>()
                .Do(t => types.Add(t));

            types.Should().BeEquivalentTo(
                typeof(ServiceA), 
                typeof(SpecialServiceA));
        }
        
        [Fact]
        public void ForEachConcreteClassImplementing_MultipleBaseClasses_ExpectedTypesAreProcessed()
        {
            var types = new List<Type>();
            
            AssemblyScanner.Scan(typeof(IService))
                .ForEachConcreteClassImplementing(typeof(BaseServiceA), typeof(BaseServiceC))
                .Do(t => types.Add(t));

            types.Should().BeEquivalentTo(
                typeof(ServiceA), 
                typeof(SpecialServiceA),
                typeof(ServiceC));
        }
 
        [Fact]
        public void ForEachConcreteClassImplementing_BaseClassesEnumerable_ExpectedTypesAreProcessed()
        {
            var types = new List<Type>();
            
            AssemblyScanner.Scan(typeof(IService))
                .ForEachConcreteClassImplementing(new List<Type>
                {
                    typeof(BaseServiceA), 
                    typeof(BaseServiceC)
                })
                .Do(t => types.Add(t));

            types.Should().BeEquivalentTo(
                typeof(ServiceA), 
                typeof(SpecialServiceA),
                typeof(ServiceC));
        }
        
        #endregion

        #region ForEachType

        [Fact]
        public void ForEachTypeImplementing_InterfaceViaTypeArgument_ExpectedTypesAreProcessed()
        {
            var types = new List<Type>();
            
            AssemblyScanner.Scan(typeof(IService))
                .ForEachTypeImplementing<IServiceA>()
                .Do(t => types.Add(t));

            types.Should().BeEquivalentTo(
                typeof(BaseServiceA), 
                typeof(ServiceA), 
                typeof(SpecialServiceA), 
                typeof(IServiceA));
        }
        
        [Fact]
        public void ForEachTypeImplementing_MultipleInterfaces_ExpectedTypesAreProcessed()
        {
            var types = new List<Type>();
            
            AssemblyScanner.Scan(typeof(IService))
                .ForEachTypeImplementing(typeof(IServiceA), typeof(IServiceB))
                .Do(t => types.Add(t));

            types.Should().BeEquivalentTo(
                typeof(BaseServiceA), 
                typeof(ServiceA), 
                typeof(SpecialServiceA), 
                typeof(ServiceB),
                typeof(IServiceA),
                typeof(IServiceB));
        }
                
        [Fact]
        public void ForEachTypeImplementing_InterfacesEnumerable_ExpectedTypesAreProcessed()
        {
            var types = new List<Type>();

            AssemblyScanner.Scan(typeof(IService))
                .ForEachTypeImplementing(new List<Type>
                {
                    typeof(IServiceA),
                    typeof(IServiceB)
                })
                .Do(t => types.Add(t));

            types.Should().BeEquivalentTo(
                typeof(BaseServiceA), 
                typeof(ServiceA), 
                typeof(SpecialServiceA), 
                typeof(ServiceB),
                typeof(IServiceA),
                typeof(IServiceB));
        }
        [Fact]
        public void ForEachTypeImplementing_BaseClassViaTypeArgument_ExpectedTypesAreProcessed()
        {
            var types = new List<Type>();
            
            AssemblyScanner.Scan(typeof(IService))
                .ForEachTypeImplementing<BaseServiceA>()
                .Do(t => types.Add(t));

            types.Should().BeEquivalentTo(
                typeof(BaseServiceA), 
                typeof(ServiceA), 
                typeof(SpecialServiceA));
        }
        
        [Fact]
        public void ForEachTypeImplementing_MultipleBaseClasses_ExpectedTypesAreProcessed()
        {
            var types = new List<Type>();
            
            AssemblyScanner.Scan(typeof(IService))
                .ForEachTypeImplementing(typeof(BaseServiceA), typeof(BaseServiceC))
                .Do(t => types.Add(t));

            types.Should().BeEquivalentTo(
                typeof(BaseServiceA),
                typeof(ServiceA), 
                typeof(SpecialServiceA),
                typeof(BaseServiceC),
                typeof(ServiceC));
        }
 
        [Fact]
        public void ForEachTypeImplementing_BaseClassesEnumerable_ExpectedTypesAreProcessed()
        {
            var types = new List<Type>();
            
            AssemblyScanner.Scan(typeof(IService))
                .ForEachTypeImplementing(new List<Type>
                {
                    typeof(BaseServiceA), 
                    typeof(BaseServiceC)
                })
                .Do(t => types.Add(t));

            types.Should().BeEquivalentTo(
                typeof(BaseServiceA),
                typeof(ServiceA), 
                typeof(SpecialServiceA),
                typeof(BaseServiceC),
                typeof(ServiceC));
        }
        
        #endregion

        #region ForEachTypeMatching
        
        [Fact]
        public void ForEachTypeMatching_Predicate_ExpectedTypesAreProcessed()
        {
            var types = new List<Type>();
            
            AssemblyScanner.Scan(typeof(IService))
                .ForEachTypeMatching(t => t.Name == "SpecialServiceA")
                .Do(t => types.Add(t));

            types.Should().BeEquivalentTo(typeof(SpecialServiceA));
        }

        #endregion
    }
}