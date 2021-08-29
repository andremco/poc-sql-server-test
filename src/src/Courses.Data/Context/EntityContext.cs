using Courses.Data.Mappings;
using Courses.Domain.Models.Category;
using Courses.Domain.Models.Course;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Courses.Data.Context
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options)
             : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new CourseMap());
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
