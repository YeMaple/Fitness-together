using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DietPlansAccess : DietPlansAccessInterface
    {
        private readonly cse136Context _context;

        public DietPlansAccess(cse136Context context)
        {
            _context = context;
        }

        public DietPlans GetDietPlanById(int id)
        {
            return _context.DietPlans.Include(d => d.Meals).Include(d => d.Person).FirstOrDefault(d => d.Id == id);
        }

        public IEnumerable<DietPlans> GetDietPlansByCreatorId(int creator_id)
        {
            return _context.DietPlans.Include(d => d.Person).Where(d => d.PersonId == creator_id).ToList();
        }

        public IEnumerable<DietPlans> GetDietPlansByName(string name)
        {
            return _context.DietPlans.Include(d => d.Person).Where(d => d.Name.Contains(name)).ToList();
        }
    }
}
