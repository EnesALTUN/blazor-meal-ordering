﻿using AutoMapper;
using MealOrdering.Business.Abstract;
using MealOrdering.Core.Entities.Dto;
using MealOrdering.Core.Utilities;
using MealOrdering.Entities.Concrete;
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

        public async Task<UserDto> AddUser(UserDto user)
        {
            User dbUser = await _unitOfWork.User.GetByIdAsync(user.Id);

            if (dbUser is not null)
                throw new Exception("The corresponding record already exists.");

            user.Password = HashingHelper.HashPassword(user.Password);

            dbUser = _mapper.Map<User>(user);

            await _unitOfWork.User.InsertAsync(dbUser);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<UserDto>(dbUser);
        }

        public async Task<UserDto> GetUserById(Guid id)
        {
            User dbUser = await _unitOfWork.User.GetByIdAsync(id);

            return _mapper.Map<UserDto>(dbUser);
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            User dbUser = await _unitOfWork.User.GetAsync(predicate => predicate.EmailAddress.Contains(email));

            if (dbUser is null)
                throw new Exception("The corresponding record already exists.");

            if (!dbUser.IsActive)
                throw new Exception("The user is inactive");

            return _mapper.Map<UserDto>(dbUser);
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            List<User> dbUsers = await _unitOfWork.User.GetAllAsync();

            return _mapper.Map<List<UserDto>>(dbUsers);
        }

        public async Task<UserDto> UpdateUser(UserDto user)
        {
            User dbUser = await _unitOfWork.User.GetByIdAsync(user.Id);

            if (dbUser is null)
                throw new Exception("The corresponding record already exists.");

            _mapper.Map(user, dbUser);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<UserDto>(dbUser);
        }

        public async Task<bool> DeleteUserById(Guid id)
        {
            User dbUser = await _unitOfWork.User.GetByIdAsync(id);

            if (dbUser is null)
                throw new Exception("User not found");

            await _unitOfWork.User.DeleteAsync(dbUser);

            int result = await _unitOfWork.SaveAsync();

            return result > 0;
        }
    }
}
