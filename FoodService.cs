using movie.Entities;
using movie.Playloads.Converter;
using movie.Playloads.DataRequest;
using movie.Playloads.DataResponses;
using movie.Playloads.Responses;
using movie.Services.Interface;

namespace movie.Services.Implement
{
    public class FoodService:BaseService,IFoodService
    {
        private readonly FoodConverter converter;
        private readonly IConfiguration _configuration;
        private readonly DataresposesFood dataresposesFood;
        private readonly ResponsesObject<DataresposesFood> responses;
        public FoodService(IConfiguration configuration)
        {
            _configuration = configuration;
            converter = new FoodConverter();
           dataresposesFood = new DataresposesFood();
            responses = new ResponsesObject<DataresposesFood>();
        }


        public ResponsesObject<DataresposesFood> FixFood(Request_FixFood request)
        {
            var fix = contex.Foods.FirstOrDefault(x => x.NameOfFood == request.NameOfFood);
            if (contex.Foods == null)
            {
                return responses.ResponsesErr(StatusCodes.Status400BadRequest, "ko ton tai ", null);
            }
            else
            {
                if (contex.Foods.Any(x => x.NameOfFood == request.NameOfFood))
                {
                    contex.Foods.Remove(fix);
                    var food= new Food();
                   food.Price = request.Price;
                    food.NameOfFood = request.NameOfFood;
                    food.Description = request.Description;
                    food.image = request.image;
                    food.IsActive = true;
                    contex.Foods.Add(food);
                    contex.SaveChanges();
                    var fixsBillfood = contex.BillFoods.FirstOrDefault(x => x.FoodId == fix.Id);
                    if (fixsBillfood != null)
                    {
                        return responses.ResponsesErr(StatusCodes.Status400BadRequest, " room ko ton tai ", null);
                    }
                    else
                    {
                        if (contex.BillFoods.Any(x => x.FoodId == fix.Id))
                        {
                            contex.BillFoods.Remove(fixsBillfood);
                            var Billfood = new BillFood();
                            Billfood.Quantity = request.Quantity;
                            contex.BillFoods.Add(fixsBillfood);
                            contex.SaveChanges();
                            DataresposesFood resultbillfood = converter.EntitytoDTO(Billfood);
                            return responses.ResponsesSucsess("sua shedules thanh cong", resultbillfood);
                        }
                    }
                    
                    DataresposesFood result = converter.EntitytoDTO(food);
                    return responses.ResponsesSucsess("sua thanh cong", result);
                }
                else
                    return responses.ResponsesErr(StatusCodes.Status400BadRequest, " room ko ton tai ", null);
            }
        }

        public ResponsesObject<DataresposesFood> Morefood(Request_MoreFood request)
        {
            if (string.IsNullOrWhiteSpace(request.Price.ToString()) ||
                string.IsNullOrWhiteSpace(request.Description) ||
                string.IsNullOrWhiteSpace(request.image) ||
                string.IsNullOrWhiteSpace(request.NameOfFood)
                
                )
            {
                return responses.ResponsesErr(StatusCodes.Status400BadRequest, "vui long dien day du thong tin", null);
            }
            var food = new Food();
            food.IsActive = true;
            food.Price = request.Price;
            food.Description = request.Description;
            food.NameOfFood = request.NameOfFood;
            food.image=request.image;
            contex.SaveChanges();

            DataresposesFood result = converter.EntitytoDTO(food);
            return responses.ResponsesSucsess("them thanh cong", result);
        }

        public ResponsesObject<DataresposesFood> Rmvfood(int Id)
        {
            var remove = contex.Foods.FirstOrDefault(x => x.Id == Id);
            if (contex.Foods == null)
            {
                return responses.ResponsesErr(StatusCodes.Status400BadRequest, "ko ton tai ", null);
            }
            else
            {
                if (remove == null)
                {
                    return responses.ResponsesErr(StatusCodes.Status400BadRequest, "ko ton tai ", null);
                }
                else
                {
                    contex.Foods.Remove(remove);
                    contex.SaveChanges();
                    foreach (var item in contex.BillFoods)
                    {
                        if (item.Id == Id)
                        {
                            contex.BillFoods.Remove(item);
                            return responses.ResponsesSucsess("xoa thanh cong", null);
                        }
                        else
                        {
                            return responses.ResponsesErr(StatusCodes.Status400BadRequest, "room ko ton tai ", null);
                        }
                    }
                    
                    return responses.ResponsesSucsess("xoa thanh cong", null);
                }


            }
        }
    }
}
