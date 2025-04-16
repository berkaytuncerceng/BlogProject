using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.Dtos;

public class BlogCreateDto
{
    public required string Title { get; set; }

    public required string Content { get; set; }

    public required int CategoryId { get; set; }

    public IFormFile? ImageFile { get; set; }
}
