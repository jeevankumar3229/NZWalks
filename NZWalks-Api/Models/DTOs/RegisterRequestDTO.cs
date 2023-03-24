using System.ComponentModel.DataAnnotations;

namespace NZWalks_Api.Models.DTOs
{
    public class RegisterRequestDTO
    {
        
        [Required]
        [DataType(DataType.Password)]//since we want username as emailaddress
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] Roles { get; set; }
    }
}
