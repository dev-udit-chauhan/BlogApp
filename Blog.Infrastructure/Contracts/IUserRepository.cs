using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Contracts
{
    public interface IUserRepository
    {
        Task<int> RegisterUserAsync(Entities.User user, string password);

        Task<bool> UserExistsAsync(string userName);

        Task<string> LoginAsync(string userName, string password);
    }
}
