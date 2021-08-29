using Courses.Data.Context;
using Courses.Data.Repositories;
using Courses.Data.UoW;
using Courses.Unit.Tests.Mock;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using Xunit;

namespace Courses.Unit.Tests.Repositories
{
    public class CourseRepositoryTest
    {
        private readonly DbContextOptions<EntityContext> _entityOptions;
        private readonly Mock<IDbConnection> _dbConnectionMock;

        public CourseRepositoryTest()
        {
            _entityOptions = new DbContextOptionsBuilder<EntityContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _dbConnectionMock = new Mock<IDbConnection>();
        }

        [Fact]
        public void Crud_EntityTest()
        {
            var course = CourseMock.CourseModelFaker.Generate();

            var entityContext = new EntityContext(_entityOptions);
            var unitOfWork = new UnitOfWork(entityContext);
            var dapperContext = new DapperContext(_dbConnectionMock.Object);
            var courseRepository = new CourseRepository(entityContext, dapperContext);

            courseRepository.Add(course);
            var IsSaveCourse = unitOfWork.Commit();

            courseRepository.Update(course);
            var IsUpdateCourse = unitOfWork.Commit();

            courseRepository.Remove(course);
            var IsRemoveCourse = unitOfWork.Commit();

            Assert.Equal(1, IsSaveCourse);
            Assert.Equal(1, IsUpdateCourse);
            Assert.Equal(1, IsRemoveCourse);
        }
    }
}
