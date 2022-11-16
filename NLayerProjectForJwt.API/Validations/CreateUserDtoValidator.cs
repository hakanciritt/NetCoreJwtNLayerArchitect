using FluentValidation;
using NLayerProjectForJwt.Core.Dtos;

namespace NLayerProjectForJwt.API.Validations
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(c => c.UserName).NotEmpty().WithMessage("Kullanıcı adı buoş olamaz");
            RuleFor(c => c.Email).NotEmpty().WithMessage("Email boş olamaz");
            RuleFor(c => c.Email).EmailAddress().WithMessage("Lütfen doğru bir email giriniz");
            RuleFor(c => c.Password).NotEmpty().WithMessage("Şifre alanı zorunludur");
        }
    }
}
