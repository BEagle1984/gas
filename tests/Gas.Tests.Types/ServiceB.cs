using System;

namespace Gas.Tests.Types
{
    public class ServiceB : IServiceB, IDisposable
    {
        public void Dispose() { }
    }
}