using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;

namespace DAL
{
    public interface FoodInMealsAccessInterface
    {
        IEnumerable<FoodInMeals> GetFoodsInMeals(int meal_id);
        void DeleteFoodsInMeal(int meal_id);
    }
}
