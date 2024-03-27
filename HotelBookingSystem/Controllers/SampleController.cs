using HotelBookingSystem.DAL.Contexts;
using HotelBookingSystem.DAL.Queries.FileQ;
using HotelBookingSystem.ImageKit.Interfaces;
using HotelBookingSystem.Models.Constants;
using HotelBookingSystem.Utils.Filters;
using Imagekit.Models;
using Imagekit.Sdk;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Net.Http.Headers;
using System.Drawing;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelBookingSystem_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(LogActionFilter))]
    public class SampleController : ControllerBase
    {
        public HotelBookingContext Context { get; }
        public IConfiguration Configuration { get; }
        public IFileProvider Provider { get; }
        public ISender Mediator { get; }
        public IFileProcessing Imagekit { get; }

        public SampleController
            (
            HotelBookingContext context, 
            IConfiguration configuration, 
            IFileProvider provider,
            ISender Mediator,
            IFileProcessing imagekit
            )
        {
            Context = context;
            //Provider = provider;
            Configuration = configuration;
            Provider = provider;
            this.Mediator = Mediator;
            Imagekit = imagekit;
        }
        // GET: api/<SampleController>
        [HttpGet]
        public async Task<object> Get()
        {
            return await Context.Hotels.Select(x => new
            {
                x.Name,
                x.City,
                Count = Context.Hotels.Count(),
            }).ToListAsync();
        }
        [HttpGet("2")]
        public async Task<IActionResult> Get2(string id)
        {
            var res = await Imagekit.GetFileDetailsAsync(id);
            return Ok(res);
        }
        [HttpGet("3")]
        public async Task<IActionResult> Get2()
        {
            var data = FileEndPoint.HotelPictureNotFound;
            var res = await Imagekit.GetFileListAsync(new GetFileListRequest()
            {
                Path = FileEndPoint.HotelFolder
            }) ;
            return Ok(res);
        }
    }
}
