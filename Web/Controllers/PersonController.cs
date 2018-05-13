using BLL;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly GenericAccessInterface _genericAccess;
        private readonly PersonsAccessInterface _personsAccess;
        private readonly FollowingsAccessInterface _followingsAccess;

        public PersonController(GenericAccessInterface genericAccess, PersonsAccessInterface personsAccess, FollowingsAccessInterface followingsAccess)
        {
            _genericAccess = genericAccess;
            _personsAccess = personsAccess;
            _followingsAccess = followingsAccess;
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
    }
}
