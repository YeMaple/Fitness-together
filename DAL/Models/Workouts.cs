using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Workouts
    {
        public Workouts()
        {
            PinnedWorkouts = new HashSet<PinnedWorkouts>();
            WorkoutSteps = new HashSet<WorkoutSteps>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int CreatorId { get; set; }

        public Persons Creator { get; set; }
        public ICollection<PinnedWorkouts> PinnedWorkouts { get; set; }
        public ICollection<WorkoutSteps> WorkoutSteps { get; set; }
    }
}
