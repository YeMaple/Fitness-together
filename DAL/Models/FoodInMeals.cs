using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class FoodInMeals
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public int MealId { get; set; }
        public double Amount { get; set; }
        public string Units { get; set; }

        public Foods Food { get; set; }
        public Meals Meal { get; set; }
    }
}
