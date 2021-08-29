using Bogus;
using Courses.Application.ViewModels;
using Courses.Domain.Models.Course;
using System;

namespace Courses.Unit.Tests.Mock
{
    public class CourseMock
    {
        public static Faker<Course> CourseModelFaker =>
            new Faker<Course>()
            .CustomInstantiator(x => new Course { 
                Id = x.Random.Int(),
                CategoryId = x.Random.Int(),
                Name = x.Finance.AccountName(),
                Description = x.Database.Collation(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(1),
                StudentsPerClass = x.Random.Int()
            });

        public static Faker<CourseViewModel> CourseViewModelFaker =>
            new Faker<CourseViewModel>()
            .CustomInstantiator(x => new CourseViewModel
            {
                Id = x.Random.Int(),
                CategoryId = x.Random.Int(),
                Name = x.Finance.AccountName(),
                Description = x.Database.Collation(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(1),
                StudentsPerClass = x.Random.Int()
            });
    }
}
