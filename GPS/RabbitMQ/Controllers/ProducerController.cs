using GPS.RabbitMQ.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GPS.RabbitMQ.Controllers{

    [ApiController]
    [Route("[controller]")]
    public class ProducerController : ControllerBase {

        private readonly IProducer _producer;
        public ProducerController(IProducer producer){
            _producer = producer;
        }

        [HttpPost]
        public async Task<IActionResult> Produce(string message){
            try{
                var result = await _producer.ProduceAsync(message);
                if (result != null){
                    return Created("", message);
                }

                return BadRequest();
            }
            catch (Exception e){
                return Problem(e.Message, null, 500);
            }
            
        }
        

    }

}