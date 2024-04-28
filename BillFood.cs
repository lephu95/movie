using System.ComponentModel.DataAnnotations;

namespace movie.Entities
{
    public class BillFood
    {
        [Key]
        public int Id { get; set; }
        public int Quantity {  get; set; }
        public int BillId { get; set; }
        public int FoodId {  get; set; }
        public Food Food { get; set; }
        public Bill Bill { get; set; }
    }
}
