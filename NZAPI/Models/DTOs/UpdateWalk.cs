using System.ComponentModel.DataAnnotations;

namespace NZAPI.Models.DTOs
{
    public class UpdateWalk
    {
        [Required]
        [MaxLength(100, ErrorMessage = "name has to be of length 100")]
        public string Name { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "name has to be of length 100")]
        public string Description { get; set; }
        [Required]
        [Range(0, 50)]
        public double Length { get; set; }

        public string? WalkImageURL { get; set; }
        [Required]
        public Guid RegionId { get; set; }
        [Required]
        public Guid WalkDifficultyId { get; set; }
    }
}
