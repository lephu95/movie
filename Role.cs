using System.ComponentModel.DataAnnotations;

namespace movie.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Code {  get; set; }
        public string RoleName {  get; set; }
        public User User { get; set; }
    }
}
