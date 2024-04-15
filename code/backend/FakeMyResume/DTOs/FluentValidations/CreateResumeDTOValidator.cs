using FluentValidation;

namespace FakeMyResume.DTOs.FluentValidations;

public class CreateResumeDTOValidator : AbstractValidator<CreateResumeDTO>
{
    public CreateResumeDTOValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().MaximumLength(150).WithMessage("FullName is required with a maximum lenght of 150 characters");
        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000).WithMessage("FullName is required with a maximum lenght of 1000 characters");
        RuleFor(x => x.Technologies).NotEmpty();
        RuleFor(x => x.Methodologies).NotEmpty();
        RuleFor(x => x.WorkExperience).NotEmpty();
    }
}
