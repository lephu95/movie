using Aspose.Email.Clients.Exchange.WebService.Schema_2016;
using Azure;
using Microsoft.EntityFrameworkCore;
using movie.Entities;
using movie.Playloads.Converter;
using movie.Playloads.DataRequest;
using movie.Playloads.DataResponses;
using movie.Playloads.Responses;
using movie.Services.Interface;
using System.Net.Mail;

namespace movie.Services.Implement
{
    public class BillService:BaseService,IBillService
    {
        private readonly BillConverter converter;
        private readonly IConfiguration _configuration;
        private readonly DataRespnsesBill dataRespnsesBill;
        private readonly ResponsesObject<DataRespnsesBill> _responses;
        public BillService(IConfiguration configuration)
        {
            _configuration = configuration;
            converter = new BillConverter();
            dataRespnsesBill = new DataRespnsesBill();
            _responses = new ResponsesObject<DataRespnsesBill>();
        }

        public ResponsesObject<DataRespnsesBill> CreateBill(Request_Bill request)
        { 
            var movie = new Movie();
            var bill = new Bill();
            var food = new Food();
            var cinema = new Cinema();
            var billTicket = new BillTicket();
            var ticket= new Ticket();
            var billfood= new BillFood();
            var promotion= new Promotion();
            var room= new Room();
            var schedules = new Schedule();

             movie=contex.Movies.FirstOrDefault(x=>x.Id==request.Moviesid);
            bill = contex.Bills.FirstOrDefault(x => x.Id == request.billid);
            food = contex.Foods.FirstOrDefault(x => x.Id == request.foodid);
            cinema = contex.Cinemas.FirstOrDefault(x => x.Id == request.Cinemaid);
            billTicket = contex.BillTickets.FirstOrDefault(x => x.Id == request.billticketid);
            ticket = contex.Tickets.FirstOrDefault(x => x.Id == request.ticketid);
            billfood = contex.BillFoods.FirstOrDefault(x => x.Id == request.billfoodid);
            promotion = contex.Promotions.FirstOrDefault(x => x.Id == request.promotionid);
            room = contex.Rooms.FirstOrDefault(x => x.Id == request.Roomid);
            schedules = contex.Schedules.FirstOrDefault(x => x.Id == request.Schedulesid);
            if (movie==null || bill==null ||food==null||cinema==null||billTicket==null||ticket==null||billfood==null||promotion==null
                ||room==null||schedules==null)
            {
                return _responses.ResponsesErr(StatusCodes.Status400BadRequest, "khong ton tai ", null);
            }
            else
            {
                if(contex.Movies.Any(x=>x.Id== request.Moviesid)&& contex.Bills.Any(x => x.Id == request.billid)&&
                    contex.Foods.Any(x => x.Id == request.foodid)&& contex.Cinemas.Any(x => x.Id == request.Cinemaid)&&
                    contex.BillTickets.Any(x => x.Id == request.billticketid)&& contex.Tickets.Any(x => x.Id == request.ticketid)
                    && contex.BillFoods.Any(x => x.Id == request.billfoodid)&& contex.Promotions.Any(x => x.Id == request.promotionid)&&
                    contex.Rooms.Any(x => x.Id == request.Roomid)&& contex.Schedules.Any(x => x.Id == request.Schedulesid))
                {
                    bill.TatalMoney = (ticket.PriceTicket * billTicket.Quantity + food.Price * billfood.Quantity) * promotion.Percent / 100;
                    contex.Bills.Add(bill);
                    contex.SaveChanges();
                    DataRespnsesBill resault = converter.EntitytoDTO(bill.TatalMoney,movie.Name,food.NameOfFood,cinema.NameOfCinema,billTicket.Quantity,room.Name,schedules.StartAt,schedules.EndAt);
                    return _responses.ResponsesSucsess("bill tao thanh cong",resault);
                }
                else
                {
                    return _responses.ResponsesErr(StatusCodes.Status400BadRequest, "ko tim thay dl", null);
                }
            }

        }

        

        public DataRespnsesBill PaymentExecute(IQueryCollection collections)
        {
            throw new NotImplementedException();
        }

         

        public  bool SendConfirmEmail(Request_Bill request)
        {
            try
            {
                var fromadress = new MailAddress("lp@gmail.com", "hp");
                var toadress = new MailAddress(request.Email);
                const string fromPass = "1";
                const string fromsubject = "tao bill thanh cong";
                string body = $"thanh cong";

                var stmp = new SmtpClient()
                {
                    Host = "stmp.email.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(fromadress.Address, fromPass)
                };

                using (var message = new MailMessage(fromadress, toadress)
                {
                    Subject = fromsubject,
                    Body = body
                })
                {
                    stmp.Send(message);
                }

                return true; // Gửi email thành công
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi gửi email: " + ex.Message);
                return false; // Gửi email thất bại
            }
        }
        public List<DataRespnsesBill> GetTopSellingFoodsLast7Days()
        {
            var sevenDaysAgo = DateTime.Now.Date.AddDays(-7);

            var topSellingFoods = (from f in contex.Foods
                                   join bf in contex.BillFoods on f.Id equals bf.FoodId
                                   join b in contex.Bills on bf.BillId equals b.Id
                                   where b.UpdateTime >= sevenDaysAgo
                                   group bf by new { f.Id, f.NameOfFood } into g
                                   select new DataRespnsesBill
                                   {
                                      
                                       foodname = g.Key.NameOfFood,
                                       quantityfood = g.Sum(x => x.Quantity)
                                   })
                                   .OrderByDescending(x => x.quantityfood)
                                   .ToList();

            return topSellingFoods;
        }

        public string CreatePaymentUrl(DataRespnsesBill model, HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
