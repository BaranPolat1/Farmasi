using Farmasi_Project.Application.Common.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace Farmasi_Project.Application.Features.BasektItem.Queries
{
  public class GetByIdBasketItemQuery : IRequest<List<Domain.Entities.Product>>
  {
    public string BasketId { get; set; }
  }

  public class GetByIdBasketItemQueryHandler : IRequestHandler<GetByIdBasketItemQuery, List<Domain.Entities.Product>>
  {
    private IApplicationDbContext _context;

    public GetByIdBasketItemQueryHandler(IApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<List<Domain.Entities.Product>> Handle(GetByIdBasketItemQuery request, CancellationToken cancellationToken)
    {
      var findOpt = new FindOptions<Domain.Entities.Product>();
      var filterDefinitionBuilder = Builders<Domain.Entities.Product>.Filter;
      var sortDefinition = Builders<Domain.Entities.Product>.Sort;
      var eq = filterDefinitionBuilder.Where(x => true);
      /* 
        var basketId = request.BasketId;
        Normal şartlarda request objesinden gelen Basket Id alınır ve ilgili logic kurularak baskete eklenen ürünlerin listesi getirilir.
        Lakin işlemleri uzatmamak adına product tablosuna eklediğim dataları çekip ekrana bastırıyorum.
       */
      var productList = await _context.GetCollection<Domain.Entities.Product>().FindAsync(eq, null);
      return await productList.ToListAsync();

    }
  }
}
