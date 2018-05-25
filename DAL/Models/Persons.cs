using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Persons
    {
        public Persons()
        {
            DietPlans = new HashSet<DietPlans>();
            FollowingsFollowerNavigation = new HashSet<Followings>();
            FollowingsFollowingNavigation = new HashSet<Followings>();
            PinnedDietPlansPerson = new HashSet<PinnedDietPlans>();
            PinnedWorkouts = new HashSet<PinnedWorkouts>();
            Workouts = new HashSet<Workouts>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Profile { get; set; }
        public string Sex { get; set; }
        public byte[] Image { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<DietPlans> DietPlans { get; set; }
        public ICollection<Followings> FollowingsFollowerNavigation { get; set; }
        public ICollection<Followings> FollowingsFollowingNavigation { get; set; }
        public ICollection<PinnedDietPlans> PinnedDietPlansPerson { get; set; }
        public ICollection<PinnedWorkouts> PinnedWorkouts { get; set; }
        public ICollection<Workouts> Workouts { get; set; }
    }
}
