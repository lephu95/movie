using movie.Entities;
using movie.Playloads.DataResponses;

namespace movie.Playloads.Converter
{
    public class CinimaConverter
    {
        private readonly AppDbcontex contex;
        public CinimaConverter()
        {
            contex = new AppDbcontex();
        }
        public DataresponsesCinima EntitytoDTO(Cinema cinema)
        {
            return new DataresponsesCinima
            {
                Address = cinema.Address,
                Description = cinema.Description,
                Code = cinema.Code,
                NameOfCinema = cinema.NameOfCinema,

            };
        }
        public DataresponsesCinima EmtitytoDTO(Room room)
        {
            return new DataresponsesCinima
            {

                Capacity = room.Capacity,
                Type = room.Type,
                Code = room.Code,
                Name = room.Name,
                Description = room.Description,
                CinemaName = contex.Cinemas.SingleOrDefault(x => x.Id == room.CinemaId).NameOfCinema,
            };
        }

        internal DataresponsesCinima EntitytoDTO(Room room)
        {
            throw new NotImplementedException();
        }
        public DataresponsesCinima EmtitytoDTO(double revenue,string name)
        {
            return new DataresponsesCinima
            {
               revenue = revenue,
               NameOfCinema=name,
            };
        }
    }
}
