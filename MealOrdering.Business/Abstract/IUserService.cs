using MealOrdering.Entities.Dto;

namespace MealOrdering.Business.Abstract
{
    public interface IUserService
    {
        Task<UserDto> GetUserById(Guid id);

        Task<List<UserDto>> GetAllUsers();

        Task AddUser(UserDto user);
    }
}
