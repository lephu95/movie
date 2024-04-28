using movie.Entities;
using movie.Playloads.Converter;
using movie.Playloads.DataRequest;
using movie.Playloads.DataResponses;
using movie.Playloads.Responses;
using movie.Services.Interface;

namespace movie.Services.Implement
{
    public class MoviesService : BaseService, IMoviesService
    {


        public List<DataResponsesMovies> NameMovies(int limit)
        {
            try
            {
                var ArrangeMovies = contex.Movies
                    .OrderByDescending(m => m.Schedules.Sum(s => s.Ticket.Count()))
                    .Take(limit)
                    .ToList();
                var result = ArrangeMovies.Select(x => converter.EntitytoDTO(x)).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        private readonly MovieConverter converter;
        private readonly IConfiguration _configuration;
        private readonly DataResponsesMovies dataResponsesMovies;
        private readonly ResponsesObject<DataResponsesMovies> responses;
        public MoviesService(IConfiguration configuration)
        {
            _configuration = configuration;
            converter = new MovieConverter();
            dataResponsesMovies = new DataResponsesMovies();
            responses = new ResponsesObject<DataResponsesMovies>();
        }


        public ResponsesObject<DataResponsesMovies> fixmovie(Request_FixMovie request)
        {
            var fix = contex.Movies.FirstOrDefault(x => x.Name == request.Name);
            if (contex.Movies == null)
            {
                return responses.ResponsesErr(StatusCodes.Status400BadRequest, "ko ton tai ", null);
            }
            else
            {
                if (contex.Movies.Any(x => x.Name == request.Name))
                {
                    contex.Movies.Remove(fix);
                    var movi = new Movie();
                    movi.Name = request.Name;
                    movi.Descripition = request.Descripition;
                    movi.MoviDuration = request.MoviDuration;
                    movi.director = request.director;
                    movi.EndTime = request.EndTime;
                    movi.PremiereDate = request.PremiereDate;
                    movi.Trailer = request.Trailer;
                    movi.HeroImage = request.HeroImage;
                    movi.Language = request.Language;
                    var type = contex.MoviesTypes.FirstOrDefault(x => x.MoVieType == request.MovieTypeName);
                    movi.MovieTypeId = type.Id;
                    var rate = contex.Rates.FirstOrDefault(x => x.Descripiton == request.RateName);
                    movi.RateId = rate.Id;
                    movi.IsActive = true;
                    contex.Movies.Add(movi);
                    contex.SaveChanges();
                    var fixshedules = contex.Schedules.FirstOrDefault(x => x.Roomid == fix.Id);
                    if (fixshedules != null)
                    {
                        return responses.ResponsesErr(StatusCodes.Status400BadRequest, " room ko ton tai ", null);
                    }
                    else
                    {
                        if (contex.Schedules.Any(x => x.Roomid == fix.Id))
                        {
                            contex.Schedules.Remove(fixshedules);
                            var shedules = new Schedule();
                            shedules.StartAt = request.StartAt;
                            shedules.Price = request.Price;
                            shedules.EndAt = request.EndAt;
                            shedules.Code = request.CodeSheduces;
                            shedules.Name = request.NameSheduces;
                            shedules.IsActive = true;
                            contex.Schedules.Add(shedules);
                            contex.SaveChanges();
                            DataResponsesMovies resulschedules = converter.EntitytoDTO(shedules);
                            return responses.ResponsesSucsess("sua shedules thanh cong", resulschedules);
                        }
                    }

                    DataResponsesMovies result = converter.EntitytoDTO(movi);
                    return responses.ResponsesSucsess("sua thanh cong", result);
                }
                else
                    return responses.ResponsesErr(StatusCodes.Status400BadRequest, "  ko ton tai ", null);
            }
        }

        public ResponsesObject<DataResponsesMovies> moremovie(Request_MoreMovies request)
        {
            if (string.IsNullOrWhiteSpace(request.MoviDuration.ToString()) ||
                string.IsNullOrWhiteSpace(request.EndTime.ToString()) ||
                string.IsNullOrWhiteSpace(request.PremiereDate.ToString()) ||
                string.IsNullOrWhiteSpace(request.Descripition) ||
                string.IsNullOrWhiteSpace(request.director) ||
                string.IsNullOrWhiteSpace(request.image) ||
                string.IsNullOrWhiteSpace(request.HeroImage) ||
                string.IsNullOrWhiteSpace(request.Language) ||
                string.IsNullOrWhiteSpace(request.Name) ||
                string.IsNullOrWhiteSpace(request.Trailer)
                )
            {
                return responses.ResponsesErr(StatusCodes.Status400BadRequest, "vui long dien day du thong tin", null);
            }
            var movi = new Movie();
            movi.Name = request.Name;
            movi.Descripition = request.Descripition;
            movi.MoviDuration = request.MoviDuration;
            movi.director = request.director;
            movi.EndTime = request.EndTime;
            movi.PremiereDate = request.PremiereDate;
            movi.Trailer = request.Trailer;
            movi.HeroImage = request.HeroImage;
            movi.Language = request.Language;
            var type = contex.MoviesTypes.FirstOrDefault(x => x.MoVieType == request.MovieTypeName);
            movi.MovieTypeId = type.Id;
            var rate = contex.Rates.FirstOrDefault(x => x.Descripiton == request.RateName);
            movi.RateId = rate.Id;
            movi.IsActive = true;
            contex.Movies.Add(movi);
            contex.SaveChanges();

            DataResponsesMovies result = converter.EntitytoDTO(movi);
            return responses.ResponsesSucsess("them thanh cong", result);
        }

        public ResponsesObject<DataResponsesMovies> Rmvmovie(int Id)
        {
            var remove = contex.Movies.FirstOrDefault(x => x.Id == Id);
            if (contex.Movies == null)
            {
                return responses.ResponsesErr(StatusCodes.Status400BadRequest, "ko ton tai ", null);
            }
            else
            {
                if (remove == null)
                {
                    return responses.ResponsesErr(StatusCodes.Status400BadRequest, "ko ton tai ", null);
                }
                else
                {
                    contex.Movies.Remove(remove);
                    contex.SaveChanges();

                    foreach (var item in contex.Schedules)
                    {
                        if (item.Roomid == Id)
                        {
                            contex.Schedules.Remove(item);
                            return responses.ResponsesSucsess("xoa thanh cong", null);
                        }
                        else
                        {
                            return responses.ResponsesErr(StatusCodes.Status400BadRequest, "room ko ton tai ", null);
                        }
                    }
                    return responses.ResponsesSucsess("xoa thanh cong", null);
                }


            }
        }

       
        List<DataresponsesPresentlyMovies> IMoviesService.NameMoviesPresentlyMovies()
        {
            throw new NotImplementedException();
        }



        /* List<DataresponsesPresentlyMovies> IMoviesService.NameMoviesPresentlyMovies()
         {
             try
             {
                 var x=contex.Movies.Where(m=>m.Schedules.Where(n=>n.Room.Equals(s)))
             }
             catch (Exception ex)
             {
                 Console.WriteLine(ex.ToString());
                 return null;
             }
         }*/
    }
}
