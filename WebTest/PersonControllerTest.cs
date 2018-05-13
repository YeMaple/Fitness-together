using Microsoft.AspNetCore.Mvc;
using System;
using BLL;
using DAL;
using DAL.Models;
using Web.Controllers;
using Xunit;

namespace WebTest
{
    public class PersonControllerTest
    {
        private readonly PersonController _controller;

        public PersonControllerTest()
        {
            _controller = new PersonController(new GenericAccess(new cse136Context()), new PersonsAccess(new cse136Context()), new FollowingsAccess(new cse136Context()));
        }

        [Fact]
        public void GetPersonByIdFailed()
        {
            //// Arrange

            //// Action
            var detail = (JsonResult)_controller.GetPersonById(0);

            //// Assert
            Assert.Equal(400, detail.StatusCode);
        }

        [Fact]
        public void GetPersonByIdSuccess()
        {
            //// Arrange

            //// Action - this assume there is data for student with id=1 in the db
            var detail = (JsonResult)_controller.GetPersonById(2);

            //// Assert
            Assert.Equal(200, detail.StatusCode);
        }

        [Fact]
        public void LoginFailed()
        {
            //// Arrange

            //// Action - this assume there is data for student with id=1 in the db
            var detail = (JsonResult)_controller.Login("wrongemail.com", "pass");

            //// Assert
            Assert.Equal(400, detail.StatusCode);
        }

        [Fact]
        public void LoginSuccess()
        {
            //// Arrange

            //// Action - this assume there is data for student with id=1 in the db
            var detail = (JsonResult)_controller.Login("133@125.com", "cse120");

            //// Assert
            Assert.Equal(200, detail.StatusCode);
        }

        [Fact]
        public void AddPersonTest()
        {
            //// Arrange
            var controller = _controller;
            var person = new POCO.Person
            {
                Name = "TestControl",
                Email = "Test@email.com",
                Password = "1234"
            };

            //// Action - this assume there is data for student with id=1 in the db
            var detail = (JsonResult)controller.Create(person);

            //// Assert
            Assert.Equal(200, detail.StatusCode);

            //// Action again
            person.Id = ((POCO.Person)detail.Value).Id;
            controller.Delete(person.Id);

            //// Assert
            detail = (JsonResult)controller.GetPersonById(person.Id);
            Assert.Equal(200, detail.StatusCode);
        }

        [Fact]
        public void UpdatePersonTest()
        {
            //// Arrange
            var controller = _controller;
            var person = new POCO.Person
            {
                Name = "TestControl",
                Email = "Test@email.com",
                Password = "1234"
            };

            //// Action
            var detail = (JsonResult)controller.Create(person);

            var updatePerson = new POCO.Person
            {
                Id = ((POCO.Person)detail.Value).Id,
                Name = "Changed",
                Email = "Changed@email.com",
                Password = "Changed"
            };

            detail = (JsonResult)controller.Update(updatePerson);

            //// Assert
            Assert.Equal(200, detail.StatusCode);
            detail = (JsonResult)controller.GetPersonById(person.Id);
            Assert.Equal(200, detail.StatusCode);
            person.Id = ((POCO.Person)detail.Value).Id;
            controller.Delete(person.Id);
        }
    }
}
