using WeatherPOC.Models.DTO;

namespace WeatherPOC.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegisterationRequestDTO request);
        Task<LoginResponseDTO> Login(LoginRequestDTO request);
        Task<bool> AssignRole(string email, string role);
    }
}
