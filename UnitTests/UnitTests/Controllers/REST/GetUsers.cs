using GPS.REST.Controllers;
using GPS.REST.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.UnitTests.Controllers.REST{

    public class GetUsersControllerTest{

        private readonly Mock<IUserService> _mockedUserService;
        private readonly UsersController _usersController;

        public GetUsersControllerTest(){
            _mockedUserService = new Mock<IUserService>();
            _usersController = new UsersController(_mockedUserService.Object);
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

            _mockedUserService
                .Setup(svc => svc.GetUsers())
                .ReturnsAsync(usersList);

            //Act
            var controllerResult = await _usersController.GetUsers();

            //Assert
            _mockedUserService.Verify(svc => svc.GetUsers(), Times.Once);
            var expectedResult = Assert.IsType<OkObjectResult>(controllerResult);
            var returnValue = expectedResult.Value;

            Assert.NotNull(returnValue);
            var success = returnValue.GetType().GetProperty("success")?.GetValue(returnValue);
            var result = returnValue.GetType().GetProperty("result")?.GetValue(returnValue);

            Assert.NotNull(success);
            Assert.True((bool)success);
            Assert.Equal(usersList, result);
        }

        [Fact]
        public async Task GetUsersFailed_NullList_ShouldResultNotFound(){

            //Arrange
            _mockedUserService.Setup(svc => svc.GetUsers()).ReturnsAsync((List<UserModel>)null!);

            //Act
            var controllerResult = await _usersController.GetUsers();

            //Assert
            _mockedUserService.Verify(svc => svc.GetUsers(), Times.Once);
            Assert.IsType<NotFoundResult>(controllerResult);

        }
    }
}