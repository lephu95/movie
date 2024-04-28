using System.ComponentModel.DataAnnotations;

namespace movie.Entities
{
    public class BillStatus
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Bill> Bills { get; set; }
    }
}
