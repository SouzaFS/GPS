using GPS.DTOs;
using GPS.Models;

namespace GPS.Mappers
{
    public class UserMapper
    {
        public static UserDTO FromModelToDTO(UserModel userModel)
        {
            return new UserDTO
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Username = userModel.Username,
                Email = userModel.Email,
                FederalID = userModel.FederalID
            };
        }

        public static UserModel FromDTOToModel(UserDTO userDTO)
        {
            return new UserModel
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Username = userDTO.Username,
                Email = userDTO.Email,
                FederalID = userDTO.FederalID
            };
        }

        public static List<UserModel> FromDTOToModelList(List<UserDTO> usersDTO)
        {
            return usersDTO.Select(usersDTO => FromDTOToModel(usersDTO)).ToList();
        }

        public static List<UserDTO> FromModelToDTOList(List<UserModel> usersModel)
        {
            return usersModel.Select(usersModel => FromModelToDTO(usersModel)).ToList();
        }
    }
}