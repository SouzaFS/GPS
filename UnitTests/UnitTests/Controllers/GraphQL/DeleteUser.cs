using GPS.GraphQL.Controllers;
using GPS.GraphQL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.UnitTests.Controllers.GraphQL{

    public class DeleteUserControllerTest{

        private readonly Mock<IUserMutation> _mockedUserMutation;
        private readonly UsersMutationController _usersMutationController;

        public DeleteUserControllerTest(){
            _mockedUserMutation = new Mock<IUserMutation>();
            _usersMutationController = new UsersMutationController(_mockedUserMutation.Object);
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
                
            _mockedUserMutation
                .Setup(svc => svc.DeleteUser(It.IsAny<string>()))
                .ReturnsAsync(true);

            //Act
            var controllerResult = await _usersMutationController.DeleteUser(userModel.Id);
            var expectedResult = GraphQL<UserModel>.NoContent();

            //Assert
            var data = controllerResult.GetType().GetProperty("Data")?.GetValue(controllerResult);
            var message = controllerResult.GetType().GetProperty("Message")?.GetValue(controllerResult);
            var statusCode = controllerResult.GetType().GetProperty("StatusCode")?.GetValue(controllerResult);
            var success = controllerResult.GetType().GetProperty("Success")?.GetValue(controllerResult);
            _mockedUserMutation.Verify(svc => svc.DeleteUser(It.IsAny<string>()), Times.Once);
            Assert.IsType<GraphQL<UserModel>>(controllerResult);
            Assert.Equal(expectedResult.Data, data);
            Assert.Equal(expectedResult.Message, message);
            Assert.Equal(expectedResult.StatusCode, statusCode);
            Assert.Equal(expectedResult.Success, success);

        }

        [Fact]
        public async Task DeleteUserFailed_ShouldResultUserNotFound(){

            //Arrange
            var userModel = new UserModel(){
                Id = "2d33bc33e6e439f3b5db721f",
            };

            _mockedUserMutation
                .Setup(svc => svc.DeleteUser(It.IsAny<string>()))
                .ReturnsAsync(false);

            //Act
            var controllerResult = await _usersMutationController.DeleteUser(userModel.Id);
            var expectedResult = GraphQL<UserModel>.UnprocessableEntity();

            //Assert
            var data = controllerResult.GetType().GetProperty("Data")?.GetValue(controllerResult);
            var message = controllerResult.GetType().GetProperty("Message")?.GetValue(controllerResult);
            var statusCode = controllerResult.GetType().GetProperty("StatusCode")?.GetValue(controllerResult);
            var success = controllerResult.GetType().GetProperty("Success")?.GetValue(controllerResult);
            _mockedUserMutation.Verify(svc => svc.DeleteUser(It.IsAny<string>()), Times.Once);
            Assert.IsType<GraphQL<UserModel>>(controllerResult);
            Assert.Equal(expectedResult.Data, data);
            Assert.Equal(expectedResult.Message, message);
            Assert.Equal(expectedResult.StatusCode, statusCode);
            Assert.Equal(expectedResult.Success, success);
        }
    }
}