using System;
using DAL;

namespace BLL
{
    public class PersonService
    {
        private readonly PersonAccessInterface _personAccess;
        private readonly GenericAccessInterface _genericAccess;

        public PersonService(PersonAccessInterface personAccess, GenericAccessInterface genericAccess)
        {
            _genericAccess = genericAccess;
            _personAccess = personAccess;
        }

        public POCO.Person Create(POCO.Person person)
        {
            if(person == null)
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(person.Email) || string.IsNullOrEmpty(person.Password) ||
                string.IsNullOrEmpty(person.Name) )
            {
                throw new ArgumentOutOfRangeException();
            }

            // check reg express
            // if ()
        }

        // Needed to be implemented
        private bool checkRegularExpression(String input, String expression)
        {
            return false;
        }
    }
}
