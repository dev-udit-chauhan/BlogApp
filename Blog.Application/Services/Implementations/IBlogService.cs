using Blog.Application.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Services.Implementations
{
    public class BlogService : IBlogService
    {
        public Task<bool> AddBlogAsync(Models.Blog blog)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBlogAsync(int blogID)
        {
            throw new NotImplementedException();
        }

        public Task<List<Models.Blog>> GetAllBlogsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Models.Blog> GetBlogByIDAsync(int blogID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBlogAsync(Models.Blog blog)
        {
            throw new NotImplementedException();
        }
    }
}
