using Courses.Domain.Models.Category;
using Courses.Domain.Models.Course;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Courses.Domain.Interfaces.Repositories
{
    public interface ICourseRepository : IEntityBaseRepository<Course>
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<bool> VerifyIfExistsCourseByPeriodDate(DateTime startDate, DateTime endDate);
        Task<Course> GetByIdAsync(int id);
    }
}
