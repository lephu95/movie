using System.ComponentModel.DataAnnotations;

namespace movie.Entities
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpreidTime {  get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
