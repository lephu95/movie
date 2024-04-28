using System.ComponentModel.DataAnnotations;

namespace movie.Entities
{
    public class Rate
    {
        [Key]
        public int Id { get; set; }
        public string Descripiton {  get; set; }
        public string code {  get; set; }    
        public virtual IEnumerable<Movie> Movies { get; set; }
    }
}
