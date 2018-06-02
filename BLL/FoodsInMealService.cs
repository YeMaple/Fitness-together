using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using DAL.Models;
using BLL;


namespace BLL
{
    public class FoodsInMealService
    {
        private readonly GenericAccessInterface _genericAccess;
        private readonly FoodAccessInterface _foodAccess;
        private readonly MealsAccessInterface _mealAccess;
        private readonly FoodInMealsAccessInterface _foodInMealsAccess;

        public FoodsInMealService(GenericAccessInterface genericAccess, FoodAccessInterface foodAccess, MealsAccessInterface mealAcess, FoodInMealsAccessInterface foodInMealsAccess )
        {
            _genericAccess = genericAccess;
            _foodAccess = foodAccess;
            _mealAccess = mealAcess;
            _foodInMealsAccess = foodInMealsAccess;
        }

       
        public bool createFoodsInMeal(POCO.Meal meal)
        {
            foreach(POCO.Food food in meal.FoodsInMeal)
            {
                FoodInMeals foodInMealEntity = new FoodInMeals
                {
                    MealId = meal.Id,
                    FoodId = food.Id,
                    Amount = food.Amount
                };
                foodInMealEntity = _genericAccess.Add<FoodInMeals>(foodInMealEntity);
                if (foodInMealEntity == null) return false;
            }
            return true; 
        }

        public void deleteFoodsInMeal(int mealId)
        {
            _foodInMealsAccess.DeleteFoodsInMeal(mealId);
        }

        public bool updateFoodsInMeal(POCO.Meal meal)
        {
            _foodInMealsAccess.DeleteFoodsInMeal(meal.Id);
            return createFoodsInMeal(meal);
        }

    }
}
