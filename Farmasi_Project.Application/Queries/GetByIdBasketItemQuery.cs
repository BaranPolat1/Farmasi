using Farmasi_Project.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi_Project.Application.Queries
{
  public class GetByIdBasketItemQuery:IRequest<List<Product>>
  {
    public string BasketId { get; set; }
  }
}
