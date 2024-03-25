using FluentValidation;

namespace MakeMyResume.DTOs.FluentValidations
{
    public class WorkExperienceDTOValidator : AbstractValidator<WorkExperienceDTO>
    {
        public WorkExperienceDTOValidator()
        {
            var maxYear = DateTime.UtcNow.Year;
            var minYear = maxYear- 35;

            RuleFor(x => x.CompanyName).NotEmpty().MaximumLength(150).WithMessage("Company name is required with a maximum lenght of 150 characters");
            RuleFor(x => x.Role).NotEmpty().MaximumLength(150).WithMessage("Role is required with a maximum lenght of 150 characters");
            RuleFor(x => x.FromYear).LessThanOrEqualTo(maxYear).GreaterThanOrEqualTo(minYear).WithMessage($"FromYear is required must be <= {maxYear} and >= {minYear}");
            RuleFor(x => x.ToYear).LessThanOrEqualTo(maxYear).GreaterThanOrEqualTo(minYear).WithMessage($"FromYear is required must be <= {maxYear} and >= {minYear}");
            RuleFor(x => x.FromMonth).NotEmpty().InclusiveBetween(1, 12).Must((x, fromMonth) => BeAValidMonth(x.FromYear, fromMonth))
                .WithMessage("Month is required must be less than or equal to the current year");
            RuleFor(x => x.ToMonth).NotEmpty().InclusiveBetween(1,12).Must((x, toMonth) => BeAValidMonth(x.ToYear, toMonth))
                .WithMessage("Current month is required must be less than or equal to the current year");
            RuleFor(x => x.Description).NotEmpty().MaximumLength(1000).WithMessage("To is required with a maximum lenght of 1000 characters");

        }

        private bool BeAValidMonth(int? year, int? month)
        {
            var yearNow = DateTime.UtcNow.Year;

            if (year < yearNow) return true;

            var monthNow = DateTime.UtcNow.Month;
            return month <= monthNow;
        }
    }
}
