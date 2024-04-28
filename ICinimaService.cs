
using movie.Entities;
using movie.Playloads.DataRequest;
using movie.Playloads.DataResponses;
using movie.Playloads.Responses;

namespace movie.Services.Interface
{
    public interface ICinimaService
    {
        ResponsesObject<DataresponsesCinima>morecinima(Request_MoreCinima request);
        ResponsesObject<DataresponsesCinima>fixCinima(Request_FixCinima request);
        ResponsesObject<DataresponsesCinima> RmvCinima(int Id);
        internal List<DataresponsesCinima> revenue(Request_RevenueCinema request);
    }
}
