using AutoMapper;
using Blog.Application.Exceptions;
using Blog.Application.Services.Contracts;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WebApi.Controllers.V1
{
    [Authorize]
    [ApiController]
    [Route("api/blog/v{version}/[controller]")]
    [ApiVersion("1.0")]
    public class BlogController : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger<BlogController> logger;
        private readonly IBlogService blogService;

        /// <summary>
        /// Injecting services
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="blogService"></param>
        public BlogController(ILogger<BlogController> logger, IMapper mapper, IBlogService blogService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.blogService = blogService;
        }

        /// <summary>
        /// Create a new Blog Asynchronously
        /// </summary>
        /// <param name="blog">Blog Model</param>
        /// <returns>Blog Model</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Route("/api/blog")]
        public async Task<ActionResult<Infrastructure.Entities.Blog>> AddBlogAsync(Models.Blog blog)
        {
            if (!ModelState.IsValid)
            {
                this.logger.LogError("Invalid Blog object sent from client.");
                return BadRequest("Invalid Blog object");
            }
            try
            {
                var blogModel = await this.blogService.AddBlogAsync(blog);
                if(blogModel != null)
                {
                    this.logger.LogInformation("New Blog Created");
                    var blogEntity = mapper.Map<Infrastructure.Entities.Blog>(blogModel);
                    return Created("/api/blog", blogEntity);
                }
                return BadRequest("Unable to create blog");
            }

            catch (ArgumentException ex)
            {
                this.logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (BlogAlreadyExistsException ex)
            {
                this.logger.LogError(ex.Message);
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return Conflict(ex.Message);
            }
        }


        /// <summary>
        /// Gets a blog by its blogId Asynchronously
        /// </summary>
        /// <param name="blogId">BlogId</param>
        /// <returns>Blog Model</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("/api/blog/{blogId}")]
        public async Task<ActionResult<Infrastructure.Entities.Blog>> GetBlogByIDAsync(int blogId)
        {
            if (!ModelState.IsValid)
            {
                this.logger.LogError("There is some error");
                return BadRequest("There is some error");
            }
            try
            {
                var blogById = await blogService.GetBlogByIDAsync(blogId);
                var blogEntity = this.mapper.Map<Infrastructure.Entities.Blog>(blogById);
                return Ok(blogEntity);
            }
            catch (BlogNotFoundException ex)
            {
                this.logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Gets all blogs Asynchronously
        /// </summary>
        /// <returns>List of Blogs</returns>
        [HttpGet]
        [ProducesResponseType(statusCode:200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("/api/blogs")]
        public async Task<ActionResult<List<Infrastructure.Entities.Blog>>> GetAllBlogsAsync()
        {
            if (!ModelState.IsValid)
            {
                this.logger.LogError("There is some error");
                return BadRequest("There is some error");
            }
            try
            {
                var blogList = await this.blogService.GetAllBlogsAsync();
                var blogEntityList = this.mapper.Map<List<Infrastructure.Entities.Blog>>(blogList);
                return Ok(blogEntityList);
            }

            catch (NoBlogsFoundException ex)
            {
                this.logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Updates a Model Asynchronously
        /// </summary>
        /// <param name="blog">Blog</param>
        /// <returns>Accepted</returns>
        [HttpPut]
        [Route("/api/blog/update")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Infrastructure.Entities.Blog>> UpdateBlogAsync(Models.Blog blog)
        {
            if (!ModelState.IsValid)
            {
                this.logger.LogError("There is some error");
                return BadRequest("There is some error");
            }
            try
            {
                var blogModel = await this.blogService.UpdateBlogAsync(blog);
                var blogEntity = this.mapper.Map<Infrastructure.Entities.Blog>(blogModel);
                if (blogEntity != null)
                    return Accepted(blogEntity);
                return BadRequest("Unable to Update blog");
            }
            catch (ArgumentException ex)
            {
                this.logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Deletes a blog Asynchronously 
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/api/delete/{blogId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Infrastructure.Entities.Blog>> DeleteBlogAsync(int blogId)
        {
            if (!ModelState.IsValid)
            {
                this.logger.LogError("There is some error");
                return BadRequest("There is some error");
            }
            try
            {
                var blogModel = await this.blogService.DeleteBlogAsync(blogId);
                var blogEntity = this.mapper.Map<Infrastructure.Entities.Blog>(blogModel);
                if (blogEntity != null)
                    return Accepted(blogEntity);
                return BadRequest("Unable to Delete blog");

            }
            catch (BlogNotFoundException ex)
            {
                this.logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }           
        }
    }
}
