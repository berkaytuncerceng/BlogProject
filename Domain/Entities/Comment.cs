using System;
//using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Domain.Entities
{
    public class Comment : IEntity
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string AuthorName { get; set; }

        public int BlogId { get; set; }

        public Blog BlogPost { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
