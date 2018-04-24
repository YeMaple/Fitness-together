using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class PinnedWorkouts
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int WorkoutId { get; set; }

        public Persons Person { get; set; }
        public Workouts Workout { get; set; }
    }
}
