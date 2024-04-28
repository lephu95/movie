using System.ComponentModel.DataAnnotations;

namespace movie.Entities
{
    public class MovieType
    {
        [Key]
        public int Id { get; set; }
        public string MoVieType {  get; set; }
        public bool IsActive {  get; set; }
        public virtual IEnumerable<Movie> Movies { get; set;}
    }
}
