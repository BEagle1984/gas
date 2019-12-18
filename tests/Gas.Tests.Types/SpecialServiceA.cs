using System;

namespace Gas.Tests.Types
{
    public class SpecialServiceA : ServiceA, IDisposable, ISpecialServiceA
    {
        public void Dispose() { }
    }
}