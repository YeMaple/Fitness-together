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
    }
}
