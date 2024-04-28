using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using movie.Entities;
using movie.Playloads.DataRequest;
using movie.Services.Interface;

namespace movie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinimaController : ControllerBase
    {
        private readonly ICinimaService _cimaService;
        public CinimaController(ICinimaService cimaService)
        {
            _cimaService = cimaService;
        }
        [HttpPost("them")]
        [Authorize(Roles = "admin")]
        public IActionResult them([FromBody] Request_MoreCinima request)
        {
            return Ok(_cimaService.morecinima(request));
        }
        [HttpPost("sua")]
        [Authorize(Roles = "admin")]
        public IActionResult sua([FromBody] Request_FixCinima request)
        {
            return Ok(_cimaService.fixCinima(request));
        }
        [HttpPost("xoa")]
        [Authorize(Roles = "admin")]
        public IActionResult xoa([FromForm] int id)
        {
            return Ok(_cimaService.RmvCinima(id));
        }
        [HttpPost("doanhthu")]
        [Authorize(Roles = "admin")]
        public IActionResult doanhthu(Request_RevenueCinema request)
        {
            var money = _cimaService.revenue(request);
            return Ok(money);
        }
    }


}
