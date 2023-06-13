using Blog.Infrastructure.Contracts;
using Blog.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Implementation
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogDbContext dbContext;

        public BlogRepository(BlogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Entities.Blog> AddBlogAsync(Entities.Blog blog)
        {
            await dbContext.Blogs.AddAsync(blog);
            await dbContext.SaveChangesAsync();
            return blog;
        }

        public async Task<Entities.Blog> DeleteBlogAsync(int blogId)
        {
            var blog = await dbContext.Blogs.FirstOrDefaultAsync(x => x.BlogId == blogId);
            var blogEntity = blog;
            dbContext.Blogs.Remove(blog);
            await dbContext.SaveChangesAsync();
            return blogEntity;
        }

        public async Task<List<Entities.Blog>> GetAllBlogsAsync()
        {
            return await dbContext.Blogs.ToListAsync();
        }

        public async Task<Entities.Blog> GetBlogByIDAsync(int blogId)
        {
            var blog = await dbContext.Blogs.FirstOrDefaultAsync(x => x.BlogId == blogId);
            return blog;
        }

        public async Task<Entities.Blog> UpdateBlogAsync(Entities.Blog blog)
        {
            var blogEntity = await dbContext.Blogs.FirstOrDefaultAsync(x => x.BlogId == blog.BlogId);
            if (blogEntity == null)
                throw new ArgumentException($"Blog with BlogId: {blog.BlogId} does not exist");
            blogEntity.Author = blog.Author;
            blogEntity.Tag = blog.Tag;
            blogEntity.Content = blog.Content;
            blogEntity.DateCreated = blog.DateCreated;
            blogEntity.TypeOfBlog = blog.TypeOfBlog;
            blogEntity.Title = blog.Title;
            dbContext.Entry<Entities.Blog>(blogEntity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return blogEntity;
        }
    }
}
