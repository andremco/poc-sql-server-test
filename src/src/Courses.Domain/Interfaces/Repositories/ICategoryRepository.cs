using Courses.Domain.Models.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Courses.Domain.Interfaces.Repositories
{
    public interface ICategoryRepository : IEntityBaseRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
    }
}
