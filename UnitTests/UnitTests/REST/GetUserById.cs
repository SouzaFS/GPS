using System.Linq.Expressions;
using GPS.Repositories.Interfaces;
using GPS.REST.Services;

namespace UnitTests.UnitTests.REST{

    public class GetUserByIdTest{

        private readonly Mock<IBaseRepository<UserModel>> _mockedBaseRepository;
        private readonly UserService _userService;

        public GetUserByIdTest(){
            _mockedBaseRepository = new Mock<IBaseRepository<UserModel>>();
            
            _userService = new UserService(_mockedBaseRepository.Object);
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


            _mockedBaseRepository
                .Setup(rep => rep.GetByWhere(It.IsAny<Expression<Func<UserModel, bool>>>()))
                .ReturnsAsync(userModel);

            //Act
            var serviceResult = await _userService.GetUserById(userModel.Id);
            var expectedResult = userModel;

            //Assert
            _mockedBaseRepository.Verify(rep => rep.GetByWhere(It.IsAny<Expression<Func<UserModel, bool>>>()), Times.Once);
            Assert.Equal(expectedResult, serviceResult);
        }

        [Fact]
        public async Task GetUserFailed_UserNotFound_ShouldResultNull(){
            
            //Arrange
            _mockedBaseRepository
                .Setup(rep => rep.GetByWhere(It.IsAny<Expression<Func<UserModel,bool>>>()))
                .ReturnsAsync((UserModel)null);

            //Act
            var serviceResult = await _userService.GetUserById("2d33bc33e6e439f3b5db721f");
            
            //Assert
            _mockedBaseRepository.Verify(rep => rep.GetByWhere(It.IsAny<Expression<Func<UserModel, bool>>>()), Times.Once);
            Assert.Null(serviceResult);
        }
    }
}