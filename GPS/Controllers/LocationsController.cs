using GPS.DTOs;
using GPS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GPS.Controllers{

    [ApiController]
    [Route("[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation(LocationDTO locationDTO)
        {
            try{
                var location = await _locationService.CreateLocation(locationDTO);
                if (location != null)
                {
                    return Created("", location);
                }

                return BadRequest();
            }
            catch (Exception e){
                return Problem(e.Message, null, 500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLocations()
        {
            try{
                var locations = await _locationService.GetAllLocations();
                if (locations.Count > 0)
                {
                    return Ok(new
                    {
                        success = true,
                        result = locations
                    });
                }

                return NotFound();
            }
            catch (Exception e){
                return Problem(e.Message, null, 500);
            }
        }

        [HttpGet]
        [Route("userId")]
        public async Task<IActionResult> GetLocationByUserId(string userId)
        {
            try{
                var location = await _locationService.GetLocationByUserId(userId);
                if (location != null)
                {
                    return Ok(new
                    {
                        success = true,
                        result = location
                    });
                }

                return NotFound();
            }
            catch (Exception e){
                return Problem(e.Message, null, 500);
            }
            
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLocation(string userId, LocationDTO locationDTO)
        {
            try{
                var location = await _locationService.UpdateLocation(userId, locationDTO);
                if (location != null)
                {
                    return Ok(new
                    {
                        success = true,
                        result = location
                    });
                }
                return UnprocessableEntity();
            }
            catch (Exception e){
                return Problem(e.Message, null, 500);
            }
            
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLocation(string userId)
        {
            try{
                var location = await _locationService.DeleteLocation(userId);
                if (location != false)
                {
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