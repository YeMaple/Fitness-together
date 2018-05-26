using BLL;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Web.ViewModels;

namespace Web.Controllers
{
    public class DietPlanController : Controller
    {
        private readonly GenericAccessInterface _genericAccess;
        private readonly DietPlansAccessInterface _dietPlansAccess;
        private readonly DietPlanService _service;

        public DietPlanController(GenericAccessInterface genericAccess, DietPlansAccessInterface dietPlansAccess)
        {
            _genericAccess = genericAccess;
            _dietPlansAccess = dietPlansAccess;
            _service = new DietPlanService(_genericAccess, _dietPlansAccess);
        }

        public IActionResult Index()
        {
            var dietPlans = _service.getDietPlansByName("");
            var viewModel = new List<DietPlan>();

            foreach (var dietPlan in dietPlans)
            {
                viewModel.Add(DietPlanPOCOToViewModel(dietPlan));
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult GetDietPlanById(int id)
        {
            var service = new DietPlanService(_genericAccess, _dietPlansAccess);
            JsonResult result;

            try
            {
                var dietPlan = service.getDietPlanById(id);
                result = Json(dietPlan);
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
        public IActionResult GetDietPlansByCreatorId(int id)
        {
            var service = new DietPlanService(_genericAccess, _dietPlansAccess);
            JsonResult result;

            try
            {
                var dietPlans = service.getDietPlansByCreatorId(id);
                result = Json(dietPlans);
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
        public IActionResult GetDietPlansByName(string name)
        {
            var service = new DietPlanService(_genericAccess, _dietPlansAccess);
            JsonResult result;

            try
            {
                var dietPlans = service.getDietPlansByName(name);
                result = Json(dietPlans);
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
        public IActionResult Create(POCO.DietPlan d)
        {
            var service = new DietPlanService(_genericAccess, _dietPlansAccess);
            JsonResult result;

            try
            {
                var dietPlan = service.create(d);
                result = Json(dietPlan);
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
        public IActionResult Update(POCO.DietPlan dietPlan)
        {
            var service = new DietPlanService(_genericAccess, _dietPlansAccess);
            JsonResult result;

            try
            {
                var ret = service.update(dietPlan);
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
            var service = new DietPlanService(_genericAccess, _dietPlansAccess);
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

        private static DietPlan DietPlanPOCOToViewModel(POCO.DietPlan pDietPlan)
        {
            var result = new DietPlan
            {
                Id = pDietPlan.Id,
                Name = pDietPlan.Name,
                Information = pDietPlan.Information,
                PersonId = pDietPlan.PersonId,
                CreatorName = getCreatorName(pDietPlan.Creator)
            };

            return result;
        }

        private static string getCreatorName(POCO.Person pPerson)
        {
            if (pPerson == null)
            {
                return null;
            }

            return pPerson.Name;
        }
    }
}
