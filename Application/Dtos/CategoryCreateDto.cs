using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }

        public string? Description { get; set; }

    }
}
