using HotelBookingSystem.BAL.Absractes;
using HotelBookingSystem.Models.Constants;
using HotelBookingSystem.Models.ResponseModels;
using HotelBookingSystem.Utils.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelBookingSystem_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{Roles.Customer},{Roles.Admin}")]
    [ServiceFilter(typeof(LogActionFilter))]
    public class BookingController : BaseController
    {
        private readonly IBooking _Services;

        public BookingController(IBooking services)
        {
            this._Services = services;
        }

        [HttpPost]
        [ProducesResponseType(typeof(BookingEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddBooking([FromBody] BookingEntity entity)
        {
            string UserName = string.Empty;
            UserName = User.Claims.Where(x =>
                x.Type == ClaimTypes.Name).First().Value;
            if (string.IsNullOrEmpty(UserName))
            {
                return BadRequest();
            }
            var result = await _Services.Create(entity, UserName);
            return Ok(result);
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(typeof(BookingEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditBooking(int id, [FromBody] BookingEntity entity)
        {
            string UserName = string.Empty;
            UserName = User.Claims.Where(x =>
                x.Type == ClaimTypes.Name).First().Value;
            if (string.IsNullOrEmpty(UserName))
            {
                return BadRequest();
            }
            var result = await _Services.Update(entity, id, UserName);
            return Ok(result);
        }
        [HttpGet("{bookingno}")]
        [ProducesResponseType(typeof(BookingEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBookingByBookingNo(string bookingno)
        {
            if (bookingno == null)
                return BadRequest();
            var result = await _Services.Get(x => x.BookingNo == bookingno);
            return Ok(result);
        }
        [HttpGet()]
        [ProducesResponseType(typeof(BookingEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByUserName()
        {
            string UserName = string.Empty;
            UserName = User.Claims.Where(x =>
                x.Type == ClaimTypes.Name).First().Value;
            if (string.IsNullOrEmpty(UserName))
            {
                return BadRequest();
            }
            var result = await _Services.GetByUser(UserName);
            return Ok(result);
        }
        [HttpPatch("Approve/{bookingno}")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(BookingEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AproveBooking([FromRoute] string bookingno, [FromQuery] string status)
        {
            string UserName = string.Empty;
            UserName = User.Claims.Where(x =>
                x.Type == ClaimTypes.Name).First().Value;
            if (string.IsNullOrEmpty(UserName))
            {
                return BadRequest();
            }
            BookingEntity entity = new BookingEntity
            {
                BookingNo = bookingno,
                Status = status
            };
            var result = await _Services.Update(entity, 0, UserName);
            return Ok(result);
        }

        [HttpDelete("{bookingno}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBooking(String bookingno)
        {
            if (string.IsNullOrEmpty(bookingno))
                return BadRequest();
            var flag = await _Services.Delete(x => x.BookingNo == bookingno);
            return Ok(flag);
        }
    }
}
