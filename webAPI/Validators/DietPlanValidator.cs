using FluentValidation;
using webAPI.Models;

public class DietPlanValidator : AbstractValidator<DietPlan>
{
    public DietPlanValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(x => x.StartDate).LessThan(x => x.EndDate).WithMessage("Start date must be before end date.");
    }
}
