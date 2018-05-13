using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using DAL;
using DAL.Models;

namespace BLL
{
    public class FoodService
    {
        private readonly GenericAccessInterface _genericAccess;
        private readonly FoodAccessInterface _foodAccess;

        public FoodService(GenericAccessInterface genericAccess)
        {
            _genericAccess = genericAccess;

        }

        public POCO.Food create(POCO.Food food)
        {
            if (food == null)
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(food.Name) || string.IsNullOrEmpty(food.Category))
            {
                throw new ArgumentOutOfRangeException();
            }

            var f = new Foods
            {
                Name = food.Name,
                Category = food.Category,
                Nutrition = food.Nutrition    
            };
            f = _genericAccess.Add(f);
            food.Id = f.Id;
            return food;
        }
        public POCO.Food getFoodById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidEnumArgumentException();
            }

            var f = _genericAccess.GetById<Foods>(id);
            var food = EntityObjToPOCO(f);
            return food;
        }

        public List<POCO.Food> getFollowings(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidEnumArgumentException();
            }

            var foodList = _foodAccess.GetFoodsByName(name);
            var result_list = new List<POCO.Food>();
            foreach (Foods food in foodList)
            {
                var foodItem = EntityObjToPOCO(food);
                result_list.Add(foodItem);
            }
            return result_list;
        }

        public bool update(POCO.Food food)
        {
            if (food == null)
            {
                throw new ArgumentNullException();
            }

            var entity = POCOObjToEntity(food);
            entity = _genericAccess.Update<Foods>(entity, food.Id);
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

        public static POCO.Food EntityObjToPOCO(Foods entity)
        {
            if (entity == null)
                return null;

            var food = new POCO.Food
            {
                Name = entity.Name,
                Category = entity.Category,
                Nutrition = entity.Nutrition
            };

            return food;
        }

        public static Foods POCOObjToEntity(POCO.Food food)
        {
            if (food == null)
            {
                return null;
            }

            var f = new Foods
            {
                Id = food.Id,
                Name = food.Name,
                Category = food.Category,
                Nutrition = food.Nutrition
            };
            return f;
        }

    }

  
}
