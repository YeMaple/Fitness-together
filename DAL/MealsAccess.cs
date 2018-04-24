using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using System.Linq;

namespace DAL
{
    public class MealsAccess
    {
        private readonly cse136Context _context;

        public MealsAccess(cse136Context context)
        {
            _context = context;
        }

        public IEnumerable<Meals> GetFoodsInMeals(int diet_plan_id)
        {
            var mealsQuery = _context.Meals
                             .Where(m => m.DietPlanId.Equals(diet_plan_id))
                             .ToList();
            return mealsQuery;
        }
    }
}
