using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;

namespace DAL
{
    public class GenericAccess
    {
        private readonly cse136Context _context;

        public GenericAccess(cse136Context context)
        {
            _context = context;
        }

        public T GetById<T>(int id) where T : class
        {
            var dbSet = _context.Set<T>();
            var result = dbSet.Find(id);
            return (T)Convert.ChangeType(result, typeof(T));
        }

        public T Add<T>(T obj) where T : class
        {
            var dbSet = _context.Set<T>();
            dbSet.Add(obj);
            _context.SaveChanges();
            return obj;
        }

        public void Delete<T>(int id) where T : class
        {
            var dbSet = _context.Set<T>();
            var deleteMe = dbSet.Find(id);
            _context.Remove(deleteMe);
            _context.SaveChanges();
        }

        public T Update<T>(T obj, int id) where T : class
        {
            var dbSet = _context.Set<T>();
            var changeMe = dbSet.Find(id);
            if (changeMe == null)
            {
                return obj;
            }

            dbSet.Update(obj);
            _context.SaveChanges();
            return changeMe;
        }
    }
}
