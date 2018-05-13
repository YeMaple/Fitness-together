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

            Persons personReturn2 = access.Update(person, person.Id);
            var returned2 = access.GetById<Persons>(person.Id);
            Assert.AreEqual(person.Name, returned2.Name);
            access.Delete<Persons>(returned2.Id);
        }

        [TestMethod]
        public void TestCRUDForWorkouts()
        {
            var context = new cse136Context();
            var access = new GenericAccess(context);
            var workout = access.Add(new Workouts { Name = "Test Workout", Category = "Unit test", CreatorId = 5 });
            Assert.AreEqual("Test Workout", workout.Name);

            workout.Name = "Changed Test Workout";
            workout.Category = "Changed Workout";
            workout = access.Update(workout, workout.Id);
            var returned = access.GetById<Workouts>(workout.Id);
            Assert.AreEqual(workout.Name, returned.Name);
            access.Delete<Workouts>(returned.Id);
        }
    }
}
