using movie.Entities;
using movie.Playloads.DataResponses;

namespace movie.Playloads.Converter
{
    public class BillConverter
    {
        private readonly AppDbcontex contex;
        public BillConverter()
        {
            contex = new AppDbcontex();
        }
       
        public DataRespnsesBill EntitytoDTO( User user)
        {
            return new DataRespnsesBill
            {
                email = user.Email,
            };
        }

        internal DataRespnsesBill EntitytoDTO(double tatalMoney, string name1, string nameOfFood, string nameOfCinema, int quantity, string name2, DateTime startAt, DateTime endAt)
        {
            return new DataRespnsesBill
            {
                totamoney = tatalMoney,
                moviesName = name1,
                RoomName = name2,
                endat = endAt,
                startat = startAt,
                food = nameOfFood,
                CinemaNme =nameOfCinema,
                quantity = quantity,
            };
        }
    }
}
