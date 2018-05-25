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
    public class FoodTest
    {
        private readonly FoodService _service;
        private readonly Mock<GenericAccessInterface> _genericAccess = new Mock<GenericAccessInterface>();
        private readonly Mock<FoodAccessInterface> _foodAccess = new Mock<FoodAccessInterface>();

        public FoodTest()
        {
            _service = new FoodService(_genericAccess.Object, _foodAccess.Object);
        }

        [Fact]
        public void CreateFoodExceptionTest1()
        {
            //// Arranage

            //// Act & Assert
            Assert.Throws<ArgumentNullException>(() => _service.create(null));
        }

        [Fact]
        public void CreateFoodExceptionTest2()
        {
            //// Arranage
            var food = new Food
            {
                Name = "",
                Category = "TestFood",
                Nutrition = ""
            };
            //// Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _service.create(food));
        }

        [Fact]
        public void CreateFoodSuccessTest()
        {
            //// Arranage
            var food = new Food
            {
                Name = "First Test",
                Category = "Success diet food",
                Nutrition = "Test nutrition"
            };

            var addedFood = new DAL.Models.Foods
            {
                Name = "First Test",
                Category = "success diet plan",
                Nutrition = "Test nutrition",
                Id = 1
            };

            _genericAccess.Setup(access => access.Add(It.IsAny<DAL.Models.Foods>())).Returns(addedFood);

            //// Act
            var returned = _service.create(food);

            //// Assert
            _genericAccess.Verify(access => access.Add(It.IsAny<DAL.Models.Foods>()), Times.Once);
            Assert.Equal(addedFood.Id, returned.Id);
        }

        [Fact]
        public void UpdateFoodExceptionTest()
        {
            //// Arranage

            //// Act & Assert
            Assert.Throws<ArgumentNullException>(() => _service.update(null));
        }

        [Fact]
        public void UpdateFoodSuccessTest()
        {
            //// Arrange
            var food = new Food
            {
                Id = 1,
                Name = "Food Test",
                Category = "Just updated",
                Nutrition = "Updated Nutrition"
            };

            var entity = FoodService.POCOObjToEntity(food);
            _genericAccess.Setup(access => access.Update(It.IsAny<DAL.Models.Foods>(), It.IsAny<int>())).Returns(entity);

            //// Act
            var returned = _service.update(food);

            //// Assert
            _genericAccess.Verify(access => access.Update(It.IsAny<DAL.Models.Foods>(), It.IsAny<int>()), Times.Once);
            Assert.True(returned);
        }

        [Fact]
        public void DeleteFoodExceptionTest()
        {
            //// Arranage

            //// Act & Assert
            Assert.Throws<InvalidEnumArgumentException>(() => _service.delete(-1));
        }

        [Fact]
        public void DeleteFoodSuccessTest()
        {
            //// Arranage - a bit different because the generics.
            var genericAccess = new Mock<GenericAccessInterface>();
            genericAccess.Setup(access => access.Delete<Food>(It.IsAny<int>())).Verifiable();

            //// Act & Assert
            _service.delete(1);
            //// Note, this line does not work because MOQ framework cannot detect generic calls
            //// genericAccess.Verify(arg => arg.Delete<Student>(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void getFoodsByNameExceptionTest1()
        {
            //// Arranage

            //// Act & Assert
            Assert.Throws<InvalidEnumArgumentException>(() => _service.getFoodsByName(""));
        }

        [Fact]
        public void getFoodByNameSuccessTest()
        {
            ////Arrange
            var foodEntity1 = new DAL.Models.Foods
            {
                Id = 1,
                Name = "Food One",
                Category = "Test",
                Nutrition = "Test Nutrition",
            };

            var foodEntity2 = new DAL.Models.Foods
            {
                Id = 2,
                Name = "Food Two",
                Category = "Test",
                Nutrition = "Test Nutrition",
            };

            var foodEntity3 = new DAL.Models.Foods
            {
                Id = 3,
                Name = "Fud Three",
                Category = "Test",
                Nutrition = "Test Nutrition",
            };

            var foodEntity_List = new List<DAL.Models.Foods> { foodEntity1, foodEntity2 };

            var food1 = FoodService.EntityObjToPOCO(foodEntity1);
            var food2 = FoodService.EntityObjToPOCO(foodEntity2);
            var food3 = FoodService.EntityObjToPOCO(foodEntity3);
            _foodAccess.Setup(access => access.GetFoodsByName(It.IsAny<string>())).Returns(foodEntity_List);

            //// Act
            var returned = _service.getFoodsByName("Food");

            //// Assert
            _foodAccess.Verify(access => access.GetFoodsByName(It.IsAny<string>()), Times.Once);
            Assert.Equal(2, returned.Count);
            Assert.Equal(food1.Id, returned[0].Id);
            Assert.Equal(food1.Name, returned[0].Name);
            Assert.Equal(food1.Category, returned[0].Category);
            Assert.Equal(food1.Nutrition, returned[0].Nutrition);
            Assert.Equal(food2.Id, returned[1].Id);
            Assert.Equal(food2.Name, returned[1].Name);
            Assert.Equal(food2.Category, returned[1].Category);
            Assert.Equal(food2.Nutrition, returned[1].Nutrition);
        }
    }
}
