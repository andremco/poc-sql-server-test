using Courses.Data.Context;
using Courses.Domain.Interfaces.Repositories;
using Courses.Domain.Models.Category;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Data.Repositories
{
    public class CategoryRepository : EntityBaseRepository<Category>, ICategoryRepository
    {
        private readonly DapperContext _dapperContext;

        public CategoryRepository(EntityContext context, DapperContext dapperContext) : base(context)
        {
            _dapperContext = dapperContext;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var query = @"SELECT c.Id, c.Name
                            FROM dbo.Category c";

            return await _dapperContext.DapperConnection.QueryAsync<Category>(query);
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var query = @"SELECT c.Id, c.Name
                          FROM dbo.Category c
                          WHERE c.Id = @Id";

            return await _dapperContext.DapperConnection.QueryFirstOrDefaultAsync<Category>(query, new { Id = id });
        }
    }
}
