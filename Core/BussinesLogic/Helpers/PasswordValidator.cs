using FluentValidation;
using Recommender.BussinesLogic.Resources.Localizations;

namespace Recommender.BussinesLogic.Helpers
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
