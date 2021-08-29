using Bogus;
using Courses.Application.ViewModels;
using Courses.Domain.Models.Category;

namespace Courses.Unit.Tests.Mock
{
    public class CategoryMock
    {
        public static Faker<Category> CategoryModelFaker =>
            new Faker<Category>()
            .CustomInstantiator(x => new Category
            {
                Id = x.Random.Int(),
                Name = x.Finance.AccountName()
            });

        public static Faker<CategoryViewModel> CategoryViewModelFaker =>
            new Faker<CategoryViewModel>()
            .CustomInstantiator(x => new CategoryViewModel
            {
                Id = x.Random.Int(),
                Name = x.Finance.AccountName()
            });
    }
}
