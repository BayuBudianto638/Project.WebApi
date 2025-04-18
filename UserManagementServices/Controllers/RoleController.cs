using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.WebApi.Entities.Models;
using UserManagementServices.Services.Interfaces;

namespace UserManagementServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController(IRoleService roleService) : ControllerBase
    {
        private readonly IRoleService _roleService = roleService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _roleService.GetAllRoles();

                if (result.Status)
                    return Ok(ResponseGetBase<List<ViewModels.Res_RoleVM>>.Success(result.Data, result.Message));
                else
                    return BadRequest(ResponseGetBase<List<ViewModels.Res_RoleVM>>.Fail(result.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseGetBase<List<ViewModels.Res_RoleVM>>.Fail(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _roleService.GetRoleById(id);

                if (result.Status)
                    return Ok(ResponseGetBase<ViewModels.Res_RoleDetailVM>.Success(result.Data, result.Message));
                else
                    return BadRequest(ResponseGetBase<ViewModels.Res_RoleDetailVM>.Fail(result.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseGetBase<ViewModels.Res_RoleDetailVM>.Fail(ex.Message));
            }
        }

        [HttpGet("menu/{id}")]
        public async Task<IActionResult> GetAssignedMenu(int id)
        {
            try
            {
                var result = await _roleService.GetRoleAssignedMenu(id);

                if (result.Status)
                    return Ok(ResponseGetBase<List<ViewModels.Res_MenuVM>>.Success(result.Data, result.Message));
                else
                    return BadRequest(ResponseGetBase<List<ViewModels.Res_MenuVM>>.Fail(result.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseGetBase<List<ViewModels.Res_MenuVM>>.Fail(ex.Message));
            }
        }


        [HttpPost]
        public async Task<IActionResult> Insert(ViewModels.Req_RoleVM data)
        {
            try
            {
                var result = await _roleService.InsertRole(data);

                var output = new ResponseBase<ViewModels.Res_RoleDetailVM> { Status = result.Status, Message = result.Message, Data = result.Data };

                if (result.Status)
                    return Ok(output);
                else
                    return BadRequest(output);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseBase<ViewModels.Res_RoleDetailVM> { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(ViewModels.Req_RoleUpdateVM data)
        {
            try
            {
                var result = await _roleService.UpdateRole(data);

                var output = new ResponseBase<ViewModels.Res_RoleDetailVM> { Status = result.Status, Message = result.Message, Data = result.Data };

                if (result.Status)
                    return Ok(output);
                else
                    return BadRequest(output);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseBase<ViewModels.Res_RoleDetailVM> { Status = false, Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _roleService.DeleteRole(id);

                var output = new ResponseBase<ViewModels.Res_RoleVM> { Status = result.Status, Message = result.Message, Data = result.Data };

                if (result.Status)
                    return Ok(output);
                else
                    return BadRequest(output);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseBase<ViewModels.Res_RoleVM> { Status = false, Message = ex.Message });
            }
        }
    }
}
