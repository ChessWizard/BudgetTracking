using BudgetTracking.Service.Services.User.Commands.RegisterUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Service.Validators.User
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email).NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Lütfen geçerli bir email giriniz!");
            RuleFor(x => x.Password).NotNull()
                .NotEmpty();
            RuleFor(x => x.RetryPassword).NotNull()
                .NotEmpty();
            // şifre ile şifre tekrar aynı olmalıdır!
            RuleFor(x => x.Password).Equal(x => x.RetryPassword)
                .WithMessage("Şifre ile şifre tekrar aynı olmalıdır!");
        }
    }
}
