using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using movie.Playloads.DataRequest;
using movie.Services.Interface;

namespace movie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private readonly ISeatservice _seatservice;
        public SeatsController(ISeatservice seatservice)
        {
           _seatservice = seatservice;
        }
        [HttpPost("them")]
        [Authorize(Roles = "admin")]
        public IActionResult them([FromBody] Request_MoreSeat request)
        {
            return Ok(_seatservice.MoreSeat(request));
        }
        [HttpPost("sua")]
        [Authorize(Roles = "admin")]
        public IActionResult sua([FromBody] Request_FixSeat request)
        {
            return Ok(_seatservice.FixSeat(request));
        }
        [HttpPost("xoa")]
        [Authorize(Roles = "admin")]
        public IActionResult xoa([FromForm] int id)
        {
            return Ok(_seatservice.RmvSeat(id));
        }
    }
}
