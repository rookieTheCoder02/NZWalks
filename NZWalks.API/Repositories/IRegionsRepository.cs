﻿using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionsRepository
    {
        Task<IEnumerable<Region>> GetAllRegionsAsync();
        Task<Region> GetRegionAsync(Guid id);
        Task<Region> AddRegionAsync(Region region);
        Task<Region> DeleteRegionAsync(Guid id);
        Task<Region> UpdateRegionAsync(Guid id, Region region);
    }
}
