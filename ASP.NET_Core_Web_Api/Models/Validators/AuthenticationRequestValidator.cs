using ASP.NET_Core_Web_Api.Models.Requests;
using FluentValidation;

namespace ASP.NET_Core_Web_Api.Models.Validators
{
    public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
    {
        public AuthenticationRequestValidator()
        {
            RuleFor(x => x.Login)
                .NotNull()
                .NotEmpty()
                .Length(7, 255)
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .Length(5, 30);
        }
    }
}
