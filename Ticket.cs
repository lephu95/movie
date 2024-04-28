using System.ComponentModel.DataAnnotations;

namespace movie.Entities
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public string Code {  get; set; }
        public int ScheduleId {  get; set; }
        public int? SeatId {  get; set; }
        public Double PriceTicket {  get; set; }
        public bool? IsActive { get; set; }
        public Seat Seat { get; set; }
        public IEnumerable<BillTicket> Bill { get; set; }
        public Schedule Schedule { get; set; }
    }
}
