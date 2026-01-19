using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace DotnetExamSystem.Api.Application.Validators.Base
{
    public abstract class UserBaseValidator<T> : AbstractValidator<T>
    {
        protected void AddCommonRules(
            Expression<Func<T, string?>> mobileExpr,
            Expression<Func<T, string?>> addressExpr,
            Expression<Func<T, IFormFile?>> profileImageExpr)
        {
            RuleFor(mobileExpr)
                .Matches(@"^01[0-9]{9}$")
                .WithMessage("Invalid Bangladeshi mobile number")
                .When(x => !string.IsNullOrEmpty(mobileExpr.Compile().Invoke(x)));

            RuleFor(addressExpr)
                .MaximumLength(200);

            RuleFor(profileImageExpr)
            .Must(file =>
            {
                if (file == null) return true;
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(file.FileName)?.ToLower();
                return allowedExtensions.Contains(extension) && file.Length <= 2 * 1024 * 1024;
            })
            .WithMessage("Profile image must be a JPG or PNG file and max size 2MB");
        }
    }
}
