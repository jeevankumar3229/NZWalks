namespace NZAPI.Models.Domains
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
        public Region Regions { get; set; }

        public WalkDifficulty WalkDifficulty { get; set; }
    }
}
