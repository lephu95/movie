using System.ComponentModel.DataAnnotations;

namespace movie.Entities
{
    public class ComfimEmail
    {
        [Key]
        public int Id { get; set; }
        public int UserId {  get; set; }
        public DateTime ExpiredTime {  get; set; }
        public string ConFirmCode {  get; set; }
        public bool IsConFirm {  get; set; }
        public User User { get; set; }
    }
}
