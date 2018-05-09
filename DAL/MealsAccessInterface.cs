using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;

namespace DAL
{
    public interface MealsAccessInterface
    {
        IEnumerable<Meals> GetMealsInDietPlan(int diet_plan_id);
    }
}
