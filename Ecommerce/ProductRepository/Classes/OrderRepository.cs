using ProductData.Contexts;
using ProductData.Models;
using ProductRepository.Interfaces;

namespace ProductRepository.Classes;


public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(ProductsContext context) : base(context)
    {
    }

    public void Update(Order orderRepository)
    {
        throw new NotImplementedException();
    }

    public Task<Order> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
