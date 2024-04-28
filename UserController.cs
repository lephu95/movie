using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using movie.Playloads.DataRequest;
using movie.Services.Implement;
using movie.Services.Interface;
using System.Security.Claims;

namespace movie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserservice _userservice;
        public UserController(IUserservice userservice)
        {
            _userservice = userservice;
        }
        [HttpPost("register")]
        public IActionResult Register([FromForm] Request_Registers request)
        {
            return Ok(_userservice.Register(request));
        }
        [HttpPost("login")]
        public IActionResult login([FromForm] Request_Login request)
        {
            return Ok(_userservice.login(request));
        }
        [HttpGet("getall")]
        [Authorize(Roles = "Admin")]
        public IActionResult Getall()
        {
            return Ok(_userservice.Getall());
        }
        [HttpPost("xacthuc")]
        public IActionResult SendConfirmEmail([FromForm]Request_Registers request)
        {
            return Ok(_userservice.SendConfirmEmail(request));
        }
        [HttpPost("doimk")]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]

       public async Task<IActionResult> changepass ([FromForm]Request_ForgotPassword request)
        {
            var userclaim = HttpContext.User.FindFirst("id");
            if (userclaim == null)
            {
                return BadRequest("mk ko ton tai");
            }
            int id;
            string str = userclaim.Value;
            bool pasrseresult=int.TryParse(str, out id);
            if (pasrseresult)
            {
                return BadRequest("ko ton ta user id");
            }
            return Ok( _userservice.ChangePass(id,request));
        }
        
        [HttpPost("xoa")]
        [Authorize(Roles = "admin")]
        public IActionResult xoa([FromBody] int id)
        {
            return Ok(_userservice.ADMrmv(id));
        }
        [HttpPost("capquyenadmin")]
        public async Task<IActionResult> capquyenadmin(string username)
        {
            bool success = await _userservice.AssignAdminRoleAsync(username);
            if (success)
            {
                return Ok($"cap quyen thanh cong {username}");
            }
            else
            {
                return NotFound($"cap quyen khong thanh cong {username}");
            }
        }
        [HttpPost("lock-account")]
        public IActionResult LockAccount([FromBody] int userid)
        {
            try
            {
                _userservice.LockUserAccount(userid);
                return Ok(new { message = "khóa thành công" });
            }
            catch (Exception ex)
            {
                
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost("unlock-account")]
        public IActionResult UnlockAccount([FromBody] int userid)
        {
            try
            {
                _userservice.UnlockUserAccount(userid);
                return Ok(new { message = "User account unlocked successfully" });
            }
            catch (Exception ex)
            {
                
                return BadRequest(new { message = ex.Message });
            }
        }
    }
    
}
