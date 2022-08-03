using Farmasi_Project.Application.Features.Product.Commands;
using Farmasi_Project.Application.Models;
using Farmasi_Project.Domain.Entities;
using Farmasi_Project_API.Helper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Farmasi_Project_API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductController : ControllerBase
  {
    readonly IMediator _mediator;
    readonly ICacheHelper _cacheHelper;

    public ProductController(IMediator mediator, ICacheHelper cacheHelper)
    {
      _mediator = mediator;
      _cacheHelper = cacheHelper;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Create([FromBody] Product product)
    {
      ReturnModel<Product> returnModel = new ReturnModel<Product>() { IsSuccess = true};
      try
      {
        var creator = new CreateProductCommand() { Product = product };
        var result = await _mediator.Send(creator);
        returnModel.Data = result;
        returnModel.Message = "Ürün başarıyla eklendi.";
        await _cacheHelper.ClearCaches();

      }
      catch (Exception ex)
      {
        returnModel.IsSuccess = false;
        returnModel.Message = "Ürün eklenirken bir hata meydana geldi.";
        returnModel.Errors = new string[] { ex.Message };
      }
      return Ok(returnModel);
    }
    

  }
}
