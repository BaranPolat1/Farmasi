using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi_Project.Domain.Entities
{
  public class Product:BaseEntity
  {
    public string ProductName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

  }
}
