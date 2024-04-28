using System.ComponentModel.DataAnnotations;

namespace movie.Entities
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Capacity {  get; set; }
        public string Type {  get; set; }
        public string Description {  get; set; }
        public int CinemaId {  get; set; }
        public string Code {  get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Cinema Cinema { get; set; }
    }
}
