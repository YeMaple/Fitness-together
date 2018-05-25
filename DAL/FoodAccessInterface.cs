using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;

namespace DAL
{
    public interface FoodAccessInterface
    {
        IEnumerable<Foods> GetFoodsByName(string name);
    }
}
