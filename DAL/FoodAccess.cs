using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using System.Linq;

namespace DAL
{
    class FoodAccess : FoodAccessInterface
    {
        private readonly cse136Context _context;

        public IEnumerable<Foods> GetFoodsByName(string name)
        {
            var foodQuery = _context.Foods
                                 .Where(f => f.Name.Equals(name))
                                 .ToList();
            return foodQuery;

        }
    }
}
