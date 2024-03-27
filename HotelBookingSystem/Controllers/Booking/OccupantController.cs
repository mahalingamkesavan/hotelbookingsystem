using HotelBookingSystem.BAL.Absractes;
using HotelBookingSystem.Models.ResponseModels;
using HotelBookingSystem.Utils.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelBookingSystem_2.Controllers.Booking
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(LogActionFilter))]
    public class OccupantController : BaseController
    {

        private readonly IOccupant _Services;

        public OccupantController(IOccupant services)
        {
            _Services = services;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OccupantEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddOccupant([FromBody] OccupantEntity entity)
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
        [ProducesResponseType(typeof(OccupantEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditOccupant(int id, [FromBody] OccupantEntity entity)
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
        [HttpGet("{bookingId:int}")]
        [ProducesResponseType(typeof(OccupantEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBookingByBookingNo(int bookingId)
        {
            if (bookingId <= 0)
                return BadRequest();
            var result = await _Services.Get(x => x.BookingId == bookingId);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            if (id <= 0)
                return BadRequest();
            var flag = await _Services.Delete(x => x.Id == id);
            return Ok(flag);
        }
    }
}
