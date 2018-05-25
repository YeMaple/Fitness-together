using BLL;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Web.Controllers
{
    public class FoodController : Controller
    {
        private readonly GenericAccessInterface _genericAccess;
        private readonly FoodAccessInterface _foodAccess;

        public FoodController(GenericAccessInterface genericAccess, FoodAccessInterface foodAccess)
        {
            _genericAccess = genericAccess;
            _foodAccess = foodAccess;
        }

        [HttpGet]
        public IActionResult GetFoodById(int id)
        {
            var service = new FoodService(_genericAccess, _foodAccess);
            JsonResult result;

            try
            {
                var food = service.getFoodById(id);
                result = Json(food);
                result.StatusCode = 200;
            }
            catch (Exception e)
            {
                result = Json(e);
                result.StatusCode = 400;
            }

            return result;
        }

        [HttpGet]
        public IActionResult GetFoodByName(string name)
        {
            var service = new FoodService(_genericAccess, _foodAccess);
            JsonResult result;

            try
            {
                var foods = service.getFoodsByName(name);
                result = Json(foods);
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
        public IActionResult Create(POCO.Food f)
        {
            var service = new FoodService(_genericAccess, _foodAccess);
            JsonResult result;

            try
            {
                var food = service.create(f);
                result = Json(food);
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
        public IActionResult Update(POCO.Food food)
        {
            var service = new FoodService(_genericAccess, _foodAccess);
            JsonResult result;

            try
            {
                var ret = service.update(food);
                result = Json(ret);
                result.StatusCode = 200;
            }
            catch (Exception e)
            {
                result = Json(e);
                result.StatusCode = 400;
            }

            return result;
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var service = new FoodService(_genericAccess, _foodAccess);
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
