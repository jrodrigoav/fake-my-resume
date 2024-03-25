using FluentValidation;

namespace MakeMyResume.DTOs.FluentValidations
{
    public class EducationDTOValidator : AbstractValidator<EducationDTO>
    {
        public EducationDTOValidator()
        {
            //RuleFor(x => x.UniversityName).NotEmpty().MaximumLength(150).WithMessage("University Name is required with a maximum lenght of 150 characters");
        }
    }
}
