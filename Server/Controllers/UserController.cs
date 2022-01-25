using MealOrdering.Business.Abstract;
using MealOrdering.Entities.Dto;
using MealOrdering.Entities.Response;
using Microsoft.AspNetCore.Mvc;

namespace MealOrdering.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ServiceResponse<UserDto>> CreateUser([FromBody] UserDto user)
        {
            return new ServiceResponse<UserDto>
            {
                Data = await _userService.AddUser(user)
            };
        }

        [HttpGet("{id:guid}")]
        public async Task<ServiceResponse<UserDto>> GetUserById(Guid id)
        {
            return new ServiceResponse<UserDto>
            {
                Data = await _userService.GetUserById(id)
            };
        }

        [HttpGet]
        public async Task<ServiceResponse<List<UserDto>>> GetUsers()
        {
            return new ServiceResponse<List<UserDto>>
            {
                Data = await _userService.GetAllUsers()
            };
        }

        [HttpPut]
        public async Task<ServiceResponse<UserDto>> UpdateUser([FromBody] UserDto user)
        {
            return new ServiceResponse<UserDto>
            {
                Data = await _userService.UpdateUser(user)
            };
        }

        [HttpDelete]
        public async Task<ServiceResponse<bool>> DeleteUser([FromBody] Guid id)
        {
            return new ServiceResponse<bool>
            {
                Data = await _userService.DeleteUserById(id)
            };
        }
    }
}
