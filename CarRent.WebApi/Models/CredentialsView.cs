using System.ComponentModel.DataAnnotations;

namespace CarRent.WebApi.Models
{
    public class CredentialsView
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 6)]
        public string Password { get; set; }
        //public string Token { get; set; }
    }
}
