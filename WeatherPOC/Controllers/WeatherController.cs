﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherPOC.Models.DTO;
using WeatherPOC.Service.IService;

namespace WeatherPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        public WeatherController(IWeatherService weatherService) 
        { 
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<ResponseDTO> Get(string location)
        {
            ResponseDTO responseDTO = await _weatherService.GetWeatherByLocation(location);
            return responseDTO;
        }

    }
}
