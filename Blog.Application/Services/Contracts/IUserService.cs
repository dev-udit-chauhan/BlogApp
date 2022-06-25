using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Services.Contracts
{
    public interface IUserService
    {
        Task<int> RegisterUserAsync(Models.User user, string password);

        Task<bool> UserExistsAsync(string userName);

        Task<string> LoginAsync(string userName, string password);
    }
}
