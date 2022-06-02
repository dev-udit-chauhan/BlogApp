using Blog.Infrastructure.Contracts;
using Blog.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Implementation
{
    public class BlogRepository : IBlogRepository
    {
        public Task<bool> AddBlogAsync(BlogEntity blog)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBlogAsync(int blogId)
        {
            throw new NotImplementedException();
        }

        public Task<List<BlogEntity>> GetAllBlogsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BlogEntity> GetBlogByIDAsync(int blogId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBlogAsync(BlogEntity blog)
        {
            throw new NotImplementedException();
        }
    }
}
