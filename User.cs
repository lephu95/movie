using System.ComponentModel.DataAnnotations;

namespace movie.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Point {  get; set; }
        public string Username {  get; set; }
        public string Email {  get; set; }
        public string Password { get; set; }
        public string Name {  get; set; }
        public string PhoneNumber { get; set; }
        public int RankCustomerId {  get; set; }
        public int UserStatusId {  get; set; }
        public bool IsActive {  get; set; }
        public int RoleId {  get; set; }
        public Role Role { get; set; }
        public UserStatus Status { get; set; }
        public RankCustomer rankCustomer { get; set; }
        public IEnumerable<Bill> Bill{ get; set; }
        public IEnumerable<ComfimEmail> ComfimEmail { get; set;}
        public IEnumerable<RefreshToken> RefreshTokens { get; set; }    
    }
}
