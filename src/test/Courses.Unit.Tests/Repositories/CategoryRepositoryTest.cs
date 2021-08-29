using Courses.Data.Context;
using Courses.Data.Repositories;
using Courses.Data.UoW;
using Courses.Domain.Models.Category;
using Courses.Unit.Tests.Mock;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Data;
using Xunit;

namespace Courses.Unit.Tests.Repositories
{
    public class CategoryRepositoryTest
    {
        private readonly DbContextOptions<EntityContext> _entityOptions;
        private readonly Mock<IDbConnection> _dbConnectionMock;

        public CategoryRepositoryTest()
        {
            _entityOptions = new DbContextOptionsBuilder<EntityContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _dbConnectionMock = new Mock<IDbConnection>();
        }

        [Fact]
        public void Crud_EntityTest()
        {
            var category = CategoryMock.CategoryModelFaker.Generate();
            var entityContext = new EntityContext(_entityOptions);
            var unitOfWork = new UnitOfWork(entityContext);
            var dapperContext = new DapperContext(_dbConnectionMock.Object);
            var categoryRepository = new CategoryRepository(entityContext, dapperContext);
            var courseRepository = new CourseRepository(entityContext, dapperContext);

            categoryRepository.Add(category);
            var IsSaveCategory = unitOfWork.Commit();

            categoryRepository.Update(category);
            var IsUpdateCategory = unitOfWork.Commit();

            categoryRepository.Remove(category);
            var IsRemoveCategory = unitOfWork.Commit();

            Assert.Equal(1, IsSaveCategory);
            Assert.Equal(1, IsUpdateCategory);
            Assert.Equal(1, IsRemoveCategory);
        }
    }
}
