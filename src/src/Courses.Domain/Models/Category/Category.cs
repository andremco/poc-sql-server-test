using System.Collections.Generic;

namespace Courses.Domain.Models.Category
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Course.Course> Courses { get; private set; }

        public Category()
        {
            Courses = new HashSet<Course.Course>();
        }

    }
}
