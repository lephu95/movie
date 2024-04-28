using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace movie.Entities
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }
        public double TatalMoney {  get; set; }
        public string TradingCode {  get; set; }
        public DateTime CreateTime {  get; set; }
        public int?CustomerId {  get; set; }
        public string Name {  get; set; }
        public DateTime UpdateTime { get; set; }
        public int PromotionId {  get; set; }
        public int BillStatusId {  get; set; }
        public bool IsActive {  get; set; }
        public IEnumerable<BillFood>billFoods { get; set; }
        public BillStatus BillStatus { get; set; }
        [ForeignKey("CustomerId")]
        public User User { get; set; }
        public Promotion Promotion { get; set; }
        public IEnumerable<BillTicket> Ticket { get; set; }

    }
}
