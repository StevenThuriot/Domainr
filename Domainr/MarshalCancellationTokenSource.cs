using System;
using System.Threading;

namespace Domainr
{
    public class MarshalCancellationTokenSource : MarshalByRefObject, ITokenSource
    {
        private readonly CancellationTokenSource _source;

        public MarshalCancellationTokenSource()
            : this(new CancellationTokenSource())
        {
        }

        public MarshalCancellationTokenSource(CancellationTokenSource source)
        {
            _source = source;
        }

        public bool IsCancellationRequested
        {
            get { return _source.IsCancellationRequested; }
        }

        public CancellationToken Token
        {
            get { return _source.Token; }
        }

        public void Cancel()
        {
            _source.Cancel();
        }

        public void ThrowIfCancellationRequested()
        {
            _source.Token.ThrowIfCancellationRequested();
        }

        public void Dispose()
        {
            _source.Dispose();
        }
    }
}