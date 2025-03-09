using GPS.GraphQL.Controllers;
using GPS.GraphQL.Services.Interfaces;
using GPS.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.UnitTests.Controllers.GraphQL{

    public class UpdateUserServiceTest{

        private readonly Mock<IUserMutation> _mockedUserMutation;
        private readonly UsersMutationController _usersMutationController;

        public UpdateUserServiceTest(){
            _mockedUserMutation = new Mock<IUserMutation>();
            _usersMutationController = new UsersMutationController(_mockedUserMutation.Object);
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

            _mockedUserMutation
                .Setup(svc => svc.UpdateUser(It.IsAny<string>(), It.IsAny<UserDTO>()))
                .ReturnsAsync(returnResult);

            //Act
            var controllerResult = await _usersMutationController.UpdateUser(userModel.Id, userDTO);
            var expectedResult = GraphQL<UserModel>.Ok(returnResult);

            //Assert
            var data = controllerResult.GetType().GetProperty("Data")?.GetValue(controllerResult);
            var message = controllerResult.GetType().GetProperty("Message")?.GetValue(controllerResult);
            var statusCode = controllerResult.GetType().GetProperty("StatusCode")?.GetValue(controllerResult);
            var success = controllerResult.GetType().GetProperty("Success")?.GetValue(controllerResult);
            _mockedUserMutation.Verify(svc => svc.UpdateUser(It.IsAny<string>(), It.IsAny<UserDTO>()), Times.Once);
            Assert.IsType<GraphQL<UserModel>>(controllerResult);
            Assert.Equal(expectedResult.Data, data);
            Assert.Equal(expectedResult.Message, message);
            Assert.Equal(expectedResult.StatusCode, statusCode);
            Assert.Equal(expectedResult.Success, success);

        }

        [Fact]
        public async Task UpdateUserFailed_ShouldUserNotFound(){

            //Arrange
            var userDTO = new UserDTO(){
                Username = "another_username",
                FirstName = "another_firstName",
                FederalID = "another_federalID"
            };

            _mockedUserMutation
                .Setup(svc => svc.UpdateUser(It.IsAny<string>(), It.IsAny<UserDTO>()))
                .ReturnsAsync((UserModel)null!);

            //Act
            var controllerResult = await _usersMutationController.UpdateUser("2d33bc33e6e439f3b5db721f", userDTO);
            var expectedResult = GraphQL<UserModel>.BadRequest();

            //Assert
            var data = controllerResult.GetType().GetProperty("Data")?.GetValue(controllerResult);
            var message = controllerResult.GetType().GetProperty("Message")?.GetValue(controllerResult);
            var statusCode = controllerResult.GetType().GetProperty("StatusCode")?.GetValue(controllerResult);
            var success = controllerResult.GetType().GetProperty("Success")?.GetValue(controllerResult);
            _mockedUserMutation.Verify(svc => svc.UpdateUser(It.IsAny<string>(), It.IsAny<UserDTO>()), Times.Once);
            Assert.IsType<GraphQL<UserModel>>(controllerResult);
            Assert.Equal(expectedResult.Data, data);
            Assert.Equal(expectedResult.Message, message);
            Assert.Equal(expectedResult.StatusCode, statusCode);
            Assert.Equal(expectedResult.Success, success);

        }
    }
}
