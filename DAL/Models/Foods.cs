using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Foods
    {
        public Foods()
        {
            FoodInMeals = new HashSet<FoodInMeals>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Nutrition { get; set; }

        public ICollection<FoodInMeals> FoodInMeals { get; set; }
    }
}
