using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductRepository.Interfaces;

namespace ProductsApiService.Controllers;

[ApiController]
public class ProductController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("Products/All")]
    public async Task<IActionResult> GetAllProductsAsync()
    {
        return Ok(await _unitOfWork.ProductRepository.GetAllAsync());
    }

    [HttpGet("Products/{page}/{pagesize}")]
    public async Task<IActionResult> GetProductsAsync(int page, int pagesize)
    {
        return Ok(await _unitOfWork.ProductRepository.GetAllPaginatedAsync(page, pagesize,
            includeProperties: "OrderItems"));
    }
}