using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Requests
{
    public class Register
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("[a-zA-Z]+[a-zA-z0-9]*[@][a-zA-z]+[.][a-zA-z]+")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

     
        public string[] roles { get; set; }
    }
}
