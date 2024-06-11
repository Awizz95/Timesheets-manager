using FluentValidation;
using TimesheetsProj.Models;
using TimesheetsProj.Models.Dto.Requests;

namespace TimesheetsProj.Infrastructure.Validation
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Неверный формат почтового адреса!");

            RuleFor(x => x.Password)
                .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{8,}$")
                .WithMessage("Пароль должен состоять из минимум 8 символов и содержать как минимум одну цифру, одну заглавную букву, одну прописную букву и один спецсимвол!");

            RuleFor(x => x.Role)
                .IsEnumName(typeof(UserRoles), false)
                .WithMessage("Такой роли не существует!");


        }
    
    }
}
