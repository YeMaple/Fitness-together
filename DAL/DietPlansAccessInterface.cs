using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;

namespace DAL
{
    public interface DietPlansAccessInterface
    {
        DietPlans GetDietPlanById(int id);
        IEnumerable<DietPlans> GetDietPlansByCreatorId(int creator_id);
        IEnumerable<DietPlans> GetDietPlansByName(string name);
    }
}
