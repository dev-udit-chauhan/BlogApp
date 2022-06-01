﻿using Blog.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Contracts
{
    public interface IBlogRepository
    {
        Task<bool> AddBlogAsync(BlogEntity blog);
        
        Task<bool> DeleteBlogAsync(int blogID);
        
        Task<List<BlogEntity>> GetAllBlogsAsync();

        Task<BlogEntity> GetBlogByIDAsync(int blogID);

        Task<bool> UpdateBlogAsync(BlogEntity blog);

    }
}