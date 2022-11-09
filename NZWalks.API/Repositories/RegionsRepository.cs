using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionsRepository : IRegionsRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public RegionsRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }
        public async Task<IEnumerable<Region>> GetAllRegionsAsync()
        {
            return await nZWalksDbContext.Regions.ToListAsync();
        }
    }
}
