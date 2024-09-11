using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductData.Contexts;
using ProductRepository.Interfaces;

namespace ProductsApiService.Controllers;

[ApiController]
public class OrderController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderController(IUnitOfWork unitOfWork, ProductsContext context)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("Orders/All")]
    public async Task<IActionResult> GetAllOrdersAsync()
    {
        return Ok(await _unitOfWork.ProductRepository.GetAllAsync());
    }

    [HttpGet("Orders/{page}/{pagesize}")]
    public async Task<IActionResult> GetOrdersAsync(int page, int pagesize)
    {
        return Ok(await _unitOfWork.OrderRepository.GetAllPaginatedAsync(page, pagesize, includeProperties: "OrderItems"));
    }
    
}