using movie.Entities;
using movie.Playloads.DataResponses;

namespace movie.Playloads.Converter
{
    public class UserConverter
    {
        private readonly AppDbcontex contex;
        public UserConverter()
        {
            contex = new AppDbcontex();
        }
        public DataResponsesUser EntityToDTO(User user)
        {
            return new DataResponsesUser
            {
                Username = user.Username,
                Point = user.Point,
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                RankCustomName = contex.RankCustomers.SingleOrDefault(x => x.Id == user.RankCustomerId).Name,
                RoleName=contex.Roles.SingleOrDefault(x=>x.Id==user.RoleId).RoleName,
                UserStatusName=contex.UsersStatus.SingleOrDefault(x=>x.Id==user.UserStatusId).Name,
            };
        }
    }
}
