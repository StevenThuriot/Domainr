using System;
using System.Threading;

namespace Domainr
{
    public interface ITokenSource : IDisposable
    {
        CancellationToken Token { get; }
        bool IsCancellationRequested { get; }
        void Cancel();
        void ThrowIfCancellationRequested();
    }
}