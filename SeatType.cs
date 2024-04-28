using System.ComponentModel.DataAnnotations;

namespace movie.Entities
{
    public class SeatType
    {
        [Key]
        public int Id { get; set; }
        public string NameType {  get; set; }
        public IEnumerable<SeatType> SeatTypes { get; set; }
    }
}
