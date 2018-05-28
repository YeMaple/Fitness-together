using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using System.Linq;

namespace DAL
{
    public class FoodInMealsAccess : FoodInMealsAccessInterface
    {
        private readonly cse136Context _context;

        public FoodInMealsAccess(cse136Context context)
        {
            _context = context;
        }

        public IEnumerable<FoodInMeals> GetFoodsInMeals(int meal_id)
        {
            var foodInMealsQuery = _context.FoodInMeals
                                  .Where(f => f.MealId.Equals(meal_id))
                                  .ToList();
            return foodInMealsQuery;
        }

        public void DeleteFoodsInMeal(int meal_id)
        {
            var foodInMealsQuery = GetFoodsInMeals(meal_id);
            _context.Remove(foodInMealsQuery);
            _context.SaveChanges();
        }

    }
}
