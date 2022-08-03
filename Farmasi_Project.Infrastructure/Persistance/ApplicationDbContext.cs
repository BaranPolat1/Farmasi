
using Farmasi_Project.Application.Common.Interfaces;
using Farmasi_Project.Application.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi_Project.Infrastructure.Persistance
{
  public class ApplicationDbContext:IApplicationDbContext
  {
    public MongoClient MongoClient { get; set; }
    private IMongoDatabase _database = null;
    private readonly AppSettings _appSettings;
    public ApplicationDbContext(AppSettings appSettings)
    {
      _appSettings = appSettings;
    }
    private void ConfigureMongo()
    {
      if (MongoClient != null)
      {
        return;
      }

      MongoClient = new MongoClient(_appSettings.MongoConnection.ConnectionString);

      _database = MongoClient.GetDatabase(_appSettings.MongoConnection.Database);
    }

		public IMongoCollection<T> GetCollection<T>() where T : class, new()
		{
      ConfigureMongo();

      return _database.GetCollection<T>(typeof(T).Name);
    }
	}
}
