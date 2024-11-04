using AuthorizationLib.Enums;
using AuthorizationLib.Tools;
using CustomersServices.Services.Interfaces;
using CustomersServices.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project.WebApi.Entities.Data;
using Project.WebApi.Entities.Models;

namespace CustomersServices.Services
{
    public class CustomerService(IHttpContextAccessor httpContextAccessor, AppDbContext context) : ICustomerService
    {
        private readonly AppDbContext _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly AuthorizationTool _authorizationTool = new AuthorizationTool(context);

        public async Task<ResponseBase<Res_CustomerVM>> DeleteCustomer(string name)
        {
            try
            {
                var authed = await _authorizationTool.IsAuthorized(_httpContextAccessor.HttpContext.User, AuthGrantEnum.DELETE);

                if (!authed.Auth)
                    throw new Exception(authed.Message);

                if (name.Length < 1)
                    throw new Exception("Id cannot be empty");

                Customer? removedCustomer = await _context.Customers.Where(x => x.Name == name).FirstOrDefaultAsync();

                if (removedCustomer == null || removedCustomer.IsDeleted == true)
                    throw new Exception("Customer Name not found");

                removedCustomer.IsDeleted = true;
                removedCustomer.DeletedBy = authed.UserId;
                removedCustomer.DeletedAt = DateTime.Now;
                removedCustomer.UpdatedBy = authed.UserId;
                removedCustomer.UpdatedAt = DateTime.Now;

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
                        x.Name == name &&
                        x.IsDeleted == false).FirstOrDefaultAsync() ?? throw new Exception("Username not found");

                return new ResponseBase<ViewModels.Res_CustomerDetailVM> { Status = true, Message = "OK", Data = await CustomerToDetailVM(customer) };
            }
            catch (Exception ex)
            {
                return new ResponseBase<ViewModels.Res_CustomerDetailVM> { Status = false, Message = ex.Message };
            }
        }        

        public async Task<ResponseBase<Res_CustomerDetailVM>> GetCustomerById(int id)
        {
            try
            {
                var authed = await _authorizationTool.IsAuthorized(_httpContextAccessor.HttpContext.User, AuthGrantEnum.READ);

                if (!authed.Auth)
                    throw new Exception(authed.Message);

                if (id < 1)
                    throw new Exception("Invalid body");

                Customer? customer = await _context.Customers.Where(x =>
                        x.Id == id &&
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
                        x.Name == name &&
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

                if (string.IsNullOrEmpty(data.Name))
                    throw new Exception("Please fill all value");

                if (await _context.Customers.Where(x => x.Name == data.Name).AnyAsync())
                    throw new Exception("Customer Name already exist");

                Customer newCustomer = new Customer
                {
                    Name = data.Name,
                    Address = data.Address,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    CreatedBy = authed.UserId,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = authed.UserId
                };

                var registeredUser = await _context.Customers.AddAsync(newCustomer);

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

                if (
                    string.IsNullOrEmpty(data.Name))
                    throw new Exception("Please fill all value");

                Customer? updateCustomer = await _context.Customers.FindAsync(data.Id) ?? throw new Exception("Customer not found");

                if (updateCustomer.Name != data.Name)
                {
                    if (await _context.Customers.Where(x => x.Name == data.Name).AnyAsync())
                        throw new Exception("Username already exist");

                    updateCustomer.Name = data.Name;
                }

                updateCustomer.Address = data.Address;

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
                Id = (int)data.Id,
                Name = data.Name,
                Address = data.Address
            };
        }

        private async Task<Res_CustomerDetailVM> CustomerToDetailVM(Customer data)
        {
            return new ViewModels.Res_CustomerDetailVM
            {
                Id = (int)data.Id,
                Name = data.Name,
                Address = data.Address
            };
        }
    }
}
