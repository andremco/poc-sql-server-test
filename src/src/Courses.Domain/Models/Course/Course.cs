using System;

namespace Courses.Domain.Models.Course
{
    public class Course
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? StudentsPerClass { get; set; }
        public Category.Category Category { get; set; }
    }
}
