using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    class WorkoutAccess
    {
        private readonly cse136Context _context;

        public WorkoutAccess(cse136Context context)
        {
            _context = context;
        }

        public IEnumerable<Workouts> GetWorkoutsByCreator(int creator_id)
        {
            return _context.Workouts.Where(w => w.CreatorId == creator_id).ToList();
        }

        public IEnumerable<Workouts> GetWorkoutsBySearch(string name, string category)
        {
            return _context.Workouts.Where(w => w.Name.Contains(name) && w.Category.Contains(category)).ToList();
        }
    }
}
