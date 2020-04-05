using System.ComponentModel.DataAnnotations;

namespace CarRent.WebApi.Models
{
    public class Token
    {
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
