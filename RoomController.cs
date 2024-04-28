using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using movie.Playloads.DataRequest;
using movie.Services.Interface;

namespace movie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   

        public class RoomController : ControllerBase
        {
            private readonly IRoomService _roomService;
            public RoomController(IRoomService roomService)
            {
                _roomService = roomService;
            }
            [HttpPost("them")]
            [Authorize(Roles = "admin")]
            public IActionResult them([FromBody] Request_MoreRoom request)
            {
                return Ok(_roomService.MoreRoom(request));
            }
            [HttpPost("sua")]
            [Authorize(Roles = "admin")]
            public IActionResult sua([FromBody] Request_FixRoom request)
            {
                return Ok(_roomService.FixRoom(request));
            }
            [HttpPost("xoa")]
            [Authorize(Roles = "admin")]
            public IActionResult xoa([FromForm] int id)
            {
                return Ok(_roomService.Rmvroom(id));
            }
        }
    }

