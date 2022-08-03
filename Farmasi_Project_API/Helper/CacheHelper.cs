using Farmasi_Project.Application.Common.Interfaces;
using Farmasi_Project.Application.Features.BasektItem.Queries;
using Farmasi_Project.Domain.Entities;
using MediatR;

namespace Farmasi_Project_API.Helper
{

  public interface ICacheHelper
  {
   public Task<List<Product>> GetBasketItems(string basketId);
   public Task ClearCaches();
  }

  public class CacheHelper : ICacheHelper
  {
    private IMediator _mediator;
    private IRedisCacheManager _cache;

    public CacheHelper(IMediator mediator, IRedisCacheManager cache)
    {
      _mediator = mediator;
      _cache = cache;
    }

    public async Task ClearCaches()
    {
      await _cache.AllClearAsync();
    }

    public async Task<List<Product>> GetBasketItems(string basketId)
    {
      var result = await _cache.GetAsync(basketId, async () =>
      {
        var creator = new GetByIdBasketItemQuery() { BasketId = basketId };
        var result = await _mediator.Send(creator);
        return result;
      }, TimeSpan.FromHours(24));
    
      return result;
    }
  }
}
