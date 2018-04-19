using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    class WorkoutAccess
    {
        // WARNING!!! NEEDED TO BE CHANGED
        private readonly POCContext _context;

        public WorkoutAccess(POCContext context)
        {
            _context = context;
        }

        ////////////////////////READER////////////////////

        public M_Workout GetPersonWorkouts(int creator_id)
        {
            return _context.Workout.Find(w => w.Creator_id == creator_id);
        }

        public T GetWorkoutById<T>(int id) where T : class
        {
            var dbSet = _context.Set<T>();
            var result = dbSet.Find(id);
            return (T)Convert.ChangeType(result, typeof(T));
        }

        public M_Workout GeWorkoutsBySearch(string name, string category)
        {
            return _context.Workout.Where(w => w.Name.Contains(name) && w.Category.Contains(category));
        }

        ////////////////////////CREATER////////////////////

        public T CreateWorkout<T>(T w_obj) where T : class
        {
            var dbSet = _context.Set<T>();
            dbSet.Add(w_obj);
            _context.SaveChanges();
            return w_obj;
        }

        ////////////////////////DELETE////////////////////

        public void DeleteWorkout<T>(int id) where T : class
        {
            var dbSet = _context.Set<T>();
            var deleteMe = dbSet.Find(id);
            _context.Remove(deleteMe);
            _context.SaveChanges();
        }

        ////////////////////////UPDATE////////////////////

        public T UpdateWorkout<T>(T obj, int id) where T : class
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
