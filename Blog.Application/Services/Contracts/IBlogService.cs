using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Services.Contracts
{
    public interface IBlogService
    {
        Task<bool> AddBlogAsync(Models.Blog blog);

        Task<bool> DeleteBlogAsync(int blogID);

        Task<List<Models.Blog>> GetAllBlogsAsync();

        Task<Models.Blog> GetBlogByIDAsync(int blogID);

        Task<bool> UpdateBlogAsync(Models.Blog blog);
    }
}
