using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class PinnedDietPlans
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int DietPlanId { get; set; }

        public DietPlans DietPlan { get; set; }
        public Persons Person { get; set; }
    }
}
