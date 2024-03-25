
using FluentValidation;

namespace MakeMyResume.DTOs.FluentValidations
{
    public class ResumeDTOValidator : AbstractValidator<ResumeDTO>
    {
        public ResumeDTOValidator()
        {
            RuleFor(x => x.Id).InclusiveBetween(1, int.MaxValue).WithMessage("Id is required");
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(150).WithMessage("FullName is required with a maximum lenght of 150 characters");
            RuleFor(x => x.CurrentRole).NotEmpty().MaximumLength(150).WithMessage("CurrentRole is required with a maximum lenght of 150 characters");
            RuleFor(x => x.Description).NotEmpty().MaximumLength(1000).WithMessage("FullName is required with a maximum lenght of 1000 characters");
            RuleFor(x => x.Technologies).NotEmpty();
            RuleFor(x => x.Methodologies).NotEmpty();
            RuleFor(x => x.WorkExperience).NotEmpty();
        }
    }
}
