using MealOrdering.Entities.Dto;

namespace MealOrdering.Business.Abstract
{
    public interface IUserService
    {
        Task<UserDto> AddUser(UserDto user);

        Task<UserDto> GetUserById(Guid id);

        Task<List<UserDto>> GetAllUsers();

        Task<UserDto> UpdateUser(UserDto user);

        Task<bool> DeleteUserById(Guid id);
    }
}
