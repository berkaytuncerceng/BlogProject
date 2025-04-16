using Application.Dtos;
using AutoMapper;
using Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BlogCreateDto, Blog>();
        CreateMap<BlogEditDto, Blog>().ReverseMap();
        CreateMap<Blog, BlogDetailsDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));
       
        CreateMap<Blog, BlogListDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.Comments.Count));
        
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<CategoryEditDto, Category>().ReverseMap();
        CreateMap<Category, CategoryDetailsDto>();
        CreateMap<Category, CategoryListDto>();
        CreateMap<Category, CategoryDeleteDto>();

        CreateMap<CommentCreateDto, Comment>();
        CreateMap<Comment, CommentDetailDto>();

    }
}
