using movie.Entities;
using movie.Playloads.DataResponses;

namespace movie.Playloads.Converter
{
    public class SeatConverter
    {
        private readonly AppDbcontex contex;
        public SeatConverter()
        {
            contex = new AppDbcontex();
        }
        public DataResponsesSeat EntitytoDTO(Seat seat)
        {
            return new DataResponsesSeat
            {
                number=seat.Number,
                line=seat.Line,
                SeatStatusName=contex.SeatsStatus.FirstOrDefault(x=>x.Id==seat.seatStatusId).NameStatus,
                RoomName=contex.Rooms.FirstOrDefault(x=>x.Id==seat.Roomid).Name,
                NameType=contex.SeatsTypes.FirstOrDefault(x=>x.Id==seat.SeatTypeId).NameType,

            };
        }
        public DataResponsesSeat EntitytoDTO(Ticket ticket)
        {
            return new DataResponsesSeat
            {
                code=ticket.Code,
                priceTicket=ticket.PriceTicket,

            };
        }
    }
}
