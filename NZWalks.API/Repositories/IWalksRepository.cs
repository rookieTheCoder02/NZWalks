using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalksRepository
    {
        Task<IEnumerable<Walk>> GetAllWalksAsync();
        Task<Walk> GetWalkAsync(Guid id);
        Task<Walk> AddWalkAsync(Walk walk);
    }
}
