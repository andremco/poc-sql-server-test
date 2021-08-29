using Courses.Domain.Models.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courses.Data.Mappings
{
    public class CourseMap : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Course", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CategoryId)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnType("VARCHAR(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnType("VARCHAR(250)")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(x => x.StartDate)
                .HasColumnType("DATETIME2")
                .IsRequired();

            builder.Property(x => x.EndDate)
                .HasColumnType("DATETIME2")
                .IsRequired();

            builder.Property(x => x.StudentsPerClass)
                .HasColumnType("INT");

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Courses)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
