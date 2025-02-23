using GPS.DTOs;
using GPS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GPS.Controllers{

    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDTO userDTO)
        {
            try{
                var user = await _userService.CreateUser(userDTO);
                if (user != null)
                {
                    return Created("", user);
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return Problem(e.Message, null, 500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try{
                var users = await _userService.GetUsers();
                
                if (users.Count > 0){
                    return Ok(new {
                        success = true,
                        result = users
                    });
                }

                return NotFound();
            }
            catch (Exception e){
                return Problem(e.Message, null, 500);
            }
        }

        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try{
                var user = await _userService.GetUserById(id);
                if (user != null){
                    return Ok(new {
                        success = true,
                        result = user
                    });
                }

                return NotFound();
            }
            catch (Exception e){
                return Problem(e.Message, null, 500);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(string id, UserDTO userDTO)
        {
            try{
                var user = await _userService.UpdateUser(id, userDTO);
                if (user != null){
                    return Ok(new {
                        sucess = true,
                        result = user
                    });
                }

                return UnprocessableEntity();
            }
            catch (Exception e){
                return Problem(e.Message, null, 500);
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string id){
            try{
                var result = await _userService.DeleteUser(id);
                if (result){
                    return NoContent();
                }

                return NotFound();
            }
            catch (Exception e){
                return Problem(e.Message, null, 500);
            }
        }

    }
    
}
