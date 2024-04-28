using System.ComponentModel.DataAnnotations;

namespace movie.Entities
{
    public class Food
    {
        [Key]
        public int Id { get; set; }
        public double Price {  get; set; }
        public string Description {  get; set; }
        public string image {  get; set; }
        public string NameOfFood {  get; set; }
        public bool IsActive {  get; set; }
        public IEnumerable<BillFood> BillFood { get; set;}
    }
}
