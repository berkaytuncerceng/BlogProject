using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Abstract
{
    public interface IBlogService
    {
        Task<List<BlogListDto>> GetFilteredAsync(string? title, string? userName, int? categoryId, DateTime? startDate, DateTime? endDate);
        Task<BlogDetailsDto?> GetDetailsByIdAsync(int id);
        Task<List<BlogListDto>> GetUserBlogsAsync(string userId);
        Task CreateAsync(BlogCreateDto dto, string userId);
        Task<BlogEditDto?> GetEditDtoByIdAsync(int id, string userId, bool isAdmin);
        Task UpdateAsync(BlogEditDto dto, string userId, bool isAdmin);
        Task DeleteAsync(int id, string userId, bool isAdmin);
    }


}
