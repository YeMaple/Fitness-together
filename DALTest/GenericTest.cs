using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using DAL.Models;

namespace DALTest
{
    [TestClass]
    public class GenericTest
    {
        [TestMethod]
        public void TestGetById()
        {
            var context = new cse136Context();
            var access = new GenericAccess(context);
            var person = access.GetById<Persons>(2);
            var diet_plan = access.GetById<DietPlans>(5);
            var food = access.GetById<Foods>(1);
            var meal = access.GetById<Meals>(2);

            Assert.AreEqual(2, person.Id);
            Assert.AreEqual("Fruit", food.Category);
            Assert.AreEqual("FirstMeal", meal.Name);
            Assert.IsTrue(diet_plan.Meals.Contains(meal));
        }

        [TestMethod]
        public void TestCRUDForPersons()
        {
            var context = new cse136Context();
            var access = new GenericAccess(context);
            var person = access.Add(new Persons { Age = 12, Email = "itest@test.com", Name = "UnitTest", Password = "notsecure", Sex = "M", Profile = "For unit test"});
            Assert.AreEqual("UnitTest", person.Name);

            person.Name = "Changed UnitTest";
            person.Profile = "Changed profile";
            person = access.Update(person, person.Id);
            var returned = access.GetById<Persons>(person.Id);
            Assert.AreEqual(person.Name, returned.Name);
            access.Delete<Persons>(returned.Id);
        }
    }
}
