using System.ComponentModel.DataAnnotations;

namespace logindemo.Models.DTO
{
    public class Register
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string[] role { get; set; }
    }
}
