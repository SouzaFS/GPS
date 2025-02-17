using GPS.GraphQL.Interfaces;
using GPS.Models;
using GPS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPS.GraphQL{

    [ExtendObjectType("Query")]
    public class UserQuery : IUserQuery{

        private readonly IBaseRepository<UserModel> _baseRepository;

        public UserQuery(IBaseRepository<UserModel> baseRepository){
            _baseRepository = baseRepository;
        }
        public async Task<List<UserModel>> GetUsers(){
            return await _baseRepository.GetAll().ToListAsync();   
        }

        public async Task<UserModel> GetUserById(string id){
            
            var user = await _baseRepository.GetByWhere(a => a.Id == id).FirstOrDefaultAsync();
            if (user != null)
            {
                return user;
            }

            throw new Exception($"User with id {id} not found.");
        }

    }
}