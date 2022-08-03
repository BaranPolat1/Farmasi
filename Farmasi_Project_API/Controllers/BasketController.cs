using Farmasi_Project_API.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Farmasi_Project_API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BasketController : ControllerBase
  {
    private readonly ICacheHelper _cacheHelper;

    public BasketController(ICacheHelper cacheHelper)
    {
      _cacheHelper = cacheHelper;
    }

    /// <summary>
    /// Databasedaki ürünleri set ediyorum.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetBasketItems(string basketId)
    {
      var basketItems = await _cacheHelper.GetBasketItems(basketId);
      return Ok(basketItems ?? new List<Farmasi_Project.Domain.Entities.Product>());
    }
  }
}
