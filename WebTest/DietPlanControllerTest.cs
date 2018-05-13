using Microsoft.AspNetCore.Mvc;
using System;
using BLL;
using DAL;
using DAL.Models;
using Web.Controllers;
using Xunit;

namespace WebTest
{
    public class DietPlanControllerTest
    {
        private readonly DietPlanController _controller;

        public DietPlanControllerTest()
        {
            _controller = new DietPlanController(new GenericAccess(new cse136Context()), new DietPlansAccess(new cse136Context()));
        }

        [Fact]
        public void GetDietPlanByIdFailed()
        {
            //// Arrange

            //// Action
            var detail = (JsonResult)_controller.GetDietPlanById(0);

            //// Assert
            Assert.Equal(400, detail.StatusCode);
        }

        [Fact]
        public void GetDietPlanByIdSuccess()
        {
            //// Arrange

            //// Action - this assume there is data for student with id=1 in the db
            var detail = (JsonResult)_controller.GetDietPlanById(3);

            //// Assert
            Assert.Equal(200, detail.StatusCode);
        }

        [Fact]
        public void GetDietPlansByCreatorIdFailed()
        {
            //// Arrange

            //// Action - this assume there is data for student with id=1 in the db
            var detail = (JsonResult)_controller.GetDietPlansByCreatorId(0);

            //// Assert
            Assert.Equal(400, detail.StatusCode);
        }

        [Fact]
        public void GetDietPlanByCreatorIdSuccess()
        {
            //// Arrange

            //// Action - this assume there is data for student with id=1 in the db
            var detail = (JsonResult)_controller.GetDietPlansByCreatorId(2);

            //// Assert
            Assert.Equal(200, detail.StatusCode);
        }

        [Fact]
        public void GetDietPlanByNameSuccess()
        {
            //// Arrange

            //// Action - this assume there is data for student with id=1 in the db
            var detail = (JsonResult)_controller.GetDietPlansByName("Diet");

            //// Assert
            Assert.Equal(200, detail.StatusCode);
        }

        [Fact]
        public void AddDietPlanTest()
        {
            //// Arrange
            var controller = _controller;
            var dietPlan = new POCO.DietPlan
            {
                Name = "TestControl",
                Information = "Test diet plan",
                PersonId = 2
            };

            //// Action - this assume there is data for student with id=1 in the db
            var detail = (JsonResult)controller.Create(dietPlan);

            //// Assert
            Assert.Equal(200, detail.StatusCode);

            //// Action again
            dietPlan.Id = ((POCO.DietPlan)detail.Value).Id;
            controller.Delete(dietPlan.Id);

            //// Assert
            detail = (JsonResult)controller.GetDietPlanById(dietPlan.Id);
            Assert.Equal(200, detail.StatusCode);
        }

        [Fact]
        public void UpdateDietPlanTest()
        {
            //// Arrange
            var controller = _controller;
            var dietPlan = new POCO.DietPlan
            {
                Name = "TestControl",
                Information = "Test Dietplan",
                PersonId = 10
            };

            //// Action
            var detail = (JsonResult)controller.Create(dietPlan);

            //// Assert
            Assert.Equal(200, detail.StatusCode);

            //// Action again
            var updateDietPlan = new POCO.DietPlan
            {
                Id = ((POCO.DietPlan)detail.Value).Id,
                Name = "Changed",
                Information = "Changed Diet plan",
                PersonId = 10
            };

            detail = (JsonResult)controller.Update(updateDietPlan);

            //// Assert
            Assert.Equal(200, detail.StatusCode);
            Assert.Equal("Changed", ((POCO.DietPlan)detail.Value).Name);
            detail = (JsonResult)controller.GetDietPlanById(dietPlan.Id);
            Assert.Equal(200, detail.StatusCode);
            dietPlan.Id = ((POCO.DietPlan)detail.Value).Id;
            controller.Delete(dietPlan.Id);
        }
    }
}
