using WeatherPOC.Models.DTO;

namespace WeatherPOC.Service.IService
{
    public interface IWeatherService
    {
        Task<ResponseDTO> GetWeatherByLocation(string location);
    }
}
