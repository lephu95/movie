using System.ComponentModel.DataAnnotations;

namespace movie.Entities
{
    public class BillTicket
    {
        [Key]
        public int Id { get; set; }
        public int Quantity {  get; set; }
        public int BillId {  get; set; }
        public int TicketId {  get; set; }
        public Ticket Ticket { get; set; }
        public Bill Bill { get; set; }
    }
}
