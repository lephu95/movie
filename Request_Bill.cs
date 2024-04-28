using movie.Entities;

namespace movie.Playloads.DataRequest
{
    public class Request_Bill
    {
        public int Moviesid{  get; set; }
        public int Cinemaid{  get; set; }
        public int Roomid {  get; set;}
        public int Schedulesid {  get; set;}
        public int foodid {  get; set;}
        public int billid {  get; set;}
        public int billticketid {  get; set; }
        public int ticketid {  get; set; }
        public int billfoodid { get; set; }
        public int promotionid{ get; set; }
        public string Email { get;  set; }
    }
}
