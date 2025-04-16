using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace DataAccess.Repositories.Abstract
{
    public interface ICommentRepository
    {
        Task AddAsync(Comment comment);
        Task<IEnumerable<Comment>> GetByBlogIdAsync(int blogId);
    }
}
