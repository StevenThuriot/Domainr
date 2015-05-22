using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Domainr
{
    class Domain : IDomain
    {
        private readonly AppDomain _domain;
        private bool _disposed;

        public Domain(AppDomain domain, Action<Exception> handler = null)
        {
            _domain = domain;

            if (handler != null)
            {
                var exceptionHandler = new ExceptionHandler(handler);
                domain.UnhandledException += exceptionHandler.Handle;
            }
        }


        public void Run(CrossAppDomainDelegate run)
        {
            _domain.DoCallBack(run);
        }

        public Task RunAsync(CrossAppDomainDelegate run)
        {
            return Task.Run(() => Run(run));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Disposing();
        }

        public T GetValue<T>(string key)
        {
            return (T)_domain.GetData(key);
        }

        public Task<T> GetValueAsync<T>(string key)
        {
            return Task.Run(() => GetValue<T>(key));
        }

        public object[] GetValues(params string[] keys)
        {
            return keys.Select(_domain.GetData).ToArray();
        }

        public Task<object[]> GetValuesAsync(params string[] keys)
        {
            return Task.Run(() => GetValues(keys));
        }

        ~Domain()
        {
            Disposing();
        }

        private void Disposing()
        {
            if (_disposed) return;

            _disposed = true;

            if (_domain != null)
            {
                try
                {
                    AppDomain.Unload(_domain);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }
    }
}