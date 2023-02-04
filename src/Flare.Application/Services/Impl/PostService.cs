using System.Collections.ObjectModel;
using System.Security.Claims;
using AutoMapper;
using Flare.Application.Models.Post;
using Flare.DataAccess;
using Flare.Domain.Entities;
using Flare.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Flare.Application.Services.Impl;

public class PostService : IPostService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileHandlingService _fileService;
    private readonly IHttpContextAccessor _accessor;
    private readonly IMapper _mapper;

    public PostService(IUnitOfWork unitOfWork, IFileHandlingService fileService, IHttpContextAccessor accessor, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _accessor = accessor;
        _mapper = mapper;
    }

    public Task<Post> GetAsync(Guid id)
    {
        var post = _unitOfWork.Posts.GetWithIncludeAsync(x => x.Id == id,  x => x.Comments!);
        if (post == null) throw new Exception("Post not found");

        return post;
    }

    public async Task<List<Post>> GetAllAsync(ContentType? type = null, string? category = null)
    {
        if (type == null && category == null) return await _unitOfWork.Posts.GetAllAsync();

        var posts = (category != null && type != null)
            ? await _unitOfWork.Posts.GetAllAsync(x => x.Type == type && x.Category == category)
            : await _unitOfWork.Posts.GetAllAsync(x => x.Type == type || x.Category == category);

        return posts;
    }

    public async Task<CreatePostResponseModel> CreatePostAsync(CreatePostModel createPostModel)
    {
        string? createdBy = _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);

        if (createdBy == null) throw new Exception("Author of a post is undefined");

        Guid id = Guid.NewGuid();
        string contentExtension = Path.GetExtension(createPostModel.Content.FileName);
        string contentPath = Path.Combine("files", createdBy, id.ToString() + contentExtension);

        var post = new Post
        {
            Id = id,
            Description = createPostModel.Description,
            ContentPath = contentPath,
            Orientation = createPostModel.Orientation,
            Type = createPostModel.Type,
            Category = createPostModel.Category,
            Tags = createPostModel.Tags,
            Comments = new Collection<Comment>(),
            Views = default,
            Downloads = default,
            CreatedBy = createdBy,
            CreatedOn = DateTime.UtcNow
        };

        await _unitOfWork.Posts.AddAsync(post);
        await _fileService.UploadFileAsync(createPostModel.Content, post.ContentPath);

        return new CreatePostResponseModel { Id = post.Id };
    }

    public async Task<UpdatePostResponseModel> UpdatePostAsync(UpdatePostModel updatePostModel)
    {
        var post = await _unitOfWork.Posts.GetAsync(x => x.Id == updatePostModel.Id);
        var accountName = _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);

        if (post == null) throw new Exception("Post not found");
        if (accountName != post.CreatedBy) throw new Exception("Incorrect author of the post");

        var updatedPost = _mapper.Map(updatePostModel, post);
        updatedPost.UpdatedOn = DateTime.UtcNow;

        await _unitOfWork.Posts.UpdateAsync(updatedPost);

        return new UpdatePostResponseModel { Id = updatedPost.Id };
    }

    public async Task<DeletePostResponseModel> DeletePostAsync(DeletePostModel deletePostModel)
    {
        var post = await _unitOfWork.Posts.GetAsync(x => x.Id == deletePostModel.Id);
        var accountName = _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);

        if (post == null) throw new Exception("Post not found");
        if (accountName != post.CreatedBy) throw new Exception("Incorrect author of the post");

        await _unitOfWork.Posts.RemoveAsync(post);
        _fileService.DeleteFile(post.ContentPath);
        return new DeletePostResponseModel { Id = post.Id };
    }
}