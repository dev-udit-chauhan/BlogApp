using AutoMapper;
using Blog.Application.Exceptions;
using Blog.Application.Services.Contracts;
using Blog.Infrastructure.Contracts;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository blogRepository;
        private readonly IMapper mapper;

        public BlogService(IBlogRepository blogRepository, IMapper mapper)
        {
            this.blogRepository = blogRepository;
            this.mapper = mapper;
        }

        public async Task<Models.Blog> AddBlogAsync(Models.Blog blog)
        {
            ModelNullValidate(blog);
            var blogEntity = MapBlog(blog);
            if (blogEntity != null)
            {
                var blogEntityData = await blogRepository.AddBlogAsync(blogEntity);
                return this.mapper.Map<Models.Blog>(blogEntityData);
            }
            throw new BlogAlreadyExistsException($"A Blog with blogId : {blogEntity.BlogId} already exists");            
        }

        public async Task<Models.Blog> DeleteBlogAsync(int blogId)
        {
            var blogEntity = await blogRepository.GetBlogByIDAsync(blogId);
            if (blogEntity == null)
                throw new BlogNotFoundException($"Blog with blogId : {blogId} does not exist");
            var blog = await blogRepository.DeleteBlogAsync(blogId);
            return this.mapper.Map<Models.Blog>(blog);
        }

        public async Task<List<Models.Blog>> GetAllBlogsAsync()
        {
            var blogList = await blogRepository.GetAllBlogsAsync();
            var blogModelList = this.mapper.Map<List<Models.Blog>>(blogList);
            if (blogModelList.Count <= 0)
                throw new NoBlogsFoundException("No blogs found");
            return blogModelList;
        }

        public async Task<Models.Blog> GetBlogByIDAsync(int blogId)
        {
            var blogEntity = await blogRepository.GetBlogByIDAsync(blogId);
            var blog = this.mapper.Map<Models.Blog>(blogEntity);
            if (blog == null)
                throw new BlogNotFoundException($"Blog with blogId : {blogId} does not exist");
            return blog;
        }

        public async Task<Models.Blog> UpdateBlogAsync(Models.Blog blog)
        {
            ModelNullValidate(blog);
            var blogEntity = MapBlog(blog);
            var blogEntityData = await blogRepository.UpdateBlogAsync(blogEntity);
            return this.mapper.Map<Models.Blog>(blogEntityData);
        }

        private Infrastructure.Entities.Blog MapBlog(Models.Blog blog) => mapper.Map<Infrastructure.Entities.Blog>(blog);

        private static void ModelNullValidate(Models.Blog blog)
        {
            if (blog == null)
                throw new ArgumentException("Invalid Blog Details");
        }
    }
}
