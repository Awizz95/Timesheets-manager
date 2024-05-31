using FluentValidation;
using TimesheetsProj.Models.Dto.Requests;

namespace TimesheetsProj.Infrastructure.Validation
{
    public class SheetRequestValidator : AbstractValidator<SheetRequest>
    {
        public SheetRequestValidator()
        {
            RuleFor(x => x.Amount)
                .InclusiveBetween(0, 1_000_000)
                .WithMessage("Значение должно быть между 0 и 1000000.");
        }
    }
}
