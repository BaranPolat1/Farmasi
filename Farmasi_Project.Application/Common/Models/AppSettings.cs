using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi_Project.Application.Models
{
  public class AppSettings
  {
    public MongoConnection MongoConnection { get; set; }
    public RedisSettings RedisSettings { get; set; }
  }
  public class MongoConnection
  {
    public string ConnectionString { get; set; }
    public string Database { get; set; }
  }

  public class RedisSettings
  {
    public string RedisConnectionString { get; set; }
    public string Host { get; set; }
    public string Port { get; set; }
  }
}
