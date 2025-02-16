using GPS.Models;
using GPS.Repositories.Interfaces;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GPS.GraphQL.Queries{

    public class Users{
        
        private readonly IBaseRepository<UserModel> _baseRepository;

        public Users(IBaseRepository<UserModel> baseRepository){
            _baseRepository = baseRepository;
        }

        public async Task<List<UserModel>> GetUsers(){//([Service] IBaseRepository<UserModel> baseRepository){
            return await _baseRepository.GetAll().ToListAsync();
        }

    }
}