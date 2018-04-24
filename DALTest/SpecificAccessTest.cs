using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using DAL.Models;
using System.Linq;

namespace DALTest
{
    [TestClass]
    public class SpecificAccessTest
    {
        [TestMethod]
        public void TestPersonsLogin()
        {
            var context = new cse136Context();
            var access = new GenericAccess(context);
            var spec_access = new PersonsAccess(context);
            var person = access.Add(new Persons { Age = 12, Email = "mytest@test.com", Name = "MyUnitTest", Password = "notsecure", Sex = "M", Profile = "For unit test" });
            Assert.AreEqual("MyUnitTest", person.Name);
            var returned = spec_access.Login(person.Email, person.Password);
            Assert.AreEqual(person, returned);
            access.Delete<Persons>(returned.Id);
        }

        [TestMethod]
        public void TestDietPlansGetDietPlansByCreator()
        {
            var context = new cse136Context();
            var access = new GenericAccess(context);
            var spec_access = new DietPlansAccess(context);
            var person = access.Add(new Persons { Age = 12, Email = "mytest@test.com", Name = "MyUnitTest", Password = "notsecure", Sex = "M", Profile = "For unit test" });
            Assert.AreEqual("MyUnitTest", person.Name);
            var diet_plan_1 = access.Add(new DietPlans { Name = "Unit Diet 1", Information = "Unit test diet 1", PersonId = person.Id });
            var diet_plan_2 = access.Add(new DietPlans { Name = "Unit Diet 2", Information = "Unit test diet 2", PersonId = person.Id });
            var returned = spec_access.GetDietPlansByCreator(person.Id);
            Assert.IsTrue(returned.ToList().Contains(diet_plan_1));
            Assert.IsTrue(returned.ToList().Contains(diet_plan_2));
            access.Delete<Persons>(person.Id);
            access.Delete<DietPlans>(diet_plan_1.Id);
            access.Delete<DietPlans>(diet_plan_2.Id);
        }

        [TestMethod]
        public void TestDietPlansGetDietPlansBySearch()
        {
            var context = new cse136Context();
            var access = new GenericAccess(context);
            var spec_access = new DietPlansAccess(context);
            var person = access.Add(new Persons { Age = 12, Email = "mytest@test.com", Name = "MyUnitTest", Password = "notsecure", Sex = "M", Profile = "For unit test" });
            Assert.AreEqual("MyUnitTest", person.Name);
            var diet_plan_1 = access.Add(new DietPlans { Name = "Unit Diet 1", Information = "Unit test diet 1", PersonId = person.Id });
            var diet_plan_2 = access.Add(new DietPlans { Name = "Unit Diet 2", Information = "Unit test diet 2", PersonId = person.Id });
            var returned = spec_access.GetDietPlansBySearch("Unit Diet");
            Assert.IsTrue(returned.ToList().Contains(diet_plan_1));
            Assert.IsTrue(returned.ToList().Contains(diet_plan_2));
            access.Delete<Persons>(person.Id);
            access.Delete<DietPlans>(diet_plan_1.Id);
            access.Delete<DietPlans>(diet_plan_2.Id);
        }
    }
}
