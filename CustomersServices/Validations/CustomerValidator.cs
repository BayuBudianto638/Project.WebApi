using FluentValidation;
using Project.WebApi.Entities.Models;

namespace CustomersServices.Validations
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(user => user.CustomerCode).NotEmpty().WithMessage("Customer Code is required.");
            RuleFor(user => user.CustomerName).NotEmpty().WithMessage("Customer Name is required.");
            RuleFor(user => user.CustomerAddress).NotEmpty().WithMessage("Customer Address is required.");
        }
    }
}
