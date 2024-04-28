using movie.Playloads.DataRequest;
using movie.Playloads.DataResponses;
using movie.Playloads.Responses;

namespace movie.Services.Interface
{
    public interface IFoodService
    {
        ResponsesObject<DataresposesFood> Morefood(Request_MoreFood request);
        ResponsesObject<DataresposesFood> FixFood(Request_FixFood request);
        ResponsesObject<DataresposesFood> Rmvfood(int Id);
    }
}
