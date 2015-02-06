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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domainr
{
    public static class Domainr
    {
        public static Argument AsArgument(this object value, string name)
        {
            return new Argument(name, value);
        }



        public static IDomain Init(string name, params Argument[] parameters)
        {
            return Init(name, null, parameters);
        }
        public static IDomain Init(params Argument[] parameters)
        {
            return Init("TempDomain", null, parameters);
        }

        public static IDomain Init(Action<Exception> handler, params Argument[] parameters)
        {
            return Init("TempDomain", handler, parameters);
        }

        public static IDomain Init(string name, Action<Exception> handler, params Argument[] parameters)
        {
            var domain = AppDomain.CreateDomain(name);
            
            parameters.AsParallel().ForAll(x => domain.SetData(x.Key, x.Value));
            return new Domain(domain, handler);
        }








        public static Task<IDomain> InitAsync(string name, params Argument[] parameters)
        {
            return Task.Run(() => Init(name, null, parameters));
        }
        public static Task<IDomain> InitAsync(params Argument[] parameters)
        {
            return Task.Run(() => Init("TempDomain", null, parameters));
        }

        public static Task<IDomain> InitAsync(Action<Exception> handler, params Argument[] parameters)
        {
            return Task.Run(() => Init("TempDomain", handler, parameters));
        }

        public static Task<IDomain> InitAsync(string name, Action<Exception> handler, params Argument[] parameters)
        {
            return Task.Run(() => Init(name, handler, parameters));
        }






        public static T GetValue<T>(string key)
        {
            return (T)AppDomain.CurrentDomain.GetData(key);
        }

        public static object[] GetValues(params string[] keys)
        {
            var currentDomain = AppDomain.CurrentDomain;
            return keys.Select(currentDomain.GetData).ToArray();
        }

        public static void SetValueInDomain(this object value, string key)
        {
            AppDomain.CurrentDomain.SetData(key, value);
        }

        public static void SetValues(IDictionary<string, object> dictionary)
        {
            var currentDomain = AppDomain.CurrentDomain;
            
            foreach (var kvp in dictionary)
                currentDomain.SetData(kvp.Key, kvp.Value);
        }
    }
}
