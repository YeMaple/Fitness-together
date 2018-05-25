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
    public class DietPlanTest
    {
        private readonly DietPlanService _service;
        private readonly Mock<GenericAccessInterface> _genericAccess = new Mock<GenericAccessInterface>();
        private readonly Mock<DietPlansAccessInterface> _dietPlanAccess = new Mock<DietPlansAccessInterface>();

        public DietPlanTest()
        {
            _service = new DietPlanService(_genericAccess.Object, _dietPlanAccess.Object);
        }

        [Fact]
        public void CreateDietPlanExceptionTest1()
        {
            //// Arranage

            //// Act & Assert
            Assert.Throws<ArgumentNullException>(() => _service.create(null));
        }

        [Fact]
        public void CreateDietPlanExceptionTest2()
        {
            //// Arranage
            var dietPlan = new DietPlan
            {
                Name = "",
                Information = "TestDietPlan",
                PersonId = 1
            };
            //// Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _service.create(dietPlan));
        }

        [Fact]
        public void CreateDietPlanSuccessTest()
        {
            //// Arranage
            var dietPlan = new DietPlan
            {
                Name = "First Test",
                Information = "success diet plan",
                PersonId = 12
            };

            var addedDietPlan = new DAL.Models.DietPlans
            {
                Name = "First Test",
                Information = "success diet plan",
                PersonId = 12,
                Id = 1
            };

            _genericAccess.Setup(access => access.Add(It.IsAny<DAL.Models.DietPlans>())).Returns(addedDietPlan);

            //// Act
            var returned = _service.create(dietPlan);

            //// Assert
            _genericAccess.Verify(access => access.Add(It.IsAny<DAL.Models.DietPlans>()), Times.Once);
            Assert.Equal(addedDietPlan.Id, returned.Id);
        }

        [Fact]
        public void UpdateDietPlanExceptionTest()
        {
            //// Arranage

            //// Act & Assert
            Assert.Throws<ArgumentNullException>(() => _service.update(null));
        }

        [Fact]
        public void UpdateDietPlanSuccessTest()
        {
            //// Arrange
            var dietPlan = new DietPlan
            {
                Id = 1,
                Name = "Diet Plan 1",
                Information = "Just updated",
                PersonId = 12
            };

            var entity = DietPlanService.POCOObjToEntity(dietPlan);
            _genericAccess.Setup(access => access.Update(It.IsAny<DAL.Models.DietPlans>(), It.IsAny<int>())).Returns(entity);

            //// Act
            var returned = _service.update(dietPlan);

            //// Assert
            _genericAccess.Verify(access => access.Update(It.IsAny<DAL.Models.DietPlans>(), It.IsAny<int>()), Times.Once);
            Assert.True(returned);
        }

        [Fact]
        public void DeleteDietPlanExceptionTest()
        {
            //// Arranage

            //// Act & Assert
            Assert.Throws<InvalidEnumArgumentException>(() => _service.delete(-1));
        }

        [Fact]
        public void DeleteDietPlanSuccessTest()
        {
            //// Arranage - a bit different because the generics.
            var genericAccess = new Mock<GenericAccessInterface>();
            genericAccess.Setup(access => access.Delete<DietPlan>(It.IsAny<int>())).Verifiable();

            //// Act & Assert
            _service.delete(1);
            //// Note, this line does not work because MOQ framework cannot detect generic calls
            //// genericAccess.Verify(arg => arg.Delete<Student>(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void getDietPlanByCreatorIdExceptionTest1()
        {
            //// Arranage

            //// Act & Assert
            Assert.Throws<InvalidEnumArgumentException>(() => _service.getDietPlansByCreatorId(0));
        }

        [Fact]
        public void getDietPlanByCreatorIdSuccessTest()
        {
            ////Arrange
            var dietPlanEntity = new DAL.Models.DietPlans
            {
                Id = 1,
                Name = "Diet plan",
                Information = "Test",
                PersonId = 12,
                Person = new DAL.Models.Persons
                {
                    Id = 2,
                    Name = "Tom",
                    Email = "Test@email.com",
                    Password = "Pass"
                },
                Meals = new List<DAL.Models.Meals>
                {
                    new DAL.Models.Meals { Id = 1, Name = "Meal1", Information = "Test1", DietPlanId = 1 },
                    new DAL.Models.Meals { Id = 2, Name = "Meal2", Information = "Test2", DietPlanId = 1 }
                }
            };

            var dietPlanEntity_List = new List<DAL.Models.DietPlans> { dietPlanEntity };

            var dietPlan = DietPlanService.DietPlanEntityObjToPOCO(dietPlanEntity);
            _dietPlanAccess.Setup(access => access.GetDietPlansById(It.IsAny<int>())).Returns(dietPlanEntity_List);

            //// Act
            var returned = _service.getDietPlansByCreatorId(2);

            //// Assert
            _dietPlanAccess.Verify(access => access.GetDietPlansById(It.IsAny<int>()), Times.Once);
            Assert.Equal(dietPlan.Id, returned[0].Id);
            Assert.Equal(dietPlan.Name, returned[0].Name);
            Assert.Equal(dietPlan.Information, returned[0].Information);
            Assert.Equal(dietPlan.Meals[1].Id, returned[0].Meals[1].Id);
        }

        [Fact]
        public void getDietPlanByNameSuccessTest()
        {
            ////Arrange
            var dietPlanEntity = new DAL.Models.DietPlans
            {
                Id = 1,
                Name = "Diet plan",
                Information = "Test",
                PersonId = 12,
                Person = new DAL.Models.Persons
                {
                    Id = 2,
                    Name = "Tom",
                    Email = "Test@email.com",
                    Password = "Pass"
                },
                Meals = new List<DAL.Models.Meals>
                {
                    new DAL.Models.Meals { Id = 1, Name = "Meal1", Information = "Test1", DietPlanId = 1 },
                    new DAL.Models.Meals { Id = 2, Name = "Meal2", Information = "Test2", DietPlanId = 1 }
                }
            };

            var dietPlanEntity_List = new List<DAL.Models.DietPlans> { dietPlanEntity };

            var dietPlan = DietPlanService.DietPlanEntityObjToPOCO(dietPlanEntity);
            _dietPlanAccess.Setup(access => access.GetDietPlansByName(It.IsAny<string>())).Returns(dietPlanEntity_List);

            //// Act
            var returned = _service.getDietPlansByName("");

            //// Assert
            _dietPlanAccess.Verify(access => access.GetDietPlansByName(It.IsAny<string>()), Times.Once);
            Assert.Equal(dietPlan.Id, returned[0].Id);
            Assert.Equal(dietPlan.Name, returned[0].Name);
            Assert.Equal(dietPlan.Information, returned[0].Information);
            Assert.Equal(dietPlan.Meals[1].Id, returned[0].Meals[1].Id);
        }
    }
}
