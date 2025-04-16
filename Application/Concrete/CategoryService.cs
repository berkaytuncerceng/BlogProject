using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstract;
using Application.Dtos;
using AutoMapper;
using DataAccess.Repositories.Abstract;
using Domain.Entities;

namespace Application.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<CategoryListDto>> GetAllAsync()
        {
            var categories = await _repo.GetAllAsync();
            return _mapper.Map<List<CategoryListDto>>(categories);
        }

        public async Task<CategoryDetailsDto?> GetByIdAsync(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            return _mapper.Map<CategoryDetailsDto?>(category);
        }

        public async Task CreateAsync(CategoryCreateDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            category.CreatedAt = DateTime.Now;
            await _repo.AddAsync(category);
        }

        public async Task<CategoryEditDto?> GetEditDtoByIdAsync(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            return category == null ? null : _mapper.Map<CategoryEditDto>(category);
        }

        public async Task UpdateAsync(CategoryEditDto dto)
        {
            var category = await _repo.GetByIdAsync(dto.Id);
            if (category == null) throw new Exception("Kategori bulunamadı.");

            _mapper.Map(dto, category);
            category.UpdatedAt = DateTime.Now;
            await _repo.UpdateAsync(category);
        }

        public async Task<CategoryDeleteDto?> GetDeleteDtoByIdAsync(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            return category == null ? null : _mapper.Map<CategoryDeleteDto>(category);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
