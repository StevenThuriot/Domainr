#region License
//  
// Copyright 2015 Steven Thuriot
//  
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
#endregion

using System;
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
                AppDomain.Unload(_domain);
            }
        }
    }
}