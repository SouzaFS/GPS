using System.Linq.Expressions;
using GPS.REST.Controllers;
using GPS.REST.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.UnitTests.Controllers.REST{

    public class GetUserByIdControllerTest{

        private readonly Mock<IUserService> _mockedUserService;
        private readonly UsersController _usersController;

        public GetUserByIdControllerTest(){
            _mockedUserService = new Mock<IUserService>();
            _usersController = new UsersController(_mockedUserService.Object);
        }

        [Fact]
        public async Task GetUserSuccess_ShouldResultOk(){
            

            //Arrange
            var userModel = new UserModel(){
                Id = "2d33bc33e6e439f3b5db721f",
                Username = "username",
                FirstName = "firstname",
                LastName = "lastname",
                Email = "email",
                FederalID = "federalID"
            };

            _mockedUserService
                .Setup(svc => svc.GetUserById(It.IsAny<string>()))
                .ReturnsAsync(userModel);

            //Act
            var controllerResult = await _usersController.GetUserById(userModel.Id);

            //Assert
            _mockedUserService.Verify(svc => svc.GetUserById(It.IsAny<string>()), Times.Once);
            var expectedResult = Assert.IsType<OkObjectResult>(controllerResult);
            var returnValue = expectedResult.Value;
            Assert.NotNull(returnValue);

            var success = returnValue.GetType().GetProperty("success")?.GetValue(returnValue);
            var result = returnValue.GetType().GetProperty("result")?.GetValue(returnValue);

            Assert.NotNull(success);
            Assert.True((bool)success);
            Assert.Equal(userModel, result);
        }

        [Fact]
        public async Task GetUserFailed_ShouldResultUserNotFound(){
            
            //Arrange
            _mockedUserService
                .Setup(svc => svc.GetUserById(It.IsAny<string>()))
                .ReturnsAsync((UserModel)null!);

            //Act
            var controllerResult = await _usersController.GetUserById("2d33bc33e6e439f3b5db721f");
            
            //Assert
            _mockedUserService.Verify(svc => svc.GetUserById(It.IsAny<string>()), Times.Once);
            Assert.IsType<NotFoundResult>(controllerResult);
        }
    }
}