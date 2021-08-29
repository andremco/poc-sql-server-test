using Courses.Data.Context;
using Courses.Domain.Interfaces.Repositories;
using Courses.Domain.Models.Category;
using Courses.Domain.Models.Course;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Data.Repositories
{
    public class CourseRepository : EntityBaseRepository<Course>, ICourseRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly EntityContext _context;

        public CourseRepository(EntityContext context, DapperContext dapperContext) : base(context)
        {
            _dapperContext = dapperContext;
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            var query = @"SELECT co.Id, co.Name, co.Description, co.StartDate, 
                                 co.EndDate, co.StudentsPerClass, ca.Id, ca.Name
                            FROM dbo.Course co
                            INNER JOIN dbo.Category ca
                            ON co.CategoryId = ca.Id";

            return await _dapperContext.DapperConnection.QueryAsync<Course, Category, Course>(query, (course, category) =>
            {
                course.Category = category;
                course.CategoryId = category.Id;
                return course;
            }, 
            splitOn: "Id");
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            return await Task.FromResult(_context.Courses.FirstOrDefault(c => c.Id == id));
        }

        public async Task<bool> VerifyIfExistsCourseByPeriodDate(System.DateTime startDate, System.DateTime endDate)
        {
            var query = @"SELECT CASE WHEN EXISTS (
                            SELECT co.Id
                                FROM dbo.Course co
                                WHERE co.StartDate BETWEEN @StartDate AND @EndDate
                        )
                        THEN CAST(1 AS BIT)
                        ELSE CAST(0 AS BIT) END";

            return await _dapperContext.DapperConnection.QueryFirstOrDefaultAsync<bool>(query, new { StartDate = startDate, EndDate = endDate });
        }
    }
}
