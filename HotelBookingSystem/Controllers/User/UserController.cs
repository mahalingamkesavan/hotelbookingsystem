using HotelBookingSystem.BAL.Absractes;
using HotelBookingSystem.Models.Constants;
using HotelBookingSystem.Models.ResponseModels;
using HotelBookingSystem.Utils.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelBookingSystem_2.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(LogActionFilter))]
    public class UserController : BaseController
    {
        private readonly IUser UserServices;

        public UserController(IUser user)
        {

            UserServices = user;
        }
        [HttpPost("GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Get()
        {
            string? username = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            var user = await UserServices.GetUser(username ?? throw new Exception());
            if (user != null)
            {
                return Ok(user);
            }
            return Unauthorized();
        }
        // GET api/<UserController>/5
        [HttpPost("Get")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetToken([FromBody] LogIn logIn)
        {
            var token = await UserServices.GetToken(logIn);
            if (token != null)
            {
                return Ok(token);
            }
            return Unauthorized();
        }

        //POST api/<UserController>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserEntity>> NewUser([FromBody] UserEntity user)
        {
            var NewUser = await UserServices.Create(user);
            if (NewUser != null)
            {
                return Ok(NewUser);
            }
            return BadRequest();
        }

        //POST api/<UserController>
        [HttpPost(Roles.Admin)]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserEntity>> NewAdmin([FromBody] UserEntity user)
        {
            var NewUser = await UserServices.CreateAdmin(user);
            if (NewUser != null)
            {
                return Ok(NewUser);
            }
            return BadRequest();
        }

        // PUT api/<UserController>/5
        [HttpPut()]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserEntity>> Put([FromBody] UserEntity user)
        {
            string username = User.Claims.First(x =>
                                        x.Type == ClaimTypes.Name).Value;
            user.Username = username;
            var result = await UserServices.Update(user);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        // DELETE api/<UserController>/5
        [HttpDelete()]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([FromBody] string password)
        {
            string UserName = User.Claims.First(x =>
                                        x.Type == ClaimTypes.Name).Value;
            var result = await UserServices.Delete(user =>
                            user.Username == UserName &&
                            user.Password == password);
            return Ok(result);
        }
    }
}
