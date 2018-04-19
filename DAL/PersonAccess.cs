using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    class PersonAccess
    {
        // WARNING!!! NEEDED TO BE CHANGED
        private readonly POCContext _context;

        public PersonAccess(POCContext context)
        {
            _context = context;
        }

        ////////////////////////READER////////////////////

        public M_Person GetPersonByLogin(string email, string password)
        {
            return _context.Person.FirstOrDefault(p => p.Email == email && p.Password == password);
        }

        public T GetPersonById<T>(int id) where T : class
        {
            var dbSet = _context.Set<T>();
            var result = dbSet.Find(id);
            return (T)Convert.ChangeType(result, typeof(T));
        }

        ////////////////////////CREATER////////////////////

        public T CreatePerson<T>(T p_obj) where T : class
        {
            var dbSet = _context.Set<T>();
            dbSet.Add(p_obj);
            _context.SaveChanges();
            return p_obj;
        }

        public void DeletePerson<T>(int id) where T : class
        {
            var dbSet = _context.Set<T>();
            var deleteMe = dbSet.Find(id);
            _context.Remove(deleteMe);
            _context.SaveChanges();
        }

        public T UpdatePerson<T>(T obj, int id) where T : class
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
