using BLL;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class MealController : Controller
    {
        private readonly GenericAccessInterface _genericAccess;
        private readonly FoodInMealsAccessInterface _foodsInMealAccess;
        private readonly MealsAccessInterface _mealAccess;
        private readonly FoodAccessInterface _foodAccess;

        public MealController(GenericAccessInterface genericAccess, FoodAccessInterface foodAccess, 
            MealsAccessInterface mealAccess, FoodInMealsAccessInterface foodsInMealAccess)
        {
            _genericAccess = genericAccess;
            _foodAccess = foodAccess;
            _mealAccess = mealAccess;
            _foodsInMealAccess = foodsInMealAccess;
        }

        [HttpGet]
        public IActionResult GetMealById(int id)
        {
            var service = new MealService(_genericAccess, _foodAccess, _mealAccess, _foodsInMealAccess);
            JsonResult result;

            try
            {
                var meal = service.getMealById(id);
                result = Json(meal);
                result.StatusCode = 200;
            }
            catch (Exception e)
            {
                result = Json(e);
                result.StatusCode = 400;
            }

            return result;
        }

        [HttpPost]
        public IActionResult Create(POCO.Meal m)
        {
            var service = new MealService(_genericAccess, _foodAccess, _mealAccess, _foodsInMealAccess);
            JsonResult result;

            try
            {
                var meal = service.create(m);
                result = Json(meal);
                result.StatusCode = 200;
            }
            catch (Exception e)
            {
                result = Json(e);
                result.StatusCode = 400;
            }

            return result;
        }

        [HttpPut]
        public IActionResult Update(POCO.Meal m)
        {
            var mealService = new MealService(_genericAccess, _foodAccess, _mealAccess, _foodsInMealAccess);
            var foodInMealService = new FoodsInMealService(_genericAccess, _foodAccess, _mealAccess, _foodsInMealAccess);
            var foodService = new FoodService(_genericAccess, _foodAccess);
            JsonResult mealResult;
            JsonResult foodInMealResult;

            try
            {
                var ret = mealService.update(m);
                mealResult = Json(ret);
                mealResult.StatusCode = 200;
            }
            catch (Exception e)
            {
                mealResult = Json(e);
                mealResult.StatusCode = 400;
            }

            try
            {
                var ret = foodInMealService.updateFoodsInMeal(m);
                foodInMealResult = Json(ret);
                mealResult.StatusCode = 200;
            }
            catch (Exception e)
            {
                foodInMealResult = Json(e);
                foodInMealResult.StatusCode = 400;
            }

            try
            {
                bool exists = true;
                //var ret;
                foreach (POCO.Food newFood in m.FoodsInMeal)
                {
                    if ( foodService.getFoodsByName(newFood.Name).Count() == 0)
                    {
                        foodService.create(newFood);
                    }
                    else
                    {
                        exists = false;
                        break;
                    }

                    if (exists == true)
                    {
                        var ret = "Successfully added" + newFood.Name;
                        mealResult = Json(ret);
                        mealResult.StatusCode = 200;
                    }

                    else
                    {
                        var ret = newFood.Name + "already exists";
                        mealResult = Json(ret);
                        mealResult.StatusCode = 400;
                    }
                }
            }
            catch(Exception e)
            {
                mealResult = Json(e);
                mealResult.StatusCode = 400;
            }

            return mealResult;
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var service = new MealService(_genericAccess, _foodAccess, _mealAccess, _foodsInMealAccess);
            JsonResult result;

            try
            {
                service.delete(id);
                result = Json("");
                result.StatusCode = 200;
            }
            catch (Exception e)
            {
                result = Json(e);
                result.StatusCode = 400;
            }

            return result;
        }
    }
}
