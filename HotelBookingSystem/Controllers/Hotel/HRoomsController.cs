using HotelBookingSystem.BAL.Absractes;
using HotelBookingSystem.Models.ResponseModels;
using HotelBookingSystem.Utils.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem_2.Controllers.Hotel
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    [ServiceFilter(typeof(LogActionFilter))]
    public class HRoomsController : BaseController
    {
        public HRoomsController(IRoom servies)
        {
            _Servies = servies;
        }

        public IRoom _Servies { get; }

        // GET: api/<HotelController>
        [HttpGet("Get/{HotelId}")]
        [ProducesResponseType(typeof(IEnumerable<HotelRoomEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int HotelId)
        {
            var res = await _Servies.Get(H => H.HotelId == HotelId);
            if (res != null)
                return Ok(res);
            else
                return BadRequest();
        }

        //GET api/<HotelController>/5
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(HotelRoomEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {
            var hotel = await _Servies.GetById(id);
            if (hotel == null)
                return BadRequest();
            return Ok(hotel);
        }
        [HttpPost]
        [ProducesResponseType(typeof(HotelRoomEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddRoom([FromBody] HotelRoomEntity room)
        {
            var result = await _Servies.Create(room);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpPatch("Edit/{id:int}")]
        [ProducesResponseType(typeof(HotelRoomEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> EditRoom([FromRoute] int id, [FromBody] HotelRoomEntity room)
        {

            var result = await _Servies.Update(room, id);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteRoom([FromRoute] int id)
        {

            var result = await _Servies.Delete(x => x.Id == id);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
