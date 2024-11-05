using FluentValidation;
using Project.WebApi.Entities.Models;
using UserManagementServices.ViewModels;

namespace UserManagementServices.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Username).NotEmpty().WithMessage("Username is required.");
            RuleFor(user => user.Password).NotEmpty().WithMessage("Password is required.");

            RuleFor(user => user.Fullname).NotEmpty().WithMessage("Full name is required.");
            RuleFor(user => user.Email).NotEmpty().WithMessage("Email is required.");
        }
    }
}
