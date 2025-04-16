namespace Application.Dtos
{
    public class BlogListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; }
        public string? ImagePath { get; set; }
        public string UserName { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CommentCount { get; set; }

    }
}
