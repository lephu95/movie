using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using movie.Playloads.DataRequest;
using movie.Services.Implement;

namespace movie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly BillService _billService;
        public BillController(BillService billService)
        {
            _billService = billService;
        }
        [HttpPost("creatbill")]
        public IActionResult createbill([FromForm] Request_Bill request)
        {
            return Ok(_billService.CreateBill(request));
        }
        [HttpPost("send")]
        public IActionResult send([FromForm] Request_Bill request)
        {
            return Ok(_billService.SendConfirmEmail(request));
        }
        [HttpPost("top-selling-foods-last-7-days")]
        public IActionResult GetTopSellingFoodsLast7Days()
        {
            var topSellingFoods = _billService.GetTopSellingFoodsLast7Days();
            return Ok(topSellingFoods);
        }
    }
}
