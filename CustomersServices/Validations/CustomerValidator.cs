using FluentValidation;
using Project.WebApi.Entities.Models;

namespace CustomersServices.Validations
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(user => user.Address).NotEmpty().WithMessage("Address is required.");
        }
    }
}
