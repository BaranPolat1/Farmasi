using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi_Project.Application.Common.Interfaces
{
  public interface IApplicationDbContext
  {
    public IMongoCollection<T> GetCollection<T>();

  }
}
