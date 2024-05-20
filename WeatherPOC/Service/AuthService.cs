using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System;
using WeatherPOC.Models;
using WeatherPOC.Service.IService;
using WeatherPOC.Data;
using WeatherPOC.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace WeatherPOC.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtOptions _jwtOptions;
        public AuthService(AppDbContext appDbContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<JwtOptions> jwtOptions)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<string> Register(RegisterationRequestDTO request)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = request.Email,
                Name = request.Name,
                NormalizedEmail = request.Email.ToUpper(),
                PhoneNumber = request.PhoneNumber,
                UserName = request.Email
            };

            try
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _appDbContext.ApplicationUsers
                        .First(x => x.Email == request.Email);

                    return "";
                }
                else
                {
                    return result.Errors.First().Description;
                }
            }
            catch (Exception ex)
            {
                //  return ex.Message;
            }

            return "Error encountered";
        }
        public async Task<LoginResponseDTO> Login(LoginRequestDTO request)
        {
            var user = await _appDbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.UserName == request.UserName);
            bool isPassValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (user is null || !isPassValid)
                return new LoginResponseDTO()
                {
                    User = null,
                    Token = ""
                };
            else
            {
                //generate token
                UserDTO userDTO = new UserDTO()
                {
                    Email = user.Email,
                    Name = user.Name,
                    ID = user.Id,
                    PhoneNumber = user.PhoneNumber
                };
                var token = await GenerateToken(user);

                return new LoginResponseDTO()
                {
                    User = userDTO,
                    Token = token
                };
            }

        }
        public async Task<string> GenerateToken(ApplicationUser applicationUser)
        {
            _jwtOptions.Secret = "this is the key to generate a token for loggedin users";
            _jwtOptions.Audience = "WeatherPOCFrontend";
            _jwtOptions.Issuer = "WeatherPOC";
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
            var claimsList = new List<Claim>()
            {
                new Claim (JwtRegisteredClaimNames.Email,applicationUser.Email ),
                new Claim (JwtRegisteredClaimNames.Sub,applicationUser.Id ),
                new Claim (JwtRegisteredClaimNames.Name,applicationUser.UserName ),
            };

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claimsList),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        #region Role
        public async Task<bool> AssignRole(string email, string role)
        {
            var user = await _appDbContext.ApplicationUsers
                .FirstOrDefaultAsync(x => x.Email == email);
            if (user != null)
            {
                if (_roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
                    _roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();

                await _userManager.AddToRoleAsync(user, role);

                return true;
            }

            return false;
        }

        #endregion
    }
}
