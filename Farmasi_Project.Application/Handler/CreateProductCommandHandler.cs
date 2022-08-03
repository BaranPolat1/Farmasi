using Farmasi_Project.Application.Command;
using Farmasi_Project.Application.Common.Interfaces;
using Farmasi_Project.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmasi_Project.Application.Handler
{
  public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
  {
    readonly IApplicationDbContext _context;
    public CreateProductCommandHandler(IApplicationDbContext context)
    {
      _context = context;
    }
    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
      await _context.GetCollection<Product>().InsertOneAsync(request.Product);
      return request.Product;
    }
  }
}
