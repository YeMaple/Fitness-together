using System;
using System.Collections.Generic;
using System.Text;

namespace POCO
{
    class DietPlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public int PersonId { get; set; }

        public Person Creator{ get; set; }
        public List<Meal> Meals { get; set; }
    }
}
