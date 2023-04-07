namespace NZAPI.Models.Domains
{
    public class Region
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string? RegionImageURL { get; set; }

        //Navigation property- relationship between classes
        //one region can have multiple walks

        public IEnumerable<Walk> Walks { get; set; }
    }
}
