using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(walkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteWalkDifficultyAsync(Guid id)
        {
            var request = await nZWalksDbContext.WalkDifficulties.FirstOrDefaultAsync(x => x.Id == id);
            if (request == null) return null;
            nZWalksDbContext.WalkDifficulties.Remove(request);
            await nZWalksDbContext.SaveChangesAsync();
            return request;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficultiesAsync()
        {
            return await nZWalksDbContext.WalkDifficulties.ToListAsync();
        }
        
        public async Task<WalkDifficulty> GetWalkDifficultyAsync(Guid id)
        {
            return await nZWalksDbContext.WalkDifficulties.FirstOrDefaultAsync();
        }

        public async Task<WalkDifficulty> UpdateWalkDifficultyAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var request = await nZWalksDbContext.WalkDifficulties.FirstOrDefaultAsync(x => x.Id == id);
            if (String.IsNullOrWhiteSpace(walkDifficulty.Code))
            {
                request.Code = request.Code;
                await nZWalksDbContext.SaveChangesAsync();
                return request;
            }
            request.Code = walkDifficulty.Code;
            await nZWalksDbContext.SaveChangesAsync();
            return request;
        }
    }
}
