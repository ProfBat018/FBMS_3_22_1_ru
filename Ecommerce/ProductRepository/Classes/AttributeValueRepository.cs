using ProductData.Contexts;
using ProductData.Models;
using ProductRepository.Interfaces;

namespace ProductRepository.Classes;


public class AttributeValueRepository : Repository<AttributeValue>, IAttributeValueRepository
{
    public AttributeValueRepository(ProductsContext context) : base(context)
    {
    }

    public void Update(AttributeValue attributeValue)
    {
        throw new NotImplementedException();
    }

    public Task<AttributeValue> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}




