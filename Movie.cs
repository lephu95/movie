using System.ComponentModel.DataAnnotations;

namespace movie.Entities
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public int MoviDuration {  get; set; }
        public DateTime EndTime {  get; set; }
        public DateTime PremiereDate {  get; set; }
        public string Descripition {  get; set; }
        public string director { get; set; }
        public string image {  get; set; }
        public string HeroImage {  get; set; }
        public string Language {  get; set; }
        public int MovieTypeId {  get; set; }
        public string Name {  get; set; }
        public int RateId {  get; set; }
        public string Trailer {  get; set; }
        public bool IsActive {  get; set; }
        public Rate? Rate { get; set; }
        public MovieType MovieType { get; set; }
        public virtual IEnumerable<Schedule> Schedules { get; set; }
    }
}
