//using NZWalks_Api.Models.Domains;

namespace NZWalks_Api.Models.DTOs
{
    public class Walk
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Length { get; set; }

        public string? WalkImageURL { get; set; }

        public Guid RegionId { get; set; }

        public Guid WalkDifficultyId { get; set; }
        //navigation property
        public Models.DTOs.Region Regions { get; set; }

        public Models.DTOs.WalkDifficulty WalkDifficulty { get; set; }
    }
}
