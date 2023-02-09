using Flare.Application.Models.Post;
using Flare.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Flare.Domain.Enums;

namespace Flare.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PostsController : ControllerBase
{
    private readonly IPostService _service;

    public PostsController(IPostService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPostAsync(Guid id)
    {
        return Ok(await _service.GetAsync(id));
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllPostsAsync([FromQuery] PostParameters postParameters)
    {
        return Ok(await _service.GetAllAsync(postParameters));
    }

    [HttpPost]
    public async Task<IActionResult> CreatePostAsync([FromForm] CreatePostModel createPostModel)
    {
        return Ok(await _service.CreatePostAsync(createPostModel));
    }

    [HttpPatch]
    public async Task<IActionResult> UpdatePostAsync([FromForm] UpdatePostModel updatePostModel)
    {
        return Ok(await _service.UpdatePostAsync(updatePostModel));
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePostAsync([FromBody] DeletePostModel deletePostModel)
    {
        return Ok(await _service.DeletePostAsync(deletePostModel));
    }
}