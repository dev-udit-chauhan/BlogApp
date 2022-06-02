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

        Task<bool> DeleteBlogAsync(int blogId);

        Task<List<Models.Blog>> GetAllBlogsAsync();

        Task<Models.Blog> GetBlogByIDAsync(int blogId);

        Task<bool> UpdateBlogAsync(Models.Blog blog);
    }
}
