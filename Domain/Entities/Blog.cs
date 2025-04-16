using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class Blog : IEntity
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Başlık alanı gereklidir.")]
    [StringLength(100, ErrorMessage = "Başlık en fazla 100 karakter olmalıdır.")]
    public required string Title { get; set; }

    [Required(ErrorMessage = "İçerik alanı gereklidir.")]
    public required string Content { get; set; }

    public string? UserId { get; set; }

    public IdentityUser User { get; set; }
    public int CategoryId { get; set; }

    public Category Category { get; set; }
    public string? ImagePath { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
