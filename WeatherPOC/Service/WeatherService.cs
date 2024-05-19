using Newtonsoft.Json;
using RestSharp;
using WeatherPOC.Models.DTO;
using WeatherPOC.Service.IService;

namespace WeatherPOC.Service
{
    public class WeatherService : IWeatherService
    {
        private readonly IConfiguration _configuration;
        public WeatherService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<ResponseDTO> GetWeatherByLocation(string location)
        {
            ResponseDTO _responseDTO = new ResponseDTO();
            try
            {
                string? appId = _configuration["AppKey"];
                string queryURL = $"http://api.openweathermap.org/data/2.5/weather?q={location}&units=metric&appid={appId}";
                RestClient client = new RestClient(queryURL);
                RestRequest request = new RestRequest();
                request.AddHeader("Content-Type", "application/json");

                RestResponse response = await client.ExecuteAsync(request);
                if (response.IsSuccessful) 
                    _responseDTO.Result = JsonConvert.DeserializeObject<WeatherResponseDTO>(response?.Content);
                else
                {
                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response?.Content);
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Msg = errorResponse?.Message;
                }
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Msg = "Something went wrong";
            }
       
            return _responseDTO;
        }

    }
}
