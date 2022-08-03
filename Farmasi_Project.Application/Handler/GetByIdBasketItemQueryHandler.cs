using Farmasi_Project.Application.Common.Interfaces;
using Farmasi_Project.Application.Queries;
using Farmasi_Project.Domain.Entities;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi_Project.Application.Handler
{
  public class GetByIdBasketItemQueryHandler : IRequestHandler<GetByIdBasketItemQuery, List<Product>>
  {
    private IApplicationDbContext _context;

    public GetByIdBasketItemQueryHandler(IApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<List<Product>> Handle(GetByIdBasketItemQuery request, CancellationToken cancellationToken)
    {
      /* 
        var basketId = request.BasketId;
        Normal şartlarda request objesinden gelen Basket Id alınır ve ilgili logic kurularak baskete eklenen ürünlerin listesi getirilir.
        Lakin işlemleri uzatmamak adına product tablosuna eklediğim dataları çekip ekrana bastırıyorum.
       */
      var productList = await _context.GetCollection<Product>().FindAsync(_ => true, null);
      return await productList.ToListAsync();

    }
  }
}
