using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class WorkoutSteps
    {
        public int Id { get; set; }
        public int StepNum { get; set; }
        public string Instruction { get; set; }
        public byte[] Image { get; set; }
        public int WorkoutId { get; set; }

        public Workouts Workout { get; set; }
    }
}
