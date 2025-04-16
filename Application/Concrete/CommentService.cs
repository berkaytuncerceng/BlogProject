
using Application.Abstract;
using Application.Dtos;
using AutoMapper;
using DataAccess.Repositories.Abstract;
using Domain.Entities;

namespace Application.Concrete
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepo, IMapper mapper)
        {
            _commentRepo = commentRepo;
            _mapper = mapper;
        }

        public async Task AddAsync(CommentCreateDto dto)
        {
            var comment = _mapper.Map<Comment>(dto);
            comment.CreatedAt = DateTime.Now;
            await _commentRepo.AddAsync(comment);
        }

        public async Task<List<CommentDetailDto>> GetByBlogIdAsync(int blogId)
        {
            var comments = await _commentRepo.GetByBlogIdAsync(blogId);
            return _mapper.Map<List<CommentDetailDto>>(comments);
        }
    }
}