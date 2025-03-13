using GPS.GraphQL.Controllers;
using GPS.GraphQL.Services.Interfaces;
using GPS.REST.Controllers;
using GPS.REST.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.UnitTests.Controllers.GraphQL{

    public class GetUserByIdControllerTest{

        private readonly Mock<IUserQuery> _mockedUserQuery;
        private readonly UsersQueryController _usersQueryController;

        public GetUserByIdControllerTest(){
            _mockedUserQuery = new Mock<IUserQuery>();
            _usersQueryController = new UsersQueryController(_mockedUserQuery.Object);
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

            _mockedUserQuery
                .Setup(svc => svc.GetUserById(It.IsAny<string>()))
                .ReturnsAsync(userModel);

            //Act
            var controllerResult = await _usersQueryController.GetUserById(userModel.Id);
            var expectedResult = GraphQL<UserModel>.Ok(userModel);

            //Assert
            var data = controllerResult.GetType().GetProperty("Data")?.GetValue(controllerResult);
            var message = controllerResult.GetType().GetProperty("Message")?.GetValue(controllerResult);
            var statusCode = controllerResult.GetType().GetProperty("StatusCode")?.GetValue(controllerResult);
            var success = controllerResult.GetType().GetProperty("Success")?.GetValue(controllerResult);
            _mockedUserQuery.Verify(svc => svc.GetUserById(It.IsAny<string>()), Times.Once);
            Assert.IsType<GraphQL<UserModel>>(controllerResult);
            Assert.Equal(expectedResult.Data, data);
            Assert.Equal(expectedResult.Message, message);
            Assert.Equal(expectedResult.StatusCode, statusCode);
            Assert.Equal(expectedResult.Success, success);
        }

        [Fact]
        public async Task GetUserFailed_ShouldResultUserNotFound(){
            
            //Arrange
            _mockedUserQuery
                .Setup(svc => svc.GetUserById(It.IsAny<string>()))
                .ReturnsAsync((UserModel)null!);

            //Act
            var controllerResult = await _usersQueryController.GetUserById("2d33bc33e6e439f3b5db721f");
            var expectedResult = GraphQL<UserModel>.NotFound();
            
            //Assert
            var data = controllerResult.GetType().GetProperty("Data")?.GetValue(controllerResult);
            var message = controllerResult.GetType().GetProperty("Message")?.GetValue(controllerResult);
            var statusCode = controllerResult.GetType().GetProperty("StatusCode")?.GetValue(controllerResult);
            var success = controllerResult.GetType().GetProperty("Success")?.GetValue(controllerResult);
            _mockedUserQuery.Verify(svc => svc.GetUserById(It.IsAny<string>()), Times.Once);
            Assert.IsType<GraphQL<UserModel>>(controllerResult);
            Assert.Equal(expectedResult.Data, data);
            Assert.Equal(expectedResult.Message, message);
            Assert.Equal(expectedResult.StatusCode, statusCode);
            Assert.Equal(expectedResult.Success, success);
        }
    }
}