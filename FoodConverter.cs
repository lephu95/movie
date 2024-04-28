using movie.Entities;
using movie.Playloads.DataResponses;

namespace movie.Playloads.Converter
{
    public class FoodConverter
    {
        private readonly AppDbcontex contex;
        public FoodConverter()
        {
            contex = new AppDbcontex();
        }
        public DataresposesFood EntitytoDTO(Food food)
        {
            return new DataresposesFood
            {
                price=food.Price,
                Descripition=food.Description,
                Image=food.image,
                Name=food.NameOfFood,

            };
        }
        public DataresposesFood EntitytoDTO(BillFood billFood)
        {
            return new DataresposesFood
            {
                Quantity=billFood.Quantity

            };
        }
    }
}
