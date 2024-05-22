using FluentValidation;
using TimesheetsProj.Models.Dto.Requests;

namespace TimesheetsProj.Infrastructure.Validation
{
    public class SheetRequestValidator : AbstractValidator<SheetRequest>
    {
        public SheetRequestValidator()
        {
            RuleFor(x => x.Amount)
                .InclusiveBetween(1, 8)
                .WithMessage("Incorrect value");
        }
    }
}
