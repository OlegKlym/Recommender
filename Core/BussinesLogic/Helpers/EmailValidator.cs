using FluentValidation;
using Recommender.BussinesLogic.Resources.Localizations;

namespace Recommender.BussinesLogic.Helpers
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
