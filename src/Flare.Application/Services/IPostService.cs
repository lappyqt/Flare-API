using Flare.Application.Models.Post;
using Flare.Domain.Entities;
using Flare.Domain.Enums;

namespace Flare.Application.Services;

public interface IPostService
{
    Task<Post> GetAsync(Guid id);
    Task<List<Post>> GetAllAsync(PostParameters postParameters);
    Task<CreatePostResponseModel> CreatePostAsync(CreatePostModel createPostModel);
    Task<UpdatePostResponseModel> UpdatePostAsync(UpdatePostModel updatePostModel);
    Task<DeletePostResponseModel> DeletePostAsync(DeletePostModel deletePostModel);
}