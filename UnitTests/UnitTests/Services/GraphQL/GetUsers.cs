using GPS.Repositories.Interfaces;
using GPS.GraphQL.Services;

namespace UnitTests.UnitTests.Services.GraphQL{

    public class GetUsersQueryTest{

        private readonly Mock<IBaseRepository<UserModel>> _mockedBaseRepository;
        private readonly UserQuery _userQuery;

        public GetUsersQueryTest(){
            _mockedBaseRepository = new Mock<IBaseRepository<UserModel>>();
            _userQuery = new UserQuery(_mockedBaseRepository.Object);
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

            _mockedBaseRepository
                .Setup(rep => rep.GetAll())
                .ReturnsAsync(usersList);

            //Act
            var serviceResult = await _userQuery.GetUsers();
            var expectedResult = usersList;

            //Assert
            _mockedBaseRepository.Verify(rep => rep.GetAll(), Times.Once);
            Assert.Equal(serviceResult, expectedResult);
        }

        [Fact]
        public async Task GetUsersFailed_NullList_ShouldResultNull(){

            //Arrange
            _mockedBaseRepository.Setup(rep => rep.GetAll()).ReturnsAsync((List<UserModel>)null!);

            //Act
            var serviceResult = await _userQuery.GetUsers();

            //Assert
            _mockedBaseRepository.Verify(rep => rep.GetAll(), Times.Once);
            Assert.Null(serviceResult);

        }
    }
}