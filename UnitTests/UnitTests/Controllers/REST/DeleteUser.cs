using GPS.REST.Controllers;
using GPS.REST.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.UnitTests.Controllers.REST{

    public class DeleteUserControllerTest{

        private readonly Mock<IUserService> _mockedUserService;
        private readonly UsersController _usersController;

        public DeleteUserControllerTest(){
            _mockedUserService = new Mock<IUserService>();
            _usersController = new UsersController(_mockedUserService.Object);
        }

        [Fact]
        public async Task DeleteUserSuccess_ShouldResultNoContent(){
            
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
                .Setup(svc => svc.DeleteUser(It.IsAny<string>()))
                .ReturnsAsync(true);

            //Act
            var controllerResult = await _usersController.DeleteUser(userModel.Id);

            //Assert
            _mockedUserService.Verify(svc => svc.DeleteUser(It.IsAny<string>()), Times.Once);
            Assert.IsType<NoContentResult>(controllerResult);

        }

        [Fact]
        public async Task DeleteUserFailed_ShouldResultUserNotFound(){

            //Arrange
            var userModel = new UserModel(){
                Id = "2d33bc33e6e439f3b5db721f",
            };

            _mockedUserService
                .Setup(svc => svc.DeleteUser(It.IsAny<string>()))
                .ReturnsAsync(false);

            //Act
            var controllerResult = await _usersController.DeleteUser(userModel.Id);

            //Assert
            _mockedUserService.Verify(svc => svc.DeleteUser(It.IsAny<string>()), Times.Once);
            Assert.IsType<NotFoundResult>(controllerResult);
        }
    }
}