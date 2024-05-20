using Microsoft.AspNetCore.Identity;

namespace WeatherPOC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
