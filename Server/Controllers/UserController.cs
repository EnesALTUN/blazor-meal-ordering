using MealOrdering.Business.Abstract;
using MealOrdering.Core.Entities.Dto;
using MealOrdering.Core.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MealOrdering.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ApiResult<UserDto>> CreateUser([FromBody] UserDto user)
        {
            var addedUser = await _userService.AddUser(user);

            return new ApiResult<UserDto>
            {
                Success = addedUser.Success,
                Message = addedUser.Message,
                Data = addedUser.Data
            };
        }

        [HttpGet("{id:guid}")]
        public async Task<ApiResult<UserDto>> GetUserById(Guid id)
        {
            var user = await _userService.GetUserById(id);

            return new ApiResult<UserDto>
            {
                Success = user.Success,
                Message = user.Message,
                Data = user.Data
            };
        }

        [HttpGet]
        public async Task<ApiResult<List<UserDto>>> GetUsers()
        {
            var users = await _userService.GetAllUsers();

            return new ApiResult<List<UserDto>>
            {
                Success = users.Success,
                Message = users.Message,
                Data = users.Data
            };
        }

        [HttpPut]
        public async Task<ApiResult<UserDto>> UpdateUser([FromBody] UserDto user)
        {
            var updatedUser = await _userService.UpdateUser(user);

            return new ApiResult<UserDto>
            {
                Success = updatedUser.Success,
                Message = updatedUser.Message,
                Data = updatedUser.Data
            };
        }

        [HttpDelete]
        public async Task<ApiResult<bool>> DeleteUser([FromBody] Guid id)
        {
            var isDeleteUser = await _userService.DeleteUserById(id);

            return new ApiResult<bool>
            {
                Success = isDeleteUser.Success,
                Message = isDeleteUser.Message,
                Data = isDeleteUser.Data
            };
        }
    }
}
