using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain;

namespace Domain.Entities;

public class Category : IEntity
{
	public int Id { get; set; }

	[Required(ErrorMessage = "Kategori adı gereklidir.")]
	[StringLength(50, ErrorMessage = "Kategori adı en fazla 50 karakter olabilir.")]
	public string Name { get; set; }
    public string? Description { get; set; }

    public ICollection<Blog> Blogs { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
