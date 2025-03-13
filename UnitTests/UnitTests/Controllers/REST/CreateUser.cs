using GPS.Mappers;
using GPS.REST.Services.Interfaces;
using GPS.REST.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.UnitTests.Controllers.REST{

    public class CreateUserControllerTest{
        
        private readonly Mock<IUserService> _mockedUserService;
        private readonly UsersController _usersController;

        public CreateUserControllerTest(){
            _mockedUserService = new Mock<IUserService>();
            _usersController = new UsersController(_mockedUserService.Object);
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
            _mockedUserService.Setup(svc => svc.CreateUser(It.IsAny<UserDTO>())).ReturnsAsync(userModel);
            
            //Act
            var controllerResult = await _usersController.CreateUser(userDTO);

            //Assert
            _mockedUserService.Verify(svc => svc.CreateUser(It.IsAny<UserDTO>()), Times.Once);
            var expectedResult = Assert.IsType<CreatedResult>(controllerResult);
            Assert.NotNull(userDTO);
            Assert.Equal("", expectedResult.Location);
            Assert.Equal(userModel, expectedResult.Value);
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

            _mockedUserService
                .Setup(svc => svc.CreateUser(It.IsAny<UserDTO>()))
                .ReturnsAsync((UserModel)null!);
            
            //Act
            var controllerResult = await _usersController.CreateUser(userDTO);

            //Assert
            _mockedUserService.Verify(svc => svc.CreateUser(It.IsAny<UserDTO>()), Times.Once);
            Assert.IsType<BadRequestResult>(controllerResult);
            Assert.NotNull(userDTO);
            
        }

        [Fact]
        public async Task CreateUserFailed_NullUserDTO_ShouldResultBadRequest(){

            //Act
            var controllerResult = await _usersController.CreateUser(null!);

            //Assert
            Assert.IsType<BadRequestResult>(controllerResult);
        }
    }
    
}