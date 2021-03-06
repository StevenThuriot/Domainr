using System;
using System.Threading.Tasks;

namespace Domainr
{
    public interface IDomain : IDisposable
    {
        void Run(CrossAppDomainDelegate run);
        Task RunAsync(CrossAppDomainDelegate run);
        T GetValue<T>(string key);
        Task<T> GetValueAsync<T>(string key);
        object[] GetValues(params string[] keys);
        Task<object[]> GetValuesAsync(params string[] keys);
        void SetData(string key, object value);
        void SetData(Argument argument);
        void SetData(params Argument[] arguments);
        void ClearData();
    }
}