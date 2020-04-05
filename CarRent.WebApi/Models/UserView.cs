using System;
using System.ComponentModel.DataAnnotations;

namespace CarRent.WebApi.Models
{
    public class UserView
    {
        public int? Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 9)]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; }
    }
}
