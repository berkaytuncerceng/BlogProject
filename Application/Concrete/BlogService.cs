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
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepo;
        private readonly IMapper _mapper;

        public BlogService(IBlogRepository blogRepo, IMapper mapper)
        {
            _blogRepo = blogRepo;
            _mapper = mapper;
        }

        public async Task<List<BlogListDto>> GetFilteredAsync(string? title, string? userName, int? categoryId, DateTime? startDate, DateTime? endDate)
        {
            var blogs = await _blogRepo.GetAllAsync();
            var query = blogs.AsQueryable();

            if (!string.IsNullOrEmpty(title))
                query = query.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(userName))
                query = query.Where(b => b.User.UserName.Contains(userName, StringComparison.OrdinalIgnoreCase));

            if (categoryId.HasValue)
                query = query.Where(b => b.CategoryId == categoryId.Value);

            if (startDate.HasValue)
                query = query.Where(b => b.CreatedAt >= startDate.Value);

            if (endDate.HasValue)
            {
                var endOfDay = endDate.Value.AddDays(1).AddTicks(-1);
                query = query.Where(b => b.CreatedAt <= endOfDay);
            }

            var ordered = query.OrderByDescending(b => b.CreatedAt).ToList();
            return _mapper.Map<List<BlogListDto>>(ordered);
        }

        public async Task<BlogDetailsDto?> GetDetailsByIdAsync(int id)
        {
            var blog = await _blogRepo.GetByIdAsync(id);
            return _mapper.Map<BlogDetailsDto>(blog);
        }

        public async Task<List<BlogListDto>> GetUserBlogsAsync(string userId)
        {
            var allBlogs = await _blogRepo.GetAllAsync();
            var userBlogs = allBlogs.Where(b => b.UserId == userId).ToList();
            return _mapper.Map<List<BlogListDto>>(userBlogs);
        }

        public async Task CreateAsync(BlogCreateDto dto, string userId)
        {
            var blog = _mapper.Map<Blog>(dto);
            blog.UserId = userId;
            blog.CreatedAt = DateTime.Now;

            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "blogs");
                Directory.CreateDirectory(uploadsFolder);
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await dto.ImageFile.CopyToAsync(stream);
                blog.ImagePath = "/images/blogs/" + fileName;
            }

            await _blogRepo.AddAsync(blog);
        }

        public async Task<BlogEditDto?> GetEditDtoByIdAsync(int id, string userId, bool isAdmin)
        {
            var blog = await _blogRepo.GetByIdAsync(id);
            if (blog == null || (blog.UserId != userId && !isAdmin))
                return null;

            return _mapper.Map<BlogEditDto>(blog);
        }

        public async Task UpdateAsync(BlogEditDto dto, string userId, bool isAdmin)
        {
            var blog = await _blogRepo.GetByIdAsync(dto.Id);
            if (blog == null || (blog.UserId != userId && !isAdmin))
                throw new UnauthorizedAccessException();

            blog.Title = dto.Title;
            blog.Content = dto.Content;
            blog.CategoryId = dto.CategoryId;

            if (dto.ImageFile is { Length: > 0 })
            {
                if (!string.IsNullOrEmpty(blog.ImagePath))
                {
                    string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", blog.ImagePath.TrimStart('/'));
                    if (File.Exists(oldPath)) File.Delete(oldPath);
                }

                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "blogs");
                Directory.CreateDirectory(uploadsFolder);
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await dto.ImageFile.CopyToAsync(stream);

                blog.ImagePath = "/images/blogs/" + fileName;
            }

            await _blogRepo.UpdateAsync(blog);
        }

        public async Task DeleteAsync(int id, string userId, bool isAdmin)
        {
            var blog = await _blogRepo.GetByIdAsync(id);
            if (blog == null || (blog.UserId != userId && !isAdmin))
                throw new UnauthorizedAccessException();

            if (!string.IsNullOrEmpty(blog.ImagePath))
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", blog.ImagePath.TrimStart('/'));
                if (File.Exists(fullPath)) File.Delete(fullPath);
            }

            await _blogRepo.DeleteAsync(id);
        }
    }
}
