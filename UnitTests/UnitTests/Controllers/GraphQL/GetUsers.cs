using GPS.GraphQL.Controllers;
using GPS.GraphQL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.UnitTests.Controllers.GraphQL{

    public class GetUsersControllerTest{

        private readonly Mock<IUserQuery> _mockedUserQuery;
        private readonly UsersQueryController _usersQueryController;

        public GetUsersControllerTest(){
            _mockedUserQuery = new Mock<IUserQuery>();
            _usersQueryController = new UsersQueryController(_mockedUserQuery.Object);
        }

        [Fact]
        public async Task GetUsersSuccess_ShouldResultOk(){
            
            //Arrange
            var usersList = new List<UserModel>(){
                new UserModel{
                    Id = "3bec565a7b1f8453147f5d93",
                    FirstName = "firstname1",
                    LastName = "lastname1",
                    Username = "username1",
                    Email = "email1",
                    FederalID = "ID1"
                },
                new UserModel{
                    Id = "36507a859bf183717904ae8a",
                    FirstName = "firstname2",
                    LastName = "lastname2",
                    Username = "username2",
                    Email = "email2",
                    FederalID = "ID2"
                }
            };

            _mockedUserQuery
                .Setup(svc => svc.GetUsers())
                .ReturnsAsync(usersList);

            //Act
            var controllerResult = await _usersQueryController.GetUsers();
            var expectedResult = GraphQL<List<UserModel>>.Ok(usersList);

            //Assert
            var data = controllerResult.GetType().GetProperty("Data")?.GetValue(controllerResult);
            var message = controllerResult.GetType().GetProperty("Message")?.GetValue(controllerResult);
            var statusCode = controllerResult.GetType().GetProperty("StatusCode")?.GetValue(controllerResult);
            var success = controllerResult.GetType().GetProperty("Success")?.GetValue(controllerResult);
            _mockedUserQuery.Verify(svc => svc.GetUsers(), Times.Once);
            Assert.IsType<GraphQL<List<UserModel>>>(controllerResult);
            Assert.Equal(expectedResult.Data, data);
            Assert.Equal(expectedResult.Message, message);
            Assert.Equal(expectedResult.StatusCode, statusCode);
            Assert.Equal(expectedResult.Success, success);
            
        }

        [Fact]
        public async Task GetUsersFailed_NullList_ShouldResultNotFound(){

            //Arrange
            _mockedUserQuery.Setup(svc => svc.GetUsers()).ReturnsAsync((List<UserModel>)null!);

            //Act
            var controllerResult = await _usersQueryController.GetUsers();
            var expectedResult = GraphQL<List<UserModel>>.NotFound();

            //Assert
            var data = controllerResult.GetType().GetProperty("Data")?.GetValue(controllerResult);
            var message = controllerResult.GetType().GetProperty("Message")?.GetValue(controllerResult) as string;
            var statusCode = controllerResult.GetType().GetProperty("StatusCode")?.GetValue(controllerResult);
            var success = controllerResult.GetType().GetProperty("Success")?.GetValue(controllerResult);
            _mockedUserQuery.Verify(svc => svc.GetUsers(), Times.Once);
            Assert.IsType<GraphQL<List<UserModel>>>(controllerResult);
            Assert.Equal(expectedResult.Data, data);
            Assert.Equal(expectedResult.StatusCode, statusCode);
            Assert.Equal(expectedResult.Message, message);
            Assert.Equal(expectedResult.Success, success);

        }
    }
}