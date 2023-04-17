using Flare.Application.Exceptions;
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

    public async Task<List<Category>> GetAllAsync()
    {
        var categories = await _unitOfWork.Categories.GetAllAsync(orderBy: x => x.Name);

        return categories.ToList();
    }

    public async Task<CreateCategoryResponseModel> CreateCategoryAsync(CreateCategoryModel createCategoryModel)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = createCategoryModel.Name
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

        if (category == null) throw new NotFoundException($"Category {deleteCategoryModel.Id} not found");

        await _unitOfWork.Categories.RemoveAsync(category);
        return new DeleteCategoryResponseModel { Id = category.Id }; 
    }
}