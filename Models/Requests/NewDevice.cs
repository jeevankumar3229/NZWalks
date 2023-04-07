using System.ComponentModel.DataAnnotations;

namespace Models.Requests
{
    public class NewDevice
    {
        [Required]
        [MaxLength(12)]
        [RegularExpression("[a-zA-Z0-9]+")]
        public string MACID { get; set; }
        [Required]
        public Types type { get; set; }
    }
}
