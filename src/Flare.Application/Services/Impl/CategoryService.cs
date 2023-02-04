using Flare.Application.Models.Category;
using Flare.DataAccess;
using Flare.Domain.Entities;

namespace Flare.Application.Services.Impl;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateCategoryResponseModel> CreateCategoryAsync(CreateCategoryModel createCategoryModel)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = createCategoryModel.Name,
            Type = createCategoryModel.Type
        };

        await _unitOfWork.Categories.AddAsync(category);

        return new CreateCategoryResponseModel
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task<DeleteCategoryResponseModel> DeleteCategoryAsync(DeleteCategoryModel deleteCategoryModel)
    {
        var category = await _unitOfWork.Categories.GetAsync(x => x.Id == deleteCategoryModel.Id);

        if (category == null) throw new Exception("Category not found");

        await _unitOfWork.Categories.RemoveAsync(category);
        return new DeleteCategoryResponseModel { Id = category.Id }; 
    }
}