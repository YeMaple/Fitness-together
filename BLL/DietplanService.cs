using System;
using System.Collections.Generic;
using System.ComponentModel;
using DAL;
using DAL.Models;

namespace BLL
{
    public class DietPlanService
    {
        private readonly DietPlansAccessInterface _dietPlanAccess;
        private readonly GenericAccessInterface _genericAccess;

        public DietPlanService(GenericAccessInterface genericAccess, DietPlansAccessInterface dietPlansAccess)
        {
            _dietPlanAccess = dietPlansAccess;
            _genericAccess = genericAccess;
        }

        public POCO.DietPlan create(POCO.DietPlan dietPlan)
        {
            if (dietPlan == null)
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(dietPlan.Name) || dietPlan.PersonId <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            var d = new DietPlans
            {
                Name = dietPlan.Name,
                Information = dietPlan.Information,
                PersonId = dietPlan.PersonId
            };
            d = _genericAccess.Add(d);
            dietPlan.Id = d.Id;
            return dietPlan;
        }

        public POCO.DietPlan getDietPlanById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidEnumArgumentException();
            }

            var d = _genericAccess.GetById<DietPlans>(id);
            var dietPlan = DietPlanEntityObjToPOCO(d);
            return dietPlan;
        }

        public List<POCO.DietPlan> getDietPlansByCreatorId(int creator_id)
        {
            if(creator_id <= 0)
            {
                throw new InvalidEnumArgumentException();
            }

            var d_list = _dietPlanAccess.GetDietPlansById(creator_id);
            List<POCO.DietPlan> result_list = new List<POCO.DietPlan>();
            foreach(DietPlans dietPlan in d_list)
            {
                var dp = DietPlanEntityObjToPOCO(dietPlan);
                result_list.Add(dp);
            }
            return result_list;
        }

        public List<POCO.DietPlan> getDietPlansByName(string dietPlan_name)
        {
            var d_list = _dietPlanAccess.GetDietPlansByName(dietPlan_name);
            List<POCO.DietPlan> result_list = new List<POCO.DietPlan>();
            foreach (DietPlans dietPlan in d_list)
            {
                var dp = DietPlanEntityObjToPOCO(dietPlan);
                result_list.Add(dp);
            }
            return result_list;
        }

        public bool update(POCO.DietPlan dietPlan)
        {
            if (dietPlan == null)
            {
                throw new ArgumentNullException();
            }

            var entity = POCOObjToEntity(dietPlan);
            entity = _genericAccess.Update<DietPlans>(entity, dietPlan.Id);
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

            return _genericAccess.Delete<DietPlans>(id);
        }

        public static POCO.DietPlan DietPlanEntityObjToPOCO(DietPlans entity)
        {
            if (entity == null)
                return null;

            var dietPlan = new POCO.DietPlan
            {
                Id = entity.Id,
                Name = entity.Name,
                Information = entity.Information,
                PersonId = entity.PersonId,
                CreatorName = entity.Person.Name,
                Meals = getMealList(entity.Meals)
            };

            return dietPlan;
        }

        public static DietPlans POCOObjToEntity(POCO.DietPlan dietPlan)
        {
            if (dietPlan == null)
            {
                return null;
            }

            var d = new DietPlans
            {
                Id = dietPlan.Id,
                Name = dietPlan.Name,
                Information = dietPlan.Information,
                PersonId = dietPlan.PersonId
            };
            return d;
        }

        // List of meal with only partial information
        public static List<POCO.Meal> getMealList(IEnumerable<Meals> meals)
        {
            var result_list = new List<POCO.Meal>();
            foreach(Meals meal in meals)
            {
                var m = MealEntityObjToPOCO(meal);
                result_list.Add(m);
            }
            return result_list;
        }

        // Only get partial information about meal
        public static POCO.Meal MealEntityObjToPOCO(Meals entity)
        {
            if(entity == null)
            {
                return null;
            }

            var meal = new POCO.Meal
            {
                Id = entity.Id,
                Name = entity.Name,
                Information = entity.Information,
                DietPlanId = entity.DietPlanId
            };

            return meal;
        }
    }
}
