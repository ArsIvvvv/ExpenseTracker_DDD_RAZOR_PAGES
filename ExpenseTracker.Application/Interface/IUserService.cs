using ExpenseTracker.Application.DTO;
using ExpenseTracker.Application.DTO.Command;
using ExpenseTracker.Application.ErrorHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Interface
{
    public interface IUserService
    {
        /// <summary>
        /// Возвращает результат поиска пользователя по его id.
        /// В случае неудачи возвращает результат ошибки с сообщением.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<UserDTO>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Регестрирует пользователя.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<UserDTO>> RegisterAsync(RegisterUserCommand command, CancellationToken cancellationToken);

        /// <summary>
        /// Авторизирует пользователя 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<UserDTO>> LoginAsync(LoginUserCommand command, CancellationToken cancellationToken);

    }
}
