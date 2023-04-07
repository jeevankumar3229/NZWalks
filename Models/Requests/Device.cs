using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Models.Requests
{
    
    public class Device
    {
        [Key]
        [Required]
        [MaxLength(12,ErrorMessage ="Length cannot exceed 12")]
        [RegularExpression("[a-zA-Z0-9]+",ErrorMessage ="Enter a alphanumericID ")]
        public string MACID { get; set; }
        [Required]
        public Types type{ get; set; }
        
    }
    public enum Types
    {
        None,
        Jasper,
        Flycatcher
    }
}
