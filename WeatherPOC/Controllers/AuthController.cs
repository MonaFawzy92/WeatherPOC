
using Microsoft.AspNetCore.Mvc;
using WeatherPOC.Models;
using WeatherPOC.Models.DTO;
using WeatherPOC.Models.DTO.Shared;
using WeatherPOC.Service.IService;

namespace WeatherPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        ResponseDTO _responseDTO;

        public AuthController(
           IAuthService authService)
        {
            _authService = authService;
            _responseDTO = new ResponseDTO();
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterationRequestDTO requestDTO)
        {
            var errorMsg = await _authService.Register(requestDTO);
            if(!string.IsNullOrEmpty(errorMsg)) { 
            
                _responseDTO.IsSuccess = false;
                _responseDTO.Msg= errorMsg;
                return BadRequest(_responseDTO);
            }
            return Ok(_responseDTO);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDTO requestDTO)
        {
            var loginResponse = await _authService.Login(requestDTO);
            if (loginResponse.User is null)
            {

                _responseDTO.IsSuccess = false;
                _responseDTO.Msg = "user is not exist";
                return BadRequest(_responseDTO);
            }
            _responseDTO.Result = loginResponse;
            return Ok(_responseDTO);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole(RegisterationRequestDTO requestDTO)
        {
            var assignSuccessfully = await _authService.AssignRole(requestDTO.Email, requestDTO.Role);
            if (!assignSuccessfully)
            {

                _responseDTO.IsSuccess = false;
                _responseDTO.Msg = "Error encountered";
                return BadRequest(_responseDTO);
            }
           
            return Ok(_responseDTO);
        }
    }
}
