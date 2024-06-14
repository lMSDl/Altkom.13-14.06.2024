using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using Services.Interfaces;
using WebAPI.Controllers;

namespace WebAPI.Test.Controllers
{
    public class UsersControllerTest
    {
        [Fact]
        public async void Get_OkWithAllUsers()
        {
            //Arrange
            var service = new Mock<ICrudService<User>>();
            var expectedList = new Fixture().CreateMany<User>().ToList();

            service.Setup(x => x.ReadAsync()).ReturnsAsync(expectedList);

            var controller = new UsersController(service.Object);

            //Act
            var result = await controller.Get();

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var resultList = Assert.IsAssignableFrom<IEnumerable<User>>(actionResult.Value);
            Assert.Equal(expectedList, resultList);
        }

        [Fact]
        public async void Get_ExistingId_OkWithUser()
        {
            //Arrange
            var service = new Mock<ICrudService<User>>();
            var expectedUser = new Fixture().Create<User>();

            service.Setup(x => x.ReadAsync(expectedUser.Id)).ReturnsAsync(expectedUser);

            var controller = new UsersController(service.Object);

            //Act
            var result = await controller.Get(expectedUser.Id);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var resultUser = Assert.IsAssignableFrom<User>(actionResult.Value);
            Assert.Equal(expectedUser, resultUser);
        }

        [Fact]
        public void Get_NotExistingId_NotFound()
        {
            /*//Arrange
            var service = new Mock<ICrudService<User>>();
            int userId = new Fixture().Create<int>();

            var controller = new UsersController(service.Object);

            //Act
            var result = await controller.Get(userId);

            //Assert
            Assert.IsType<NotFoundResult>(result);*/
            ReturnsNotFound((controller, id) => controller.Get(id));
        }
        [Fact]
        public void Update_NotExistingId_NotFound()
        {
            ReturnsNotFound((controller, id) => controller.Put(id, default));
        }
        [Fact]
        public void Delete_NotExistingId_NotFound()
        {
            ReturnsNotFound((controller, id) => controller.Delete(id));
        }

        private async void ReturnsNotFound(Func<UsersController, int, Task<IActionResult>> func)
        {
            //Arrange
            var service = new Mock<ICrudService<User>>();
            int userId = new Fixture().Create<int>();

            var controller = new UsersController(service.Object);

            //Act
            var result = await func(controller, userId);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}