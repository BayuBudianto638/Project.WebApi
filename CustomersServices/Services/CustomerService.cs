﻿using AuthorizationLib.Enums;
using AuthorizationLib.Tools;
using CustomersServices.Services.Interfaces;
using CustomersServices.ViewModels;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Project.WebApi.Entities.Data;
using Project.WebApi.Entities.Models;

namespace CustomersServices.Services
{
    public class CustomerService(IHttpContextAccessor httpContextAccessor, AppDbContext context, IValidator<Customer> customerValidator) : ICustomerService
    {
        private readonly AppDbContext _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly AuthorizationTool _authorizationTool = new AuthorizationTool(context);
        private readonly IValidator<Customer> _customerValidator = customerValidator;

        public async Task<ResponseBase<Res_CustomerVM>> DeleteCustomer(string name)
        {
            try
            {
                var authed = await _authorizationTool.IsAuthorized(_httpContextAccessor.HttpContext.User, AuthGrantEnum.DELETE);

                if (!authed.Auth)
                    throw new Exception(authed.Message);

                if (name.Length < 1)
                    throw new Exception("Id cannot be empty");

                Customer? removedCustomer = await _context.Customers.Where(x => x.CustomerName == name).FirstOrDefaultAsync();

                if (removedCustomer == null || removedCustomer.IsDeleted == true)
                    throw new Exception("Customer Name not found");

                removedCustomer.IsDeleted = true;
                removedCustomer.DeletedBy = authed.UserId;
                removedCustomer.DeletedAt = DateTimeOffset.UtcNow;
                removedCustomer.UpdatedBy = authed.UserId;
                removedCustomer.UpdatedAt = DateTimeOffset.UtcNow;

                _context.Attach(removedCustomer);
                _context.Entry(removedCustomer).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return new ResponseBase<ViewModels.Res_CustomerVM> { Status = true, Message = "OK", Data = CustomerToVM(removedCustomer) };
            }
            catch (Exception ex)
            {
                return new ResponseBase<ViewModels.Res_CustomerVM> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ResponseBase<List<Res_CustomerVM>>> GetAllCustomers()
        {
            try
            {
                var authed = await _authorizationTool.IsAuthorized(_httpContextAccessor.HttpContext.User, AuthGrantEnum.READ);

                if (!authed.Auth)
                    throw new Exception(authed.Message);

                List<ViewModels.Res_CustomerVM> customers = await _context.Customers
                    .Where(x => x.IsDeleted == false)
                    .Select(x => CustomerToVM(x))
                    .ToListAsync();

                return new ResponseBase<List<ViewModels.Res_CustomerVM>> { Status = true, Message = "OK", Data = customers };

            }
            catch (Exception ex)
            {
                return new ResponseBase<List<ViewModels.Res_CustomerVM>> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ResponseBase<Res_CustomerDetailVM>> GetCustomerByCustomerName(string name)
        {
            try
            {
                var authed = await _authorizationTool.IsAuthorized(_httpContextAccessor.HttpContext.User, AuthGrantEnum.READ);

                if (!authed.Auth)
                    throw new Exception(authed.Message);

                if (string.IsNullOrEmpty(name))
                    throw new Exception("Invalid body");

                Customer? customer = await _context.Customers.Where(x =>
                        x.CustomerName == name &&
                        x.IsDeleted == false).FirstOrDefaultAsync() ?? throw new Exception("Username not found");

                return new ResponseBase<ViewModels.Res_CustomerDetailVM> { Status = true, Message = "OK", Data = await CustomerToDetailVM(customer) };
            }
            catch (Exception ex)
            {
                return new ResponseBase<ViewModels.Res_CustomerDetailVM> { Status = false, Message = ex.Message };
            }
        }        

        public async Task<ResponseBase<Res_CustomerDetailVM>> GetCustomerById(int customerId)
        {
            try
            {
                var authed = await _authorizationTool.IsAuthorized(_httpContextAccessor.HttpContext.User, AuthGrantEnum.READ);

                if (!authed.Auth)
                    throw new Exception(authed.Message);

                if (customerId < 1)
                    throw new Exception("Invalid body");

                Customer? customer = await _context.Customers.Where(x =>
                        x.CustomerId == customerId &&
                        x.IsDeleted == false).FirstOrDefaultAsync() ?? throw new Exception("Id not found");

                return new ResponseBase<ViewModels.Res_CustomerDetailVM> { Status = true, Message = "OK", Data = await CustomerToDetailVM(customer) };

            }
            catch (Exception ex)
            {
                return new ResponseBase<ViewModels.Res_CustomerDetailVM> { Status = false, Message = ex.Message };
            }
        }

        public async Task<Customer> GetCustomerInfo(string name)
        {
            return await _context.Customers.FirstOrDefaultAsync(x =>
                        x.CustomerName == name &&
                        x.IsDeleted == false)
                        ?? throw new Exception($"Customer {name} is not registered in the System");
        }

        public async Task<ResponseBase<Res_CustomerVM>> InsertCustomer(Req_CustomerVM data)
        {
            try
            {
                var authed = await _authorizationTool.IsAuthorized(_httpContextAccessor.HttpContext.User, AuthGrantEnum.CREATE);

                if (!authed.Auth)
                    throw new Exception(authed.Message);

                if (data == null)
                    throw new Exception("Invalid body");

                if (await _context.Customers.Where(x => x.CustomerName == data.CustomerName).AnyAsync())
                    throw new Exception("Customer Name already exist");

                Customer newCustomer = new Customer
                {
                    CustomerCode = data.CustomerCode,
                    CustomerName = data.CustomerName,
                    CustomerAddress = data.CustomerAddress,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = DateTimeOffset.UtcNow,
                    CreatedBy = authed.UserId,
                    UpdatedAt = DateTimeOffset.UtcNow,
                    UpdatedBy = authed.UserId
                };

                if (ValidateCustomer(newCustomer).Equals(false))
                    throw new Exception("Please fill all value");

                await _context.Customers.AddAsync(newCustomer);

                await _context.SaveChangesAsync();

                return new ResponseBase<ViewModels.Res_CustomerVM> { Status = true, Message = "OK", Data = CustomerToVM(newCustomer) };
            }
            catch (Exception ex)
            {
                return new ResponseBase<ViewModels.Res_CustomerVM> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ResponseBase<Res_CustomerDetailVM>> UpdateCustomer(ViewModels.Req_CustomerUpdateVM data)
        {
            try
            {
                var authed = await _authorizationTool.IsAuthorized(_httpContextAccessor.HttpContext.User, AuthGrantEnum.UPDATE);

                if (!authed.Auth)
                    throw new Exception(authed.Message);

                if (data == null)
                    throw new Exception("Invalid body");

                Customer? updateCustomer = await _context.Customers.FindAsync(data.CustomerId) ?? throw new Exception("Customer not found");

                if (updateCustomer.CustomerName != data.CustomerName)
                {
                    if (await _context.Customers.Where(x => x.CustomerName == data.CustomerName).AnyAsync())
                        throw new Exception("Customer already exist");

                    updateCustomer.CustomerName = data.CustomerName;
                }

                updateCustomer.CustomerAddress = data.CustomerAddress;

                if (ValidateCustomer(updateCustomer).Equals(false))
                    throw new Exception("Please fill all value");

                _context.Attach(updateCustomer);
                _context.Entry(updateCustomer).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return new ResponseBase<ViewModels.Res_CustomerDetailVM> { Status = true, Message = "OK", Data = await CustomerToDetailVM(updateCustomer) };
            }
            catch (Exception ex)
            {
                return new ResponseBase<ViewModels.Res_CustomerDetailVM> { Status = false, Message = ex.Message };
            }
        }


        private static Res_CustomerVM CustomerToVM(Customer data)
        {
            return new ViewModels.Res_CustomerVM
            {
                CustomerId = (int)data.CustomerId,
                CustomerCode = data.CustomerCode,
                CustomerName = data.CustomerName,
                CustomerAddress = data.CustomerAddress
            };
        }

        private async Task<Res_CustomerDetailVM> CustomerToDetailVM(Customer data)
        {
            return new ViewModels.Res_CustomerDetailVM
            {
                CustomerId = (int)data.CustomerId,
                CustomerCode = data.CustomerCode,
                CustomerName = data.CustomerName,
                CustomerAddress = data.CustomerAddress
            };
        }

        private bool ValidateCustomer(Customer customer)
        {
            var validationResult = _customerValidator.Validate(customer);
            return validationResult.IsValid;
        }
    }
}
