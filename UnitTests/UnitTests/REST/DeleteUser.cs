using System.Linq.Expressions;
using GPS.Repositories.Interfaces;
using GPS.REST.Services;

namespace UnitTests.UnitTests.REST{

    public class DeleteUserTest{

        private readonly Mock<IBaseRepository<UserModel>> _mockedBaseRepository;
        private readonly UserService _userService;
        public DeleteUserTest(){
            _mockedBaseRepository = new Mock<IBaseRepository<UserModel>>();
            _userService = new UserService(_mockedBaseRepository.Object);
        }

        [Fact]
        public async Task DeleteUserSuccess_ShouldResultTrue(){
            
            //Arrange
            var userModel = new UserModel(){
                Id = "2d33bc33e6e439f3b5db721f",
                Username = "username",
                FirstName = "firstname",
                LastName = "lastname",
                Email = "email",
                FederalID = "federalID"
            };

            _mockedBaseRepository
                .Setup(rep => rep.GetByWhere(It.IsAny<Expression<Func<UserModel, bool>>>()))
                .ReturnsAsync(userModel);
                
            _mockedBaseRepository
                .Setup(rep => rep.DeleteAsync(It.IsAny<UserModel>()))
                .Returns(Task.CompletedTask);

            //Act
            var serviceResult = await _userService.DeleteUser(userModel.Id);

            //Assert
            _mockedBaseRepository.Verify(rep => rep.GetByWhere(It.IsAny<Expression<Func<UserModel, bool>>>()), Times.Once);
            _mockedBaseRepository.Verify(rep => rep.DeleteAsync(It.IsAny<UserModel>()), Times.Once);
            Assert.True(serviceResult);
            
        }

        [Fact]
        public async Task DeleteUserFailed_UserNotFound_ShouldResultFalse(){

            //Arrange
            var userModel = new UserModel(){
                Id = "2d33bc33e6e439f3b5db721f",
            };

            _mockedBaseRepository
                .Setup(rep => rep.GetByWhere(It.IsAny<Expression<Func<UserModel, bool>>>()))
                .ReturnsAsync((UserModel)null);

            //Act
            var serviceResult = await _userService.DeleteUser(userModel.Id);

            //Assert
            _mockedBaseRepository.Verify(rep => rep.GetByWhere(It.IsAny<Expression<Func<UserModel, bool>>>()), Times.Once);
            Assert.False(serviceResult);
        }
    }
}