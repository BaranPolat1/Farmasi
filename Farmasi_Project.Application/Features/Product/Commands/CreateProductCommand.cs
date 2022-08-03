using Farmasi_Project.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi_Project.Application.Features.Product.Commands
{
	public class CreateProductCommand:IRequest<Domain.Entities.Product>
	{
		public Domain.Entities.Product Product { get; set; }
	}


  public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Domain.Entities.Product>
  {
    readonly IApplicationDbContext _context;
    public CreateProductCommandHandler(IApplicationDbContext context)
    {
      _context = context;
    }
    public async Task<Domain.Entities.Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
      await _context.GetCollection<Domain.Entities.Product>().InsertOneAsync(request.Product);
      return request.Product;
    }
  }
}
