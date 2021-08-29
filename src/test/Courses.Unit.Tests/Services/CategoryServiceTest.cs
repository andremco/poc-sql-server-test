using AutoMapper;
using Courses.Application.Services;
using Courses.Application.ViewModels;
using Courses.Domain.Interfaces.Repositories;
using Courses.Domain.Interfaces.UoW;
using Courses.Domain.Models.Category;
using Courses.Unit.Tests.Mock;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Courses.Unit.Tests.Services
{
    public class CategoryServiceTest
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;

        public CategoryServiceTest()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task GetAll_ReturnCategoriesViewModelTestAsync()
        {
            _categoryRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(CategoryMock.CategoryModelFaker.Generate(5));

            _mapperMock.Setup(x => x.Map<IEnumerable<CategoryViewModel>>(It.IsAny<IEnumerable<Category>>()))
                .Returns(CategoryMock.CategoryViewModelFaker.Generate(5));

            var categoryService = new CategoryService(_categoryRepositoryMock.Object,
                _unitOfWorkMock.Object, _mapperMock.Object);

            var result = await categoryService.GetAllAsync();

            var categoryResult = Assert.IsAssignableFrom<IEnumerable<CategoryViewModel>>(result);

            Assert.NotNull(categoryResult);
            Assert.NotEmpty(categoryResult);
        }

        [Fact]
        public async Task GetById_ReturnCategoriesViewModelTestAsync()
        {
            int id = 1;

            _categoryRepositoryMock.Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync(CategoryMock.CategoryModelFaker.Generate());

            _mapperMock.Setup(x => x.Map<CategoryViewModel>(It.IsAny<Category>()))
                .Returns(CategoryMock.CategoryViewModelFaker.Generate());

            var categoryService = new CategoryService(_categoryRepositoryMock.Object,
                _unitOfWorkMock.Object, _mapperMock.Object);

            var result = await categoryService.GetByIdAsync(id);

            var categoryResult = Assert.IsAssignableFrom<CategoryViewModel>(result);

            Assert.NotNull(categoryResult);
        }
    }
}
