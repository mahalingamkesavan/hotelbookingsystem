using HotelBookingSystem.BAL.Absractes;
using HotelBookingSystem.BAL.Utils;
using HotelBookingSystem.Models.Constants;
using HotelBookingSystem.Utils.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelBookingSystem_2.Controllers.Hotel
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json", "application/Blob")]
    [Authorize(Roles = Roles.Admin)]
    [ServiceFilter(typeof(LogActionFilter))]
    public class HotelPicturesController : BaseController
    {
        //public IHotelPictureHander PictureHander { get; }

        //public HotelPicturesController(IHotelPictureHander pictureHander)
        //{
        //    PictureHander = pictureHander;
        //}

        //// GET: api/<HotelPicturesController>
        //[HttpGet("{id:int}")]
        //[AllowAnonymous]
        //public async Task<ActionResult<IEnumerable<HotelPicturesEntity>>> GetByHotelId(int id)
        //{
        //    var res = await PictureHander.GetById(id);
        //    return Ok(res);
        //}

        //// GET api/<HotelPicturesController>/5
        //[HttpGet("thumnail/{id:int}")]
        //[AllowAnonymous]
        //public async Task<ActionResult<HotelPicturesEntity>> GetThumnailByHotelId(int id)
        //{
        //    var res = await PictureHander.GetThumnailById(id);
        //    if (res != null)
        //    //    return File(res.Picture, "image/png");
        //    //else
        //    //    return NoContent();
        //    return Ok(res);
        //    else
        //        return NoContent();
        //}

        //// POST api/<HotelPicturesController>
        //[HttpPost("{id:int}")]
        //public async Task<ActionResult> AddPictureHotelId(int id, params IFormFile[] file)
        //{
        //    var res = await PictureHander.AddPictureByHotelId(id, file);
        //    return Ok(res);
        //}

        //// PUT api/<HotelPicturesController>/5
        //[HttpPut("{id}")]
        //public async Task<ActionResult> UpdateByHotelId(int id, params IFormFile[] file)
        //{
        //    var res = await PictureHander.UpdateByHotelId(id, file);
        //    return Ok(res);
        //}

        //// DELETE api/<HotelPicturesController>/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteByHotel(int id)
        //{
        //    var res = await PictureHander.RemovePictureBYHotelId(id);
        //    return Ok(res); 
        //}
        //[HttpPost("sample")]
        //[AllowAnonymous]
        //public async Task<ActionResult> ConvertToPic(byte[] bytes)
        //{
        //    return await Task.FromResult(File(bytes, "image/png"));
        //    //return Ok(bytes);
        //}
    }
}
