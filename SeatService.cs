using Azure;
using Azure.Core;
using movie.Entities;
using movie.Playloads.Converter;
using movie.Playloads.DataRequest;
using movie.Playloads.DataResponses;
using movie.Playloads.Responses;
using movie.Services.Interface;
using System;

namespace movie.Services.Implement
{
    public class SeatService : BaseService, ISeatservice
    {
        private readonly SeatConverter converter;
        private readonly IConfiguration _configuration;
        private readonly DataResponsesSeat dataResponsesSeat;
        private readonly ResponsesObject<DataResponsesSeat> responses;
        public SeatService(IConfiguration configuration)
        {
            _configuration = configuration;
            converter = new SeatConverter();
            dataResponsesSeat = new DataResponsesSeat();
            responses = new ResponsesObject<DataResponsesSeat>();
        }

        public ResponsesObject<DataResponsesSeat> FixSeat(Request_FixSeat request)
        {
            var fix = contex.Seats.FirstOrDefault(x => x.Number == request.number);
            if (contex.Seats == null)
            {
                return responses.ResponsesErr(StatusCodes.Status400BadRequest, "ko ton tai ", null);
            }
            else
            {
                if (contex.Seats.Any(x => x.Number == request.number))
                {
                    contex.Seats.Remove(fix);
                    var seat = new Seat();
                    var seatStatus = contex.SeatsStatus.FirstOrDefault(x => x.NameStatus == request.SeatStatusName);
                    seat.seatStatusId = seatStatus.Id;
                    seat.Number = request.number;
                    seat.Line = request.line;
                    var seattype = contex.SeatsTypes.FirstOrDefault(x => x.NameType == request.NameType);
                    seat.SeatTypeId = seattype.Id;
                    var room = contex.Rooms.FirstOrDefault(x => x.Name == request.RoomName);
                    seat.Roomid = room.Id;
                    seat.IsActive = true;
                    contex.Seats.Add(seat);
                    contex.SaveChanges();
                    var fixticket = contex.Tickets.FirstOrDefault(x => x.SeatId==fix.Id);
                    if (fixticket != null)
                    {
                        return responses.ResponsesErr(StatusCodes.Status400BadRequest, " room ko ton tai ", null);
                    }
                    else
                    {
                        if (contex.Tickets.Any(x => x.SeatId == fix.Id))
                        {
                            contex.Tickets.Remove(fixticket);
                            var ticket = new Ticket();
                            ticket.Code = request.code;
                            ticket.PriceTicket = request.price;
                            contex.Rooms.Add(room);
                            contex.SaveChanges();
                            DataResponsesSeat resultroom = converter.EntitytoDTO(ticket);
                            return responses.ResponsesSucsess("sua room thanh cong", resultroom);
                        }
                    }
                    DataResponsesSeat result = converter.EntitytoDTO(seat);
                    return responses.ResponsesSucsess("sua thanh cong", result);
                }
                else
                    return responses.ResponsesErr(StatusCodes.Status400BadRequest, " room ko ton tai ", null);
            }
        }

        public ResponsesObject<DataResponsesSeat> MoreSeat(Request_MoreSeat request)
        {
            if (string.IsNullOrWhiteSpace(request.SeatStatusName) ||
               string.IsNullOrWhiteSpace(request.RoomName) ||
               string.IsNullOrWhiteSpace(request.NameType) ||
               string.IsNullOrWhiteSpace(request.number.ToString()))
            {
                return responses.ResponsesErr(StatusCodes.Status400BadRequest, "vui long dien day du thong tin", null);
            }
            var seat = new Seat();
            var seatStatus=contex.SeatsStatus.FirstOrDefault(x => x.NameStatus==request.SeatStatusName);
            seat.seatStatusId = seatStatus.Id;
            seat.Number=request.number;
            seat.Line=request.line;
            var seattype=contex.SeatsTypes.FirstOrDefault(x => x.NameType==request.NameType);
            seat.SeatTypeId = seattype.Id;
            var room=contex.Rooms.FirstOrDefault(x => x.Name==request.RoomName);
            seat.Roomid = room.Id;
            seat.IsActive = true;
            contex.Seats.Add(seat);
            contex.SaveChanges();

            DataResponsesSeat result = converter.EntitytoDTO(seat);
            return responses.ResponsesSucsess("them thanh cong", result);
        }

       

        public ResponsesObject<DataResponsesSeat> RmvSeat(int Id)
        {
            var remove = contex.Seats.FirstOrDefault(x => x.Id == Id);
            if (contex.Seats == null)
            {
                return responses.ResponsesErr(StatusCodes.Status400BadRequest, "ko ton tai ", null);
            }
            else
            {
                if (remove == null)
                {
                    return responses.ResponsesErr(StatusCodes.Status400BadRequest, "ko ton tai ", null);
                }
                else
                {
                    contex.Seats.Remove(remove);
                    contex.SaveChanges();
                    foreach (var item in contex.Tickets)
                    {
                        if (item.SeatId == Id)
                        {
                            contex.Tickets.Remove(item);
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
    }
}
