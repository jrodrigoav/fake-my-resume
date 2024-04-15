using FluentValidation;

namespace FakeMyResume.DTOs.FluentValidations;

public class ProjectDTOValidator : AbstractValidator<ProjectDTO>
{
    public ProjectDTOValidator()
    {
        RuleFor(x => x.TechnologiesUsed).NotEmpty().NotNull().WithMessage("Tecnologies Used is required at least must contain one element");
        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000).WithMessage("Project description is required with a maximum lenght of 1000 characters");
        RuleFor(x => x.Name).NotEmpty().MaximumLength(150).WithMessage("Project name is required with a maximum lenght of 150 characters");
    }
}
