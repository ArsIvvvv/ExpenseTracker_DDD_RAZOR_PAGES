using ExpenseTracker.Application.DTO;
using ExpenseTracker.Application.DTO.Command;
using ExpenseTracker.Application.ErrorHandler;
using ExpenseTracker.Application.Interface;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Exceptions;
using ExpenseTracker.Domain.Repository;
using ExpenseTracker.Domain.Value_Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Service
{ 
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result<UserDTO>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken);
            if(user == null)
                return Result<UserDTO>.Failure("Пользователь не найден");

            var userDto = new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };

            return Result<UserDTO>.Success(userDto);
        }

        public async Task<Result<UserDTO>> LoginAsync(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);
            if (user == null || !user.IsPasswordValid(command.Password))
                return Result<UserDTO>.Failure($"Данные неверно введены");

            var userdto = new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };

            return Result<UserDTO>.Success(userdto);

        }

        public async Task<Result<UserDTO>> RegisterAsync(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);
            if (user != null )
                return Result<UserDTO>.Failure($"Пользователь с таким email уже существует");

            try
            {
                var password = Password.Create(command.Password);

                var userRegister = new User(command.Name,new Email(command.Email),password);

                await _userRepository.AddAsync(userRegister,cancellationToken);

                var userdto = new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                };

                return Result<UserDTO>.Success(userdto);

            }
            catch (DomainException ex)
            {
                return Result<UserDTO>.Failure(ex.Message);
            }
        }
    }
}
