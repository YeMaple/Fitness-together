using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;

namespace DAL
{
    public interface MealsAccessInterface
    {
        Meals GetMealById(int id);
        //IEnumerable<Meals> GetMealsInDietPlan(int diet_plan_id);
    }
}
