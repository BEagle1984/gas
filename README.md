[![NuGet](http://img.shields.io/nuget/vpre/Gas.svg)](https://www.nuget.org/packages/Gas/)
[![Build Status](https://beagle1984.visualstudio.com/Gas/_apis/build/status/BEagle1984.gas?branchName=develop)](https://beagle1984.visualstudio.com/Gas/_build/latest?definitionId=3&branchName=develop)
[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/BEagle1984/gas/blob/master/LICENSE)

# Generic Assembly Scanner (aka Gas)

This simple library implements assembly scanning in a generic and reusable way.

# Usage Examples

The generic nature of this library makes it suitable for multiple purposes, here are just a few examples.

## .NET Core DI

```c#
using Gas;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        AssemblyScanner
            .Scan(Assembly.GetExecutingAssembly())
            .ForEachConcreteClassImplementing<IService>()
                .ForEachImplementedInterface()
                .Do((classType, interfaceType) => 
                    services.AddScoped(interfaceType, classType))
            .ForEachConcreteClassImplementing<ITextParser>()
                .Do(t => services.AddScoped(typeof(ITextParser), t));
    }
}
```
