using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductData.Configs;
using ProductData.DTO;
using ProductData.Models;
using ProductRepository.Interfaces;

namespace ProductsApiService.Controllers;

[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly Mapper _mapper;

    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = MappingConfiguration.InitializeConfig();
    }

    [HttpGet("Category/All")]
    public async Task<IActionResult> GetAllCategoriesAsync()
    {
        var res = await _unitOfWork.CategoryRepository.GetAllAsync();

        var mappingRes = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(res);
        
        return Ok(mappingRes);
    }
}