using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi_Project.Application.Common.Interfaces
{
  public interface IRedisCacheManager : IDisposable
  {
    T Get<T>(string key, Func<T> acquire, TimeSpan date);
    Task<T> GetAsync<T>(string key, Func<Task<T>> acquire, TimeSpan date);
    Task RemoveAsync(string key);
    void Remove(string key);
    Task AllClearAsync();
    void AllClear();
  }
}
