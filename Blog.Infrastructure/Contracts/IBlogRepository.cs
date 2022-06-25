using Blog.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Contracts
{
    public interface IBlogRepository
    {
        Task<Entities.Blog> AddBlogAsync(Entities.Blog blog);
        
        Task<Entities.Blog> DeleteBlogAsync(int blogId);
        
        Task<List<Entities.Blog>> GetAllBlogsAsync();

        Task<Entities.Blog> GetBlogByIDAsync(int blogId);

        Task<Entities.Blog> UpdateBlogAsync(Entities.Blog blog);

    }
}
