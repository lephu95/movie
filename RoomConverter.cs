using movie.Entities;
using movie.Playloads.DataResponses;

namespace movie.Playloads.Converter
{
    public class RoomConverter
    {
        private readonly AppDbcontex contex;
        public RoomConverter()
        {
            contex = new AppDbcontex();
        }
        public DataResponsesRoom EntitytoDTO(Room room)
        {
            return new DataResponsesRoom
            {
               Capacity = room.Capacity,
               Type = room.Type,
               Code = room.Code,
               Name = room.Name,
               Description = room.Description,
               CinemaName=contex.Cinemas.SingleOrDefault(x=>x.Id==room.CinemaId).NameOfCinema,
            };
        }
        public DataResponsesRoom EntitytoDTO(Schedule schedule)
        {
            return new DataResponsesRoom
            {
                Price = schedule.Price,
                EndAt = schedule.EndAt,
                StartAt = schedule.StartAt,
                NameSheduces = schedule.Name,
                CodeSheduces = schedule.Code,

            };
        }
        public DataResponsesRoom EntitytoDTO(Seat seat)
        {
            return new DataResponsesRoom
            {
                Number = seat.Number,
                seatStatusId=seat.seatStatusId,
                Line = seat.Line,

            };
        }
    }
}
