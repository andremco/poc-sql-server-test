using AutoMapper;
using Courses.Application.ViewModels;
using Courses.Domain.Models.Category;
using Courses.Domain.Models.Course;

namespace Courses.Application.AutoMapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryViewModel, Category>().ForMember(s => s.Courses, d => d.Ignore()).ReverseMap();

            CreateMap<CourseViewModel, Course>()
                .ForMember(s => s.CategoryId, d => d.MapFrom(a => a.CategoryId))
                .ForPath(s => s.Category.Name, opt => opt.MapFrom(a => a.CategoryName));

            CreateMap<Course, CourseViewModel>()
                .ForMember(s => s.CategoryId, d => d.MapFrom(a => a.CategoryId))
                .ForPath(s => s.CategoryName, d => d.MapFrom(a => a.Category.Name));
        }
    }
}
