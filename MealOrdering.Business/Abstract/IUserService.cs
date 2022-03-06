using MealOrdering.Core.Entities.Dto;
using MealOrdering.Core.Utilities.Results.Abstract;

namespace MealOrdering.Business.Abstract;

public interface IUserService
{
    Task<IDataResult<UserDto>> AddUser(UserDto user);

    Task<IDataResult<UserDto>> GetUserById(Guid id);

    Task<IDataResult<UserDto>> GetUserByEmail(string email);

    Task<IDataResult<List<UserDto>>> GetAllUsers();

    Task<IDataResult<UserDto>> UpdateUser(UserDto user);

    Task<IDataResult<bool>> DeleteUserById(Guid id);
}