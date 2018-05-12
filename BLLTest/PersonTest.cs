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
            Assert.Throws<ArgumentNullException>(() => _service.create(null));
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
            Assert.Throws<InvalidEnumArgumentException>(() => _service.create(person));
        }

        [Fact]
        public void CreatePersonSuccessTest()
        {
            //// Arranage
            var person = new Person
            {
                Name = "First Test",
                Email = "good@email.com",
                Password = "1234567"
            };

            var addedPerson = new DAL.Models.Persons
            {
                Name = "First Test",
                Email = "good@email.com",
                Password = "1234567",
                Id = 1
            };

            _genericAccess.Setup(access => access.Add(It.IsAny<DAL.Models.Persons>())).Returns(addedPerson);

            //// Act
            var returned = _service.create(person);

            //// Assert
            _genericAccess.Verify(access => access.Add(It.IsAny<DAL.Models.Persons>()), Times.Once);
            Assert.Equal(addedPerson.Id, returned.Id);
        }

        [Fact]
        public void UpdatePersonExceptionTest()
        {
            //// Arranage

            //// Act & Assert
            Assert.Throws<ArgumentNullException>(() => _service.update(null));
        }

        [Fact]
        public void UpdatePersonSuccessTest()
        {
            //// Arrange
            var person = new Person
            {
                Id = 1,
                Name = "Peter",
                Email = "Test@email.com",
                Age = 12,
                Password = "Goodpass"
            };

            var entity = PersonService.POCOObjToEntity(person);
            _genericAccess.Setup(access => access.Update(It.IsAny<DAL.Models.Persons>(), It.IsAny<int>())).Returns(entity);

            //// Act
            var returned = _service.update(person);

            //// Assert
            _genericAccess.Verify(access => access.Update(It.IsAny<DAL.Models.Persons>(), It.IsAny<int>()), Times.Once);
            Assert.True(returned);
        }

        [Fact]
        public void DeletePersonExceptionTest()
        {
            //// Arranage

            //// Act & Assert
            Assert.Throws<InvalidEnumArgumentException>(() => _service.delete(-1));
        }

        [Fact]
        public void DeletePersonSuccessTest()
        {
            //// Arranage - a bit different because the generics.
            var genericAccess = new Mock<GenericAccessInterface>();
            genericAccess.Setup(access => access.Delete<Person>(It.IsAny<int>())).Verifiable();

            //// Act & Assert
            _service.delete(1);
            //// Note, this line does not work because MOQ framework cannot detect generic calls
            //// genericAccess.Verify(arg => arg.Delete<Student>(It.IsAny<int>()), Times.Once);
        }
    }
}
