using System.ComponentModel.DataAnnotations;

namespace NZWalks_Api.Models.DTOs
{
    public class UpdateWalkDiffRequest
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be of maximum length 100")]
        public string Name { get; set; }
    }
}
