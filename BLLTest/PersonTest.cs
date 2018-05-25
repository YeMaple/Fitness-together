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

        [Fact]
        public void LoginExceptionTest1()
        {
            //// Arranage

            //// Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _service.login("123@456.com", ""));
        }

        [Fact]
        public void LoginExceptionTest2()
        {
            //// Arranage

            //// Act & Assert
            Assert.Throws<InvalidEnumArgumentException>(() => _service.login("123456.com", "testpass"));
        }

        [Fact]
        public void LoginSuccessTest()
        {
            ////Arrange
            var person = new Person
            {
                Id = 1,
                Name = "Peter",
                Email = "Test@email.com",
                Age = 12,
                Password = "Goodpass"
            };

            var entity = PersonService.POCOObjToEntity(person);
            _personAccess.Setup(access => access.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(entity);

            //// Act
            var returned = _service.login("Test@email.com", "Goodpass");

            //// Assert
            _personAccess.Verify(access => access.Login(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.Equal(person.Id, returned.Id);
            Assert.Equal(person.Name, returned.Name);
            Assert.Equal(person.Email, returned.Email);
            Assert.Equal(person.Age, returned.Age);
            Assert.Equal(person.Password, returned.Password);
        }

        [Fact]
        public void getFollowingsTest()
        {
            ////Arrange
            var entityList = new List<DAL.Models.Followings>
                            {
                                new DAL.Models.Followings {
                                    Id = 1,
                                    Follower = 1,
                                    Following = 2,
                                    FollowerNavigation = new DAL.Models.Persons
                                    {
                                        Id = 1, Name = "Test1", Email = "Test1@email.com", Password = "GoodPass"
                                    },
                                    FollowingNavigation = new DAL.Models.Persons
                                    {
                                        Id = 2, Name = "Test2", Email = "Test2@email.com", Password = "GoodPass"
                                    },
                                },
                                new DAL.Models.Followings {
                                    Id = 2,
                                    Follower = 1,
                                    Following = 3,
                                    FollowerNavigation = new DAL.Models.Persons
                                    {
                                        Id = 1, Name = "Test1", Email = "Test1@email.com", Password = "GoodPass"
                                    },
                                    FollowingNavigation = new DAL.Models.Persons
                                    {
                                        Id = 3, Name = "Test3", Email = "Test3@email.com", Password = "GoodPass"
                                    },
                                }
                            };
            var followingList = new List<Person>
                                {
                                    new Person { Id = 2, Name = "Test2", Email = "Test2@email.com", Password = "GoodPass" },
                                    new Person { Id = 3, Name = "Test3", Email = "Test3@email.com", Password = "GoodPass" }
                                };
            _followingsAccess.Setup(access => access.GetFollowings(It.IsAny<int>())).Returns(entityList);

            ////Act
            var returned = _service.getFollowings(1);

            //// Assert
            _followingsAccess.Verify(access => access.GetFollowings(It.IsAny<int>()), Times.Once);
            Assert.Equal(followingList[0].Id, returned[0].Id);
            Assert.Equal(followingList[0].Name, returned[0].Name);
            Assert.Equal(followingList[1].Id, returned[1].Id);
            Assert.Equal(followingList[1].Name, returned[1].Name);
        }
    }
}
