using Flare.Application.Models.Category;

namespace Flare.Application.Services;

public interface ICategoryService
{
    Task<CreateCategoryResponseModel> CreateCategoryAsync(CreateCategoryModel createCategoryModel);
    Task<DeleteCategoryResponseModel> DeleteCategoryAsync(DeleteCategoryModel deleteCategoryModel);
}