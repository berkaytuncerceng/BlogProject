using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Repositories.Abstract;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Concrete
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly ApplicationDbContext _context;

		public CategoryRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Category>> GetAllAsync()
		{
			return await _context.Categories.ToListAsync();
		}

		public async Task<Category?> GetByIdAsync(int id)
		{
			return await _context.Categories.FindAsync(id);
		}

		public async Task AddAsync(Category category)
		{
			await _context.Categories.AddAsync(category);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Category category)
		{
			_context.Categories.Update(category);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var category = await GetByIdAsync(id);
			if (category != null)
			{
				_context.Categories.Remove(category);
				await _context.SaveChangesAsync();
			}
		}
	}
}
