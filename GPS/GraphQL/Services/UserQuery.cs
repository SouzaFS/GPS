using GPS.GraphQL.Services.Interfaces;
using GPS.Models;
using GPS.Repositories.Interfaces;
using MongoDB.Driver.Linq;

namespace GPS.GraphQL.Services{

    public class UserQuery : IUserQuery{

        private readonly IBaseRepository<UserModel> _baseRepository;

        public UserQuery(IBaseRepository<UserModel> baseRepository){
            _baseRepository = baseRepository;
        }

        public async Task<List<UserModel>?> GetUsers(){
           
            var users = await _baseRepository.GetAll();
            if (users != null){
                return users;
            }

            return null;
        }

        public async Task<UserModel?> GetUserById(string id){

            var user = await _baseRepository.GetByWhere(a => a.Id == id);
            if (user != null){
                return user;
            }

            return null;
        }
        

    }
}