using AutoMapper;
using Blog.Application.Exceptions;
using Blog.Application.Services.Contracts;
using Blog.Infrastructure.Contracts;
using Blog.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<string> LoginAsync(string userName, string password)
        {
            return await userRepository.LoginAsync(userName, password);
        }

        public async Task<int> RegisterUserAsync(Models.User user, string password)
        {
            var userExist = await UserExistsAsync(user.UserName);
            if (userExist)
                throw new UserAlreadyExistsException("User already exists");
            var userEntity = mapper.Map<Infrastructure.Entities.User>(user);
            var result = await userRepository.RegisterUserAsync(userEntity, password);
            if (result > 0)
                return result;
            throw new ArgumentException("User not added");
        }

        public async Task<bool> UserExistsAsync(string userName)
        {
            return await userRepository.UserExistsAsync(userName);
        }
    }
}
