namespace GPS.RabbitMQ.Services.Interfaces{

    public interface IProducer{

        Task<string> ProduceAsync(string message);
    }
}