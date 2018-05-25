using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class DietPlans
    {
        public DietPlans()
        {
            Meals = new HashSet<Meals>();
            PinnedDietPlansDietPlan = new HashSet<PinnedDietPlans>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public int PersonId { get; set; }

        public Persons Person { get; set; }
        public ICollection<Meals> Meals { get; set; }
        public ICollection<PinnedDietPlans> PinnedDietPlansDietPlan { get; set; }
    }
}
