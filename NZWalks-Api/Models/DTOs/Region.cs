namespace NZWalks_Api.Models.DTOs
{
    public class Region
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string? RegionImageURL { get; set; }
    }
}
