using movie.Playloads.DataRequest;
using movie.Playloads.DataResponses;
using movie.Playloads.Responses;

namespace movie.Services.Interface
{
    public interface ISeatservice
    {
        ResponsesObject<DataResponsesSeat> MoreSeat(Request_MoreSeat request);
        ResponsesObject<DataResponsesSeat> FixSeat(Request_FixSeat request);
        ResponsesObject<DataResponsesSeat> RmvSeat(int Id);
    }
}
