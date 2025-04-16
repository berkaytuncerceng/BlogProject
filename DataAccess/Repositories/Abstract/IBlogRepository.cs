using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace DataAccess.Repositories.Abstract
{
	public interface IBlogRepository
	{
		Task<IEnumerable<Blog>> GetAllAsync();
		Task<Blog?> GetByIdAsync(int id);
		Task AddAsync(Blog blog);
		Task UpdateAsync(Blog blog);
		Task DeleteAsync(int id);
	}
}
