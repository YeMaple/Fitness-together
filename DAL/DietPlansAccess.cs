﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL.Models;

namespace DAL
{
    public class DietPlansAccess
    {
        private readonly cse136Context _context;

        public DietPlansAccess(cse136Context context)
        {
            _context = context;
        }

        public IEnumerable<DietPlans> GetDietPlansByCreator(int creator_id)
        {
            return _context.DietPlans.Where(d => d.PersonId == creator_id).ToList();
        }

        public IEnumerable<DietPlans> GetDietPlansBySearch(string name)
        {
            return _context.DietPlans.Where(d => d.Name.Contains(name)).ToList();
        }
    }
}