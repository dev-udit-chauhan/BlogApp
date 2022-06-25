using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Services.Contracts
{
    public interface IBlogService
    {
        Task<Models.Blog> AddBlogAsync(Models.Blog blog);

        Task<Models.Blog> DeleteBlogAsync(int blogId);

        Task<List<Models.Blog>> GetAllBlogsAsync();

        Task<Models.Blog> GetBlogByIDAsync(int blogId);

        Task<Models.Blog> UpdateBlogAsync(Models.Blog blog);
    }
}
