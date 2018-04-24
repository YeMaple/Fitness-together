using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace DAL
{
    class PinnedDietPlansAccess
    {
        private readonly cse136Context _context;

        public PinnedDietPlansAccess(cse136Context context)
        {
            _context = context;
        }

        public IEnumerable<PinnedDietPlans> GetPinnedDietPlansByPerson(int person_id)
        {
            var PinnedDietPlansQuery = _context.PinnedDietPlans
                                                       .Where(p => p.PersonId.Equals(person_id))
                                                       .ToList();
            return PinnedDietPlansQuery;
        }
    }
}
