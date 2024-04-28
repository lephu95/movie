using System.ComponentModel.DataAnnotations;

namespace movie.Entities
{
    public class Seat
    {
        [Key]
        public int Id { get; set; }
        public int Number {  get; set; }
        public int seatStatusId {  get; set; }
        public string Line {  get; set; }
        public int Roomid {  get; set; }
        public bool IsActive {  get; set; }
        public int SeatTypeId {  get; set; }
        public IEnumerable<Ticket> Ticket { get; set; }
        public Schedule Schedule { get; set; }
        public SeatStatus Status { get; set; }
        public SeatType Type { get; set; }
    }
}
