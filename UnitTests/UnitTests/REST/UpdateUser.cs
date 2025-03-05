using System.Linq.Expressions;
using GPS.Mappers;
using GPS.Repositories.Interfaces;
using GPS.REST.Services;

namespace UnitTests.UnitTests.REST{

    public class UpdateUserTest{

        private readonly Mock<IBaseRepository<UserModel>> _mockedBaseRepository;
        private readonly UserService _userService;

        public UpdateUserTest(){
            _mockedBaseRepository = new Mock<IBaseRepository<UserModel>>();
            _userService = new UserService(_mockedBaseRepository.Object);
        }
        
        [Fact]
        public async Task UpdateUserSuccess_ShouldResultUpdated(){

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

            var expectedResult = UserMapper.FromDTOToExistingModel(userModel, userDTO);
            
            _mockedBaseRepository
                .Setup(rep => rep.GetByWhere(It.IsAny<Expression<Func<UserModel, bool>>>()))
                .ReturnsAsync(userModel);

            _mockedBaseRepository
                .Setup(rep => rep.UpdateAsync(It.IsAny<UserModel>()))
                .ReturnsAsync(expectedResult);

            //Act
            var serviceResult = await _userService.UpdateUser(userModel.Id, userDTO);

            //Assert
            _mockedBaseRepository.Verify(rep => rep.GetByWhere(It.IsAny<Expression<Func<UserModel, bool>>>()), Times.Once);
            _mockedBaseRepository.Verify(rep => rep.UpdateAsync(It.IsAny<UserModel>()), Times.Once);
            Assert.Equal(expectedResult, serviceResult);

        }

        [Fact]
        public async Task UpdateUserFailed_UserNotFound_ShouldResultNull(){

            //Arrange
            var userDTO = new UserDTO(){
                Username = "another_username",
                FirstName = "another_firstName",
                FederalID = "another_federalID"
            };

            _mockedBaseRepository
                .Setup(rep => rep.GetByWhere(It.IsAny<Expression<Func<UserModel, bool>>>()))
                .ReturnsAsync((UserModel)null);

            //Act
            var serviceResult = await _userService.UpdateUser("2d33bc33e6e439f3b5db721f", userDTO);

            //Assert
            _mockedBaseRepository.Verify(rep => rep.GetByWhere(It.IsAny<Expression<Func<UserModel, bool>>>()), Times.Once);
            Assert.Null(serviceResult);

        }

        [Fact]
        public async Task UpdateUserFailed_UserNotUpdated_ShouldResultNull(){

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

            _mockedBaseRepository
                .Setup(rep => rep.GetByWhere(It.IsAny<Expression<Func<UserModel, bool>>>()))
                .ReturnsAsync(userModel);

            _mockedBaseRepository
                .Setup(rep => rep.UpdateAsync(It.IsAny<UserModel>()))
                .ReturnsAsync((UserModel)null);

            //Act
            var serviceResult = await _userService.UpdateUser(userModel.Id, userDTO);

            //Assert
            _mockedBaseRepository.Verify(rep => rep.GetByWhere(It.IsAny<Expression<Func<UserModel, bool>>>()), Times.Once);
            _mockedBaseRepository.Verify(rep => rep.UpdateAsync(It.IsAny<UserModel>()), Times.Once);
            Assert.Null(serviceResult);
        }

        [Fact]
        public async Task UpdatedUserFailed_NullUserDTO_ShouldResultNull(){

            var userModel = new UserModel(){
                Id = "2d33bc33e6e439f3b5db721f",
                Username = "username",
                FirstName = "firstname",
                LastName = "lastname",
                Email = "email",
                FederalID = "federalID"
            };

            //Act
            var serviceResult = await _userService.UpdateUser(userModel.Id, null);

            //Assert
            Assert.Null(serviceResult);
        }

        [Fact]
        public async Task UpdatedLocationOfUserSuccess_ShouldResultUpdated(){
            
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
                Location = new LocationDTO(){
                    Latitude = -14.55,
                    Longitude = -60.22
                }
            };

            var expectedResult = UserMapper.FromDTOToExistingModel(userModel, userDTO);
            
            _mockedBaseRepository
                .Setup(rep => rep.GetByWhere(It.IsAny<Expression<Func<UserModel, bool>>>()))
                .ReturnsAsync(userModel);

            _mockedBaseRepository
                .Setup(rep => rep.UpdateAsync(It.IsAny<UserModel>()))
                .ReturnsAsync(expectedResult);

            //Act
            var serviceResult = await _userService.UpdateUser(userModel.Id, userDTO);

            //Assert
            _mockedBaseRepository.Verify(rep => rep.GetByWhere(It.IsAny<Expression<Func<UserModel, bool>>>()), Times.Once);
            _mockedBaseRepository.Verify(rep => rep.UpdateAsync(It.IsAny<UserModel>()), Times.Once);
            Assert.Equal(expectedResult, serviceResult);

        }
    }

}
