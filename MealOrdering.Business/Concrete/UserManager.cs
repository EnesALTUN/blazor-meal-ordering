using AutoMapper;
using MealOrdering.Business.Abstract;
using MealOrdering.Entities.Concrete;
using MealOrdering.Entities.Dto;
using MealOrdering.Repository.Abstract;

namespace MealOrdering.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddUser(UserDto user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "The user object cannot be empty.");

            User userEntity = _mapper.Map<User>(user);

            await _unitOfWork.User.InsertAsync(userEntity);
        }

        public async Task<UserDto> GetUserById(Guid id)
        {
            User userEntity = await _unitOfWork.User.GetByIdAsync(id);

            UserDto userDto = _mapper.Map<UserDto>(userEntity);

            return userDto;
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            List<User> usersEntity = await _unitOfWork.User.GetAllAsync();

            List<UserDto> usersDto = _mapper.Map<List<UserDto>>(usersEntity);

            return usersDto;
        }
    }
}
