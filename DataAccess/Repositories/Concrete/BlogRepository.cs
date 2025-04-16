using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Repositories.Abstract;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Concrete
{
	public class BlogRepository : IBlogRepository
	{
		private readonly ApplicationDbContext _context;

		public BlogRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Blog>> GetAllAsync()
		{
			return await _context.Blogs
				.Include(b => b.User)
				.Include(b => b.Category)
				.ToListAsync();
		}

        public async Task<Blog?> GetByIdAsync(int id)
        {
            return await _context.Blogs
                .Include(b => b.User)
                .Include(b => b.Category)
                .Include(b => b.Comments) 
                .FirstOrDefaultAsync(b => b.Id == id);
        }


        public async Task AddAsync(Blog blog)
		{
			await _context.Blogs.AddAsync(blog);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Blog blog)
		{
			blog.UpdatedAt = DateTime.Now;
			_context.Blogs.Update(blog);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var blog = await GetByIdAsync(id);
			if (blog != null)
			{
				_context.Blogs.Remove(blog);
				await _context.SaveChangesAsync();
			}
		}
	}
}
