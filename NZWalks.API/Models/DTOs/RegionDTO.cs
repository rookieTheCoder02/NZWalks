using NZWalks.API.Models.Domain;

namespace NZWalks.API.Models.DTOs
{
    public class RegionDTO
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
        public long Population { get; set; }
        public double Area { get; set; }


        // Navigation Property
        public IEnumerable<Walk>? Walks { get; set; }
    }
}
