using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.WebApi.Entities.Models;
using UserManagementServices.Services.Interfaces;

namespace UserManagementServices.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _userService.GetAllUsers();

                var output = new ResponseBase<List<ViewModels.Res_UserVM>> { Status = result.Status, Message = result.Message, Data = result.Data };

                if (result.Status)
                    return Ok(output);
                else
                    return BadRequest(output);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseBase<List<ViewModels.Res_UserVM>> { Status = false, Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _userService.GetUserById(id);

                var output = new ResponseBase<ViewModels.Res_UserDetailVM> { Status = result.Status, Message = result.Message, Data = result.Data };

                if (result.Status)
                    return Ok(output);
                else
                    return BadRequest(output);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseBase<ViewModels.Res_UserDetailVM> { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ViewModels.Req_UserVM data)
        {
            try
            {
                var result = await _userService.InsertUser(data);

                var output = new ResponseBase<ViewModels.Res_UserVM> { Status = result.Status, Message = result.Message, Data = result.Data };

                if (result.Status)
                    return Ok(output);
                else
                    return BadRequest(output);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseBase<ViewModels.Res_UserVM> { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(ViewModels.Req_UserUpdateVM data)
        {
            try
            {
                var result = await _userService.UpdateUser(data);

                var output = new ResponseBase<ViewModels.Res_UserDetailVM> { Status = result.Status, Message = result.Message, Data = result.Data };

                if (result.Status)
                    return Ok(output);
                else
                    return BadRequest(output);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseBase<ViewModels.Res_UserDetailVM> { Status = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string username)
        {
            try
            {
                var result = await _userService.DeleteUser(username);

                var output = new ResponseBase<ViewModels.Res_UserVM> { Status = result.Status, Message = result.Message, Data = result.Data };

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
