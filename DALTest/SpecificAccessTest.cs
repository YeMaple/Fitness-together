using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using DAL.Models;

namespace DALTest
{
    [TestClass]
    public class SpecificAccessTest
    {
        [TestMethod]
        public void TestPersonsGetByEmailAndPassword()
        {
            var context = new cse136Context();
            var access = new GenericAccess(context);
            var spec_access = new PersonsAccess(context);
            var person = access.Add(new Persons { Age = 12, Email = "itest@test.com", Name = "UnitTest", Password = "notsecure", Sex = "M", Profile = "For unit test" });
            Assert.AreEqual("UnitTest", person.Name);
            var returned = spec_access.Login(person.Email, person.Password);
            Assert.AreEqual(person, returned);
        }
    }
}
