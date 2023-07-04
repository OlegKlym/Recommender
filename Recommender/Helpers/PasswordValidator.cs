using System;
using FluentValidation;
using Recommender.Resources.Localizations;

namespace Recommender.Helpers
{
    public class PasswordValidator : AbstractValidator<string>
    {
        public PasswordValidator()
        {
            RuleFor(password => password)
                .NotEmpty().WithMessage(AppResources.LoginPasswordRequired)
                .MinimumLength(6).WithMessage(AppResources.LoginPasswordInvalid);
        }
    }
}
