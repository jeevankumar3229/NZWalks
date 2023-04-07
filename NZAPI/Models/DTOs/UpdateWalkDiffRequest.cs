using System.ComponentModel.DataAnnotations;

namespace NZAPI.Models.DTOs
{
    public class UpdateWalkDiffRequest
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be of maximum length 100")]
        public string Name { get; set; }
    }
}
