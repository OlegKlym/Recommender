using FluentValidation;
using Recommender.Resources.Localizations;

namespace Recommender.Helpers
{
    public class EmailValidator : AbstractValidator<string>
    {
        public EmailValidator()
        {
            RuleFor(email => email)
                .NotEmpty().WithMessage(AppResources.LoginEmailRequired)
                .EmailAddress().WithMessage(AppResources.LoginEmailInvalid);
        }
    }
}
