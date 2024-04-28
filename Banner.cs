using System.ComponentModel.DataAnnotations;

namespace movie.Entities
{
    public class Banner
    {
        [Key]
        public int Id { get; set; }
        public string ImageUri {  get; set; }
        public string Title { get; set; }
    }
}
