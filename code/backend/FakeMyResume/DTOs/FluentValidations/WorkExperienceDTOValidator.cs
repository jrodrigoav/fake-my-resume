using FluentValidation;

namespace FakeMyResume.DTOs.FluentValidations;

public class WorkExperienceDTOValidator : AbstractValidator<WorkExperienceDTO>
{
    public WorkExperienceDTOValidator()
    {
        var maxYear = DateTime.UtcNow.Year;
        var minYear = maxYear- 35;

        RuleFor(x => x.CompanyName).NotEmpty().MaximumLength(150).WithMessage("Company name is required with a maximum lenght of 150 characters");
        RuleFor(x => x.Role).NotEmpty().MaximumLength(150).WithMessage("Role is required with a maximum lenght of 150 characters");
        RuleFor(x => x.DateBegin.Year).LessThanOrEqualTo(maxYear).GreaterThanOrEqualTo(minYear)
            .WithMessage($"Begin year is required, must be <= {maxYear} and >= {minYear}");
        RuleFor(x => x.DateEnd.Year).LessThanOrEqualTo(maxYear).GreaterThanOrEqualTo(minYear)
            .WithMessage($"End year is required, must be <= {maxYear} and >= {minYear}");
        RuleFor(x => x.DateBegin.Month).NotEmpty().InclusiveBetween(1, 12)
            .Must((x, beginMonth) => BeAValidMonth(x.DateBegin.Year, beginMonth))
            .WithMessage("Month is required, must be less than or equal to the current year");
        RuleFor(x => x.DateEnd.Month).NotEmpty().InclusiveBetween(1, 12)
            .Must((x, endMonth) => BeAValidMonth(x.DateEnd.Year, endMonth))
            .WithMessage("Month is required, must be less than or equal to the current year");
        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000).WithMessage("To is required with a maximum lenght of 1000 characters");
        RuleFor(x => x.Technologies).NotEmpty();
    }

    private bool BeAValidMonth(int? year, int? month)
    {
        var yearNow = DateTime.UtcNow.Year;

        if (year < yearNow) return true;

        var monthNow = DateTime.UtcNow.Month;
        return month <= monthNow;
    }
}
