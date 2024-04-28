using movie.Entities;
using movie.Playloads.DataRequest;
using movie.Playloads.DataResponses;
using movie.Playloads.Responses;
using System.Security.Claims;

namespace movie.Services.Interface
{
    public interface IUserservice
    {
        ResponsesObject<DataResponsesUser> Register(Request_Registers request);
        internal DataResponsesToken GennerateAccessToken(User user);
        DataResponsesToken RenewAccessToken(Request_RenewAccessToken request);
        ResponsesObject<DataResponsesToken> login(Request_Login request);
        IQueryable<DataResponsesUser>Getall();
       
        internal bool SendConfirmEmail(Request_Registers request);
        internal bool FogotPass (Request_ForgotPassword request);
        ResponsesObject<DataResponsesUser> ChangePass(int id, Request_ForgotPassword request);
        
        ResponsesObject<DataResponsesUser>ADMrmv(int id);
        Task<bool> AssignAdminRoleAsync(string username);
        void LockUserAccount(int userId);
        void UnlockUserAccount(int userId);
    } 
}
