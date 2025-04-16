namespace Application.Dtos
{
    //public class CommentDetailDto
    //{
    //    public string AuthorName { get; set; }
    //    public string Content { get; set; }
    //    public DateTime CreatedAt { get; set; }
    //}
    public class CommentDetailDto
    {
        public int BlogId { get; set; }

        public string AuthorName { get; set; } 

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
    }

}
