using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
            domain.AssemblyResolve += AssemblyResolve;

            if (handler != null)
            {
                var exceptionHandler = new ExceptionHandler(handler);
                domain.UnhandledException += exceptionHandler.Handle;
            }
        }

        ///<remarks>Assembly Resolving sometimes needs a little bit of help.</remarks>
        private static Assembly AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
        }


        public void Run(CrossAppDomainDelegate run)
        {
            _domain.DoCallBack(run);
        }

        public Task RunAsync(CrossAppDomainDelegate run)
        {
            return Task.Run(() => Run(run));
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

        public void SetData(string key, object value)
        {
            _domain.SetData(key, value);
        }

        public void SetData(Argument argument)
        {
            _domain.SetData(argument.Key, argument.Value);
        }

        public void SetData(params Argument[] arguments)
        {
            switch (arguments.Length)
            {
                default:
                    arguments.AsParallel().ForAll(x => _domain.SetData(x.Key, x.Value));
                    break;

                case 1:
                    SetData(arguments[0]);
                    break;

                case 0:
                    break;
            }
        }

        static readonly Lazy<Func<Dictionary<string, object[]>>> _getLocalStore = new Lazy<Func<Dictionary<string, object[]>>>(
                () =>
                {
                    var currentDomain = Expression.Property(null, typeof(AppDomain), "CurrentDomain");
                    var localStore = Expression.Field(currentDomain, "_LocalStore");
                    var lambda = Expression.Lambda<Func<Dictionary<string, object[]>>>(localStore);
                    var getLocalStore = lambda.Compile();

                    return getLocalStore;
                });

        /// <remarks>
        /// Because the <see cref="System.AppDomain"/>'s data cache is private, clearing the data is dependent on a reflection call. 
        /// This method will stop working if Microsoft ever decides to rename the <see cref="System.AppDomain._LocalStore"/> field. (Even though this is highly unlikely)
        /// </remarks>
        public void ClearData()
        {
            Run(ClearDataInDomain);
        }

        private static void ClearDataInDomain()
        {
            try
            {
                var localStore = _getLocalStore.Value();

                if (localStore == null)
                    return;

                localStore.Clear();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }






        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Disposing();
        }

        ~Domain()
        {
            Disposing();
        }

        private void Disposing()
        {
            if (_disposed) return;

            _disposed = true;

            if (_domain == null) return;

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