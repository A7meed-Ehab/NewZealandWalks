using NewZealandWalks.Api.Models.Domain;
using NewZealandWalks.Api.Models.DTO;

namespace NewZealandWalks.Api.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync();
        Task<Walk?> GetByIdAsync(Guid? Id);
        Task<Walk?> UpdateAsync(Guid? Id, updateWalkDto? dto);
        Task<Walk?> DeleteAsync(Guid? Id);
    }
}
