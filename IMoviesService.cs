using movie.Entities;
using movie.Playloads.DataRequest;
using movie.Playloads.DataResponses;
using movie.Playloads.Responses;

namespace movie.Services.Interface
{
    public interface IMoviesService
    {
        internal List<DataResponsesMovies> NameMovies(int limit);
        ResponsesObject<DataResponsesMovies> moremovie(Request_MoreMovies request);
        ResponsesObject<DataResponsesMovies> fixmovie(Request_FixMovie request);
        ResponsesObject<DataResponsesMovies> Rmvmovie(int Id);
        internal List<DataresponsesPresentlyMovies> NameMoviesPresentlyMovies();
    }
}
