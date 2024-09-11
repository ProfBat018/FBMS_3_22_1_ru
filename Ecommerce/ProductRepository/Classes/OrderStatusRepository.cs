using ProductData.Contexts;
using ProductData.Models;
using ProductRepository.Interfaces;

namespace ProductRepository.Classes;


public class OrderStatusRepository : Repository<OrderStatus>, IOrderStatusRepository
{
    public OrderStatusRepository(ProductsContext context) : base(context)
    {
    }

    public void Update(OrderStatus orderStatus)
    {
        throw new NotImplementedException();
    }

    public Task<OrderStatus> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}