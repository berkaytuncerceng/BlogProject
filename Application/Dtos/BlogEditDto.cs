using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.Dtos;

public class BlogEditDto
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    [Required(ErrorMessage = "Başlık alanı gereklidir.")]
    [StringLength(100)]
    public string Title { get; set; }

    [Required(ErrorMessage = "İçerik alanı gereklidir.")]
    public string Content { get; set; }

    [Required(ErrorMessage = "Kategori seçimi zorunludur.")]
    public int CategoryId { get; set; }

    public string? ImagePath { get; set; }

    public IFormFile? ImageFile { get; set; }
}
