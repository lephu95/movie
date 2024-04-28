using Azure;

using movie.Entities;
using movie.Handle.Email;
using movie.Playloads.Converter;
using movie.Playloads.DataRequest;
using movie.Playloads.DataResponses;
using movie.Playloads.Responses;
using movie.Services.Interface;
using movie.Playloads.Converter;

namespace movie.Services.Implement
{
    public class Roomservice : BaseService, IRoomService
    {
        private readonly RoomConverter converter;
        private readonly IConfiguration _configuration;
        private readonly DataResponsesRoom dataResponsesRoom;
        private readonly ResponsesObject<DataResponsesRoom> responses;
        public Roomservice(IConfiguration configuration)
        {
            _configuration = configuration;
            converter = new RoomConverter();
            dataResponsesRoom = new DataResponsesRoom();
            responses = new ResponsesObject<DataResponsesRoom>();
        }


        public ResponsesObject<DataResponsesRoom> FixRoom(Request_FixRoom request)
        {
            var fix = contex.Rooms.FirstOrDefault(x => x.Name == request.Name);
            if (contex.Rooms == null)
            {
                return responses.ResponsesErr(StatusCodes.Status400BadRequest, "ko ton tai ", null);
            }
            else
            {
                if (contex.Rooms.Any(x => x.Name == request.Name))
                {
                    contex.Rooms.Remove(fix);
                    var room = new Room();
                    room.Name = request.Name;
                    room.Capacity = request.Capacity;
                    room.Type = request.Type;
                    room.Description = request.Description;
                    room.Code = request.Code;
                    var cinimaid = contex.Cinemas.FirstOrDefault(x => x.NameOfCinema == request.Cinemaname);
                    room.CinemaId = cinimaid.Id;
                    room.IsActive = true;
                    contex.Rooms.Add(room);
                    contex.SaveChanges();
                    var fixshedules = contex.Schedules.FirstOrDefault(x => x.Roomid == fix.Id);
                    if (fixshedules != null)
                    {
                        return responses.ResponsesErr(StatusCodes.Status400BadRequest, " room ko ton tai ", null);
                    }
                    else
                    {
                        if (contex.Schedules.Any(x => x.Roomid == fix.Id))
                        {
                            contex.Schedules.Remove(fixshedules);
                            var shedules = new Schedule();
                            shedules.StartAt = request.StartAt;
                            shedules.Price = request.Price;
                            shedules.EndAt=request.EndAt;
                            shedules.Code = request.CodeSheduces;
                            shedules.Name=request.NameSheduces;
                            shedules.IsActive = true;
                            contex.Schedules.Add(shedules);
                            contex.SaveChanges();
                            DataResponsesRoom resulschedules = converter.EntitytoDTO(shedules);
                            return responses.ResponsesSucsess("sua shedules thanh cong", resulschedules);
                        }
                    }
                    var fixseat = contex.Seats.FirstOrDefault(x => x.Roomid == fix.Id);
                    if (fixseat != null)
                    {
                        return responses.ResponsesErr(StatusCodes.Status400BadRequest, " seat ko ton tai ", null);
                    }
                    else
                    {
                        if (contex.Seats.Any(x => x.Roomid == fix.Id))
                        {
                            contex.Seats.Remove(fixseat);
                            var seat = new Seat();
                            seat.seatStatusId=request.seatStatusId;
                            seat.Line=request.Line;
                            seat.Number=request.Number;
                            contex.Seats.Add(seat);
                            contex.SaveChanges();
                            DataResponsesRoom resulseat = converter.EntitytoDTO(seat);
                            return responses.ResponsesSucsess("sua seat thanh cong", resulseat);
                        }
                    }
                    DataResponsesRoom result = converter.EntitytoDTO(room);
                    return responses.ResponsesSucsess("sua thanh cong", result);
                }
                else
                    return responses.ResponsesErr(StatusCodes.Status400BadRequest, " room ko ton tai ", null);
            }
        }

        public ResponsesObject<DataResponsesRoom> MoreRoom(Request_MoreRoom request)
        {
            if (string.IsNullOrWhiteSpace(request.Description) ||
                string.IsNullOrWhiteSpace(request.Name) ||
                string.IsNullOrWhiteSpace(request.Capacity) ||
                string.IsNullOrWhiteSpace(request.Type) ||
                string.IsNullOrWhiteSpace(request.Cinemaname) ||
                string.IsNullOrWhiteSpace(request.Code)
                )
            {
                return responses.ResponsesErr(StatusCodes.Status400BadRequest, "vui long dien day du thong tin", null);
            }
            var room = new Room();
            room.Name = request.Name;
            room.Capacity = request.Capacity;
            room.Type = request.Type;
            room.Description = request.Description;
            room.Code = request.Code;
            var cinimaid = contex.Cinemas.FirstOrDefault(x => x.NameOfCinema == request.Cinemaname);
            room.CinemaId = cinimaid.Id;
            room.IsActive = true;
            contex.Rooms.Add(room);
            contex.SaveChanges();

            DataResponsesRoom result = converter.EntitytoDTO(room);
            return responses.ResponsesSucsess("them thanh cong", result);
        }

        public ResponsesObject<DataResponsesRoom> Rmvroom(int Id)
        {
            var remove = contex.Rooms.FirstOrDefault(x => x.Id == Id);
            if (contex.Rooms == null)
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
                    contex.Rooms.Remove(remove);
                    contex.SaveChanges();
                    foreach (var item in contex.Seats)
                    {
                        if (item.Roomid == Id)
                        {
                            contex.Seats.Remove(item);
                            return responses.ResponsesSucsess("xoa thanh cong", null);
                        }
                        else
                        {
                            return responses.ResponsesErr(StatusCodes.Status400BadRequest, "room ko ton tai ", null);
                        }
                    }
                    foreach (var item in contex.Schedules)
                    {
                        if (item.Roomid == Id)
                        {
                            contex.Schedules.Remove(item);
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
