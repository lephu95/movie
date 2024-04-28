using System.ComponentModel.DataAnnotations;

namespace movie.Entities
{
    public class SeatStatus
    {
        [Key]
        public int Id { get; set; }
        public string Code {  get; set; }
        public string NameStatus {  get; set; }
        public IEnumerable<Seat> Seats { get; set; }
    }
}
