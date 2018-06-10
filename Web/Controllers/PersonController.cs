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
            // Force the id = 5
            var person = _service.getPersonById(5);
            var viewModel = PersonPOCOToViewModel(person);

            return View(viewModel);
        }

        // Person edit page
        public IActionResult Edit(int id)
        {
            var person = _service.getPersonById(id);
            var viewModel = PersonPOCOToViewModel(person);
            return View(viewModel);
        }

        // Call update function
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Person person)
        {
            //// Note, there is no error checking on this one and it re-uses
            //// the MVC's [HttpPut] Update method.
            var pocoPerson = PersonViewModelToPOCO(person);
            Update(pocoPerson);
            return RedirectToAction("Index");
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
        public IActionResult Update([FromBody]POCO.Person person)
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

        private static Person PersonPOCOToViewModel(POCO.Person person)
        {
            var result = new Person
            {
                Id = person.Id,
                Name = person.Name,
                Email = person.Email,
                Password = person.Password,
                Sex = person.Sex,
                Age = (int)person.Age,
                Profile = person.Profile,
                MyDietPlans = DietPlansPOCOToViewModel(person.MyDietPlans),
                MyPinnedDietPlans = DietPlansPOCOToViewModel(person.MyPinnedDietPlans)
            };

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

        private static POCO.Person PersonViewModelToPOCO(Person person)
        {
            var result = new POCO.Person
            {
                Id = person.Id,
                Name = person.Name,
                Email = person.Email,
                Password = person.Password,
                Sex = person.Sex,
                Age = person.Age,
                Profile = person.Profile
            };

            return result;
        }
    }
}
