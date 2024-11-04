using Project.WebApi.Entities.Models;

namespace CustomersServices.Services.Interfaces
{
    public interface ICustomerService
    {
        public Task<ResponseBase<List<ViewModels.Res_CustomerVM>>> GetAllCustomers();
        public Task<ResponseBase<ViewModels.Res_CustomerDetailVM>> GetCustomerByCustomerName(string name);
        public Task<ResponseBase<ViewModels.Res_CustomerDetailVM>> GetCustomerById(int id);
        public Task<ResponseBase<ViewModels.Res_CustomerVM>> InsertCustomer(ViewModels.Req_CustomerVM data);
        public Task<ResponseBase<ViewModels.Res_CustomerVM>> DeleteCustomer(string name);
        public Task<ResponseBase<ViewModels.Res_CustomerDetailVM>> UpdateCustomer(ViewModels.Req_CustomerUpdateVM data);
        public Task<Customer> GetCustomerInfo(string name);
    }
}
