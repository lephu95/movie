
using Azure;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using movie.Entities;
using movie.Playloads.Converter;
using movie.Playloads.DataRequest;
using movie.Playloads.DataResponses;
using movie.Playloads.Responses;
using movie.Services.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace movie.Services.Implement
{
    public class CinimaService : BaseService, ICinimaService
    {
        private readonly CinimaConverter converter;
        private readonly IConfiguration _configuration;
        private readonly DataresponsesCinima dataresponsesCinima;
        private readonly ResponsesObject<DataresponsesCinima> responses;
        public CinimaService(IConfiguration configuration)
        {
            _configuration = configuration;
            converter = new CinimaConverter();
            dataresponsesCinima = new DataresponsesCinima();
            responses=new ResponsesObject<DataresponsesCinima>();   
        }

        public ResponsesObject<DataresponsesCinima> fixCinima(Request_FixCinima request)
        {
            var fix=contex.Cinemas.FirstOrDefault(x=>x.NameOfCinema==request.NameOfCinema);
            if(contex.Cinemas==null)
            {
                return responses.ResponsesErr(StatusCodes.Status400BadRequest, "ko ton tai ", null);
            }
            else
            {
                if (contex.Cinemas.Any(x => x.NameOfCinema == request.NameOfCinema))
                {
                    contex.Cinemas.Remove(fix);
                    var cinima = new Cinema();
                    cinima.Address = request.Address;
                    cinima.NameOfCinema = request.NameOfCinema;
                    cinima.Code = request.Code;
                    cinima.Description = request.Description;
                    cinima.IsActive = true;
                    contex.Cinemas.Add(cinima);
                    contex.SaveChanges();
                    var fixroom=contex.Rooms.FirstOrDefault(x=>x.CinemaId==fix.Id);
                    if (fixroom != null)
                    {
                        return responses.ResponsesErr(StatusCodes.Status400BadRequest, " room ko ton tai ", null);
                    }
                    else
                    {
                        if (contex.Rooms.Any(x => x.CinemaId == fix.Id))
                        {
                            contex.Rooms.Remove(fixroom);
                            var room=new Room();
                            room.Capacity=request.Capacity;
                            room.Code = request.CodeRoom;
                            room.Description=request.DescriptionRoom;
                            room.IsActive = true;
                            room.Name=request.Name;
                            room.Type=request.Type;
                            contex.Rooms.Add(room);
                            contex.SaveChanges();
                            DataresponsesCinima resultroom = converter.EntitytoDTO(room);
                            return responses.ResponsesSucsess("sua room thanh cong", resultroom);
                        }
                    }
                    DataresponsesCinima result = converter.EntitytoDTO(cinima);
                    return responses.ResponsesSucsess("sua thanh cong", result);
                }
                else
                    return responses.ResponsesErr(StatusCodes.Status400BadRequest, " room ko ton tai ", null);
            }
        }

        public ResponsesObject<DataresponsesCinima> morecinima(Request_MoreCinima request)
        {
            if (string.IsNullOrWhiteSpace(request.Address) ||
                string.IsNullOrWhiteSpace(request.NameOfCinema) ||
                string.IsNullOrWhiteSpace(request.Code) ||
                string.IsNullOrWhiteSpace(request.Description))
            {
                return responses.ResponsesErr(StatusCodes.Status400BadRequest, "vui long dien day du thong tin", null);
            }
            var cinima=new Cinema();
            cinima.Address = request.Address;
            cinima.NameOfCinema = request.NameOfCinema;
            cinima.Code = request.Code;
            cinima.Description = request.Description;
            cinima.IsActive = true;
            contex.Cinemas.Add(cinima);
            contex.SaveChanges();

            DataresponsesCinima result= converter.EntitytoDTO(cinima);
            return responses.ResponsesSucsess("them thanh cong", result);
        }

       

        public ResponsesObject<DataresponsesCinima> RmvCinima(int Id)
        {
            var remove = contex.Cinemas.FirstOrDefault(x => x.Id == Id);
            if (contex.Cinemas == null)
            {
                return responses.ResponsesErr(StatusCodes.Status400BadRequest, "ko ton tai ", null);
            }
            else
            {
                if(remove == null)
                {
                    return responses.ResponsesErr(StatusCodes.Status400BadRequest, "ko ton tai ", null);
                }
                else
                {
                    contex.Cinemas.Remove(remove);
                    contex.SaveChanges();
                    foreach (var item in contex.Rooms)
                    {
                        if (item.CinemaId == Id)
                        {
                            contex.Rooms.Remove(item);
                            return responses.ResponsesSucsess("xoa thanh cong", null);
                        }
                        else
                        {
                            return responses.ResponsesErr(StatusCodes.Status400BadRequest, "room ko ton tai ", null);
                        }
                    }
                    return responses.ResponsesSucsess("xoa thanh cong", null);  
                }
                
                
            }
        }

       public List<DataresponsesCinima> revenue(Request_RevenueCinema request)
        {
            var money=(from c in contex.Cinemas
                       join r in contex.Rooms on c.Id equals r.CinemaId
                       join s in contex.Schedules on r.Id equals s.Roomid
                       join t in contex.Tickets on s.Id equals t.ScheduleId
                       join bt in contex.BillTickets on t.Id equals bt.TicketId
                       join b in contex.Bills on bt.BillId equals b.Id
                       where b.CreateTime >= request.fromdate && b.CreateTime <= request.todate
                       group b by new { c.Id, c.NameOfCinema } into g
                       select new DataresponsesCinima
                       {
                           NameOfCinema=g.Key.NameOfCinema,
                           revenue=g.Sum(x=>x.TatalMoney),
                       }).ToList();
            return money;
        }
    }
}
