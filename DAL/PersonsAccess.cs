using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using System.Linq;

namespace DAL
{
    public class PersonsAccess
    {
        private readonly cse136Context _context;

        public PersonsAccess(cse136Context context)
        {
            _context = context;
        }

        public Persons Login(string email, string password)
        {
            var personQuery = _context.Persons
                             .FirstOrDefault(p => p.Email.Equals(email) && p.Password.Equals(password));
            return personQuery;
        }
    }
}
