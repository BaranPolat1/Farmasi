using Farmasi_Project.Application.Common.Interfaces;
using Farmasi_Project.Application.Models;
using Microsoft.Extensions.Caching.Distributed;

using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi_Project.Infrastructure.Services
{
  public class RedisCacheManager : IRedisCacheManager
  {
   
 
    private  bool _disposed;
    private readonly AppSettings _appsettings;
    private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
    private readonly ConfigurationOptions configuration = null;
    private Lazy<IConnectionMultiplexer> _Connection = null;
    public RedisCacheManager(AppSettings appSettings, AppSettings appsettings)
    {
      configuration = new ConfigurationOptions()
      {
        EndPoints = { { appSettings.RedisSettings.Host, int.Parse(appSettings.RedisSettings.Port) }, },
        AllowAdmin = true,
        ClientName = "Test",
        ReconnectRetryPolicy = new LinearRetry(5000),
        AbortOnConnectFail = false,
      };
      _Connection = new Lazy<IConnectionMultiplexer>(() =>
      {
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(configuration);
        return redis;
      });
      _appsettings = appsettings;
    }


    public IConnectionMultiplexer Connection { get { return _Connection.Value; } }
    public IDatabase _cache => Connection.GetDatabase();

    public T Get<T>(string key, Func<T> acquire, TimeSpan date)
    {
    
      var cache = _cache.StringGet(key);
      var cacheObject = default(T);
      if (!string.IsNullOrWhiteSpace(cache))
        return JsonConvert.DeserializeObject<T>(cache);

      try
      {
        semaphore.Wait();

        cache = _cache.StringGet(key);
        if (!string.IsNullOrWhiteSpace(cache))
          return JsonConvert.DeserializeObject<T>(cache);

        cacheObject = acquire();

        if (date.TotalMinutes > 0 && cacheObject != null && !cacheObject.Equals(default(T)))
          Set(key, cacheObject, date);
      }
      finally
      {
        semaphore.Release();
      }
      return cacheObject;
    }

    public async Task<T> GetAsync<T>(string key, Func<Task<T>> acquire, TimeSpan date)
    {
      var cache = await _cache.StringGetAsync(key);
      var cacheObject = default(T);

      if (!string.IsNullOrWhiteSpace(cache))
        return JsonConvert.DeserializeObject<T>(cache);

      try
      {
        await semaphore.WaitAsync();

        cache = await _cache.StringGetAsync(key);
        if (!string.IsNullOrWhiteSpace(cache))
          return JsonConvert.DeserializeObject<T>(cache);

        cacheObject = await acquire();

        if (date.TotalMinutes > 0 && cacheObject != null && !cacheObject.Equals(default(T)))
          await SetAsync(key, cacheObject, date);
      }
      finally
      {
        semaphore.Release();
      }
      return cacheObject;
    }

    public bool IsSet(string key)
    {
      var cache = _cache.StringGet(key);
      if (!string.IsNullOrWhiteSpace(cache))
        return true;

      return false;
    }

    public async Task<bool> IsSetAsync(string key)
    {
      var cache = await _cache.StringGetAsync(key);
      if (!string.IsNullOrWhiteSpace(cache))
        return true;

      return false;
    }

    public void Remove(string key)
    {
      _cache.KeyDelete(key);
    }

    public async Task RemoveAsync(string key)
    {
      await _cache.KeyDeleteAsync(key);
    }

    private void Set(string key, object data, TimeSpan date)
    {
      try
      {
        _cache.StringSet(key, JsonConvert.SerializeObject(data),expiry: date);
      }
      catch (Exception) { }
    }

    private async Task SetAsync(string key, object data, TimeSpan date)
    {
      try
      {
        await _cache.StringSetAsync(key, JsonConvert.SerializeObject(data), expiry: date);
      }
      catch (Exception) { }
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
      if (_disposed)
        return;

      _disposed = true;
    }

    public async Task AllClearAsync()
    {
      
      try
      {
        var server = Connection.GetServer(host: _appsettings.RedisSettings.Host, port: int.Parse(_appsettings.RedisSettings.Port));
        await server.FlushAllDatabasesAsync();
      }
      catch (Exception) { }
     
    }
    public void AllClear()
    {
      try
      {
        var server = Connection.GetServer(host: _appsettings.RedisSettings.Host, port: int.Parse(_appsettings.RedisSettings.Port));
        server.FlushAllDatabases();
      }
      catch (Exception) { }
    }
  }

}
