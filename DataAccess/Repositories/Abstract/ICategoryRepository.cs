using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace DataAccess.Repositories.Abstract
{

	public interface ICategoryRepository
	{
		Task<IEnumerable<Category>> GetAllAsync();
		Task<Category?> GetByIdAsync(int id);
		Task AddAsync(Category category);
		Task UpdateAsync(Category category);
		Task DeleteAsync(int id);
	}
}
