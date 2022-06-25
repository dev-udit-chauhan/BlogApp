using AutoMapper;
using Blog.Application.Services.Contracts;
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
    /// <summary>
    /// User Controller
    /// </summary>
    [ApiController]
    [Route("api/user/v{version}/[controller]")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly ILogger<UserController> logger;

        public UserController(IUserService userService, ILogger<UserController> logger, IMapper mapper)
        {
            this.userService = userService;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// Register User Asynchronously
        /// </summary>
        /// <param name="user">User</param>
        /// <returns></returns>
        [HttpPost]
        [Route("registerUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> RegisterUserAsync(Models.User user)
        {
            if (!ModelState.IsValid)
            {
                this.logger.LogError("Invalid User Details");
                return BadRequest("Invalid User details");
            }
            try
            {
                var userModel = new Models.User
                {
                    UserName = user.UserName
                };
                var result = await userService.RegisterUserAsync(userModel, user.Password);
                if (result > 0)
                    return Created("/api/user", user.UserName + " is Successfully registered");
                return
                    BadRequest("There is some error while registering user. Please try again");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Logs in user Asynchronously
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginAsync(Models.User user)
        {
            if (!ModelState.IsValid)
            {
                this.logger.LogError("There is some error");
                return BadRequest("There is some error");
            }
            try
            {
                var result = await userService.LoginAsync(user.UserName, user.Password);
                return Ok(result);               
            }
            catch (ArgumentException ex)
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
