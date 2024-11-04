using CustomersServices.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.WebApi.Entities.Models;

namespace CustomersServices.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CustomerController(ICustomerService customerService) : ControllerBase
    {
        private readonly ICustomerService _customerService = customerService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _customerService.GetAllCustomers();

                var output = new ResponseBase<List<ViewModels.Res_CustomerVM>> { Status = result.Status, Message = result.Message, Data = result.Data };

                if (result.Status)
                    return Ok(output);
                else
                    return BadRequest(output);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseBase<List<ViewModels.Res_CustomerVM>> { Status = false, Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _customerService.GetCustomerById(id);

                var output = new ResponseBase<ViewModels.Res_CustomerDetailVM> { Status = result.Status, Message = result.Message, Data = result.Data };

                if (result.Status)
                    return Ok(output);
                else
                    return BadRequest(output);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseBase<ViewModels.Res_CustomerDetailVM> { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ViewModels.Req_CustomerVM data)
        {
            try
            {
                var result = await _customerService.InsertCustomer(data);

                var output = new ResponseBase<ViewModels.Res_CustomerVM> { Status = result.Status, Message = result.Message, Data = result.Data };

                if (result.Status)
                    return Ok(output);
                else
                    return BadRequest(output);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseBase<ViewModels.Res_CustomerVM> { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(ViewModels.Req_CustomerUpdateVM data)
        {
            try
            {
                var result = await _customerService.UpdateCustomer(data);

                var output = new ResponseBase<ViewModels.Res_CustomerDetailVM> { Status = result.Status, Message = result.Message, Data = result.Data };

                if (result.Status)
                    return Ok(output);
                else
                    return BadRequest(output);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseBase<ViewModels.Res_CustomerDetailVM> { Status = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string username)
        {
            try
            {
                var result = await _customerService.DeleteCustomer(username);

                var output = new ResponseBase<ViewModels.Res_CustomerVM> { Status = result.Status, Message = result.Message, Data = result.Data };

                if (result.Status)
                    return Ok(output);
                else
                    return BadRequest(output);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseBase<bool> { Status = false, Message = ex.Message });
            }
        }
    }
}
