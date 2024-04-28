using System.ComponentModel.DataAnnotations;

namespace movie.Handle.Email
{
    public class validate
    {
        public static bool IsvalidEmail(string email)
        {
            var checkemail = new EmailAddressAttribute();
            return checkemail.IsValid(email);
        }
        
    }
}
