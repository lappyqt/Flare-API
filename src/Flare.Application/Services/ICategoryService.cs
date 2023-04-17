using Flare.Application.Models.Category;
using Flare.Domain.Entities;

namespace Flare.Application.Services;

public interface ICategoryService
{
    Task<List<Category>> GetAllAsync();
    Task<CreateCategoryResponseModel> CreateCategoryAsync(CreateCategoryModel createCategoryModel);
    Task<DeleteCategoryResponseModel> DeleteCategoryAsync(DeleteCategoryModel deleteCategoryModel);
}