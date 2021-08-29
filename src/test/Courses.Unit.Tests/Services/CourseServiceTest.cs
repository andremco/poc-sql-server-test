using AutoMapper;
using Courses.Application.Services;
using Courses.Application.ViewModels;
using Courses.Domain.Interfaces.Notifications;
using Courses.Domain.Interfaces.Repositories;
using Courses.Domain.Interfaces.UoW;
using Courses.Domain.Models.Course;
using Courses.Unit.Tests.Mock;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Courses.Unit.Tests.Services
{
    public class CourseServiceTest
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IDomainNotification> _domainNotificationMock;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<ICourseRepository> _courseRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CourseServiceTest()
        {
            _mapperMock = new Mock<IMapper>();
            _domainNotificationMock = new Mock<IDomainNotification>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _courseRepositoryMock = new Mock<ICourseRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task AddAsync_ShouldNotifyValidationsTest()
        {
            var courseViewModelFaker = CourseMock.CourseViewModelFaker.Generate();
            var courseModelFaker = CourseMock.CourseModelFaker.Generate();
            courseModelFaker.CategoryId = 0;

            var domainNotification = new DomainNotification();
            _mapperMock.Setup(c => c.Map<Course>(It.IsAny<CourseViewModel>())).Returns(courseModelFaker);

            var service = new CourseService(_mapperMock.Object, domainNotification, _unitOfWorkMock.Object, _categoryRepositoryMock.Object, _courseRepositoryMock.Object);
            var result = await service.AddAsync(courseViewModelFaker);

            Assert.True(domainNotification.HasNotifications);
        }

        [Fact]
        public async Task UpdateAsync_ShouldNotifyValidationStartDateIsLessThanNowTest()
        {
            var courseViewModelFaker = CourseMock.CourseViewModelFaker.Generate();
            var courseModelFaker = CourseMock.CourseModelFaker.Generate();
            var categoryModelFaker = CategoryMock.CategoryModelFaker.Generate();
            courseModelFaker.StartDate = new System.DateTime(2021, 1, 1);

            var domainNotification = new DomainNotification();
            _mapperMock.Setup(c => c.Map<Course>(It.IsAny<CourseViewModel>())).Returns(courseModelFaker);
            _categoryRepositoryMock.Setup(c => c.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(categoryModelFaker);

            var service = new CourseService(_mapperMock.Object, domainNotification, _unitOfWorkMock.Object, _categoryRepositoryMock.Object, _courseRepositoryMock.Object);
            var result = await service.UpdateAsync(courseViewModelFaker);

            Assert.Contains(domainNotification.Notifications.ToList(), c => c.Key == "StartDateIsLessThanNow");
        }
    }
}
