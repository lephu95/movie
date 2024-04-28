namespace movie.Playloads.DataRequest
{
    public class Request_ForgotPassword
    {
        public string username {  get; set; }
        public string password { get; set; }
        public string email {  get; set; }
        public string newpass {  get; set; }
    }
}
