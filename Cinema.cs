using System.ComponentModel.DataAnnotations;

namespace movie.Entities
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }
        public string Address {  get; set; }
        public string Description {  get; set; }
        public string Code {  get; set; }
        public string NameOfCinema {  get; set; }
        public bool IsActive {  get; set; }
        public IEnumerable<Room> Rooms { get; set; }
    }
}
