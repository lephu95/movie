using movie.Playloads.DataRequest;
using movie.Playloads.DataResponses;
using movie.Playloads.Responses;

namespace movie.Services.Interface
{
    public interface IBillService
    {
        ResponsesObject<DataRespnsesBill> CreateBill(Request_Bill request);
        internal bool SendConfirmEmail(Request_Bill request);
        string CreatePaymentUrl(DataRespnsesBill model, HttpContext context);
        DataRespnsesBill PaymentExecute(IQueryCollection collections);
        List<DataRespnsesBill> GetTopSellingFoodsLast7Days();
    }
}
