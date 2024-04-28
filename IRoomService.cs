
using movie.Entities;
using movie.Playloads.DataRequest;
using movie.Playloads.DataResponses;
using movie.Playloads.Responses;

namespace movie.Services.Interface
{
    public interface IRoomService
    {
        ResponsesObject<DataResponsesRoom> MoreRoom(Request_MoreRoom request);
        ResponsesObject<DataResponsesRoom> FixRoom(Request_FixRoom request);
        ResponsesObject<DataResponsesRoom> Rmvroom(int Id);
    }
}
