namespace NZWalks_Api.Models.DTOs
{
    public class AddNewWalk
    {
        public string Name { get; set; }

        public double Length { get; set; }

        public Guid RegionId { get; set; }

        public Guid WalkDifficultyId { get; set; }

    }
}
