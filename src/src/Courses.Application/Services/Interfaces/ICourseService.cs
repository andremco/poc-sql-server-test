using Courses.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Courses.Application.Services.Interfaces
{
    public interface ICourseService
    {
        Task<CourseViewModel> GetByIdAsync(int id);
        Task<IEnumerable<CourseViewModel>> GetAllAsync();
        Task<CourseViewModel> AddAsync(CourseViewModel courseViewModel);
        Task<CourseViewModel> UpdateAsync(CourseViewModel courseViewModel);
        Task DeleteAsync(int id);
    }
}
