using AutoMapper;
using MealOrdering.Business.Abstract;
using MealOrdering.Core.Entities.Dto;
using MealOrdering.Core.Utilities;
using MealOrdering.Core.Utilities.Results.Abstract;
using MealOrdering.Core.Utilities.Results.Concrete;
using MealOrdering.Entities.Concrete;
using MealOrdering.Repository.Abstract;
using Microsoft.Extensions.Logging;

namespace MealOrdering.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly ILogger<UserManager> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserManager(ILogger<UserManager> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<UserDto>> AddUser(UserDto user)
        {
            try
            {
                User dbUser = await _unitOfWork.User.GetByIdAsync(user.Id);

                if (dbUser is not null)
                    throw new Exception("The corresponding record already exists.");

                user.Password = HashingHelper.HashPassword(user.Password);

                dbUser = _mapper.Map<User>(user);

                await _unitOfWork.User.InsertAsync(dbUser);

                await _unitOfWork.SaveAsync();

                return new SuccessDataResult<UserDto>(_mapper.Map<UserDto>(dbUser));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorDataResult<UserDto>("An error occurred while adding a user.");
            }
        }

        public async Task<IDataResult<UserDto>> GetUserById(Guid id)
        {
            try
            {
                User dbUser = await _unitOfWork.User.GetByIdAsync(id);

                return new SuccessDataResult<UserDto>(_mapper.Map<UserDto>(dbUser));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorDataResult<UserDto>("An error occurred while pulling a user.");
            }
        }

        public async Task<IDataResult<UserDto>> GetUserByEmail(string email)
        {
            try
            {
                User dbUser = await _unitOfWork.User.GetAsync(predicate => predicate.EmailAddress.Contains(email));

                if (dbUser is null)
                    throw new Exception("The corresponding record already exists.");

                if (!dbUser.IsActive)
                    throw new Exception("The user is inactive");

                return new SuccessDataResult<UserDto>(_mapper.Map<UserDto>(dbUser));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorDataResult<UserDto>("An error occurred while pulling a user.");
            }
        }

        public async Task<IDataResult<List<UserDto>>> GetAllUsers()
        {
            try
            {
                List<User> dbUsers = await _unitOfWork.User.GetAllAsync();

                return new SuccessDataResult<List<UserDto>>(_mapper.Map<List<UserDto>>(dbUsers));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorDataResult<List<UserDto>>("An error occurred while pulling a user.");
            }
        }

        public async Task<IDataResult<UserDto>> UpdateUser(UserDto user)
        {
            try
            {
                User dbUser = await _unitOfWork.User.GetByIdAsync(user.Id);

                if (dbUser is null)
                    throw new Exception("The corresponding record already exists.");

                user.Password = HashingHelper.HashPassword(user.Password);

                _mapper.Map(user, dbUser);

                await _unitOfWork.SaveAsync();

                return new SuccessDataResult<UserDto>(_mapper.Map<UserDto>(dbUser));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorDataResult<UserDto>("An error occurred during the user update process.");
            }
        }

        public async Task<IDataResult<bool>> DeleteUserById(Guid id)
        {
            try
            {
                User dbUser = await _unitOfWork.User.GetByIdAsync(id);

                if (dbUser is null)
                    throw new Exception("User not found");

                await _unitOfWork.User.DeleteAsync(dbUser);

                int result = await _unitOfWork.SaveAsync();

                return new SuccessDataResult<bool>(result > 0);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorDataResult<bool>("An error occurred while deleting the user.");
            }
        }
    }
}
