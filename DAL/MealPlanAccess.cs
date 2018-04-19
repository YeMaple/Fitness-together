using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    class MealPlanAccess
    {
        // WARNING!!! NEEDED TO BE CHANGED
        private readonly POCContext _context;

        public MealPlanAccess(POCContext context)
        {
            _context = context;
        }

        ////////////////////////READER////////////////////

        public M_MealPlan GetPersonMealPlans(int creator_id)
        {
            return _context.MealPlan.Find(m => m.Creator_id == creator_id);
        }

        public T GetMealPlanById<T>(int id) where T : class
        {
            var dbSet = _context.Set<T>();
            var result = dbSet.Find(id);
            return (T)Convert.ChangeType(result, typeof(T));
        }

        public M_MealPlan GeMealPlansBySearch(string name, string category)
        {
            return _context.MealPlan.Where(m => m.Name.Contains(name) && m.Category.Contains(category));
        }

        ////////////////////////CREATER////////////////////

        public T CreateMealPlan<T>(T m_obj) where T : class
        {
            var dbSet = _context.Set<T>();
            dbSet.Add(m_obj);
            _context.SaveChanges();
            return m_obj;
        }

        ////////////////////////DELETE////////////////////

        public void DeleteMealPlan<T>(int id) where T : class
        {
            var dbSet = _context.Set<T>();
            var deleteMe = dbSet.Find(id);
            _context.Remove(deleteMe);
            _context.SaveChanges();
        }

        ////////////////////////UPDATE////////////////////

        public T UpdateMealPlan<T>(T obj, int id) where T : class
        {
            var dbSet = _context.Set<T>();
            var updateMe = dbSet.Find(id);
            if (updateMe == null)
            {
                return obj;
            }

            dbSet.Update(obj);
            _context.SaveChanges();
            return updateMe;
        }
    }
}
