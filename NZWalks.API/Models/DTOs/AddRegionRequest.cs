namespace NZWalks.API.Models.DTOs
{
    public class AddRegionRequest
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
        public long Population { get; set; }
        public double Area { get; set; }
    }
}
