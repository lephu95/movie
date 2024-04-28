using System.ComponentModel.DataAnnotations;

namespace movie.Entities
{
    public class RankCustomer
    {
        [Key]
        public int Id { get; set; }
        public int Point {  get; set; }
        public string Description {  get; set; }
        public string Name { get; set; }
        public bool IsActive {  get; set; }
        public IEnumerable<Promotion> Promotion { get; set; }
        public IEnumerable<User> Users { get; set; }    
    }
}
