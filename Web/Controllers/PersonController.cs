using BLL;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Web.ViewModels;

namespace Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly GenericAccessInterface _genericAccess;
        private readonly PersonsAccessInterface _personsAccess;
        private readonly FollowingsAccessInterface _followingsAccess;
        private readonly PersonService _service;

        public PersonController(GenericAccessInterface genericAccess, PersonsAccessInterface personsAccess, FollowingsAccessInterface followingsAccess)
        {
            _genericAccess = genericAccess;
            _personsAccess = personsAccess;
            _followingsAccess = followingsAccess;
            _service = new PersonService(_personsAccess, _genericAccess, _followingsAccess);
        }

        // Person home page
        public IActionResult Index()
        {
            // Since Login is not implemented
            // Force the id
            var Person = _service.getPersonById(5);
            var viewModel = new Person {
                Id = Person.Id,
                Name = Person.Name,
                Email = Person.Email,
                Password = Person.Password,
                Sex = Person.Sex,
                Age = (int)Person.Age,
                Profile = Person.Profile,
                MyDietPlans = DietPlansPOCOToViewModel(Person.MyDietPlans),
                MyPinnedDietPlans = DietPlansPOCOToViewModel(Person.MyPinnedDietPlans)
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult GetPersonById(int id)
        {
            var service = new PersonService(_personsAccess, _genericAccess, _followingsAccess);
            JsonResult result;

            try
            {
                var person = service.getPersonById(id);
                result = Json(person);
                result.StatusCode = 200;
            }
            catch (Exception e)
            {
                result = Json(e);
                result.StatusCode = 400;
            }

            return result;
        }

        [HttpGet]
        public IActionResult Login(string email, string password)
        {
            var service = new PersonService(_personsAccess, _genericAccess, _followingsAccess);
            JsonResult result;

            try
            {
                var person = service.login(email, password);
                result = Json(person);
                result.StatusCode = 200;
            }
            catch (Exception e)
            {
                result = Json(e);
                result.StatusCode = 400;
            }

            return result;
        }

        [HttpPost]
        public IActionResult Create(POCO.Person p)
        {
            var service = new PersonService(_personsAccess, _genericAccess, _followingsAccess);
            JsonResult result;

            try
            {
                var person = service.create(p);
                result = Json(person);
                result.StatusCode = 200;
            }
            catch (Exception e)
            {
                result = Json(e);
                result.StatusCode = 400;
            }

            return result;
        }

        [HttpPut]
        public IActionResult Update(POCO.Person person)
        {
            var service = new PersonService(_personsAccess, _genericAccess, _followingsAccess);
            JsonResult result;

            try
            {
                var ret = service.update(person);
                result = Json(ret);
                result.StatusCode = 200;
            }
            catch (Exception e)
            {
                result = Json(e);
                result.StatusCode = 400;
            }

            return result;
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var service = new PersonService(_personsAccess, _genericAccess, _followingsAccess);
            JsonResult result;

            try
            {
                service.delete(id);
                result = Json("");
                result.StatusCode = 200;
            }
            catch (Exception e)
            {
                result = Json(e);
                result.StatusCode = 400;
            }

            return result;
        }

        private static List<DietPlan> DietPlansPOCOToViewModel(List<POCO.DietPlan> pDietPlans)
        {
            if(pDietPlans == null)
            {
                return null;
            }
            List<DietPlan> result = new List<DietPlan>();
            foreach(POCO.DietPlan pDietPlan in pDietPlans)
            {
                var dietPlan = new DietPlan
                {
                    Id = pDietPlan.Id,
                    Name = pDietPlan.Name,
                    Information = pDietPlan.Information,
                    PersonId = pDietPlan.PersonId,
                    CreatorName = getCreatorName(pDietPlan.Creator)
                };
                result.Add(dietPlan);
            }

            return result;
        }

        private static string getCreatorName(POCO.Person pPerson)
        {
            if(pPerson == null)
            {
                return null;
            }

            return pPerson.Name;
        }
    }
}
