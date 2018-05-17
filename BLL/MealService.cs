using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using DAL;
using DAL.Models;
using BLL;

namespace BLL
{
    class MealService
    {
        private readonly GenericAccessInterface _genericAccess;
        private readonly FoodAccessInterface _foodAccess;
        private readonly MealsAccessInterface _mealAccess;
        private readonly FoodInMealsAccess _foodInMealsAccess;

        public MealService(GenericAccessInterface genericAccess, FoodAccessInterface foodAccess, MealsAccessInterface mealAcess, FoodInMealsAccess foodInMealsAccess)
        {
            _genericAccess = genericAccess;
            _foodAccess = foodAccess;
            _mealAccess = mealAcess;
            _foodInMealsAccess = foodInMealsAccess;
        }

        public POCO.Meal create(POCO.Meal meal)
        {
            if (meal == null)
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(meal.Name))
            {
                throw new ArgumentOutOfRangeException();
            }

            var m = new Meals
            {
                Name = meal.Name,
                Information = meal.Information,
                DietPlanId = meal.DietPlanId,
                Alarm = meal.Alarm,
                Reminder = meal.Reminder
            };
            m = _genericAccess.Add(m);
            meal.Id = m.Id;

            return meal;
        }

        public POCO.Meal getMealById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidEnumArgumentException();
            }

            var m = _genericAccess.GetById<Meals>(id);
            var meal = EntityObjToPOCO(m);
            return meal;
        }

        public bool update(POCO.Meal meal)
        {
            if (meal == null)
            {
                throw new ArgumentNullException();
            }

            var entity = POCOObjToEntity(meal);
            entity = _genericAccess.Update<Meals>(entity, meal.Id);
            if (entity != null)
            {
                return true;
            }
            return false;
        }

        public bool delete(int id)
        {
            if (id <= 0)
            {
                throw new InvalidEnumArgumentException();
            }

            return _genericAccess.Delete<Persons>(id);
        }

        public static POCO.Meal EntityObjToPOCO(Meals entity)
        {
            if (entity == null)
                return null;

            var meal = new POCO.Meal
            {
                Id = entity.Id,
                Name = entity.Name,
                Information = entity.Information,
                DietPlanId = entity.DietPlanId,
                Alarm = entity.Alarm,
                Reminder = entity.Reminder,

                FoodsInMeal = getFoodsInMeal(entity.FoodInMeals)
            };

            return meal;
        }

        public static Meals POCOObjToEntity(POCO.Meal meal)
        {
            if (meal == null)
            {
                return null;
            }

            var m = new Meals
            {
                Id = meal.Id,
                Name = meal.Name,
                Information = meal.Information,
                DietPlanId = meal.DietPlanId,
                Alarm = meal.Alarm,
                Reminder = meal.Reminder
            };
            return m;
        }

        public static List<POCO.Food> getFoodsInMeal(List<FoodInMeals> f_list)
        {
            if (f_list == null)
            {
                throw new InvalidEnumArgumentException();
            }

            //var f_list = _foodInMealsAccess.GetFoodsInMeals(mealId);
            var result_list = new List<POCO.Food>();
            foreach (FoodInMeals foodsInMeal in f_list)
            {
                var foodEntity = foodsInMeal.Food;
                var pocoFood = FoodService.EntityObjToPOCO(foodEntity);
            pocoFood.Amount = foodsInMeal.Amount;
                result_list.Add(pocoFood);
            }
            return result_list;
        }

        // List of meal with only partial information
        public static IEnumerable<Foods> getEntityFoodList(List<POCO.Food> foodList)
        {   
            var result_list = new List<Foods>();
            foreach(POCO.Food food in foodList)
            {
                var m = FoodService.POCOObjToEntity(food);
                result_list.Add(m);
            }
            return result_list;
        }


        // List of food with only partial information
        public static List<POCO.Food> getPOCOFoodList(IEnumerable<Foods> foods)
        {
            var result_list = new List<POCO.Food>();
            foreach (Foods food in foods)
            {
                var f = FoodService.EntityObjToPOCO(food);
                result_list.Add(f);
            }
            return result_list;
        }


    }
}
