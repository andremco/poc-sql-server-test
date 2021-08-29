using Courses.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Courses.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllAsync();
        Task<CategoryViewModel> GetByIdAsync(int id);
    }
}
