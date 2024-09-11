using ProductData.Contexts;
using ProductData.Models;
using ProductRepository.Interfaces;

namespace ProductRepository.Classes;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly ProductsContext _context;
    public ProductRepository(ProductsContext context) : base(context)
    {
        _context = context;
    }


    public void Update(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<Product> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}