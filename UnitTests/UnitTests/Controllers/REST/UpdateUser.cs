using System.Linq.Expressions;
using GPS.Mappers;
using GPS.REST.Controllers;
using GPS.REST.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.UnitTests.Controllers.REST{

    public class UpdateUserServiceTest{

        private readonly Mock<IUserService> _mockedUserService;
        private readonly UsersController _usersController;

        public UpdateUserServiceTest(){
            _mockedUserService = new Mock<IUserService>();
            _usersController = new UsersController(_mockedUserService.Object);
        }

        [Fact]
        public async Task UpdateUserSuccess_ShouldReturnUpdatedUser(){

            //Arrange
            var userModel = new UserModel(){
                Id = "2d33bc33e6e439f3b5db721f",
                Username = "username",
                FirstName = "firstname",
                LastName = "lastname",
                Email = "email",
                FederalID = "federalID"
            };
            
            var userDTO = new UserDTO(){
                Username = "another_username",
                FirstName = "another_firstName",
                FederalID = "another_federalID"
            };

            var returnResult = UserMapper.FromDTOToExistingModel(userModel, userDTO);

            _mockedUserService
                .Setup(svc => svc.UpdateUser(It.IsAny<string>(), It.IsAny<UserDTO>()))
                .ReturnsAsync(returnResult);

            //Act
            var controllerResult = await _usersController.UpdateUser(userModel.Id, userDTO);

            //Assert
            _mockedUserService.Verify(svc => svc.UpdateUser(It.IsAny<string>(), It.IsAny<UserDTO>()), Times.Once);
            var expectedResult = Assert.IsType<OkObjectResult>(controllerResult);
            var returnValue = expectedResult.Value;
            Assert.NotNull(returnValue);
    
            var success = returnValue.GetType().GetProperty("success")?.GetValue(returnValue);
            var result = returnValue.GetType().GetProperty("result")?.GetValue(returnValue);
            Assert.NotNull(success);
            Assert.True((bool)success);
            Assert.Equal(returnResult, result);

        }

        [Fact]
        public async Task UpdateUserFailed_ShouldUserNotFound(){

            //Arrange
            var userDTO = new UserDTO(){
                Username = "another_username",
                FirstName = "another_firstName",
                FederalID = "another_federalID"
            };

            _mockedUserService
                .Setup(svc => svc.UpdateUser(It.IsAny<string>(), It.IsAny<UserDTO>()))
                .ReturnsAsync((UserModel)null!);

            //Act
            var controllerResult = await _usersController.UpdateUser("2d33bc33e6e439f3b5db721f", userDTO);

            //Assert
            _mockedUserService.Verify(svc => svc.UpdateUser(It.IsAny<string>(), It.IsAny<UserDTO>()), Times.Once);
            Assert.IsType<UnprocessableEntityResult>(controllerResult);

        }
    }
}
