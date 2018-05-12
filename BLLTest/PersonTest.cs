using System;
using Xunit;
using System.Collections.Generic;
using System.ComponentModel;
using BLL;
using DAL;
using Moq;
using POCO;

namespace BLLTest
{
    public class PersonTest
    {
        private readonly PersonService _service;
        private readonly Mock<GenericAccessInterface> _genericAccess = new Mock<GenericAccessInterface>();
        private readonly Mock<PersonsAccessInterface> _personAccess = new Mock<PersonsAccessInterface>();
        private readonly Mock<FollowingsAccessInterface> _followingsAccess = new Mock<FollowingsAccessInterface>();

        public PersonTest()
        {
            _service = new PersonService(_personAccess.Object, _genericAccess.Object, _followingsAccess.Object);
        }

        [Fact]
        public void CreatePersonExceptionTest1()
        {
            //// Arranage

            //// Act & Assert
            Assert.Throws<ArgumentNullException>(() => _service.Create(null));
        }

        [Fact]
        public void CreatePersonExceptionTest2()
        {
            //// Arranage
            var person = new Person
            {
                Name = "TestName",
                Email = "wrongEmail.com",
                Password = "TestPassword"
            };
            //// Act & Assert
            Assert.Throws<InvalidEnumArgumentException>(() => _service.Create(person));
        }
    }
}
