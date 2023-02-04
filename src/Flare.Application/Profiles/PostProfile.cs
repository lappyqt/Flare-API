using AutoMapper;
using Flare.Application.Models.Post;
using Flare.Domain.Entities;

namespace Flare.Application.Profiles;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<UpdatePostModel, Post>();
    }
}