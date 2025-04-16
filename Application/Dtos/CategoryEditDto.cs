using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class CategoryEditDto
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Kategori adı gereklidir.")]
        [StringLength(50, ErrorMessage = "Kategori adı en fazla 50 karakter olabilir.")]
        public string Name { get; set; }

        public string? Description { get; set; }

    }
}
