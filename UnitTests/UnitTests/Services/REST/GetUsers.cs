using GPS.Repositories.Interfaces;
using GPS.REST.Services;

namespace UnitTests.UnitTests.Services.REST{

    public class GetUsersServiceTest{

        private readonly Mock<IBaseRepository<UserModel>> _mockedBaseRepository;
        private readonly UserService _userService;

        public GetUsersServiceTest(){
            _mockedBaseRepository = new Mock<IBaseRepository<UserModel>>();
            _userService = new UserService(_mockedBaseRepository.Object);
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
            var serviceResult = await _userService.GetUsers();
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
            var serviceResult = await _userService.GetUsers();

            //Assert
            _mockedBaseRepository.Verify(rep => rep.GetAll(), Times.Once);
            Assert.Null(serviceResult);

        }
    }
}