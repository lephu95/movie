using movie.Entities;
using movie.Playloads.DataResponses;

namespace movie.Playloads.Converter
{
    public class MovieConverter
    {
        private readonly AppDbcontex contex;
        public MovieConverter()
        {
            contex = new AppDbcontex();
        }
        public DataResponsesMovies EntitytoDTO(Movie movie)
        {
            return new DataResponsesMovies
            {
                MoviDuration=movie.MoviDuration,
                EndTime=movie.EndTime,
                PremiereDate=movie.PremiereDate,
                Descripition=movie.Descripition,
                director=movie.director,
                image=movie.image,
                HeroImage=movie.HeroImage,
                Trailer=movie.Trailer,
                Language=movie.Language,
                MovieTypeName=contex.MoviesTypes.FirstOrDefault(x=>x.Id==movie.MovieTypeId).MoVieType,
                Name=movie.Name,
                RateName=contex.Rates.FirstOrDefault(x=>x.Id==movie.RateId).Descripiton
            };
        }
        public DataResponsesMovies EntitytoDTO(Schedule schedule)
        {
            return new DataResponsesMovies
            {
                Price = schedule.Price,
                EndAt = schedule.EndAt,
                StartAt = schedule.StartAt,
                NameSheduces = schedule.Name,
                CodeSheduces = schedule.Code,

            };
        }
        public DataresponsesPresentlyMovies EntitytoDTO(Movie movie, Cinema cinema,Room room , SeatStatus seatStatus)
        {
            return new DataresponsesPresentlyMovies
            {
                moviesname = movie.Name,
                cinimaname=cinema.NameOfCinema,
                roomname = room.Name,
                statusseat=seatStatus.NameStatus,

            };
        }
    }
}
