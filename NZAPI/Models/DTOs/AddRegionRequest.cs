using System.ComponentModel.DataAnnotations;

namespace NZAPI.Models.DTOs
{
    public class AddRegionRequest
    {
       
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be of length 3")]
        [MaxLength(3, ErrorMessage = "Code has to be of length 3")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be of maximum length 3")]
        public string Name { get; set; }

        public string? RegionImageURL { get; set; }
    }
}
