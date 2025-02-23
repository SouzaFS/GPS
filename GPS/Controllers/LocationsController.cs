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
        public async Task<IActionResult> GetLocations()
        {
            try{
                var locations = await _locationService.GetLocations();
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
        [Route("id")]
        public async Task<IActionResult> GetLocationById(string id)
        {
            try{
                var location = await _locationService.GetLocationById(id);
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
        public async Task<IActionResult> UpdateLocation(string id, LocationDTO locationDTO)
        {
            try{
                var location = await _locationService.UpdateLocation(id, locationDTO);
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
        public async Task<IActionResult> DeleteLocation(string id)
        {
            try{
                var location = await _locationService.DeleteLocation(id);
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