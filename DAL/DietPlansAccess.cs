using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL.Models;

namespace DAL
{
    public class DietPlansAccess : DietPlansAccessInterface
    {
        private readonly cse136Context _context;

        public DietPlansAccess(cse136Context context)
        {
            _context = context;
        }

        public IEnumerable<DietPlans> GetDietPlansByCreatorId(int creator_id)
        {
            return _context.DietPlans.Where(d => d.PersonId == creator_id).ToList();
        }

        public IEnumerable<DietPlans> GetDietPlansByName(string name)
        {
            return _context.DietPlans.Where(d => d.Name.Contains(name)).ToList();
        }
    }
}
