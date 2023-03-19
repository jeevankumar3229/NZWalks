namespace NZWalks_Api.Models.Domains
{
    public class Region
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public double Area { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public long Population { get; set; }

        //Navigation property- relationship between classes
        //one region can have multiple walks

        public IEnumerable<Walk> Walks { get; set; }
    }
}
