using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Meals
    {
        public Meals()
        {
            FoodInMeals = new HashSet<FoodInMeals>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public int DietPlanId { get; set; }
        public DateTime Alarm { get; set; }
        public bool Reminder { get; set; }

        public DietPlans DietPlan { get; set; }
        public ICollection<FoodInMeals> FoodInMeals { get; set; }
    }
}
