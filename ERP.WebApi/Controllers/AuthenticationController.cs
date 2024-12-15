using Microsoft.AspNetCore.Mvc;
using Products.DB;
using Products.Core;
using Products.Core.CustomExceptions;
using Products.Core.DTO;

namespace ERP.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(Products.Core.DTO.User user)
        {
            try
            {
                var result = await _userService.Signup(user);

                return Created("", result);
            }
            catch (UserNameAlreadyExistsException e)
            {
                return StatusCode(409, e.Message);
            }
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(Products.Core.DTO.User userDTO)
        {
            try
            {
                var result = await _userService.SignIn(userDTO);

                return Ok(result);
            }
            catch (InvalidUsernamePasswordException e)
            {
                return StatusCode(401, e.Message);
            }
        }
    }
}
