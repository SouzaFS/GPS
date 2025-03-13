using GPS.Mappers;
using GPS.Repositories.Interfaces;
using GPS.REST.Services;

namespace UnitTests.UnitTests.Services.REST{

    public class CreateUserServiceTest{
        
        private readonly Mock<IBaseRepository<UserModel>> _mockedBaseRepository;
        private readonly UserService _userService;

        public CreateUserServiceTest(){
            _mockedBaseRepository = new Mock<IBaseRepository<UserModel>>();
            
            _userService = new UserService(_mockedBaseRepository.Object);
        }

        [Fact]
        public async Task CreateUserSuccess_ShouldReturnUser(){
            
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
            _mockedBaseRepository.Setup(rep => rep.CreateAsync(It.IsAny<UserModel>())).ReturnsAsync(userModel);
            
            //Act
            var serviceResult = await _userService.CreateUser(userDTO);
            var expectedResult = userModel;

            //Assert
            _mockedBaseRepository.Verify(rep => rep.CreateAsync(It.IsAny<UserModel>()), Times.Once);
            Assert.NotNull(userDTO);
            Assert.Equal(expectedResult, serviceResult);
        }

        [Fact]
        public async Task CreateUserFailed_UserNotSuccessfullyCreatedOnDB_ShouldResultNull(){

            var userDTO = new UserDTO(){
                Username = "username",
                FirstName = "firstname",
                LastName = "lastname",
                Email = "email",
                FederalID = "federalID"
            };
            //Arrange
            _mockedBaseRepository.Setup(rep => rep.CreateAsync(It.IsAny<UserModel>())).ReturnsAsync((UserModel)null!);

            //Act
            var serviceResult = await _userService.CreateUser(userDTO);

            //Assert
            _mockedBaseRepository.Verify(rep => rep.CreateAsync(It.IsAny<UserModel>()), Times.Once);
            Assert.Null(serviceResult);
        }

        [Fact]
        public async Task CreateUserFailed_NullUserDTO_ShouldResultNull(){

            //Act
            var serviceResult = await _userService.CreateUser(null!);

            //Assert
            Assert.Null(serviceResult);
        }
    }
    
}