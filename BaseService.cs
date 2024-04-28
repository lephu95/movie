using movie.Entities;
using movie.Services.Interface;

namespace movie.Services.Implement
{
    public class BaseService
    {
        public readonly AppDbcontex contex;
        public BaseService()
        {
            contex = new AppDbcontex(); 
        }
       
    }
}
