using System.ComponentModel.DataAnnotations;

namespace movie.Entities
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public double Price {  get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string Code {  get; set; }
        public int MovieId {  get; set; }
        public string Name {  get; set; }
        public int Roomid {  get; set; }
        public bool IsActive { get; set; }
        public Movie Movie { get; set; }
        public Room Room { get; set; }
        public virtual IEnumerable<Ticket> Ticket { get; set; }

    }
}
