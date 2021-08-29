using Courses.Domain.Models.Course;
using FluentValidation;

namespace Courses.Domain.Validation
{
    public class UpdateCourseValidation : AbstractValidator<Course>
    {
        public UpdateCourseValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id nao pode ser vazia!");

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage("CategoryId não pode ser vazia!");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name não pode ser vazia!");

            RuleFor(x => x.Name)
                .MaximumLength(100)
                .WithMessage("Name possui mais de 100 caracteres!");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description não pode ser vazia!");

            RuleFor(x => x.Description)
                .MaximumLength(250)
                .WithMessage("Description possui mais de 250 caracteres!");

            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage("StartDate não pode ser vazia!");

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage("EndDate não pode ser vazia!");
        }
    }
}
