using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using System.Linq;

namespace DAL
{
    public class PinnedWorkoutsAccess
    {
        private readonly cse136Context _context;

        public PinnedWorkoutsAccess(cse136Context context)
        {
            _context = context;
        }

        public IEnumerable<PinnedWorkouts> GetPinnedWorkoutsByPerson(int person_id)
        {
            var PinnedWorkoutsQuery = _context.PinnedWorkouts
                                      .Where(p => p.PersonId.Equals(person_id))
                                      .ToList();
            return PinnedWorkoutsQuery;
        }
    }
}
