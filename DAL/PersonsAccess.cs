using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class PersonsAccess : PersonsAccessInterface
    {
        private readonly cse136Context _context;

        public PersonsAccess(cse136Context context)
        {
            _context = context;
        }

        public Persons GetPersonById(int id)
        {
            return _context.Persons
                   //.Include(p => p.DietPlans)
                   //.Include(p => p.PinnedDietPlansPerson)
                   // .Include("PinnedDietPlansPerson.DietPlan")
                   .Include(p => p.FollowingsFollowingNavigation)
                   .Include(p => p.FollowingsFollowerNavigation)
                   //.Include(p => p.Workouts)
                   //.Include(p => p.PinnedWorkouts)
                   //.Include("PinnedWorkouts.Workout")
                   .FirstOrDefault(p => p.Id == id);
        }

        public Persons Login(string email, string password)
        {
            var personQuery = _context.Persons
                             .FirstOrDefault(p => p.Email.Equals(email) && p.Password.Equals(password));
            return personQuery;
        }
    }
}
