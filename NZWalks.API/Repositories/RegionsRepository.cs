using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Repositories
{
    public class RegionsRepository : IRegionsRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public RegionsRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Region> AddRegionAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteRegionAsync(Guid id)
        {
            var request = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(request == null)
            {
                return null;
            }
            // Delete the region
            nZWalksDbContext.Regions.Remove(request);
            await nZWalksDbContext.SaveChangesAsync();
            return request;
        }
    

        public async Task<IEnumerable<Region>> GetAllRegionsAsync()
        {
            return await nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetRegionAsync(Guid id)
        {
            return await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> UpdateRegionAsync(Guid id, Region region)
        {
            var request = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(request == null)
            {
                return null;
            }

            request.Code = region.Code;
            request.Name = region.Name;
            request.Latitude = region.Latitude;
            request.Longtitude = region.Longtitude;
            request.Population = region.Population;
            request.Area = region.Area;

            await nZWalksDbContext.SaveChangesAsync();
            return request;
        }
    }
}
