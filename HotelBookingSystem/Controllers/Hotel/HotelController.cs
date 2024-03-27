using HotelBookingSystem.BAL.Absractes;
using HotelBookingSystem.Models.Constants;
using HotelBookingSystem.Models.FilteringModels;
using HotelBookingSystem.Models.ResponseModels;
using HotelBookingSystem.Utils.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelBookingSystem_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [ServiceFilter(typeof(LogActionFilter))]
    public class HotelController : BaseController
    {

        public HotelController(IHotel hotel)
        {
            Hotel = hotel;
        }

        public IHotel Hotel { get; }

        // GET: api/<HotelController>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="searchData"></param>
        /// <returns></returns>
        [HttpGet()]
        [HttpGet("{searchData?}")]
        [ProducesResponseType(typeof(IEnumerable<HotelEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<HotelEntity>>> Get
            (
                [FromQuery] HotelFilter filter, [ValidateNever] string? searchData
            )
        {
            if (searchData != null)
            {
                if (filter == null)
                    filter = new HotelFilter() { Search = searchData };
                else
                    filter.Search = searchData;
            }
            if (filter?.Search == null && filter?.HotelPincode == null && filter?.HotelName == null && filter?.HotelLocation == null)
                return BadRequest();
            var res = await Hotel.Get(filter);
            return Ok(res);
        }
        [HttpGet("GetAll")]
        //[GetForRolesAttributes("GetAll", Roles.Admin)]
        [ProducesResponseType(typeof(IEnumerable<HotelEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<IEnumerable<HotelEntity>>> GetAll
            (
                [FromQuery] HotelFilter filter
            )
        {
            var res = await Hotel.GetForAdmin(filter);
                return Ok(res);
        }

        //GET api/<HotelController>/5
        [HttpGet("Id/{id:int}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(HotelEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<HotelEntity>> GetById(int id)
        {
            var hotel = await Hotel.GetById(id);
            if (hotel == null)
                return BadRequest();
            return Ok(hotel);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(HotelEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<HotelEntity>> AddHotel([FromBody] HotelEntity hotel)
        {
            var result = await Hotel.Create(hotel);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(HotelEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<HotelEntity>> EditHotel(int id, [FromBody] HotelEntity hotel)
        {
            var result = await Hotel.Update(hotel, id);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(HotelEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<HotelEntity>> RemoveHotel(int id)
        {
            var result = await Hotel.DeleteById(id);
            
            return Ok(result);
        }

        [HttpGet("any")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<object>> AnyHotels([FromQuery] HotelFilter filter, [FromQuery] bool forSearch = false)
        {
            var result = await Hotel.AnyHotel(filter,forSearch);
            return Ok(result);
        }
    }
}
