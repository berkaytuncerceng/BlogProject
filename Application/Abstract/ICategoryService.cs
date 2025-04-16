using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Abstract
{
    public interface ICategoryService
    {
        Task<List<CategoryListDto>> GetAllAsync();
        Task<CategoryDetailsDto?> GetByIdAsync(int id);
        Task CreateAsync(CategoryCreateDto dto);
        Task<CategoryEditDto?> GetEditDtoByIdAsync(int id);
        Task UpdateAsync(CategoryEditDto dto);
        Task<CategoryDeleteDto?> GetDeleteDtoByIdAsync(int id);
        Task DeleteAsync(int id);
    }

}
