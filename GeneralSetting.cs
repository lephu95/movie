using System.ComponentModel.DataAnnotations;

namespace movie.Entities
{
    public class GeneralSetting
    {
        [Key]
        public int Id { get; set; }
        public DateTime BreakTime {  get; set; }
        public int BusinessHome {  get; set; }
        public DateTime CloseTime { get; set; }
        public int FixedTicketPrice {  get; set; }
        public int PrecentDay {  get; set; }
        public int PrecentWeeken {  get; set; }
        public DateTime TimeBeginToChange { get; set; }
    }
}
