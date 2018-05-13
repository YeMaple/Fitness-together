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
            var returned = spec_access.GetDietPlansById(person.Id);
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
            var returned = spec_access.GetDietPlansByName("Unit Diet");
            Assert.IsTrue(returned.ToList().Contains(diet_plan_1));
            Assert.IsTrue(returned.ToList().Contains(diet_plan_2));
            access.Delete<Persons>(person.Id);
            access.Delete<DietPlans>(diet_plan_1.Id);
            access.Delete<DietPlans>(diet_plan_2.Id);
        }

        [TestMethod]
        public void TestFollowingGetFollowing()
        {
            var context = new cse136Context();
            var access = new GenericAccess(context);
            var spec_access = new FollowingsAccess(context);
            var person_1 = access.Add(new Persons { Age = 12, Email = "follower@test.com", Name = "FollowerTest", Password = "notsecure", Sex = "M", Profile = "For unit test" });
            var person_2 = access.Add(new Persons { Age = 21, Email = "following@test.com", Name = "FollowingTest", Password = "notsecure", Sex = "M", Profile = "For unit test" });
            Assert.AreEqual("FollowerTest", person_1.Name);
            Assert.AreEqual("FollowingTest", person_2.Name);
            var following = access.Add(new Followings { Follower = person_1.Id, Following = person_2.Id });
            var returned = spec_access.GetFollowings(person_1.Id);
            Assert.IsTrue(returned.ToList().Contains(following));
            access.Delete<Followings>(following.Id);
            access.Delete<Persons>(person_1.Id);
            access.Delete<Persons>(person_2.Id);
        }

        [TestMethod]
        public void TestFoodInMeals()
        {
            var context = new cse136Context();
            var access = new GenericAccess(context);
            var spec_access = new FoodInMealsAccess(context);
            var kiwi = access.Add(new Foods { Name = "Kiwi", Category = "Fruit", Nutrition = "Don't know" });
            var carrot = access.Add(new Foods { Name = "Carrot", Category = "Vegetable", Nutrition = "Vitamin A" });
            Assert.AreEqual("Kiwi", kiwi.Name);
            Assert.AreEqual("Carrot", carrot.Name);
            var meal = access.Add(new Meals { Name = "Test Meal", DietPlanId = 5 });
            var mealFood1 = access.Add(new FoodInMeals { MealId = meal.Id, FoodId = kiwi.Id });
            var mealFood2 = access.Add(new FoodInMeals { MealId = meal.Id, FoodId = carrot.Id });
            var returned = spec_access.GetFoodsInMeals(meal.Id);
            Assert.IsTrue(returned.ToList().Contains(mealFood1));
            access.Delete<FoodInMeals>(mealFood1.Id);
            access.Delete<FoodInMeals>(mealFood2.Id);
            access.Delete<Meals>(meal.Id);
            access.Delete<Foods>(kiwi.Id);
            access.Delete<Foods>(carrot.Id);
        }

        [TestMethod]
        public void TestPinnedWorkouts()
        {
            var context = new cse136Context();
            var access = new GenericAccess(context);
            var spec_access = new PinnedWorkoutsAccess(context);

            var follower = access.Add(new Persons { Age = 12, Email = "follower@test.com", Name = "FollowerTest", Password = "notsecure", Sex = "M", Profile = "For unit test" });

            var workout = access.Add(new Workouts { Name = "Pull Up", Category = "Body Weight", CreatorId = follower.Id });
            Assert.AreEqual("Pull Up", workout.Name);

            var pinned = access.Add(new PinnedWorkouts { PersonId = follower.Id, WorkoutId = workout.Id });
            var returned = spec_access.GetPinnedWorkoutsByPerson(follower.Id);

            Assert.IsTrue(returned.ToList().Contains(pinned));
            access.Delete<PinnedWorkouts>(pinned.Id);
            access.Delete<Workouts>(workout.Id);
            access.Delete<Persons>(follower.Id);
        }
    }
}
