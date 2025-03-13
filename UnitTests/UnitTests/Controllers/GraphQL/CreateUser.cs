using GPS.Mappers;
using Microsoft.AspNetCore.Mvc;
using GPS.GraphQL.Services.Interfaces;
using GPS.GraphQL.Controllers;

namespace UnitTests.UnitTests.Controllers.GraphQL{

    public class CreateUserControllerTest{
        
        private readonly Mock<IUserMutation> _mockedUserMutation;
        private readonly UsersMutationController _usersMutationController;

        public CreateUserControllerTest(){
            _mockedUserMutation = new Mock<IUserMutation>();
            _usersMutationController = new UsersMutationController(_mockedUserMutation.Object);
        }

        [Fact]
        public async Task CreateUserSuccess_ShouldReturnCreated(){
            
            //Arrange
            var userModel = new UserModel(){
                Id = "2d33bc33e6e439f3b5db721f",
                Username = "username",
                FirstName = "firstname",
                LastName = "lastname",
                Email = "email",
                FederalID = "federalID"
            };

            var userDTO = UserMapper.FromModelToDTO(userModel);
            _mockedUserMutation.Setup(svc => svc.CreateUser(It.IsAny<UserDTO>())).ReturnsAsync(userModel);
            
            //Act
            var controllerResult = await _usersMutationController.CreateUser(userDTO);
            var expectedResult = GraphQL<UserModel>.Created(userModel);

            //Assert
            var data = controllerResult.GetType().GetProperty("Data")?.GetValue(controllerResult);
            var message = controllerResult.GetType().GetProperty("Message")?.GetValue(controllerResult);
            var statusCode = controllerResult.GetType().GetProperty("StatusCode")?.GetValue(controllerResult);
            var success = controllerResult.GetType().GetProperty("Success")?.GetValue(controllerResult);
            _mockedUserMutation.Verify(svc => svc.CreateUser(It.IsAny<UserDTO>()), Times.Once);
            Assert.NotNull(userDTO);
            Assert.IsType<GraphQL<UserModel>>(controllerResult);
            Assert.Equal(expectedResult.Data, data);
            Assert.Equal(expectedResult.Message, message);
            Assert.Equal(expectedResult.StatusCode, statusCode);
            Assert.Equal(expectedResult.Success, success);
        }

        [Fact]
        public async Task CreateUserFailed_UserNotSuccessfullyCreatedOnDB_ShouldResultBadRequest(){

            //Arrange
            var userDTO = new UserDTO(){
                Username = "username",
                FirstName = "firstname",
                LastName = "lastname",
                Email = "email",
                FederalID = "federalID"
            };

            _mockedUserMutation.Setup(svc => svc.CreateUser(It.IsAny<UserDTO>())).ReturnsAsync((UserModel)null!);
            
            //Act
            var controllerResult = await _usersMutationController.CreateUser(userDTO);
            var expectedResult = GraphQL<UserModel>.BadRequest();

            //Assert
            var data = controllerResult.GetType().GetProperty("Data")?.GetValue(controllerResult);
            var message = controllerResult.GetType().GetProperty("Message")?.GetValue(controllerResult);
            var statusCode = controllerResult.GetType().GetProperty("StatusCode")?.GetValue(controllerResult);
            var success = controllerResult.GetType().GetProperty("Success")?.GetValue(controllerResult);
            _mockedUserMutation.Verify(svc => svc.CreateUser(It.IsAny<UserDTO>()), Times.Once);
            Assert.NotNull(userDTO);
            Assert.IsType<GraphQL<UserModel>>(controllerResult);
            Assert.Equal(expectedResult.Data, data);
            Assert.Equal(expectedResult.Message, message);
            Assert.Equal(expectedResult.StatusCode, statusCode);
            Assert.Equal(expectedResult.Success, success);
            
        }

        [Fact]
        public async Task CreateUserFailed_NullUserDTO_ShouldResultBadRequest(){

            //Act
            var controllerResult = await _usersMutationController.CreateUser(null!);
            var expectedResult = GraphQL<UserModel>.BadRequest();

            //Assert
            var data = controllerResult.GetType().GetProperty("Data")?.GetValue(controllerResult);
            var message = controllerResult.GetType().GetProperty("Message")?.GetValue(controllerResult);
            var statusCode = controllerResult.GetType().GetProperty("StatusCode")?.GetValue(controllerResult);
            var success = controllerResult.GetType().GetProperty("Success")?.GetValue(controllerResult);
            Assert.IsType<GraphQL<UserModel>>(controllerResult);
            Assert.Equal(expectedResult.Data, data);
            Assert.Equal(expectedResult.Message, message);
            Assert.Equal(expectedResult.StatusCode, statusCode);
            Assert.Equal(expectedResult.Success, success);
        }
    }
    
}