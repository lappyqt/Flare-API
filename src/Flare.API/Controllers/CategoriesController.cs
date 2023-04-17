using Flare.Application.Models.Category;
using Flare.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flare.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoriesController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllCategoriesAsync() 
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategoryAsync(CreateCategoryModel createCategoryModel)
    {
        return Ok(await _service.CreateCategoryAsync(createCategoryModel));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCategoryAsync(DeleteCategoryModel deleteCategoryModel)
    {
        return Ok(await _service.DeleteCategoryAsync(deleteCategoryModel));
    }
}