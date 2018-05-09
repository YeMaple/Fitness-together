using System;
using System.Collections.Generic;
using System.Text;

namespace POCO
{
    class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public int DietPlanId { get; set; }
        public DateTime Alarm { get; set; }
        public bool Reminder { get; set; }

        public List<Food> FoodsInMeal { get; set; }
    }
}
