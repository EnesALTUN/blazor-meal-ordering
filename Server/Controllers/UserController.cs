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

        [HttpGet]
        public async Task<ServiceResponse<List<UserDto>>> GetUsers()
        {
            var result = await _userService.GetAllUsers();

            return new ServiceResponse<List<UserDto>>
            {
                Data = result
            };
        }
    }
}
