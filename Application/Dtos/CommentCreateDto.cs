using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CommentCreateDto
    {
        public int BlogId { get; set; }

        public required string Content { get; set; }

        public string AuthorName { get; set; } 
    }

}
