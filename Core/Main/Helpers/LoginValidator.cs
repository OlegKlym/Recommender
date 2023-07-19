using FluentValidation;
using Recommender.PageModels;

using IValidator = Recommender.Core.Helpers.IValidator<Recommender.PageModels.LoginPageModel>;

namespace Recommender.Helpers
{
    public class LoginValidator : AbstractValidator<LoginPageModel>, IValidator
    {
        public LoginValidator()
        {
            RuleFor(vm => vm.Email).NotEmpty().EmailAddress();
            RuleFor(vm => vm.Password).NotEmpty();
        }

        public bool ValidateModel(LoginPageModel model)
        {
            return Validate(model).IsValid;
        }
    }
}
