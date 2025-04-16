using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Abstract
{
    public interface ICommentService
    {
        Task AddAsync(CommentCreateDto dto);
        Task<List<CommentDetailDto>> GetByBlogIdAsync(int blogId);


    }
}
