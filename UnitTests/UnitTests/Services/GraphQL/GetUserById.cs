using System.Linq.Expressions;
using GPS.Repositories.Interfaces;
using GPS.GraphQL.Services;

namespace UnitTests.UnitTests.Services.GraphQL{

    public class GetUserByIdQueryTest{

        private readonly Mock<IBaseRepository<UserModel>> _mockedBaseRepository;
        private readonly UserQuery _userQuery;

        public GetUserByIdQueryTest(){
            _mockedBaseRepository = new Mock<IBaseRepository<UserModel>>();
            
            _userQuery = new UserQuery(_mockedBaseRepository.Object);
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
            var serviceResult = await _userQuery.GetUserById(userModel.Id);
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
                .ReturnsAsync((UserModel)null!);

            //Act
            var serviceResult = await _userQuery.GetUserById("2d33bc33e6e439f3b5db721f");
            
            //Assert
            _mockedBaseRepository.Verify(rep => rep.GetByWhere(It.IsAny<Expression<Func<UserModel, bool>>>()), Times.Once);
            Assert.Null(serviceResult);
        }
    }
}