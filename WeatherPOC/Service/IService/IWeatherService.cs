using WeatherPOC.Models.DTO.Shared;

namespace WeatherPOC.Service.IService
{
    public interface IWeatherService
    {
        Task<ResponseDTO> GetWeatherByLocation(string location);
    }
}
