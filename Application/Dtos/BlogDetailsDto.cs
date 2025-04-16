using System.Collections.Generic;
using System;
using Domain.Entities;

namespace Application.Dtos;

public class BlogDetailsDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public string? ImagePath { get; set; }

    public string UserName { get; set; }

    public string UserId { get; set; }

    public string CategoryName { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<CommentDetailDto> Comments { get; set; } = new();
}
