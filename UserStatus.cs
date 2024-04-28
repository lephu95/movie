using System.ComponentModel.DataAnnotations;

namespace movie.Entities
{
    public class UserStatus
    {
        [Key]
        public int Id { get; set; }
        public string Code {  get; set; }
        public string Name { get; set; }
        public User User { get; set; }
    }
}
