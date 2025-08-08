using ExpenseTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Repository
{
    public interface IUserRepository
    {
        /// <summary>
        /// Получает пользователя по его id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получает пользователя по его Email.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);

        /// <summary>
        /// Добовляем пользователя в базу.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddAsync(User user, CancellationToken cancellationToken);
    }
}
